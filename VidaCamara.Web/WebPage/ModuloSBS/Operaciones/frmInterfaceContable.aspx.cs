using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class frmInterfaceContable : System.Web.UI.Page
    {
        static String mes_vigente_contable,formato_moneda;
        static Int32 anio_vigente_contable;
        bValidarAcceso accesso = new bValidarAcceso();
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
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
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_i);
                concepto.SetEstablecerDataSourceConcepto(ddl_tip_operacion_i, "12");
                txt_fecha_vigente_i.Text = meses[Convert.ToInt32(Session["mesvigente"])] + "-" + Session["aniovigente"].ToString();
                mes_vigente_contable = Session["mesvigente"].ToString();
                anio_vigente_contable = Convert.ToInt32(Session["aniovigente"]);
                formato_moneda = Session["formatomoneda"].ToString();
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectContabilidad(String contrato, String tipo_info)
        {
            eInterfaceContableVC contable = new eInterfaceContableVC();
            contable.Nro_Contrato = contrato;
            contable.Tipo_Info = tipo_info;
            contable.Mes_Vigente = mes_vigente_contable;
            contable.Anio_Vigente = anio_vigente_contable;
            contable.Formato_Moneda = formato_moneda;

            bInterfaceContableVC  bi = new bInterfaceContableVC();
            List<eInterfaceContableVC> listI = bi.GetSelectContable(contable);
            return new { Result = "OK", Records = listI};
        }

        protected void btn_procesar_Click(object sender, ImageClickEventArgs e)
        {
            SetInsertarAsientoContable();
        }
        protected void btn_exportar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                exportarDataExecl(SetEntity());
            }
            catch (Exception ex) {
                MessageBox("ERROR => "+ex.Message);
            }
        }
        protected void btn_borrar_Click(object sender, ImageClickEventArgs e)
        {
            SetEliminarExactus();
        }
        protected void btn_transfer_Click(object sender, ImageClickEventArgs e)
        {
            eInterfaceContableVC contable = new eInterfaceContableVC();
            contable.Nro_Contrato = ddl_contrato_i.SelectedItem.Value;
            contable.Anio_Vigente = Convert.ToInt32(Session["aniovigente"]);
            contable.Mes_Vigente = Session["mesvigente"].ToString();
            contable.Tipo_Info = ddl_tip_operacion_i.SelectedItem.Value;
            bInterfaceContableVC bcont = new bInterfaceContableVC();
            Int32 resp = bcont.SetTransferExactus(contable);
            if (resp > 0)
                MessageBox(resp+ " Registro (s) transferido (s) correctamente");
            else
                MessageBox("No se encontraron registros para este tipo de información <br> ó ya fueron transferidos, Revise el estado de transferencia",220);
        }
        private void SetInsertarAsientoContable() {
            eInterfaceContableVC econtable = new eInterfaceContableVC(); 
            econtable.Nro_Contrato = ddl_contrato_i.SelectedItem.Value;
            econtable.Tipo_Info = ddl_tip_operacion_i.SelectedItem.Value;
            econtable.Anio_Vigente = Convert.ToInt32(Session["aniovigente"]);
            econtable.Mes_Vigente = Session["mesvigente"].ToString();
            econtable.Usuario_Registro = Session["username"].ToString();

            bInterfaceContableVC contable = new bInterfaceContableVC();
            Int32 resp = contable.SetInsertarInterfaceContable(econtable);
            if (resp > 0)
                MessageBox(resp + " Asientos Generados.");
            else if (resp == -1)
                MessageBox("Ya existe asiento contable <br> para este tipo de información");
            else if (resp == -2)
                MessageBox("No se encontró cierre para.<br> Este tipo de informacion, realice el cierre");
            else
                MessageBox("No se pudo registrar los Asientos Contables");
        }
        private eInterfaceContableVC SetEntity() {
            eInterfaceContableVC contable = new eInterfaceContableVC();
            contable.Nro_Contrato = ddl_contrato_i.SelectedItem.Value;
            contable.Tipo_Info = ddl_tip_operacion_i.SelectedItem.Value;
            contable.Anio_Vigente = Convert.ToInt32(Session["aniovigente"]);
            contable.Mes_Vigente = Session["mesvigente"].ToString();
            contable.Formato_Moneda = Session["formatomoneda"].ToString();
            return contable;
        }
        private void SetEliminarExactus() {
            eInterfaceContableVC e = new eInterfaceContableVC();
            e.Nro_Contrato = ddl_contrato_i.SelectedItem.Value;
            e.Tipo_Info = ddl_tip_operacion_i.SelectedItem.Value;
            e.Anio_Vigente = Convert.ToInt32(Session["aniovigente"]);
            e.Mes_Vigente = Session["mesvigente"].ToString();

            bInterfaceContableVC b = new bInterfaceContableVC();
            Int32 resp = b.SetEliminarExactusDetalle(e);
            if (resp > 0)
            {
                MessageBox(resp - 1 + " Asientos Eliminados");
            }
            else {
                MessageBox("Ocurrió un error, vuelva intentar nuevamente");
            }
        }
        private void exportarDataExecl(eInterfaceContableVC e)
        {
            bInterfaceContableVC bc = new bInterfaceContableVC();
            DataTable dtlist = bc.GetSelectContableExport(e);

            String filename = "Registro Contable " + ddl_contrato_i.SelectedItem.Text + "-"+ddl_tip_operacion_i.SelectedItem.Text +System.DateTime.Today.ToShortDateString()+".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            var output = new MemoryStream();
            var writer = new StreamWriter(output);

            HSSFWorkbook xssfworkbook = new HSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("INTERFACE "+ddl_tip_operacion_i.SelectedItem.Text.ToUpper());

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
            for (int h = 0; h < dtlist.Columns.Count; h++)
            {
                dataCellH = dataHeader.CreateCell(h);
                dataCellH.SetCellValue(dtlist.Columns[h].ToString());
                dataCellH.CellStyle = styleCabecera;
                dataCellH.CellStyle.WrapText = true;
            }
            for (int r = 0; r < dtlist.Rows.Count; r++)
            {
                IRow dataBody = sheet.CreateRow(1 + r);
                for (int c = 0; c < dtlist.Columns.Count; c++)
                {
                    dataBody.CreateCell(c, CellType.String).SetCellValue(dtlist.Rows[r][c].ToString());
                }
            }
            xssfworkbook.Write(output);
            writer.Flush();

            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            dtlist.Clear();

            Response.BinaryWrite(output.GetBuffer());
            Response.End();

        }
        private void MessageBox(String text, Int32 heigth = 160)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:"+heigth+",buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
    }
}