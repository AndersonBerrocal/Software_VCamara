using System;

namespace VidaCamara.Web.WebPage.Inicio
{
    public partial class mpFEPCMAC : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Session["pagina"].ToString().Equals("USUARIO"))
            {
                lbl_title.Text = "Sistema de Gestión Vida Cámara";
                lbl_titulo.Text = "";
            }else{
                lbl_title.Text = "Mantenimiento de -";
                lbl_titulo.Text = "USUARIOS";
            }
            lbl_usuario.Text = Session["username"].ToString();
            lbl_conexion.Text = System.DateTime.Now.ToString();
        }
    }
}
