using System;

namespace VidaCamara.Web.WebPage.Inicio
{
    public partial class frmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "OTROS";
        }
    }
}