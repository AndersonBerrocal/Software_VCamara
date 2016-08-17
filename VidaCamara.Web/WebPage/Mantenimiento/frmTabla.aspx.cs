using System;
using System.Collections.Generic;
using System.Web.UI;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;


namespace VidaCamara.Web.WebPage.Mantenimiento
{
    public partial class frmTabla : System.Web.UI.Page
    {
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
                bTablaVC concepto = new bTablaVC();
                concepto.SetEstablecerDataSourceConcepto(ddl_tabla_t,"9999");
                concepto.SetEstablecerDataSourceConcepto(ddl_estado_t,"09");
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ListConcepto(int jtStartIndex, int jtPageSize, string jtSorting, String TipoTabla,String descripcion)
        {
            int indexPage;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else
            {
                indexPage = jtStartIndex;
            }
            eTabla o = new eTabla();
            bTablaVC tb = new bTablaVC();
            o._id_Empresa = 0;
            o._tipo_Tabla = TipoTabla.Trim();
            o._descripcion = descripcion;
            o._valor = "N";
            o._estado = "S";
            o._inicio = indexPage;
            o._fin = jtPageSize;
            o._order = jtSorting.Substring(1).ToUpper();


            int total;
            List<eTabla> list = tb.GetSelectConcepto(o,out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetConceptoByCodigo(String codigo)
        {
            bTablaVC t = new bTablaVC();
            return new { Result = "OK", Records = t.GetConceptoByCodigo(codigo)};
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetConceptoByTipo(string tipo)
        {
            return new {Records = new bTablaVC().getConceptoByTipo(tipo)};
        }

        protected void btn_enviar_t_Click(object sender, ImageClickEventArgs e)
        {
            SetInsertTabla();
        }

        protected void btn_nuevo_t_Click(object sender, ImageClickEventArgs e)
        {
            clearScreen();
        }

        protected void btn_borrar_Click(object sender, ImageClickEventArgs e)
        {
            SetEliminarConcepto(txt_idtabla.Value);
        }

        private void SetInsertTabla() {
            try
            {
                if (txt_idtabla.Value.Equals("0") && txt_codigo_t.Text != "")
                {
                    eTabla t = new eTabla();
                    t._id_Empresa = 1;
                    if (ddl_tabla_t.SelectedItem.Value.Equals("0"))
                    {
                        t._tipo_Tabla = "9999";
                    }
                    else
                    {
                        t._tipo_Tabla = ddl_tabla_t.SelectedItem.Value;
                    }
                    t._codigo = txt_codigo_t.Text;
                    t._descripcion = txt_descripcion_t.Text;
                    t._valor = txt_valor_t.Text;
                    t._clase = txt_clase_t.Text;
                    t._tipo = txt_tipo_t.Text;
                    t._estado = ddl_estado_t.SelectedItem.Value;
                    t._usu_reg = Session["username"].ToString();

                    bTablaVC bt = new bTablaVC();
                    Int32 resp = bt.SetInsertarConcepto(t);
                    if (resp != 0)
                    {
                        MessageBox("Registro Grabado Correctamente!");
                        if (t._tipo_Tabla == "9999")
                        {
                            bTablaVC concepto = new bTablaVC();
                            concepto.SetEstablecerDataSourceConcepto(ddl_tabla_t, "9999");
                        }
                    }
                    else
                    {
                        MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
                else
                {
                    SetActualizarTabla();
                }
            }
            catch (Exception e) { 
                MessageBox("ERROR =>"+e.Message);
            }

        }
        //actualizar tabla concepto
        private void SetActualizarTabla()
        {
            try
            {
                eTabla t = new eTabla();
                if (Convert.ToInt32(txt_idtabla.Value) != 0 && txt_codigo_t.Text != "")
                {
                    t._id_Concepto = Convert.ToInt32(txt_idtabla.Value);
                    t._id_Empresa = 1;
                    if (txt_9999.Value.Equals("9999"))
                    {
                        t._tipo_Tabla = "9999";
                    }
                    else
                    {
                        t._tipo_Tabla = ddl_tabla_t.SelectedItem.Value;
                    }
                    t._codigo = txt_codigo_t.Text;
                    t._descripcion = txt_descripcion_t.Text;
                    t._valor = txt_valor_t.Text;
                    t._clase = txt_clase_t.Text;
                    t._tipo = txt_tipo_t.Text;
                    t._estado = ddl_estado_t.SelectedItem.Value;
                    t._usu_mod = Session["username"].ToString();

                    bTablaVC bt = new bTablaVC();
                    Int32 resp = bt.SetActualizarConcepto(t);
                    if (resp != 0)
                    {
                        MessageBox("Registro Actualizado Correctamente");
                        if (t._tipo_Tabla == "9999")
                        {
                            bTablaVC concepto = new bTablaVC();
                            concepto.SetEstablecerDataSourceConcepto(ddl_tabla_t, "9999");
                        }
                    }
                    else
                    {
                        MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
                else {
                    MessageBox("Ingrese Datos");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }

        private void SetEliminarConcepto(String indice) {
            try
            {
                bTablaVC bt = new bTablaVC();
                Int32 resp = bt.SetEliminarConcepto(Int32.Parse(indice));
                if (resp != 0)
                {
                    MessageBox(resp +" Registro Eliminado Correctamente!");
                    bTablaVC concepto = new bTablaVC();
                    concepto.SetEstablecerDataSourceConcepto(ddl_tabla_t, "9999");
                    clearScreen();
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) {
                MessageBox("ERROR =>" + e.Message);
            }
        }

        private void clearScreen() {
            ddl_tabla_t.SelectedValue = "0";
            txt_clase_t.Text = "";
            txt_descripcion_t.Text = "";
            txt_tipo_t.Text = "";
            txt_valor_t.Text = "";
            ddl_estado_t.SelectedValue = "A";
            txt_codigo_t.Text = "";
            txt_idtabla.Value = "0";
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
    }
}