using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Consultas
{
    public partial class frmConsultaOperaciones : System.Web.UI.Page
    {
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

            if (!IsPostBack) {
                bContratoVC contrato = new bContratoVC();
                bTablaVC concepto = new bTablaVC();
                formato_moneda = Session["formatomoneda"].ToString();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_o);
                concepto.SetEstablecerDataSourceConcepto(ddl_tipcom_o,"12");
                concepto.SetEstablecerDataSourceConcepto(ddl_ramo_o,"05");
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectOperacion(int jtStartIndex, int jtPageSize, string jtSorting, String[] dataFrm)
        {
           
            int indexPage;
            int totalRow;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else
            {
                indexPage = jtStartIndex;
            }
            eOperacionVC eo = frmConsultaOperaciones.SetParametros(dataFrm);
            bOperacionVC bo = new bOperacionVC();
            List<eOperacionVC> list = bo.GetSelectOperacion(eo, indexPage, jtPageSize, jtSorting.Substring(1), out totalRow);
            return new { Result = "OK", Records = list, TotalRecordCount = totalRow };
        }

        protected void btn_exportar_Click(object sender, ImageClickEventArgs e)
        {
            eOperacionVC eo = new eOperacionVC();
            if (txt_operacion_o.Text != "")
                eo._Nro_Operacion = Int32.Parse(txt_operacion_o.Text);
            else
                eo._Nro_Operacion = 0;
            if (ddl_tipcom_o.SelectedItem.Value != "0")
                eo._Tip_Operacion = ddl_tipcom_o.SelectedItem.Value;
            else
                eo._Tip_Operacion = "0";

            if (txt_fec_ini_o.Text != "" && txt_fec_hasta_o.Text != "")
            {
                eo._fecha_fin = Convert.ToDateTime(txt_fec_hasta_o.Text);
                eo._fecha_ini = Convert.ToDateTime(txt_fec_ini_o.Text);
                eo._token_fecha = "SI";
            }
            else
            {
                eo._fecha_fin = System.DateTime.Today;
                eo._fecha_ini = System.DateTime.Today;
                eo._token_fecha = "NO";
            }
            if (ddl_ramo_o.SelectedItem.Value != "0")
                eo._Cod_Ramo = ddl_ramo_o.SelectedItem.Value;
            else
                eo._Cod_Ramo = "0";
            if (ddl_contrato_o.SelectedItem.Value != "")
                eo._Ide_Contrato = ddl_contrato_o.SelectedItem.Value;
            else
                eo._Ide_Contrato = "0";
            eo._Formato_Moneda = formato_moneda;

            exportarDatoExcel(eo);
        }
        private static eOperacionVC SetParametros(String[] dataFrm)
        {
            Int32 nro_comprob = 0;
            String fecha_token = "NO";
            if (dataFrm[3] != "") { nro_comprob = Int32.Parse(dataFrm[3]); }
            if (dataFrm[4] != "") { fecha_token = "SI"; }
            eOperacionVC eo = new eOperacionVC();
            eo._Ide_Contrato = dataFrm[0];
            eo._Tip_Operacion = dataFrm[1];
            eo._Cod_Ramo = dataFrm[2];
            eo._Nro_Operacion = nro_comprob;
            eo._Formato_Moneda = formato_moneda;
            if (dataFrm[4] != "" || dataFrm[5] != "")
            {
                eo._token_fecha = fecha_token;
                eo._fecha_ini = Convert.ToDateTime(dataFrm[4]);
                eo._fecha_fin = Convert.ToDateTime(dataFrm[5]);
            }
            else
            {
                eo._token_fecha = fecha_token;
                eo._fecha_ini = System.DateTime.Today;
                eo._fecha_fin = System.DateTime.Today;
            }
            return eo;
        }
        private void exportarDatoExcel(eOperacionVC o) {
            
            int total;
            String filename = "OPERACION " + ddl_tipcom_o.SelectedItem.Value + " " + String.Format("{0:dd/MM/yyyy}", System.DateTime.Today) + ".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            var output = new MemoryStream();
            var writer = new StreamWriter(output);

            HSSFWorkbook xssfworkbook = new HSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("OPERACION");

            IFont titleFont = xssfworkbook.CreateFont();
            titleFont.FontName = "Calibri";
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleFont.Color = (IndexedColors.Black.Index);
            titleFont.FontHeightInPoints = 11;

            ICellStyle styleCabecera = xssfworkbook.CreateCellStyle();
            styleCabecera.Alignment = HorizontalAlignment.Center;
            styleCabecera.VerticalAlignment = VerticalAlignment.Center;
            styleCabecera.SetFont(titleFont);
            styleCabecera.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styleCabecera.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            bOperacionSelectVC operacion = new bOperacionSelectVC();
            DataTable dt = operacion.GetSelectOperacionExport(o, 0, 1000000, "NRO_OPERACION ASC", out total);
            IRow dataHeader = sheet.CreateRow(0);
            ICell dataCellH;
            for (int cl1 = 0; cl1 < 11; cl1++)
            {
                sheet.SetColumnWidth(cl1, 150 * 25);
            }
            for (int cl = 11; cl <= 21; cl++)
            {
                sheet.SetColumnWidth(cl, 300 * 25);
            }
            //cabecera excel
            for (int h = 0; h < (dt.Columns.Count); h++)
            {
                dataCellH = dataHeader.CreateCell(h);
                dataCellH.SetCellValue(dt.Columns[h].ToString());
                dataCellH.CellStyle = styleCabecera;
                dataCellH.CellStyle.WrapText = true;
            }
            //cuerpo excel
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                IRow datacell = sheet.CreateRow(1+r);
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    datacell.CreateCell(c, CellType.String).SetCellValue(dt.Rows[r][c].ToString());

                }
            }
            xssfworkbook.Write(output);
            writer.Flush();

            dt.Rows.Clear();
            dt.Columns.Clear();
            dt.Clear();

            Response.BinaryWrite(output.GetBuffer());
            Response.End();
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
    }
}