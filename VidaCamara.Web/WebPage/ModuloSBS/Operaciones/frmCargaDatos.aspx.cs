using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class CargaDatos : System.Web.UI.Page
    {
        static XSSFWorkbook xssfworkbook;
        static HSSFWorkbook hssfworkbook;
        static ISheet sheet;
        static int typeRow,RowCount,total,totalRSP,rowCountExcel;
        static int RowError = 0;
        static DataTable dtExcel = new DataTable();
        static DataTable dtExcelError = new DataTable();
        static String codigoseguro,formato_moneda;
        static String MessageError = "_";
        static int anio_vigente, mes_vigente,ide_empresa;
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        bValidarAcceso accesso = new bValidarAcceso();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "OTROS";
            if (Session["username"] == null)
                Response.Redirect("Login?go=0");
            else
            {
                if (!accesso.GetValidarAcceso(Request.QueryString["go"]))
                {
                    Response.Redirect("Error");
                }
            }
            if (!IsPostBack)
            {
                SetAnioMesVigente(1);
                SetLLenadoTipoInfo("08","F");
                SetLLenadoTipoInfoPago("08", "1");
                SetLLenadoContrato();
            }
        }
        //nuevo
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectSepelio(int jtStartIndex, int jtPageSize, string jtSorting,String nro_contrato,String tipo)
        {
            int indexPage;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else {
                indexPage = jtStartIndex;
            }
            eDatoAString das = SetEntitySelect(nro_contrato, tipo);//entidad de dato a
            bDatoAVC tb = new bDatoAVC();
            List<eDatoAString> list = tb.GetSelecionarSepelio(das, indexPage, jtPageSize, jtSorting.Substring(1).ToUpper(), out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectRSP(int jtStartIndex, int jtPageSize, string jtSorting,String nro_contrato ,String tipo)
        {
            int indexPage;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else
            {
                indexPage = jtStartIndex;
            }
            eDatoAString das = SetEntitySelect(nro_contrato, tipo);//entidad de dato a
            bDatoAVC tb = new bDatoAVC();
            List<eDataRSP> list = tb.GetSelectRSP(das, indexPage, jtPageSize, jtSorting, out totalRSP);
            return new { Result = "OK", Records = list, TotalRecordCount = totalRSP };
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(5000);
                SetClearDtExcel("3");//inicializar las grillas de datatable vacios

                String urlPath = SetSaveFileTemporal();
                String tipo_info = ddl_tipinfo_d.SelectedItem.Value;

                InitializeWorkbook(Path.GetFullPath(urlPath), 0);//inicialiazar el lector de excel
                Boolean resp = ConvertToDataTableGridView(tipo_info);//llenado datatable en memoria
                if (resp == true)
                {
                    switch (tipo_info)
                    {
                        case "01":
                            SetInsertarRsp();
                            multiTabs.ActiveViewIndex = 1;
                            break;
                        case "02":
                            SetInsertarSepelio();
                            multiTabs.ActiveViewIndex = 2;
                            break;
                        case "03":
                            SetInsertarPension();
                            multiTabs.ActiveViewIndex = 2;
                            break;
                        case "04":
                            SetInsertarPension20();
                            multiTabs.ActiveViewIndex = 2;
                            break;
                        case "05":
                            SetInsertarAporte();
                            multiTabs.ActiveViewIndex = 2;
                            break;
                        case "06":
                            SetInsertarPlanilla();
                            multiTabs.ActiveViewIndex = 2;
                            break;
                    }
                }
            }
            catch (Exception s)
            {
                MessageBox("ERROR =>" + s.Message.Replace("'", "-"));
            }
        }
        protected void btnExportError_Click(object sender, ImageClickEventArgs e)
        {
            GetExportarDatosErrados(ddl_tipinfo_d.SelectedItem.Text, ddl_tipinfo_d.SelectedItem.Value);
        }
        protected void btn_exportar_error_rsp_Click(object sender, ImageClickEventArgs e)
        {
            GetExportarDatosErrados(ddl_tipinfo_d.SelectedItem.Text, ddl_tipinfo_d.SelectedItem.Value);
        }
        protected void btn_borrar_c_Click(object sender, ImageClickEventArgs e)
        {
            Int32 tabActivado = multiTabs.ActiveViewIndex;
            if (hdf_estado_borrar.Value == "A")
            {
                    SetEliminaDatoA(tabActivado);
            }
            else {
                MessageBox("Los Registros que desea Borrar ya fuerón Procesados y no se pueden Borrar");
            }
        }
        protected void InitializeWorkbook(string path,int index)
        {
            String fileExt = Path.GetExtension(path);
            if (fileExt.ToLower() == ".xls")
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                    sheet = hssfworkbook.GetSheetAt(index);
                    typeRow = 1;
                }
            }
            else
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                    sheet = xssfworkbook.GetSheetAt(index);
                    typeRow = 0;
                }
            }
        }

        protected Boolean ConvertToDataTableGridView(String typeTable)
        {
            Boolean resp;
            try
            {
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                IRow headerRow = sheet.GetRow(0);

                int colCount = headerRow.LastCellNum;
                int rowCount = sheet.LastRowNum;

                for (int c = 0; c < colCount; c++)
                {
                    dtExcel.Columns.Add(headerRow.GetCell(c).ToString());
                    dtExcelError.Columns.Add(headerRow.GetCell(c).ToString());
                }

                bool skipReadingHeaderRow = rows.MoveNext();

                while (rows.MoveNext())
                {
                    IRow row;
                    if (typeRow == 0)
                    {
                        row = (XSSFRow)rows.Current;
                    }
                    else
                    {
                        row = (HSSFRow)rows.Current;
                    }
                    if (row.Cells[0].CellType == CellType.Blank && typeTable.Equals("01")) {
                        break;
                    }
                    int fila = 0;
                    rowCountExcel = row.RowNum;
                    DataRow dr = dtExcel.NewRow();
                    DataRow dre = dtExcelError.NewRow();
                    for (int i = 0; i < colCount; i++)
                    {

                        if (typeTable.Equals("01"))
                        {
                            if (row.Cells[0].CellType == CellType.Blank || row.Cells[3].CellType == CellType.Blank || row.Cells[6].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                               {
                                   ICell rowCellError = row.GetCell(c);
                                   dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        else if (typeTable.Equals("02"))
                        {
                            if (row.Cells[4].CellType == CellType.Blank || row.Cells[14].CellType == CellType.Blank || row.Cells[21].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                                {
                                    ICell rowCellError = row.GetCell(c);
                                    dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        else if (typeTable.Equals("03"))
                        {
                            if (row.Cells[3].CellType == CellType.Blank || row.Cells[4].CellType == CellType.Blank || row.Cells[25].CellType != CellType.Numeric ||
                                row.Cells[27].CellType == CellType.Blank || row.Cells[28].CellType == CellType.Blank || row.Cells[29].CellType == CellType.Blank || row.Cells[30].CellType == CellType.Blank ||
                                row.Cells[31].CellType == CellType.Blank || row.Cells[32].CellType == CellType.Blank || row.Cells[33].CellType == CellType.Blank || row.Cells[34].CellType == CellType.Blank ||
                                row.Cells[35].CellType == CellType.Blank || row.Cells[43].CellType == CellType.Blank || row.Cells[44].CellType == CellType.Blank || row.Cells[45].CellType == CellType.Blank || row.Cells[46].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                                {
                                    ICell rowCellError = row.GetCell(c);
                                    dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        else if (typeTable.Equals("04"))
                        {
                            if (row.Cells[3].CellType == CellType.Blank || row.Cells[4].CellType == CellType.Blank || row.Cells[23].CellType == CellType.Blank ||
                                 row.Cells[25].CellType == CellType.Blank || row.Cells[26].CellType == CellType.Blank || row.Cells[27].CellType == CellType.Blank || row.Cells[28].CellType == CellType.Blank ||
                                 row.Cells[29].CellType == CellType.Blank || row.Cells[30].CellType == CellType.Blank || row.Cells[36].CellType == CellType.Blank || row.Cells[37].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                                {
                                    ICell rowCellError = row.GetCell(c);
                                    dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        else if (typeTable.Equals("05"))
                        {
                            if (row.Cells[3].CellType == CellType.Blank || row.Cells[9].CellType != CellType.Numeric || row.Cells[11].CellType == CellType.Blank || row.Cells[12].CellType == CellType.Blank ||
                                 row.Cells[16].CellType == CellType.Blank || row.Cells[17].CellType == CellType.Blank || row.Cells[18].CellType == CellType.Blank || row.Cells[19].CellType == CellType.Blank ||
                                 row.Cells[20].CellType == CellType.Blank || row.Cells[21].CellType == CellType.Blank || row.Cells[23].CellType == CellType.Blank || row.Cells[24].CellType == CellType.Blank ||
                                 row.Cells[25].CellType == CellType.Blank || row.Cells[26].CellType == CellType.Blank || row.Cells[27].CellType == CellType.Blank || row.Cells[28].CellType == CellType.Blank ||
                                 row.Cells[29].CellType == CellType.Blank || row.Cells[35].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                                {
                                    ICell rowCellError = row.GetCell(c);
                                    dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        else if (typeTable.Equals("06"))
                        {
                            if (row.Cells[3].CellType == CellType.Blank || row.Cells[4].CellType == CellType.Blank || row.Cells[28].CellType == CellType.Blank || row.Cells[29].CellType == CellType.Blank ||
                                 row.Cells[30].CellType == CellType.Blank || row.Cells[31].CellType == CellType.Blank || row.Cells[32].CellType == CellType.Blank || row.Cells[33].CellType == CellType.Blank ||
                                 row.Cells[34].CellType == CellType.Blank || row.Cells[35].CellType == CellType.Blank || row.Cells[45].CellType == CellType.Blank || row.Cells[46].CellType == CellType.Blank ||
                                 row.Cells[47].CellType == CellType.Blank)
                            {
                                ICell cell = row.GetCell(i);
                                for (int c = 0; c < colCount; c++)
                                {
                                    ICell rowCellError = row.GetCell(c);
                                    dre[c] = rowCellError.ToString() + "_ / FILA =>" + cell.RowIndex;
                                }
                                if (RowError < 1)
                                {
                                    MessageError = "Se Identificó el error inicial en <br> Fila " + cell.RowIndex;
                                }
                                fila = 1;
                                break;
                            }
                        }
                        if (fila == 0)
                        {
                            ICell cell = row.GetCell(i);
                            if (cell == null)
                            {
                                dr[i] = "-0";
                            }
                            else
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Formula:
                                        dr[i] = cell.RichStringCellValue;
                                        break;
                                    default:
                                        dr[i] = cell.ToString();
                                        break;
                                }
                            }
                        }
                    }
                    if (fila == 0)
                    {
                        dtExcel.Rows.Add(dr);
                    }
                    else
                    {
                        RowError++;
                        dtExcelError.Rows.Add(dre);
                    }
                }
                if (typeTable != "01")
                {
                    lbl_regsitro_errado_p.Text = RowError.ToString();
                    ddl_tippago_p.SelectedValue = typeTable;
                    ddl_contrato_p.SelectedValue = dbl_contrato_d.SelectedItem.Value;
                }
                else
                {
                    lbl_regsitro_errado_r.Text = RowError.ToString();
                    ddl_contrato_r.SelectedValue = dbl_contrato_d.SelectedItem.Value;
                }
                resp = true;
            }
            catch (Exception e) {
                MessageBox("ERROR =>"+e.Message);
                resp = false;
            }
            return resp;
        }
        private String SetSaveFileTemporal() {
            String mes = "";
            if(Convert.ToInt32(Session["mesvigente"]) < 10)
                mes = "0"+Session["mesvigente"].ToString();
            else
                mes = Session["mesvigente"].ToString();

            String directory = Server.MapPath("~/Temp/" + Session["rutaexcel"].ToString().Trim() + "/" + Session["aniovigente"] + mes+"/");
            Boolean existe = System.IO.Directory.Exists(directory);
            if (!existe)
                System.IO.Directory.CreateDirectory(directory);

            txt_archivo_d.SaveAs(directory + txt_archivo_d.FileName);
            return directory + txt_archivo_d.FileName;
        }

        private void SetInsertarRsp(){
            try
            {
                String cod_ramo = "";
                String cod_moneda = "";
                String id_afp = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    //codigo ramo
                    String tip_cob = dtExcel.Rows[i][2].ToString();
                    if (tip_cob.Substring(0, 1).ToUpper().Equals("I"))
                        cod_ramo = "091";
                    else if (tip_cob.Substring(0, 1).ToUpper().Equals("S"))
                        cod_ramo = "092";
                    else
                        cod_ramo = "093";
                    //cod moneda
                    String moneda = dtExcel.Rows[i][3].ToString();
                    if (moneda.Trim().Equals("2") || moneda.Trim().Equals("4"))
                        cod_moneda = "DOL";
                    else
                        cod_moneda = "SOL";
                    //id_Afp
                    String afp = dtExcel.Rows[i][7].ToString();
                    if (afp.ToUpper().Equals("PRIMA"))
                        id_afp = afp.Substring(1, 2).ToUpper() + "00";
                    else
                       id_afp = afp.Substring(0, 2).ToUpper() + "00";

                    var io = new eDatoA();

                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]); 

                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 1;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = cod_moneda;
                    io._Ap_Mtno_Pens = " ";
                    io._Ap_Mtno_Solic = "";
                    io._Ap_Ptno_Pens = " ";
                    io._Ap_Ptno_Solic = "";
                    io._Aport_Comp = 0.00m;
                    io._Aport_Oblig = 0.00m;
                    io._Cap_Req_Pens = 0.00m;
                    io._Cap_Req_Sepe = 0.00m;
                    io._Cic_Mon_Nsol = 0.00m;
                    io._Cic_Mon_Orig = 0.00m;
                    io._Cru_Familiar = 0.00m;
                    io._Cuspp = dtExcel.Rows[i][0].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][5].ToString();
                    io._Id_Afp = id_afp;
                    io._Id_Cod_Csv1 = -0;
                    io._Id_Cod_Csv2 = -0;
                    io._Id_Cod_Csv3 = -0;
                    io._Id_Cod_Csv4 = -0;
                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][27]);
                    io._Id_Tip_Soli = -0;
                    io._Mto_Pago_Csv1 = 0.00m;
                    io._Mto_Pago_Csv2 = 0.00m;
                    io._Mto_Pago_Csv3 = 0.00m;
                    io._Mto_Pago_Csv4 = 0.00m;
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = dtExcel.Rows[i][4].ToString();
                    io._Fe_Falle = "";
                    io._Fe_Ini_Inv = "";
                    io._Fe_Pag_Aa = "";
                    io._Id_Tip_Doc = -0;
                    io._Ind_Pens_Pre = " ";
                    io._Mto_Aa_Mon_Nsol = 0.00m;
                    io._Mto_Aa_Mon_Orig = 0.00m;
                    io._Mto_Exc_Mon_Nsol = 0.00m;
                    io._Mto_Exc_Mon_Orig = 0.00m;
                    io._Mto_Pens_Pre_Mon_Nsol = 0.00m;
                    io._Mto_Pens_Pre_Mon_Orig = 0.00m;
                    io._Nro_Benef = "";
                    io._Nro_Iden_Solic = "";
                    io._Num_Csv = "";
                    io._Pri_Nomb_Solic = "";
                    io._Ram_Prom_Nsol = 0.00m;
                    io._Segu_Nomb_Solic = "";
                    io._Tasa_Int = 0.00m;
                    io._Ti_Camb_Compra = 0.00m;
                    io._Ti_Camb_Vta = 0.00m;
                    io._Ti_Reg = tip_cob;
                    io._Tip_Mon = SetValidationNumber(moneda);
                    io._Tip_Pen_Equiv = "";
                    io._Tot_Cap_Req_Mon_Nsol = 0.00m;
                    io._Tot_Cap_Req_Mon_Orig = 0.00m;
                    io._Desct_Pens = 0.00m;
                    io._Desct_Pens_Csv1 = 0.00m;
                    io._Desct_Pens_Csv2 = 0.00m;
                    io._Desct_Pens_Csv3 = 0.00m;
                    io._Desct_Pens_Csv4 = 0.00m;
                    io._Fe_Dev_Act = "";
                    io._Fe_Dev_Ini = "";
                    io._Fe_Fin_Subsi = "";
                    io._Fe_Naci_Pens = "";
                    io._Fe_Seci = dtExcel.Rows[i][8].ToString();
                    io._Fec_Pago = "";
                    io._Frac_Mes_Dev = "";
                    io._Id_Cod_Ben = "";
                    io._Id_Parent = 0;
                    io._Id_Tip_Docu_Pens = "";
                    io._Mes_Dev = "";
                    io._Mon_Equi = "";
                    io._Mto_Aport_Comp_Csv1 = 0.00m;
                    io._Mto_Aport_Comp_Csv2 = 0.00m;
                    io._Mto_Aport_Comp_Csv3 = 0.00m;
                    io._Mto_Aport_Comp_Csv4 = 0.00m;
                    io._Mto_Aport_Oblig_Csv1 = 0.00m;
                    io._Mto_Aport_Oblig_Csv2 = 0.00m;
                    io._Mto_Aport_Oblig_Csv3 = 0.00m;
                    io._Mto_Aport_Oblig_Csv4 = 0.00m;
                    io._Mto_Pago = SetValidationDecimal(dtExcel.Rows[i][6].ToString());
                    io._Nro_Docu_Pens = "";
                    io._Nro_Eess = "";
                    io._Nro_Mes_Dev = "";
                    io._Nro_Sin = -0;
                    io._Nro_Soli = "";
                    io._Pens_Base = 0.00m;
                    io._Pens_Pagar_80 = 0.00m;
                    io._Pri_Nomb_Pens = "";
                    io._Proc_Bene = "";
                    io._Prom_Act_6_Meses_Apocomp = 0.00m;
                    io._Rempro_Act = 0.00m;
                    io._Segu_Nomb_Pens = "";
                    io._Tip_Pen = "";
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }

                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s) <br><br> "+ MessageError);
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }
        //insertar sepelio
        private void SetInsertarSepelio() {
            try
            {
                String moneda = "";
                String cod_ramo = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    Int32 codmoneda = SetValidationNumber(dtExcel.Rows[i][14].ToString());
                    if (codmoneda == 1 || codmoneda == 2)
                        moneda = "SOL";
                    else
                        moneda = "DOL";
                    Int32 ramo = SetValidationNumber(dtExcel.Rows[i][12].ToString());
                    switch (ramo)
                    {
                        case 3:
                            cod_ramo = "093";
                            break;
                        case 2:
                            cod_ramo = "092";
                            break;
                        default:
                            cod_ramo = "091";
                            break;
                    }
                    var io = new eDatoA();
                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]); ;
                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 2;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = moneda;
                    io._Ap_Mtno_Pens = " ";//dtExcel.Rows[i][0];//fila[i][16].ToString();
                    io._Ap_Mtno_Solic = dtExcel.Rows[i][9].ToString();
                    io._Ap_Ptno_Pens = " ";//dtExcel.Rows[i][15].ToString();
                    io._Ap_Ptno_Solic = dtExcel.Rows[i][8].ToString();
                    io._Aport_Comp = 0.00m;//Decimal.Parse(dtExcel.Rows[i][29]);
                    io._Aport_Oblig = 0.00m;//Decimal.Parse(dtExcel.Rows[i][30]);
                    io._Cap_Req_Pens = 0.00m;
                    io._Cap_Req_Sepe = 0.00m;
                    io._Cic_Mon_Nsol = 0.00m;
                    io._Cic_Mon_Orig = 0.00m;
                    io._Cru_Familiar = 0.00m;
                    io._Cuspp = dtExcel.Rows[i][4].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][13].ToString();
                    io._Id_Afp = dtExcel.Rows[i][2].ToString();
                    io._Id_Cod_Csv1 = SetValidationNumber(dtExcel.Rows[i][18].ToString());
                    io._Id_Cod_Csv2 = SetValidationNumber(dtExcel.Rows[i][20].ToString());
                    io._Id_Cod_Csv3 = SetValidationNumber(dtExcel.Rows[i][22].ToString());
                    io._Id_Cod_Csv4 = SetValidationNumber(dtExcel.Rows[i][24].ToString());
                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][27]);
                    io._Id_Tip_Soli = ramo;
                    io._Mto_Pago_Csv1 = SetValidationDecimal(dtExcel.Rows[i][19].ToString());
                    io._Mto_Pago_Csv2 = SetValidationDecimal(dtExcel.Rows[i][21].ToString());
                    io._Mto_Pago_Csv3 = SetValidationDecimal(dtExcel.Rows[i][23].ToString());
                    io._Mto_Pago_Csv4 = SetValidationDecimal(dtExcel.Rows[i][25].ToString());
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = "-";
                    io._Fe_Falle = "-";
                    io._Fe_Ini_Inv = "-";
                    io._Fe_Pag_Aa = "-";
                    io._Id_Tip_Doc = SetValidationNumber(dtExcel.Rows[i][6].ToString());
                    io._Ind_Pens_Pre = "-";
                    io._Mto_Aa_Mon_Nsol = 0.00m;
                    io._Mto_Aa_Mon_Orig = 0.00m;
                    io._Mto_Exc_Mon_Nsol = 0.00m;
                    io._Mto_Exc_Mon_Orig = 0.00m;
                    io._Mto_Pens_Pre_Mon_Nsol = 0.00m;

                    io._Mto_Pens_Pre_Mon_Orig = 0.00m;
                    io._Nro_Benef = "-";
                    io._Nro_Iden_Solic = dtExcel.Rows[i][7].ToString();
                    io._Num_Csv = "-";
                    io._Pri_Nomb_Solic = dtExcel.Rows[i][10].ToString();
                    io._Ram_Prom_Nsol = 0.00m;
                    io._Segu_Nomb_Solic = dtExcel.Rows[i][11].ToString();
                    io._Tasa_Int = 0.00m;
                    io._Ti_Camb_Compra = 0.00m;
                    io._Ti_Camb_Vta = 0.00m;
                    io._Ti_Reg = dtExcel.Rows[i][0].ToString();
                    io._Tip_Mon = SetValidationNumber(dtExcel.Rows[i][14].ToString());
                    io._Tip_Pen_Equiv = "-";
                    io._Tot_Cap_Req_Mon_Nsol = 0.00m;
                    io._Tot_Cap_Req_Mon_Orig = 0.00m;
                    io._Desct_Pens = 0.00m;
                    io._Desct_Pens_Csv1 = 0.00m;
                    io._Desct_Pens_Csv2 = 0.00m;
                    io._Desct_Pens_Csv3 = 0.00m;
                    io._Desct_Pens_Csv4 = 0.00m;
                    io._Fe_Dev_Act = "-";
                    io._Fe_Dev_Ini = "-";
                    io._Fe_Fin_Subsi = "-";
                    io._Fe_Naci_Pens = "-";
                    io._Fe_Seci = "-";
                    io._Fec_Pago = dtExcel.Rows[i][16].ToString();
                    io._Frac_Mes_Dev = "-";
                    io._Id_Cod_Ben = "-";
                    io._Id_Parent = 0;
                    io._Id_Tip_Docu_Pens = "-";
                    io._Mes_Dev = "-";
                    io._Mon_Equi = "-";
                    io._Mto_Aport_Comp_Csv1 = 0.00m;
                    io._Mto_Aport_Comp_Csv2 = 0.00m;
                    io._Mto_Aport_Comp_Csv3 = 0.00m;
                    io._Mto_Aport_Comp_Csv4 = 0.00m;
                    io._Mto_Aport_Oblig_Csv1 = 0.00m;
                    io._Mto_Aport_Oblig_Csv2 = 0.00m;
                    io._Mto_Aport_Oblig_Csv3 = 0.00m;
                    io._Mto_Aport_Oblig_Csv4 = 0.00m;
                    io._Mto_Pago = SetValidationDecimal(dtExcel.Rows[i][15].ToString());
                    io._Nro_Docu_Pens = "-";
                    io._Nro_Eess = dtExcel.Rows[i][17].ToString();
                    io._Nro_Mes_Dev = "-";
                    io._Nro_Sin = SetValidationNumber(dtExcel.Rows[i][3].ToString());
                    io._Nro_Soli = dtExcel.Rows[i][5].ToString();
                    io._Pens_Base = 0.00m;
                    io._Pens_Pagar_80 = 0.00m;
                    io._Pri_Nomb_Pens = "-";
                    io._Proc_Bene = "-";
                    io._Prom_Act_6_Meses_Apocomp = 0.00m;
                    io._Rempro_Act = 0.00m;
                    io._Segu_Nomb_Pens = "-";
                    io._Tip_Pen = "-";
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }
                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s)");
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }
        //insertar pension
        private void SetInsertarPension(){
            try
            {
                String moneda = "";
                String cod_ramo = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    Int32 codmoneda = SetValidationNumber(dtExcel.Rows[i][25].ToString());
                    if (codmoneda == 1 || codmoneda == 2)
                        moneda = "SOL";
                    else
                        moneda = "DOL";
                    Int32 ramo = SetValidationNumber(dtExcel.Rows[i][5].ToString());
                    switch (ramo) { 
                        case 3:
                            cod_ramo = "093";
                            break;
                        case 2:
                            cod_ramo = "092";
                            break;
                        default:
                            cod_ramo = "091";
                            break;
                    }
                    var io = new eDatoA();
                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]);
                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 3;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = moneda;
                    io._Ap_Mtno_Pens = dtExcel.Rows[i][16].ToString();//fila[i][16].ToString();
                    io._Ap_Mtno_Solic = "-";
                    io._Ap_Ptno_Pens = dtExcel.Rows[i][15].ToString();
                    io._Ap_Ptno_Solic = "-";
                    io._Aport_Comp = Decimal.Parse(dtExcel.Rows[i][29].ToString());
                    io._Aport_Oblig = Decimal.Parse(dtExcel.Rows[i][30].ToString());
                    io._Cap_Req_Pens = 0.00m;
                    io._Cap_Req_Sepe = 0.00m;
                    io._Cic_Mon_Nsol = 0.00m;
                    io._Cic_Mon_Orig = 0.00m;
                    io._Cru_Familiar = 0.00m;
                    io._Cuspp = dtExcel.Rows[i][3].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][6].ToString();
                    io._Id_Afp = dtExcel.Rows[i][2].ToString();
                    io._Id_Cod_Csv1 = SetValidationNumber(dtExcel.Rows[i][37].ToString());
                    io._Id_Cod_Csv2 = SetValidationNumber(dtExcel.Rows[i][42].ToString());

                    io._Id_Cod_Csv3 = SetValidationNumber(dtExcel.Rows[i][47].ToString());
                    io._Id_Cod_Csv4 = SetValidationNumber(dtExcel.Rows[i][52].ToString());

                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][57]);
                    io._Id_Tip_Soli = ramo;
                    io._Mto_Pago_Csv1 = SetValidationDecimal(dtExcel.Rows[i][41].ToString());
                    io._Mto_Pago_Csv2 = SetValidationDecimal(dtExcel.Rows[i][46].ToString());
                    io._Mto_Pago_Csv3 = SetValidationDecimal(dtExcel.Rows[i][51].ToString());
                    io._Mto_Pago_Csv4 = SetValidationDecimal(dtExcel.Rows[i][56].ToString());
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = dtExcel.Rows[i][9].ToString();
                    io._Fe_Falle = "-";
                    io._Fe_Ini_Inv = "-";
                    io._Fe_Pag_Aa = "-";
                    io._Id_Tip_Doc = 0;
                    io._Ind_Pens_Pre = " ";
                    io._Mto_Aa_Mon_Nsol = 0.00m;
                    io._Mto_Aa_Mon_Orig = 0.00m;
                    io._Mto_Exc_Mon_Nsol = 0.00m;
                    io._Mto_Exc_Mon_Orig = 0.00m;
                    io._Mto_Pens_Pre_Mon_Nsol = 0.00m;
                    io._Mto_Pens_Pre_Mon_Orig = 0.00m;
                    io._Nro_Benef = "-";
                    io._Nro_Iden_Solic = "-";
                    io._Num_Csv = "-";
                    io._Pri_Nomb_Solic = " ";
                    io._Ram_Prom_Nsol = 0.00m;
                    io._Segu_Nomb_Solic = "-";
                    io._Tasa_Int = 0.00m;
                    io._Ti_Camb_Compra = 0.00m;
                    io._Ti_Camb_Vta = 0.00m;
                    io._Ti_Reg = dtExcel.Rows[i][0].ToString();
                    io._Tip_Mon = SetValidationNumber(dtExcel.Rows[i][25].ToString());
                    io._Tip_Pen_Equiv = "-";
                    io._Tot_Cap_Req_Mon_Nsol = 0.00m;
                    io._Tot_Cap_Req_Mon_Orig = 0.00m;
                    io._Desct_Pens = SetValidationDecimal(dtExcel.Rows[i][33].ToString());
                    io._Desct_Pens_Csv1 = SetValidationDecimal(dtExcel.Rows[i][40].ToString());
                    io._Desct_Pens_Csv2 = SetValidationDecimal(dtExcel.Rows[i][45].ToString());
                    io._Desct_Pens_Csv3 = SetValidationDecimal(dtExcel.Rows[i][50].ToString());
                    io._Desct_Pens_Csv4 = SetValidationDecimal(dtExcel.Rows[i][55].ToString());
                    io._Fe_Dev_Act = dtExcel.Rows[i][10].ToString();
                    io._Fe_Dev_Ini = dtExcel.Rows[i][9].ToString();
                    io._Fe_Fin_Subsi = dtExcel.Rows[i][8].ToString();
                    io._Fe_Naci_Pens = dtExcel.Rows[i][19].ToString();
                    io._Fe_Seci = dtExcel.Rows[i][7].ToString();
                    io._Fec_Pago = dtExcel.Rows[i][35].ToString();
                    io._Frac_Mes_Dev = dtExcel.Rows[i][13].ToString();
                    io._Id_Cod_Ben = dtExcel.Rows[i][22].ToString();
                    io._Id_Parent = SetValidationNumber(dtExcel.Rows[i][14].ToString());
                    io._Id_Tip_Docu_Pens = dtExcel.Rows[i][20].ToString();
                    io._Mes_Dev = dtExcel.Rows[i][11].ToString();
                    io._Mon_Equi = dtExcel.Rows[i][26].ToString();
                    io._Mto_Aport_Comp_Csv1 = SetValidationDecimal(dtExcel.Rows[i][38].ToString());
                    io._Mto_Aport_Comp_Csv2 = SetValidationDecimal(dtExcel.Rows[i][43].ToString());
                    io._Mto_Aport_Comp_Csv3 = SetValidationDecimal(dtExcel.Rows[i][48].ToString());
                    io._Mto_Aport_Comp_Csv4 = SetValidationDecimal(dtExcel.Rows[i][53].ToString());
                    io._Mto_Aport_Oblig_Csv1 = SetValidationDecimal(dtExcel.Rows[i][39].ToString());
                    io._Mto_Aport_Oblig_Csv2 = SetValidationDecimal(dtExcel.Rows[i][44].ToString());
                    io._Mto_Aport_Oblig_Csv3 = SetValidationDecimal(dtExcel.Rows[i][49].ToString());
                    io._Mto_Aport_Oblig_Csv4 = SetValidationDecimal(dtExcel.Rows[i][54].ToString());
                    io._Mto_Pago = SetValidationDecimal(dtExcel.Rows[i][34].ToString());
                    io._Nro_Docu_Pens = dtExcel.Rows[i][21].ToString();
                    io._Nro_Eess = dtExcel.Rows[i][36].ToString();
                    io._Nro_Mes_Dev = dtExcel.Rows[i][12].ToString();
                    io._Nro_Sin = 0;
                    io._Nro_Soli = dtExcel.Rows[i][4].ToString();
                    io._Pens_Base = SetValidationDecimal(dtExcel.Rows[i][31].ToString());
                    io._Pens_Pagar_80 = SetValidationDecimal(dtExcel.Rows[i][32].ToString());
                    io._Pri_Nomb_Pens = dtExcel.Rows[i][17].ToString();
                    io._Proc_Bene = dtExcel.Rows[i][23].ToString();
                    io._Prom_Act_6_Meses_Apocomp = SetValidationDecimal(dtExcel.Rows[i][28].ToString());
                    io._Rempro_Act = SetValidationDecimal(dtExcel.Rows[i][27].ToString());
                    io._Segu_Nomb_Pens = dtExcel.Rows[i][18].ToString();
                    io._Tip_Pen = dtExcel.Rows[i][24].ToString();
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }
                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s)");
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }

        //insertar pension 20%
        private void SetInsertarPension20() {
            try
            {
                String moneda = "";
                String cod_ramo = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    Int32 codmoneda = SetValidationNumber(dtExcel.Rows[i][23].ToString());
                    if (codmoneda == 1 || codmoneda == 2)
                        moneda = "SOL";
                    else
                        moneda = "DOL";
                    Int32 ramo = SetValidationNumber(dtExcel.Rows[i][5].ToString());
                    switch (ramo)
                    {
                        case 3:
                            cod_ramo = "093";
                            break;
                        case 2:
                            cod_ramo = "092";
                            break;
                        default:
                            cod_ramo = "091";
                            break;
                    }
                    var io = new eDatoA();
                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]); ;
                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 4;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = moneda;
                    io._Ap_Mtno_Pens = dtExcel.Rows[i][14].ToString();
                    io._Ap_Mtno_Solic = "-";
                    io._Ap_Ptno_Pens = dtExcel.Rows[i][13].ToString();
                    io._Ap_Ptno_Solic = "-";
                    io._Aport_Comp = 0.00m;//Decimal.Parse(dtExcel.Rows[i][29]);
                    io._Aport_Oblig = 0.00m;//Decimal.Parse(dtExcel.Rows[i][30]);
                    io._Cap_Req_Pens = 0.00m;
                    io._Cap_Req_Sepe = 0.00m;
                    io._Cic_Mon_Nsol = 0.00m;
                    io._Cic_Mon_Orig = 0.00m;
                    io._Cru_Familiar = 0.00m;
                    io._Cuspp = dtExcel.Rows[i][3].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][6].ToString();
                    io._Id_Afp = dtExcel.Rows[i][2].ToString();
                    io._Id_Cod_Csv1 = SetValidationNumber(dtExcel.Rows[i][32].ToString());
                    io._Id_Cod_Csv2 = SetValidationNumber(dtExcel.Rows[i][35].ToString());
                    io._Id_Cod_Csv3 = SetValidationNumber(dtExcel.Rows[i][38].ToString());
                    io._Id_Cod_Csv4 = SetValidationNumber(dtExcel.Rows[i][41].ToString());
                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][44]);
                    io._Id_Tip_Soli = ramo;
                    io._Mto_Pago_Csv1 = SetValidationDecimal(dtExcel.Rows[i][34].ToString());
                    io._Mto_Pago_Csv2 = SetValidationDecimal(dtExcel.Rows[i][37].ToString());
                    io._Mto_Pago_Csv3 = SetValidationDecimal(dtExcel.Rows[i][40].ToString());
                    io._Mto_Pago_Csv4 = SetValidationDecimal(dtExcel.Rows[i][43].ToString());
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = "-";
                    io._Fe_Falle = "-";
                    io._Fe_Ini_Inv = "-";
                    io._Fe_Pag_Aa = "-";
                    io._Id_Tip_Doc = 0;
                    io._Ind_Pens_Pre = " ";
                    io._Mto_Aa_Mon_Nsol = 0.00m;
                    io._Mto_Aa_Mon_Orig = 0.00m;
                    io._Mto_Exc_Mon_Nsol = 0.00m;
                    io._Mto_Exc_Mon_Orig = 0.00m;
                    io._Mto_Pens_Pre_Mon_Nsol = 0.00m;
                    io._Mto_Pens_Pre_Mon_Orig = 0.00m;
                    io._Nro_Benef = "-";
                    io._Nro_Iden_Solic = "-";
                    io._Num_Csv = "-";
                    io._Pri_Nomb_Solic = "-";
                    io._Ram_Prom_Nsol = 0.00m;
                    io._Segu_Nomb_Solic = "-";
                    io._Tasa_Int = 0.00m;
                    io._Ti_Camb_Compra = 0.00m;
                    io._Ti_Camb_Vta = 0.00m;
                    io._Ti_Reg = dtExcel.Rows[i][0].ToString();
                    io._Tip_Mon = SetValidationNumber(dtExcel.Rows[i][23].ToString());
                    io._Tip_Pen_Equiv = "-";
                    io._Tot_Cap_Req_Mon_Nsol = 0.00m;
                    io._Tot_Cap_Req_Mon_Orig = 0.00m;
                    io._Desct_Pens = SetValidationDecimal(dtExcel.Rows[i][28].ToString());
                    io._Desct_Pens_Csv1 = SetValidationDecimal(dtExcel.Rows[i][33].ToString());
                    io._Desct_Pens_Csv2 = SetValidationDecimal(dtExcel.Rows[i][36].ToString());
                    io._Desct_Pens_Csv3 = SetValidationDecimal(dtExcel.Rows[i][39].ToString());
                    io._Desct_Pens_Csv4 = SetValidationDecimal(dtExcel.Rows[i][42].ToString());
                    io._Fe_Dev_Act = dtExcel.Rows[i][8].ToString();
                    io._Fe_Dev_Ini = dtExcel.Rows[i][7].ToString();
                    io._Fe_Fin_Subsi = "-";
                    io._Fe_Naci_Pens = dtExcel.Rows[i][17].ToString();
                    io._Fe_Seci = "-";
                    io._Fec_Pago = dtExcel.Rows[i][30].ToString();
                    io._Frac_Mes_Dev = dtExcel.Rows[i][11].ToString();
                    io._Id_Cod_Ben = dtExcel.Rows[i][20].ToString();
                    io._Id_Parent = Int32.Parse(dtExcel.Rows[i][12].ToString());
                    io._Id_Tip_Docu_Pens = dtExcel.Rows[i][18].ToString();
                    io._Mes_Dev = dtExcel.Rows[i][9].ToString();
                    io._Mon_Equi = dtExcel.Rows[i][24].ToString();
                    io._Mto_Aport_Comp_Csv1 = 0.00m;
                    io._Mto_Aport_Comp_Csv2 = 0.00m;
                    io._Mto_Aport_Comp_Csv3 = 0.00m;
                    io._Mto_Aport_Comp_Csv4 = 0.00m;
                    io._Mto_Aport_Oblig_Csv1 = 0.00m;
                    io._Mto_Aport_Oblig_Csv2 = 0.00m;
                    io._Mto_Aport_Oblig_Csv3 = 0.00m;
                    io._Mto_Aport_Oblig_Csv4 = 0.00m;
                    io._Mto_Pago = SetValidationDecimal(dtExcel.Rows[i][29].ToString());
                    io._Nro_Docu_Pens = dtExcel.Rows[i][19].ToString();
                    io._Nro_Eess = dtExcel.Rows[i][31].ToString();
                    io._Nro_Mes_Dev = dtExcel.Rows[i][10].ToString();
                    io._Nro_Sin = 0;
                    io._Nro_Soli = dtExcel.Rows[i][4].ToString();
                    io._Pens_Base = SetValidationDecimal(dtExcel.Rows[i][26].ToString());
                    io._Pens_Pagar_80 = SetValidationDecimal(dtExcel.Rows[i][27].ToString());
                    io._Pri_Nomb_Pens = dtExcel.Rows[i][15].ToString();
                    io._Proc_Bene = dtExcel.Rows[i][21].ToString();
                    io._Prom_Act_6_Meses_Apocomp = 0.00m;
                    io._Rempro_Act = SetValidationDecimal(dtExcel.Rows[i][25].ToString());
                    io._Segu_Nomb_Pens = dtExcel.Rows[i][16].ToString();
                    io._Tip_Pen = dtExcel.Rows[i][22].ToString();
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }
                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s)");
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }
        //insertar planilla mensual
        private void SetInsertarPlanilla() {
            try
            {
                String moneda = "";
                String cod_ramo = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    Int32 codmoneda = SetValidationNumber(dtExcel.Rows[i][26].ToString());
                    if (codmoneda == 1 || codmoneda == 2)
                        moneda = "SOL";
                    else
                        moneda = "DOL";
                    Int32 ramo = SetValidationNumber(dtExcel.Rows[i][5].ToString());
                    switch (ramo)
                    {
                        case 3:
                            cod_ramo = "093";
                            break;
                        case 2:
                            cod_ramo = "092";
                            break;
                        default:
                            cod_ramo = "091";
                            break;
                    }
                    var io = new eDatoA();
                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]); ;
                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 5;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = moneda;
                    io._Ap_Mtno_Pens = dtExcel.Rows[i][16].ToString();
                    io._Ap_Mtno_Solic = "-";
                    io._Ap_Ptno_Pens = dtExcel.Rows[i][15].ToString();
                    io._Ap_Ptno_Solic = "-";
                    io._Aport_Comp = SetValidationDecimal(dtExcel.Rows[i][30].ToString());
                    io._Aport_Oblig = SetValidationDecimal(dtExcel.Rows[i][31].ToString());
                    io._Cap_Req_Pens = 0.00m;
                    io._Cap_Req_Sepe = 0.00m;
                    io._Cic_Mon_Nsol = 0.00m;
                    io._Cic_Mon_Orig = 0.00m;
                    io._Cru_Familiar = 0.00m;
                    io._Cuspp = dtExcel.Rows[i][3].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][6].ToString();
                    io._Id_Afp = dtExcel.Rows[i][2].ToString();
                    io._Id_Cod_Csv1 = SetValidationNumber(dtExcel.Rows[i][38].ToString());
                    io._Id_Cod_Csv2 = SetValidationNumber(dtExcel.Rows[i][43].ToString());

                    io._Id_Cod_Csv3 = SetValidationNumber(dtExcel.Rows[i][48].ToString());
                    io._Id_Cod_Csv4 = SetValidationNumber(dtExcel.Rows[i][53].ToString());

                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][58]);
                    io._Id_Tip_Soli = ramo;
                    io._Mto_Pago_Csv1 = SetValidationDecimal(dtExcel.Rows[i][42].ToString());
                    io._Mto_Pago_Csv2 = SetValidationDecimal(dtExcel.Rows[i][47].ToString());
                    io._Mto_Pago_Csv3 = SetValidationDecimal(dtExcel.Rows[i][52].ToString());
                    io._Mto_Pago_Csv4 = SetValidationDecimal(dtExcel.Rows[i][57].ToString());
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = "-";//DateTime.Parse(dtExcel.Rows[i][0]);
                    io._Fe_Falle = "-";
                    io._Fe_Ini_Inv = "-";
                    io._Fe_Pag_Aa = "-";
                    io._Id_Tip_Doc = 0;
                    io._Ind_Pens_Pre = " ";
                    io._Mto_Aa_Mon_Nsol = 0.00m;
                    io._Mto_Aa_Mon_Orig = 0.00m;
                    io._Mto_Exc_Mon_Nsol = 0.00m;
                    io._Mto_Exc_Mon_Orig = 0.00m;
                    io._Mto_Pens_Pre_Mon_Nsol = 0.00m;
                    io._Mto_Pens_Pre_Mon_Orig = 0.00m;
                    io._Nro_Benef = "-";
                    io._Nro_Iden_Solic = "-";
                    io._Num_Csv = "-";
                    io._Pri_Nomb_Solic = " ";
                    io._Ram_Prom_Nsol = 0.00m;
                    io._Segu_Nomb_Solic = "-";
                    io._Tasa_Int = 0.00m;
                    io._Ti_Camb_Compra = 0.00m;
                    io._Ti_Camb_Vta = 0.00m;
                    io._Ti_Reg = dtExcel.Rows[i][0].ToString();
                    io._Tip_Mon = SetValidationNumber(dtExcel.Rows[i][26].ToString());
                    io._Tip_Pen_Equiv = dtExcel.Rows[i][25].ToString();
                    io._Tot_Cap_Req_Mon_Nsol = 0.00m;
                    io._Tot_Cap_Req_Mon_Orig = 0.00m;
                    io._Desct_Pens = SetValidationDecimal(dtExcel.Rows[i][34].ToString());
                    io._Desct_Pens_Csv1 = SetValidationDecimal(dtExcel.Rows[i][41].ToString());
                    io._Desct_Pens_Csv2 = SetValidationDecimal(dtExcel.Rows[i][46].ToString());
                    io._Desct_Pens_Csv3 = SetValidationDecimal(dtExcel.Rows[i][51].ToString());
                    io._Desct_Pens_Csv4 = SetValidationDecimal(dtExcel.Rows[i][56].ToString());
                    io._Fe_Dev_Act = dtExcel.Rows[i][10].ToString();
                    io._Fe_Dev_Ini = dtExcel.Rows[i][9].ToString();
                    io._Fe_Fin_Subsi = dtExcel.Rows[i][8].ToString();
                    io._Fe_Naci_Pens = dtExcel.Rows[i][19].ToString();
                    io._Fe_Seci = dtExcel.Rows[i][7].ToString();
                    io._Fec_Pago = dtExcel.Rows[i][36].ToString();
                    io._Frac_Mes_Dev = dtExcel.Rows[i][13].ToString();
                    io._Id_Cod_Ben = dtExcel.Rows[i][22].ToString();
                    io._Id_Parent = SetValidationNumber(dtExcel.Rows[i][14].ToString());
                    io._Id_Tip_Docu_Pens = dtExcel.Rows[i][20].ToString();
                    io._Mes_Dev = dtExcel.Rows[i][11].ToString();
                    io._Mon_Equi = dtExcel.Rows[i][27].ToString();
                    io._Mto_Aport_Comp_Csv1 = SetValidationDecimal(dtExcel.Rows[i][39].ToString());
                    io._Mto_Aport_Comp_Csv2 = SetValidationDecimal(dtExcel.Rows[i][44].ToString());
                    io._Mto_Aport_Comp_Csv3 = SetValidationDecimal(dtExcel.Rows[i][49].ToString());
                    io._Mto_Aport_Comp_Csv4 = SetValidationDecimal(dtExcel.Rows[i][54].ToString());
                    io._Mto_Aport_Oblig_Csv1 = SetValidationDecimal(dtExcel.Rows[i][40].ToString());
                    io._Mto_Aport_Oblig_Csv2 = SetValidationDecimal(dtExcel.Rows[i][45].ToString());
                    io._Mto_Aport_Oblig_Csv3 = SetValidationDecimal(dtExcel.Rows[i][50].ToString());
                    io._Mto_Aport_Oblig_Csv4 = SetValidationDecimal(dtExcel.Rows[i][55].ToString());
                    io._Mto_Pago = SetValidationDecimal(dtExcel.Rows[i][35].ToString());
                    io._Nro_Docu_Pens = dtExcel.Rows[i][21].ToString();
                    io._Nro_Eess = dtExcel.Rows[i][37].ToString();
                    io._Nro_Mes_Dev = dtExcel.Rows[i][12].ToString();
                    io._Nro_Sin = 0;
                    io._Nro_Soli = dtExcel.Rows[i][4].ToString();
                    io._Pens_Base = SetValidationDecimal(dtExcel.Rows[i][32].ToString());
                    io._Pens_Pagar_80 = SetValidationDecimal(dtExcel.Rows[i][33].ToString());
                    io._Pri_Nomb_Pens = dtExcel.Rows[i][17].ToString();
                    io._Proc_Bene = dtExcel.Rows[i][23].ToString();
                    io._Prom_Act_6_Meses_Apocomp = SetValidationDecimal(dtExcel.Rows[i][29].ToString());
                    io._Rempro_Act = SetValidationDecimal(dtExcel.Rows[i][28].ToString());
                    io._Segu_Nomb_Pens = dtExcel.Rows[i][18].ToString();
                    io._Tip_Pen = dtExcel.Rows[i][24].ToString();
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }
                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s)");
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }
        //insertar aporte adicional
        private void SetInsertarAporte() {
            try
            {
                String moneda = "";
                String cod_ramo = "";
                List<eDatoA> LstData = new List<eDatoA>();
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    Int32 codmoneda = SetValidationNumber(dtExcel.Rows[i][9].ToString());
                    if (codmoneda == 1 || codmoneda == 2)
                        moneda = "SOL";
                    else
                        moneda = "DOL";
                    Int32 ramo = SetValidationNumber(dtExcel.Rows[i][4].ToString());
                    switch (ramo)
                    {
                        case 3:
                            cod_ramo = "093";
                            break;
                        case 2:
                            cod_ramo = "092";
                            break;
                        default:
                            cod_ramo = "091";
                            break;
                    }
                    var io = new eDatoA();
                    io._Ide_Empresa = Convert.ToInt32(Session["idempresa"]); ;
                    io._Nro_Contrato = dbl_contrato_d.SelectedItem.Value;
                    io._Anio_Vigente = SetAnioMesVigente(0);
                    io._Mes_Vigente = SetAnioMesVigente(2);
                    io._Tipo_Dato = ddl_tipinfo_d.SelectedItem.Value;
                    io._Cod_Ramo = cod_ramo;
                    io._Cod_Producto = 6;
                    io._Cod_Asegurado = codigoseguro;
                    io._Cod_Moneda = moneda;
                    io._Ap_Mtno_Pens = "-";
                    io._Ap_Mtno_Solic = " ";
                    io._Ap_Ptno_Pens = "-";
                    io._Ap_Ptno_Solic = "-";
                    io._Aport_Comp = 0.00m;
                    io._Aport_Oblig = 0.00m;
                    io._Cap_Req_Pens = SetValidationDecimal(dtExcel.Rows[i][20].ToString());
                    io._Cap_Req_Sepe = SetValidationDecimal(dtExcel.Rows[i][23].ToString());
                    io._Cic_Mon_Nsol = SetValidationDecimal(dtExcel.Rows[i][17].ToString());
                    io._Cic_Mon_Orig = SetValidationDecimal(dtExcel.Rows[i][16].ToString());
                    io._Cru_Familiar = SetValidationDecimal(dtExcel.Rows[i][21].ToString());

                    io._Cuspp = dtExcel.Rows[i][3].ToString();
                    io._Fe_Sin = dtExcel.Rows[i][5].ToString();
                    io._Id_Afp = dtExcel.Rows[i][2].ToString();
                    io._Id_Cod_Csv1 = SetValidationNumber(dtExcel.Rows[i][32].ToString());
                    io._Id_Cod_Csv2 = SetValidationNumber(dtExcel.Rows[i][34].ToString());

                    io._Id_Cod_Csv3 = Int32.Parse(dtExcel.Rows[i][36].ToString());
                    io._Id_Cod_Csv4 = Int32.Parse(dtExcel.Rows[i][38].ToString());

                    io._Id_Cod_Transf = 0;//Int32.Parse(dtExcel.Rows[i][40]);
                    io._Id_Tip_Soli = ramo;
                    io._Mto_Pago_Csv1 = SetValidationDecimal(dtExcel.Rows[i][33].ToString());
                    io._Mto_Pago_Csv2 = SetValidationDecimal(dtExcel.Rows[i][35].ToString());
                    io._Mto_Pago_Csv3 = SetValidationDecimal(dtExcel.Rows[i][37].ToString());
                    io._Mto_Pago_Csv4 = SetValidationDecimal(dtExcel.Rows[i][39].ToString());
                    io._Ti_Mov = dtExcel.Rows[i][1].ToString();
                    io._Fe_Dev = dtExcel.Rows[i][8].ToString();
                    io._Fe_Falle = dtExcel.Rows[i][7].ToString();
                    io._Fe_Ini_Inv = dtExcel.Rows[i][6].ToString();
                    io._Fe_Pag_Aa = dtExcel.Rows[i][30].ToString();
                    io._Id_Tip_Doc = 0;
                    io._Ind_Pens_Pre = dtExcel.Rows[i][15].ToString();
                    io._Mto_Aa_Mon_Nsol = SetValidationDecimal(dtExcel.Rows[i][29].ToString());
                    io._Mto_Aa_Mon_Orig = SetValidationDecimal(dtExcel.Rows[i][28].ToString());
                    io._Mto_Exc_Mon_Nsol = SetValidationDecimal(dtExcel.Rows[i][27].ToString());
                    io._Mto_Exc_Mon_Orig = SetValidationDecimal(dtExcel.Rows[i][26].ToString());
                    io._Mto_Pens_Pre_Mon_Nsol = SetValidationDecimal(dtExcel.Rows[i][12].ToString());
                    io._Mto_Pens_Pre_Mon_Orig = SetValidationDecimal(dtExcel.Rows[i][11].ToString());
                    io._Nro_Benef = dtExcel.Rows[i][22].ToString();
                    io._Nro_Iden_Solic = "-";
                    io._Num_Csv = dtExcel.Rows[i][31].ToString();
                    io._Pri_Nomb_Solic = " ";
                    io._Ram_Prom_Nsol = SetValidationDecimal(dtExcel.Rows[i][18].ToString());
                    io._Segu_Nomb_Solic = "-";
                    io._Tasa_Int = SetValidationDecimal(dtExcel.Rows[i][19].ToString());
                    io._Ti_Camb_Compra = SetValidationDecimal(dtExcel.Rows[i][14].ToString());
                    io._Ti_Camb_Vta = SetValidationDecimal(dtExcel.Rows[i][13].ToString());

                    io._Ti_Reg = dtExcel.Rows[i][0].ToString();
                    io._Tip_Mon = SetValidationNumber(dtExcel.Rows[i][9].ToString());
                    io._Tip_Pen_Equiv = "-";
                    io._Tot_Cap_Req_Mon_Nsol = SetValidationDecimal(dtExcel.Rows[i][25].ToString());
                    io._Tot_Cap_Req_Mon_Orig = SetValidationDecimal(dtExcel.Rows[i][24].ToString());
                    io._Desct_Pens = 0.00m;
                    io._Desct_Pens_Csv1 = 0.00m;
                    io._Desct_Pens_Csv2 = 0.00m;
                    io._Desct_Pens_Csv3 = 0.00m;
                    io._Desct_Pens_Csv4 = 0.00m;
                    io._Fe_Dev_Act = "-";
                    io._Fe_Dev_Ini = "-";
                    io._Fe_Fin_Subsi = "-";
                    io._Fe_Naci_Pens = "-";
                    io._Fe_Seci = "-";
                    io._Fec_Pago = "-";
                    io._Frac_Mes_Dev = "-";
                    io._Id_Cod_Ben = "-";
                    io._Id_Parent = 0;
                    io._Id_Tip_Docu_Pens = "-";
                    io._Mes_Dev = "-";
                    io._Mon_Equi = dtExcel.Rows[i][10].ToString();
                    io._Mto_Aport_Comp_Csv1 = 0.00m;
                    io._Mto_Aport_Comp_Csv2 = 0.00m;
                    io._Mto_Aport_Comp_Csv3 = 0.00m;
                    io._Mto_Aport_Comp_Csv4 = 0.00m;
                    io._Mto_Aport_Oblig_Csv1 = 0.00m;
                    io._Mto_Aport_Oblig_Csv2 = 0.00m;
                    io._Mto_Aport_Oblig_Csv3 = 0.00m;
                    io._Mto_Aport_Oblig_Csv4 = 0.00m;
                    io._Mto_Pago = 0.00m;
                    io._Nro_Docu_Pens = "-";
                    io._Nro_Eess = "-";
                    io._Nro_Mes_Dev = "-";
                    io._Nro_Sin = 0;
                    io._Nro_Soli = "-";
                    io._Pens_Base = 0.00m;
                    io._Pens_Pagar_80 = 0.00m;
                    io._Pri_Nomb_Pens = "-";
                    io._Proc_Bene = "-";
                    io._Prom_Act_6_Meses_Apocomp = 0.00m;
                    io._Rempro_Act = 0.00m;
                    io._Segu_Nomb_Pens = "-";
                    io._Tip_Pen = "-";
                    io._Estado = "A";
                    io._Usu_Reg = Session["username"].ToString();
                    LstData.Add(io);
                }
                bDatoAVC da = new bDatoAVC();
                Int32 resp = da.SetInsertarDatoA(LstData);
                if (resp == -1)
                {
                    MessageBox("Ya Existe Registros, <br> Debes borrar los Registros existentes <br> para Realizar una nueva carga.");
                    SetClearDtExcel("2");
                }
                else if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Grabado (s) Correctamente <br>" + RowError + " Registro (s) Observado (s)");
                    SetClearDtExcel("2");
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) { 
                MessageBox("ERROR =>"+e.Message);
            }
        }
        //validacion de campos de gridview decimal
        private Decimal SetValidationDecimal(String d)
        {
            Decimal res = 0.00m;
            try {
                res = Decimal.Parse(d);
            }catch{
                res = 0.00m;
            }
            return res;
        }

        //validacion de campos de gridview int
        private Int32 SetValidationNumber(String n) {
            Int32 res = 0;
            try {
                res = Int32.Parse(n);
            }catch{
                res = 0;
            }
            return res;
        }
        //eliminar dato_A
        private void SetEliminaDatoA(Int32 indexTab)
        {
            try
            {
                eDatoA da = new eDatoA();
                if (indexTab == 1)
                {
                    da._Nro_Contrato = ddl_contrato_r.SelectedItem.Value;
                    da._Tipo_Dato = "01";
                    da._Anio_Vigente = anio_vigente;
                    da._Mes_Vigente = mes_vigente;
                }
                else if (indexTab == 2)
                {
                    da._Nro_Contrato = ddl_contrato_p.SelectedItem.Value;
                    da._Tipo_Dato = ddl_tippago_p.SelectedItem.Value;
                    da._Anio_Vigente = anio_vigente;
                    da._Mes_Vigente = mes_vigente;
                }

                bDatoAVC bd = new bDatoAVC();
                Int32 resp = bd.SetEliminarDatoA(da);
                if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Eliminado (s) Correctamente");
                    SetClearSecreen();
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>"+e.Message);
            }
        }
        private void SetLLenadoTipoInfo(String e,String f)
        {
            var util = new VidaCamara.SBS.Utils.Utility().getConceptoByTipo(e, f, out RowCount);
            ddl_tipinfo_d.DataSource = util;
            ddl_tipinfo_d.DataTextField = "_descripcion";
            ddl_tipinfo_d.DataValueField = "_codigo";
            ddl_tipinfo_d.DataBind();
            ddl_tipinfo_d.Items.Insert(0, new ListItem("Seleccione ----", "0"));
        }
        private void SetLLenadoTipoInfoPago(String e,String a)
        {
            var util = new VidaCamara.SBS.Utils.Utility().getConceptoByTipo(e, a, out RowCount);
            ddl_tippago_p.DataSource = util;
            ddl_tippago_p.DataTextField = "_descripcion";
            ddl_tippago_p.DataValueField = "_codigo";
            ddl_tippago_p.DataBind();
            ddl_tippago_p.Items.Insert(0, new ListItem("Seleccione ----", "0"));

        }

        private void SetLLenadoContrato()
        {
            var list = new VidaCamara.SBS.Utils.Utility().getContrato(out total);
            codigoseguro = list[0]._cod_Contratante;

            dbl_contrato_d.DataSource = list;
            dbl_contrato_d.DataTextField = "_des_Contrato";
            dbl_contrato_d.DataValueField = "_nro_Contrato";
            dbl_contrato_d.DataBind();
            dbl_contrato_d.Items.Insert(0, new ListItem("Seleccione ----", "0"));

            ddl_contrato_r.DataSource = list;
            ddl_contrato_r.DataTextField = "_des_Contrato";
            ddl_contrato_r.DataValueField = "_nro_Contrato";
            ddl_contrato_r.DataBind();
            ddl_contrato_r.Items.Insert(0, new ListItem("Seleccione ----", "0"));

            ddl_contrato_p.DataSource = list;
            ddl_contrato_p.DataTextField = "_des_Contrato";
            ddl_contrato_p.DataValueField = "_nro_Contrato";
            ddl_contrato_p.DataBind();
            ddl_contrato_p.Items.Insert(0, new ListItem("Seleccione ----", "0"));


        }
        private void SetClearSecreen() {
            lbl_regsitro_errado_p.Text = "0";
            lbl_regsitro_errado_r.Text = "0";
            hdf_excel_error.Value = "0";
            dbl_contrato_d.SelectedValue = "0";
            ddl_periodo_d.SelectedValue = "0";
            dtExcelError.Clear();
            dtExcelError.Reset();
            dtExcel.Clear();
            dtExcel.Rows.Clear();
            dtExcel.Columns.Clear();
            RowError = 0;
            MessageError = "_";
            rowCountExcel = 0;
        }
        private void SetClearDtExcel(String tipo_carga) {
            if (tipo_carga == "1")
            {
                dtExcelError.Clear();
                dtExcelError.Reset();

                dtExcelError.Rows.Clear();
                dtExcelError.Columns.Clear();
            }
            else if (tipo_carga == "3")
            {
                dtExcelError.Clear();
                dtExcelError.Reset();
                dtExcelError.Rows.Clear();
                dtExcelError.Columns.Clear();
                dtExcel.Clear();
                dtExcel.Rows.Clear();
                dtExcel.Columns.Clear();
                RowError = 0;
            }
            else {
                dtExcel.Clear();
                dtExcel.Rows.Clear();
                dtExcel.Columns.Clear();
            }
        }
        private void GetExportarDatosErrados(String tipo_info,String value_info) {

            System.Threading.Thread.Sleep(1);

            String datetime = System.DateTime.Today.Day + "/" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Year;
            String filename = "Registro Observados de:" + " " + datetime + " Tipo CargaLogica" + tipo_info +  ".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            var output = new MemoryStream();

            HSSFWorkbook hssfworkbookr = new HSSFWorkbook();
            ISheet sheet = hssfworkbookr.CreateSheet("LIBRO1");

            ICellStyle background = hssfworkbookr.CreateCellStyle();
            background.FillForegroundColor = HSSFColor.Yellow.Index;
            background.FillPattern = FillPattern.SolidForeground;


            IFont titleFont = hssfworkbookr.CreateFont();
            titleFont.FontName = "Calibri";
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleFont.Color = (IndexedColors.Black.Index);
            titleFont.FontHeightInPoints = 11;

            ICellStyle styleCabecera = hssfworkbookr.CreateCellStyle();
            styleCabecera.Alignment = HorizontalAlignment.Center;
            styleCabecera.SetFont(titleFont);
            styleCabecera.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            IRow dataHeader = sheet.CreateRow(0);
            ICell dataCellH;
            for (int h = 0; h < dtExcelError.Columns.Count; h++)
            {
                dataCellH = dataHeader.CreateCell(h);
                dataCellH.SetCellValue(dtExcelError.Columns[h].ToString());
                sheet.AutoSizeColumn(dtExcelError.Columns[h].Ordinal);
                dataCellH.CellStyle = styleCabecera;
            }
            for (int r = 0; r < dtExcelError.Rows.Count; r++)
            {
                dataHeader = sheet.CreateRow(1 + r);
                ICell celldata;
                for (int c = 0; c < dtExcelError.Columns.Count; c++)
                {
                    String[] excelrow = dtExcelError.Rows[r][c].ToString().Split('/');
                    if (excelrow[0].Trim().Equals("_"))
                    {
                        celldata = dataHeader.CreateCell(c);
                        celldata.SetCellValue(dtExcelError.Rows[r][c].ToString());
                        celldata.CellStyle = background;
                    }
                    else {
                        celldata = dataHeader.CreateCell(c);
                        celldata.SetCellValue(dtExcelError.Rows[r][c].ToString());
                    }
                }
            }

            hssfworkbookr.Write(output);

            dtExcelError.Rows.Clear();
            dtExcelError.Columns.Clear();
            dtExcelError.Clear();

            Response.BinaryWrite(output.GetBuffer());
            Response.End();
        }
        private static eDatoAString SetEntitySelect(String nro_contrato, String tipo){
                var da = new eDatoAString();
                da._Ide_Empresa = ide_empresa;
                da._Nro_Contrato = nro_contrato;
                da._Tipo_Dato = tipo;
                da._Anio_Vigente = anio_vigente;
                da._Mes_Vigente = mes_vigente;
                da._Formato_Moneda = formato_moneda;
            return da;
        }
        private Int32 SetAnioMesVigente(Int32 token) {
            Int32 res = 0;
            if (token == 2)
            
                if(token == 0){
                    res = Convert.ToInt32(Session["aniovigente"]);
                }else{
                    if (Convert.ToInt32(Session["mesvigente"]) < 10)
                        res = Convert.ToInt32(Session["aniovigente"] + "0" + Session["mesvigente"]);
                    else
                        res = Convert.ToInt32(Session["aniovigente"] + "" + Session["mesvigente"]);
            }
            else
            {
                txt_mesvig_d.Text = meses[Convert.ToInt32(Session["mesvigente"])] +"-"+Session["aniovigente"];
                anio_vigente = Convert.ToInt32(Session["aniovigente"]);
                mes_vigente = Convert.ToInt32(Session["mesvigente"]);
                formato_moneda = Session["formatomoneda"].ToString();
                ide_empresa = Convert.ToInt32(Session["idempresa"]);
            }
            return res;
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">"+ text +"</div>').dialog({title:'Confirmación',modal:true,width:400,height:240,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }
    }
}