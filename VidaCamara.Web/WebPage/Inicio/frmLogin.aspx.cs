using System;
using System.Collections.Generic;
using ActiveDirectoryValidation;
using Seguridadv2;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;
using System.Configuration;

namespace VidaCamara.Web.WebPage.Inicio
{
    public partial class frmLogin : System.Web.UI.Page
    {
        #region VARIABLES
        List<eUsuarioActiveDirec> lstUsuarioAD = new List<eUsuarioActiveDirec>();
        ADFunctions oADFunctions = new ADFunctions();
        bUsuarioVC busuario = new bUsuarioVC();
        #endregion VARIABLES
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["pagina"] = "OTROS";
                if (Request.QueryString["go"].ToString().Equals("13"))
                {
                    Session.Abandon();
                    Response.Redirect("Login?go=0");
                }
            }
            catch (Exception) {
                Response.Redirect("Login?go=0");
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidarUsuarioEnActiveDirectory() == true)
                Response.Redirect("Inicio");
            else
            {
                lbl_error_login.Text = "Usuario y/o Contraseña Incorrectos";
                txtContrasena.Text = "";
                txtUsuario.Text = "";
            }
        }
        public bool ValidarUsuarioEnActiveDirectory()
        {
            //txtUsuario.Text.Equals("reaseguros"))//
            if (txtUsuario.Text.Equals("reaseguros"))//oADFunctions.FnValidarUsuario(ConfigurationManager.AppSettings.Get("Dominio"), txtUsuario.Text, txtContrasena.Text, ConfigurationManager.AppSettings.Get("UrlLDAP")))
            {
                Session["username"] = txtUsuario.Text;
                //Guardamos el usuario y la clave AD en una sesión:
                eUsuarioVC eUsuario = new eUsuarioVC();
                eUsuario._Usuario = txtUsuario.Text.ToString();
                eUsuario._Contrasena = FNSeguridad.EncriptarConClave(txtContrasena.Text.ToString(), "11254125852587458124587485215895");
                eUsuario._usu_reg = Session["username"].ToString();
                eUsuario._Aceso_Pagina = "99";

                Int32 existe_usuario = busuario.SetValidarUsuario(eUsuario);
                if (existe_usuario == 1)//validar si el usuario existe;
                {
                    Session["usernameId"] = eUsuario._ide_Usuario;
                    ListarAccesoPagina(eUsuario._Usuario, eUsuario._Contrasena);
                    return true;
                }
                else if (existe_usuario == -1)
                    return false;
                else if (existe_usuario == 0)
                {
                    //lstUsuarioAD = oADFunctions.FnRecuperarDatos("sAMAccountName", txtUsuario.Text, ConfigurationManager.AppSettings.Get("urlLDAP"), eUsuario._Usuario, eUsuario._Contrasena);
                    eUsuario._cargo = "NULL";//lstUsuarioAD[0]._title;
                    eUsuario._email = "NULL";//lstUsuarioAD[0]._mail;
                    eUsuario._tipo_Usuario = "NULL";//lstUsuarioAD[0]._memberOf;
                    eUsuario._nombres = "NULL";//lstUsuarioAD[0]._name;
                    if (busuario.SetInsertarUsuario(eUsuario) > 0) //inserta usuario return true
                    {
                        ListarAccesoPagina(eUsuario._Usuario, eUsuario._Contrasena);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void ListarAccesoPagina(String usuario,String contrasena)
        {
            List<eUsuarioVC> list = busuario.GetListAccessoSessionUsuario(usuario,contrasena);
            Session["accesos"] = list[0]._Aceso_Pagina;
            Session["ide_user"] = list[0]._ide_Usuario;
        }
    }
}