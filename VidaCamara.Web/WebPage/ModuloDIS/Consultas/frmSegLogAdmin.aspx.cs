using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;
using VidaCamara.DIS.Negocio;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloDIS.Consultas
{
    public partial class frmLogAdmin : System.Web.UI.Page
    {
        #region VARIABLES
        static bValidarAcceso accesso = new bValidarAcceso();
        static int total;
        #endregion VARIABLES
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
                var concepto = new bTablaVC();
                SetLLenadoContrato();
                //concepto.SetEstablecerDataSourceConcepto(ddl_operacion, "26");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_evento,"25");
                txt_fec_hasta_o.Text = DateTime.Now.ToShortDateString();
                txt_fec_ini_o.Text = DateTime.Now.ToShortDateString();
            }
        }
        private void SetLLenadoContrato()
        {
            var list = new VidaCamara.SBS.Utils.Utility().getContratoSys(out total);
            ddl_contrato.DataSource = list;
            ddl_contrato.DataTextField = "_des_Contrato";
            ddl_contrato.DataValueField = "_ide_Contrato";
            ddl_contrato.DataBind();
            ddl_contrato.Items.Insert(0, new ListItem("Seleccione ----", "0"));
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listLogOperacion(int jtStartIndex, int jtPageSize,string jtSorting,HLogOperacion log,object[] filters)
        {
            //var listLogOperacion = new nLogOperacion().getListLogOperacion(log, jtStartIndex, jtPageSize, out total);
            //return new { Result = "OK", Records = listLogOperacion, TotalRecordCount = total };
            var listLogOperacion = new nLogOperacion().getListLogOperacion(log, jtStartIndex, jtPageSize, filters, out total);
            return new { Result = "OK", Records = listLogOperacion, TotalRecordCount = total };
        }

        protected void btn_exportar_Click(object sender, ImageClickEventArgs e)
        {
            var filters = new object[2] {txt_fec_ini_o.Text,txt_fec_hasta_o.Text };
            var eLog = new HLogOperacion() {
                IDE_CONTRATO = Convert.ToInt32(ddl_contrato.SelectedItem.Value),
                TipoOper = ddl_tipo_evento.SelectedItem.Value,
                Evento = txt_evento_descripcion.Text
            };
            var ruta = new nLogOperacion().descargarConsultaExcel(eLog, filters);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(ruta)));
            Response.TransmitFile(ruta);
            Response.End();
        }
    }
}