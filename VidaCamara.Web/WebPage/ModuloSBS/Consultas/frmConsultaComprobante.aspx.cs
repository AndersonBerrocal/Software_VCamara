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
    public partial class frmConsultaComprobante : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                bContratoVC contrato = new bContratoVC();
                bTablaVC concepto = new bTablaVC();
                formato_moneda = Session["formatomoneda"].ToString();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_c);
                ddl_contrato_c.SelectedIndex = 1;
                concepto.SetEstablecerDataSourceConcepto(ddl_tipcom_c,"13");
                concepto.SetEstablecerDataSourceConcepto(ddl_ramo_c,"05");
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectComprobante(int jtStartIndex, int jtPageSize, string jtSorting, String[] dataFrm)
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
            eComprobanteVC ec = frmConsultaComprobante.SetParametros(dataFrm);
            bComprobanteVC bc = new bComprobanteVC();
            List<eComprobanteVC> list = bc.GetSelectComprobante(ec, indexPage, jtPageSize, jtSorting.Substring(1), out totalRow,"FORM");
            return new { Result = "OK", Records = list, TotalRecordCount = totalRow };
        }
        protected void btn_exportar_Click(object sender, ImageClickEventArgs e)
        {
            eComprobanteVC eo = new eComprobanteVC();
            if (txt_operacion_c.Text != "")
                eo._Nro_Comprobante = Int32.Parse(txt_operacion_c.Text);
            else
                eo._Nro_Comprobante = 0;
            if (ddl_tipcom_c.SelectedItem.Value != "0")
                eo._Tip_Comprobante = ddl_tipcom_c.SelectedItem.Value;
            else
                eo._Tip_Comprobante = "0";

            if (txt_fec_ini.Text != "" && txt_fec_hasta.Text != "")
            {
                eo._fecha_fin = Convert.ToDateTime(txt_fec_hasta.Text);
                eo._fecha_ini = Convert.ToDateTime(txt_fec_ini.Text);
                eo._token_fecha = "SI";
            }
            else
            {
                eo._fecha_fin = System.DateTime.Today;
                eo._fecha_ini = System.DateTime.Today;
                eo._token_fecha = "NO";
            }
            if(ddl_ramo_c.SelectedItem.Value != "0")
                eo._Cod_Ramo = ddl_ramo_c.SelectedItem.Value;
            else
                eo._Cod_Ramo = "0";
            if(ddl_contrato_c.SelectedItem.Value != "")
                eo._Ide_Contrato = ddl_contrato_c.SelectedItem.Value;
            else
                eo._Ide_Contrato = "0";

            eo._Formato_Moneda = formato_moneda;
            exportarDataExecl(eo);
        }
        private  static eComprobanteVC SetParametros( String[] dataFrm)
        {
            Int32 nro_comprob = 0;
            String fecha_token = "NO";
            if (dataFrm[3] != "") { nro_comprob = Int32.Parse(dataFrm[3]); }
            if (dataFrm[4] != "") { fecha_token = "SI"; }
            eComprobanteVC ec = new eComprobanteVC();
            ec._Ide_Contrato = dataFrm[0];
            ec._Tip_Comprobante = dataFrm[1];
            ec._Cod_Ramo = dataFrm[2];
            ec._Nro_Comprobante = nro_comprob;
            ec._Formato_Moneda = formato_moneda;
            if (dataFrm[4] != "" || dataFrm[5] != "")
            {
                ec._token_fecha = fecha_token;
                ec._fecha_ini = Convert.ToDateTime(dataFrm[4]);
                ec._fecha_fin = Convert.ToDateTime(dataFrm[5]);
            }
            else
            {
                ec._token_fecha = fecha_token;
                ec._fecha_ini = System.DateTime.Today;
                ec._fecha_fin = System.DateTime.Today;
            }
            return ec;
        }
        private void exportarDataExecl(eComprobanteVC o) {
            int total;
            bComprobanteVC bc = new bComprobanteVC();
            DataTable dt = bc.GetSelectComprobanteExport(o,0, 1000000, "NRO_COMPROBANTE ASC", out total);

            String filename = "COMPROBANTE " + ddl_tipcom_c.SelectedItem.Text + " " + String.Format("{0:dd/MM/yyyy}", System.DateTime.Today) + ".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            var output = new MemoryStream();
            var writer = new StreamWriter(output);

            HSSFWorkbook xssfworkbook = new HSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("COMPROBANTE");

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
            for (int cl1 = 0; cl1 < 11; cl1++)
            {
                sheet.SetColumnWidth(cl1, 150 * 25);
            }
            for (int cl = 11; cl <= 21; cl++)
            {
                sheet.SetColumnWidth(cl, 300 * 25);
            }

            IRow dataHeader = sheet.CreateRow(0);
            ICell dataCellH;
            for (int h = 0; h < dt.Columns.Count; h++) {
                dataCellH = dataHeader.CreateCell(h);
                dataCellH.SetCellValue(dt.Columns[h].ToString());
                dataCellH.CellStyle = styleCabecera;
                dataCellH.CellStyle.WrapText = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                IRow dataBody = sheet.CreateRow(1 + r);
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    dataBody.CreateCell(c, CellType.String).SetCellValue(dt.Rows[r][c].ToString());
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
    }
}