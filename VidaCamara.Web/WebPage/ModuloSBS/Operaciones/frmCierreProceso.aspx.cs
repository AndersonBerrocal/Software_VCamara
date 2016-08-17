using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class frmCierreProceso : System.Web.UI.Page
    {
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviebre", "Diciembre" };
        static String[] trimestre = { "Primero", "Segundo", "Tercero", "Cuarto" };
        static int anio_vigente,mes_vigente;
        static String formato_moneda;
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
                bContratoVC contrato = new bContratoVC();
                bTablaVC concepto = new bTablaVC();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_c);
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_detC);
                concepto.SetEstablecerDataSourceConcepto(ddl_tipinfo_c,"12");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipinfo_detC, "12");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_cierre,"15");
                GetDataGeneral();
                SetCalcularTrimestre("SI");
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SetEliminarCierreProceso(Int32 _Id_Cierre)
        {
            bCierreProcesoVC ope = new bCierreProcesoVC();
            ope.SetEliminarCierreProceso(_Id_Cierre);
            return new { Result = "OK" };
        }
       [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectCierreProceso(int jtStartIndex, int jtPageSize, string jtSorting, String[] data)
        {
            int totalI;
            int indexPage;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else
            {
                indexPage = jtStartIndex;
            }
            eCierreProceso ocm = new eCierreProceso();
            ocm._Nro_Contrato = data[0];
            ocm._Tipo_Info = data[1];
            ocm._Formato_Moneda = formato_moneda;

            bCierreProcesoVC bco = new bCierreProcesoVC();
            List<eCierreProceso> listI = bco.GetSelectCierreOperacion(ocm, out totalI, indexPage, jtPageSize, jtSorting.Substring(1));
            return new { Result = "OK", Records = listI, TotalRecordCount = totalI };
        }
        protected void btn_procesa_cierre_Click(object sender, ImageClickEventArgs e)
        {
            SetInertarCierreProceso();
        }

        protected void btn_exporta_excel_Click(object sender, ImageClickEventArgs e)
        {
            SetExportarExcel();
        }
        private void SetInertarCierreProceso() {
            int registro_procesado, registro_creado;
            String monto_acumulado;

            eCierreProceso ec = new eCierreProceso();
            ec._Nro_Contrato = ddl_contrato_c.SelectedItem.Value;
            ec._Tipo_Cierre = ddl_tipo_cierre.SelectedItem.Value;
            ec._Tipo_Info = ddl_tipinfo_c.SelectedItem.Value;
            ec._Anio_Cierre = anio_vigente;
            ec._Mes_Cierre = mes_vigente.ToString();
            ec._Usu_Reg = Session["username"].ToString();
            bCierreProcesoVC bc = new bCierreProcesoVC();
            Int32 resp = bc.SetInsertarCierreProceso(ec);
            if (resp != 0)
            {
                String tipo_tabla = "";
                String tipo_info = "";
                eOperacionVC opt = new eOperacionVC();
                opt._Ide_Contrato = ddl_contrato_c.SelectedItem.Value;
                if (ddl_tipo_cierre.SelectedItem.Value.ToUpper() == "TRI"){
                    tipo_tabla = "2";
                    tipo_info = "C"+ddl_tipinfo_c.SelectedItem.Value.Substring(1);
                    opt._Tip_Operacion = ddl_tipinfo_c.SelectedItem.Value;
                    opt._Ano_Operacion = anio_vigente;
                    opt._Mes_Operacion = mes_vigente.ToString();
                    opt._Formato_Moneda = formato_moneda;
                }
                else{
                    tipo_tabla = "1";
                    tipo_info = "R" + ddl_tipinfo_c.SelectedItem.Value.Substring(1);
                    opt._Tip_Operacion = "R" + ddl_tipinfo_c.SelectedItem.Value.Substring(1);
                    opt._Ano_Operacion = anio_vigente;
                    opt._Mes_Operacion = mes_vigente.ToString();
                    opt._Formato_Moneda = formato_moneda;
                }

                String[] tabla = { tipo_tabla, tipo_info };
                bOperacionSelectVC bos = new bOperacionSelectVC();
                bos.GetSelectTotalOperacion(opt, tabla, out registro_procesado, out registro_creado, out monto_acumulado);
                lbl_regprocesado_c.Text = registro_procesado.ToString();
                lbl_opecreada_c.Text = registro_creado.ToString();
                lbl_totimporte_c.Text = monto_acumulado.ToString();

                MessageBox("Operación Ejecutada Correctamente");
            }
            else {
                MessageBox("No se Encontró Registros Para Procesar");
            }
        }

        private void SetExportarExcel() {
            int total;
            eCierreProceso ec = new eCierreProceso();
            ec._Nro_Contrato = ddl_contrato_detC.SelectedItem.Value;
            ec._Tipo_Info = ddl_tipinfo_detC.SelectedItem.Value;
            ec._Formato_Moneda = formato_moneda;

            bCierreProcesoVC bc = new bCierreProcesoVC();
            List<eCierreProceso> list = bc.GetSelectCierreOperacion(ec,out total,0,10000,"TIPO_INFO ASC");
            PropertyDescriptorCollection pro = TypeDescriptor.GetProperties(typeof(eCierreProceso));
            DataTable dtexcel = new DataTable();
            foreach (PropertyDescriptor prop in pro)
            {
                dtexcel.Columns.Add(prop.Name.ToUpper(), Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (eCierreProceso item in list)
            {
                DataRow row = dtexcel.NewRow();
                foreach (PropertyDescriptor prop in pro)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    dtexcel.Rows.Add(row);
            }

            String datetime = System.DateTime.Today.Day +"/"+System.DateTime.Today.Month+"/"+System.DateTime.Today.Year;
            String filename = "CIERRE_DETALL" + " " + datetime + " " + ddl_tipinfo_detC.SelectedItem.Value + ".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            var output = new MemoryStream();

            HSSFWorkbook xssfworkbook = new HSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("CIERRE_DETALLE");

            IFont titleFont = xssfworkbook.CreateFont();
            titleFont.FontName = "Calibri";
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleFont.Color = (IndexedColors.Black.Index);
            titleFont.FontHeightInPoints = 11;

            ICellStyle styleCabecera = xssfworkbook.CreateCellStyle();
            styleCabecera.Alignment = HorizontalAlignment.Center;
            styleCabecera.SetFont(titleFont);
            styleCabecera.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            IRow dataHeader = sheet.CreateRow(0);
            ICell dataCellH;
            for (int h = 0; h < dtexcel.Columns.Count; h++)
            {
                dataCellH = dataHeader.CreateCell(h);
                dataCellH.SetCellValue(dtexcel.Columns[h].ToString().Substring(1));
                sheet.AutoSizeColumn(dtexcel.Columns[h].Ordinal);
                dataCellH.CellStyle = styleCabecera;
            }
            for (int r = 0; r < dtexcel.Rows.Count; r++)
            {
                dataHeader = sheet.CreateRow(1 + r);
                for (int c = 0; c < dtexcel.Columns.Count; c++)
                {
                    dataHeader.CreateCell(c, CellType.String).SetCellValue(dtexcel.Rows[r][c].ToString());
                }
            }
            xssfworkbook.Write(output);

            dtexcel.Rows.Clear();
            dtexcel.Columns.Clear();
            dtexcel.Clear();

            Response.BinaryWrite(output.GetBuffer());
            Response.End();
        }
        private void GetDataGeneral()
        {
            mes_vigente = Convert.ToInt32(Session["mesvigente"]);
            anio_vigente = Convert.ToInt32(Session["aniovigente"]);
            formato_moneda = Session["formatomoneda"].ToString();
        }

        private void SetCalcularTrimestre(String contrato)
        {
            /*eContratoVC o = new eContratoVC();
            o._inicio = 0;
            o._fin = 10000;
            o._orderby = "IDE_CONTRATO ASC";
            o._nro_Contrato = "NO";
            o._estado = "A";

            bContratoVC tb = new bContratoVC();
            List<eContratoVC> listCont = tb.GetSelecionarContrato(o, out totalContrato);
            if (listCont.Count > 0) {
                Int32 total_meses = CalcularMesesDeDiferencia(listCont[0]._fec_Ini_Vig,System.DateTime.Today);

            }*/

            if (mes_vigente < 4)
                lbl_trimestre_c.Text = trimestre[0];
            else if (mes_vigente < 7)
                lbl_trimestre_c.Text = trimestre[1];
            else if (mes_vigente < 10)
                lbl_trimestre_c.Text = trimestre[2];
            else if (mes_vigente < 13)
                lbl_trimestre_c.Text = trimestre[3];
            lbl_mes_c.Text = meses[mes_vigente]+"-"+anio_vigente;
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
    }
}