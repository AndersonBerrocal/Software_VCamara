using System;
using System.Collections.Generic;
using System.Web.UI;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.Mantenimiento
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        bValidarAcceso accesso = new bValidarAcceso();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "USUARIO";
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
        public static object ListUsuario(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            bUsuarioVC busuario = new bUsuarioVC();
            var indexPage = jtStartIndex != 0?jtStartIndex / jtPageSize:jtStartIndex;
            int total;
            List<eUsuarioVC> list = busuario.GetSelectUsuario(indexPage,jtPageSize,jtSorting.Substring(1),out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ListAccesopagina(String ide_usuario)
        {
            bUsuarioVC usuario = new bUsuarioVC();
            List<eAccesoPagina> list = usuario.GetListaPagina(Convert.ToInt32(ide_usuario));
            return new { Result = "OK", Records = list};
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Int32 tabselected = Convert.ToInt32(menuTabs.SelectedValue);
            if(tabselected == 0)
                SetActualizarUsuario();
            else if(tabselected == 1)
                SetActualizarAccesoUsuario();
            else
                return;
        }
        //funcion para actualizar acceso de suuarios
        private void SetActualizarAccesoUsuario()
        {
            eUsuarioVC usuario = new eUsuarioVC();
            usuario._ide_Usuario = Convert.ToInt32(ide_usuario.Value);
            usuario._Aceso_Pagina = lista_pagina.Value;
            bUsuarioVC busuario = new bUsuarioVC();
            Int32 resp = busuario.SetActualizarAccesoPagina(usuario);
            if (resp != 0)
            {
                MessageBox("Accesos Actualizados");
            }
        }
        //FUNCION DE INSERTAR USUARIO
        private void SetActualizarUsuario() {
            eUsuarioVC u = new eUsuarioVC();
            u._ide_Usuario = Convert.ToInt64(txt_id_usuario.Value);
            u._Usuario = txt_usuario.Text;
            u._nombres = txt_nombres.Text;
            u._cargo = txt_cargo.Text;
            u._email = txt_email.Text;
            u._tipo_Usuario = txt_tipo_usuario.Text;
            u._estado = ddl_estado.SelectedItem.Value;
            u._usu_mod = Session["username"].ToString();
            bUsuarioVC usuario = new bUsuarioVC();
            Int32 resp = usuario.SetActualizarUsuario(u);
            if(resp != 0){
                MessageBox("Usuario Actualizado");
            }
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div>" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:170,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }

    }
}