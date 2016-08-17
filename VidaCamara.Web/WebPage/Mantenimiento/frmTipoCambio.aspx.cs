using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Negocio;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.Mantenimiento
{
    public partial class frmTipoCambio : System.Web.UI.Page
    {
        #region VARIABLES
        static int total;
        static bValidarAcceso accesso = new bValidarAcceso();
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
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listTipoCambio(int jtStartIndex, int jtPageSize, string jtSorting, TipoCambio cambio)
        {
            var listCambio = new nTipoCambio().listTipoCambio(cambio, jtStartIndex, jtPageSize, jtSorting, out total);
            return new { Result = "OK", Records = listCambio, TotalRecordCount = total };
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object setInactivaTipo(int IdTipoCambio, bool Vigente)
        {
            try
            {
                new nTipoCambio().updateTipoCambio(new TipoCambio() { IdTipoCambio = IdTipoCambio,Vigente = Vigente});
                return new { Result = true };
            }
            catch (Exception ex)
            {
                return new { Result = ex.Message };
            }
        }
        protected void btn_guardar_Click(object sender, ImageClickEventArgs e)
        {
            grabarTipoCambio();
        }

        private void grabarTipoCambio()
        {
            try
            {
                var tipoCambio = new TipoCambio()
                {
                    Periodo = txt_periodo.Text,
                    Monto = Convert.ToDecimal(txt_monto.Text),
                    Vigente = true
                };
                new nTipoCambio().saveTipoCambio(tipoCambio);
                MessageBox(string.Format("{0}", "Tipo de cambio guardado correctamente."));
            }
            catch (Exception ex)
            {
                MessageBox(string.Format("{0}", ex.Message.ToString().Replace("'","").Replace(Environment.NewLine,"")));
            }
        }
        private void MessageBox(string text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:240,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]});", true);
        }
        
    }
}