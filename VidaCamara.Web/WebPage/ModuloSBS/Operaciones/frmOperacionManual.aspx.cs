using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class frmOperacionManual : System.Web.UI.Page
    {
        bValidarAcceso accesso = new bValidarAcceso();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "OTROS";
            if (Session["username"] == null)
                Response.Redirect("Login");
            else
            {
                if (!accesso.GetValidarAcceso(Request.QueryString["go"]))
                {
                    Response.Redirect("Login?go=0");
                }
            }
            if (!IsPostBack)
            {
                bTablaVC concepto = new bTablaVC();
                bContratoVC contrato = new bContratoVC();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_m);
                concepto.SetEstablecerDataSourceConcepto(ddl_reasegurador_m,"01");
                concepto.SetEstablecerDataSourceConcepto(ddl_codramo_m, "05");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipope_m, "12");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipreg_m, "15");
                concepto.SetEstablecerDataSourceConcepto(ddl_codasegurado_m, "14");
                concepto.SetEstablecerDataSourceConcepto(ddl_codmoneda_m, "10");
            }
        }

        protected void btnGuardar_m_Click(object sender, ImageClickEventArgs e)
        {
            SetGuardarOperacionManual();
        }
        private void SetGuardarOperacionManual() {

            eOperacionVC eo = new eOperacionVC();
            eo._Ide_Contrato = ddl_contrato_m.SelectedItem.Value;
            eo._Tip_Operacion = ddl_tipope_m.SelectedItem.Value;
            eo._Tipo_Registro = ddl_tipreg_m.SelectedItem.Value;
            eo._Cod_Reasegurador = ddl_reasegurador_m.SelectedItem.Value;
            eo._Ano_Operacion = Convert.ToInt32(Session["aniovigente"]);
            eo._Mes_Operacion = Session["mesvigente"].ToString();
            eo._Cod_Asegurado = ddl_codasegurado_m.SelectedItem.Value;
            eo._Cod_Ramo = ddl_codramo_m.SelectedItem.Value;
            eo._Cod_Moneda = ddl_codmoneda_m.SelectedItem.Value;
            eo._Pri_Ced_Mes = txt_primaced_m.Text;
            eo._Des_Reg_Mes = "OPERACION " + ddl_tipope_m.SelectedItem.Text + " (MANUAL)";
            eo._Imp_Impuesto_Mes = txt_impuesto_m.Text;
            eo._Pri_Xpag_Rea_Ced = txt_prima_x_pag_m.Text;
            eo._Pri_Xcob_Rea_Ace = txt_prima_x_cob_m.Text;
            eo._Sin_Directo = txt_sin_directo_m.Text;
            eo._Sin_Xcob_Rea_Ced = txt_sin_x_cob_m.Text;
            eo._Sin_Xpag_Rea_Ace = txt_sin_x_pag_m.Text;
            eo._Otr_Cta_Xcob_Rea_Ced = txt_otr_x_cob_m.Text;
            eo._Otr_Cta_Xpag_Rea_Ace = txt_otr_x_pag_m.Text;
            eo._Dscto_Comis_Rea = txt_dscto_comis_m.Text;
            eo._Estado = "A";
            eo._Usu_Reg = Session["username"].ToString();
            eo._Tip_Comprobante = ddl_comprobante.SelectedItem.Value;
            eo._Origen_Operacion = Convert.ToInt16(ddl_origen_operacion.SelectedItem.Value);

            bOperacionVC bo = new bOperacionVC();
            Int32 resp = bo.SetGuardarOperacionManual(eo);
            if (resp != 0) {
                MessageBox("Operación Guardada Correctamente");
            }
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:200,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }

        protected void ddl_tipope_m_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = new bTablaVC().getConceptoByTipo("13");
            var listConcepto = list.FindAll(o => o._tipo.Trim().Equals(ddl_tipope_m.SelectedItem.Value));
            ddl_comprobante.DataSource = listConcepto;
            ddl_comprobante.DataTextField = "_descripcion";
            ddl_comprobante.DataValueField = "_codigo";
            ddl_comprobante.DataBind();
            ddl_comprobante.Items.Insert(0, new ListItem("Seleccione ----", "0"));
        }
    }
}