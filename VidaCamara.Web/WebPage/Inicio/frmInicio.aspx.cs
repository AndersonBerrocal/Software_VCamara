using System;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.Inicio
{
    public partial class frmInicio : System.Web.UI.Page
    {
        bGeneralVC bg = new bGeneralVC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "OTROS";
            if (Session["username"] == null)
                Response.Redirect("Login?go=0");
            else
            {
                bg.ParametroListSession();
            }
        }
    }
}