using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class RegistroDatos : System.Web.UI.Page
    {
        static String totalDevengue, totalContable, formato_moneda;
        static int totalContrato;
        static int mes_vigente_session,anio_vigente_session;
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
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
                bContratoVC contrato = new bContratoVC();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_p);
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_ib);
                concepto.SetEstablecerDataSourceConcepto(ddl_ramo_p, "05");
                concepto.SetEstablecerDataSourceConcepto(ddl_ramo_ib, "05");
                concepto.SetEstablecerDataSourceConcepto(ddl_producto_p, "04");
                concepto.SetEstablecerDataSourceConcepto(ddl_producto_ib, "04");
                GetDataGeneral();
            }
        }
        protected void btn_nuevo_Click(object sender, ImageClickEventArgs e)
        {
            int tab = Convert.ToInt16(menuTabs.SelectedValue);
            if (tab == 0)
                clearScreenPrima();
            else if (tab == 1)
                clearScreenIbnr();
        }
        protected void btn_borrar_r_Click(object sender, ImageClickEventArgs e)
        {
            SetEliminarDatoM(Int32.Parse(menuTabs.SelectedItem.Value));
        }
        protected void ddl_producto_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_contrato_p.SelectedItem.Value == "0")
                MessageBox("Seleccione Contrato");
            else if (ddl_ramo_p.SelectedItem.Value == "0")
                MessageBox("Seleccione Ramo");
            else
            {
                gvPrima.Caption = "Registro de " + ddl_ramo_p.SelectedItem.Text + " " + meses[mes_vigente_session] + "-" + anio_vigente_session;
                GetSelectDatoM(0);
            }
        }
        protected void ddl_producto_ib_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_contrato_ib.SelectedItem.Value == "0")
                MessageBox("Seleccione Contrato");
            else if (ddl_ramo_ib.SelectedItem.Value == "0")
                MessageBox("Seleccione Ramo");
            else
            {
                gvIbnr.Caption = "Registro de " + ddl_ramo_ib.SelectedItem.Text + " " + meses[mes_vigente_session] + "-" + anio_vigente_session;
                GetSelectDatoM(1);
            }
        }
        protected void gvPrima_RowEditing(object sender, GridViewEditEventArgs e)
        {
            eDatoM dm = SetEntityDatoM(0);
            gvPrima.EditIndex = e.NewEditIndex;
            gvPrima.DataSource = GetDatasourceGrid(dm, 0);
            gvPrima.DataBind();
        }

        protected void gvPrima_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            eDatoM dm = SetEntityDatoM(0);
            gvPrima.EditIndex = -1;
            gvPrima.DataSource = GetDatasourceGrid(dm, 0);
            gvPrima.DataBind();
        }
        private DataTable GetDatasourceGrid(eDatoM o, Int32 tab_selected)
        {
            bRegistroDatoVC rdm = new bRegistroDatoVC();
            DataTable dtr = rdm.GetSelectDatoMGrid(o);
            return dtr;
        }
        private eDatoM SetEntityDatoM(Int32 tab_selected)
        {
            eDatoM p = new eDatoM();
            if (tab_selected == 0)
            {
                p._nro_Contrato = ddl_contrato_p.SelectedItem.Value;
                p._tipo_info = "07";
                p._mes_Contable = SetConcatenarMesAnioContable();
                p._cod_Ramo = ddl_ramo_p.SelectedItem.Value;
                p._Formato_Moneda = formato_moneda;
            }
            else {
                p._nro_Contrato = ddl_contrato_ib.SelectedItem.Value;
                p._tipo_info = "08";
                p._mes_Contable = SetConcatenarMesAnioContable();
                p._cod_Ramo = ddl_ramo_ib.SelectedItem.Value;
                p._Formato_Moneda = formato_moneda;
            }
            return p;
        }

        protected void gvPrima_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            eDatoM p = new eDatoM();
            GridViewRow row = gvPrima.Rows[e.RowIndex];

            p._id_Empresa = Convert.ToInt32(Session["idempresa"]);
            p._ide_Data_M = Convert.ToInt32(((TextBox)(row.Cells[2].Controls[0])).Text.ToString());
            p._tipo_info = "07";
            p._nro_Contrato = ddl_contrato_p.SelectedItem.Value;
            p._anio_Vigente = anio_vigente_session;
            p._mes_Contable = SetConcatenarMesAnioContable();
            p._cod_Ramo = ddl_ramo_p.SelectedItem.Value;
            p._cod_Producto = ddl_producto_p.SelectedItem.Value;
            p._mto_Abonado = Convert.ToDecimal(((TextBox)(row.Cells[3].Controls[0])).Text.ToString());
            p._mto_Prima_Est = Convert.ToDecimal(((TextBox)(row.Cells[4].Controls[0])).Text.ToString());
            p._Formato_Moneda = formato_moneda;
            p._estado = "A";
            p._usu_mod = Session["username"].ToString();

            gvPrima.EditIndex = -1;

            bRegistroDatoVC ba = new bRegistroDatoVC();
            Int32 resp = ba.SetActualizarDatoM(p);
            if (resp > 0)
            {
                gvPrima.DataSource = GetDatasourceGrid(p, 0);
                gvPrima.DataBind();
            }
        }

        protected void gvIbnr_RowEditing(object sender, GridViewEditEventArgs e)
        {
            eDatoM dm = SetEntityDatoM(1);
            gvIbnr.EditIndex = e.NewEditIndex;
            gvIbnr.DataSource = GetDatasourceGrid(dm, 1);
            gvIbnr.DataBind();
        }

        protected void gvIbnr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            eDatoM dm = SetEntityDatoM(1);
            gvIbnr.EditIndex = -1;
            gvIbnr.DataSource = GetDatasourceGrid(dm, 1);
            gvIbnr.DataBind();
        }

        protected void gvIbnr_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            eDatoM p = new eDatoM();
            GridViewRow row = gvIbnr.Rows[e.RowIndex];

            p._id_Empresa = Convert.ToInt32(Session["idempresa"]);
            p._ide_Data_M = Convert.ToInt32(((TextBox)(row.Cells[2].Controls[0])).Text.ToString());
            p._tipo_info = "08";
            p._nro_Contrato = ddl_contrato_ib.SelectedItem.Value;
            p._anio_Vigente = anio_vigente_session;
            p._mes_Contable = SetConcatenarMesAnioContable();
            p._cod_Ramo = ddl_ramo_ib.SelectedItem.Value;
            p._cod_Producto = ddl_producto_ib.SelectedItem.Value;
            p._mto_Abonado = Convert.ToDecimal(((TextBox)(row.Cells[3].Controls[0])).Text.ToString());
            p._mto_Prima_Est = Convert.ToDecimal(((TextBox)(row.Cells[4].Controls[0])).Text.ToString());
            p._Formato_Moneda = formato_moneda;
            p._estado = "A";
            p._usu_mod = Session["username"].ToString();

            gvIbnr.EditIndex = -1;

            bRegistroDatoVC ba = new bRegistroDatoVC();
            Int32 resp = ba.SetActualizarDatoM(p);
            if (resp > 0)
            {
                gvIbnr.DataSource = GetDatasourceGrid(p, 1);
                gvIbnr.DataBind();
            }
        }
        protected void btn_detalle_Click(object sender, EventArgs e)
        {
            int tabSelected  = Int32.Parse(menuTabs.SelectedItem.Value);
            if (tabSelected == 0)
            {
                eDatoM edm = new eDatoM();
                edm._nro_Contrato = ddl_contrato_p.SelectedItem.Value;
                edm._tipo_info = "07";
                edm._cod_Producto = ddl_producto_p.SelectedItem.Value;
                edm._cod_Ramo = ddl_ramo_p.SelectedItem.Value;
                edm._anio_Vigente = anio_vigente_session;
                edm._mes_Vigente = mes_vigente_session;
                edm._Formato_Moneda = formato_moneda;

                bRegistroDatoVC ba = new bRegistroDatoVC();
                gvDatoM.Caption = "Registro "+ddl_ramo_p.SelectedItem.Text;
                gvDatoM.DataSource = ba.GetSelectdatoM(edm, out totalContable, out totalDevengue);
                gvDatoM.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('divPopPup').style.visibility = 'visible';", true);
                lbl_totAbono.Text = totalContable;
                lbl_totDevengue.Text = totalDevengue;
            }
            else {

                eDatoM edm = new eDatoM();
                edm._nro_Contrato = ddl_contrato_ib.SelectedItem.Value;
                edm._tipo_info = "08";
                edm._cod_Producto = ddl_producto_ib.SelectedItem.Value;
                edm._cod_Ramo = ddl_ramo_ib.SelectedItem.Value;
                edm._mes_Vigente = mes_vigente_session;
                edm._Formato_Moneda = formato_moneda;

                bRegistroDatoVC ba = new bRegistroDatoVC();
                gvDatoM.Caption = "Registro "+ddl_ramo_ib.SelectedItem.Text;
                gvDatoM.DataSource = ba.GetSelectdatoM(edm, out totalContable, out totalDevengue);
                gvDatoM.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('divPopPup').style.visibility = 'visible';", true);
                lbl_toto_Abono_ib.Text = totalContable;
                lbl_tot_devengue_ib.Text = totalDevengue;
            }
        }
        private void GetSelectDatoM(Int32 tab_selected){

            eDatoM edm = this.SetEntityDatoM(tab_selected);
            DataTable dtresp = this.GetDatasourceGrid(edm, tab_selected);
            if (dtresp.Rows.Count > 0 && tab_selected == 0)
            {
                gvPrima.DataSource = dtresp;
                gvPrima.DataBind();
            }
            else if (dtresp.Rows.Count > 0 && tab_selected == 1) {
                gvIbnr.DataSource = dtresp;
                gvIbnr.DataBind();
            }
            else
            {
                Boolean resp = SetInsertarDataM(tab_selected);
                if (resp == true && tab_selected == 0)
                {
                    gvPrima.DataSource = this.GetDatasourceGrid(edm, tab_selected);
                    gvPrima.DataBind();
                }
                else if (resp == true && tab_selected == 1)
                {
                    gvIbnr.DataSource = this.GetDatasourceGrid(edm, tab_selected);
                    gvIbnr.DataBind();
                }
                else
                    MessageBox("No se encontró registros ingresados para el mes anterior");
            }

        }
        //funciones de grabado de primas Y ibnr
        private Boolean SetInsertarDataM(Int32 tab_selected)
        {
            var ecn = new eContratoVC()
            {
                _inicio = 0,
                _fin = 1000000,
                _orderby = "IDE_CONTRATO ASC",
                _nro_Contrato = tab_selected == 0 ? ddl_contrato_p.SelectedItem.Value : ddl_contrato_ib.SelectedItem.Value,
                _estado = "A"
            };
            var bcn = new bContratoVC();
            var list = bcn.GetSelecionarContrato(ecn,out totalContrato);
            DateTime inicio_contrato = list[0]._fec_Ini_Vig;
            DateTime fin_contrato = list[0]._fec_Fin_Vig;
            Int32 mes_vigente = inicio_contrato.Month;

            //Validar que se hayan ingresado todos los meses en forma secuencial
            var datoM = new eDatoM() {
                _nro_Contrato = ecn._nro_Contrato ,
                _tipo_info = tab_selected==0?"07":"08",
                _cod_Ramo = ddl_ramo_p.SelectedItem.Value,
                _cod_Producto = ddl_producto_p.SelectedItem.Value
            };
            var listDatoM = new bRegistroDatoVC().listMesDevengue(datoM);
            var mesContable = SetConcatenarMesAnioContable().ToString();
            var mesInicioContrt = Convert.ToInt32(inicio_contrato.Year.ToString() + "" + (mes_vigente<10?"0"+ mes_vigente.ToString(): mes_vigente.ToString()));
            var sigueSecuencia = false;
            if (listDatoM.Count > 0)
            {
                mesContable = Convert.ToInt32(mesContable.Substring(4, 2)) == 1 ? (Convert.ToInt32(mesContable.Substring(0, 4)) - 1).ToString() + "12" : (Convert.ToInt32(mesContable)-1).ToString();
                var encontroAnterior = listDatoM.FindAll(a => a._mes_Contable == (int.Parse(mesContable)));
                if (encontroAnterior.Count > 0)
                    sigueSecuencia = true;
            }

            if (Convert.ToInt32(mesContable) == mesInicioContrt || sigueSecuencia == true)
            {
                try
                {
                    var dm = new bRegistroDatoVC();
                    Int32 total_mes_contrato = dm.CalcularMesesDeDiferencia(inicio_contrato, fin_contrato);
                    var listdm = new List<eDatoM>();
                    for (int m = 0; m <= total_mes_contrato; m++)
                    {
                        if (mes_vigente > 12)
                        {
                            mes_vigente = 1;
                        }
                        var  p = new eDatoM();
                        if (tab_selected == 0)
                        {
                            p._id_Empresa = Convert.ToInt32(Session["idempresa"]);
                            p._tipo_info = "07";
                            p._nro_Contrato = ddl_contrato_p.SelectedItem.Value;
                            p._anio_Vigente = anio_vigente_session;
                            p._mes_Vigente = SetCalculaMesDevengue(inicio_contrato.Year, inicio_contrato.Month, m, mes_vigente);
                            p._mes_Contable = SetConcatenarMesAnioContable();
                            p._cod_Ramo = ddl_ramo_p.SelectedItem.Value;
                            p._cod_Producto = ddl_producto_p.SelectedItem.Value;
                            p._mto_Abonado = 0.00m;
                            p._mto_Prima_Est = 0.00m;
                            p._Formato_Moneda = formato_moneda;
                            p._estado = "A";
                            p._usu_reg = Session["username"].ToString();
                        }
                        else {
                            p._id_Empresa = Convert.ToInt32(Session["idempresa"]);
                            p._nro_Contrato = ddl_contrato_ib.SelectedItem.Value;
                            p._tipo_info = "08";
                            p._anio_Vigente = anio_vigente_session;
                            p._mes_Vigente = SetCalculaMesDevengue(inicio_contrato.Year, inicio_contrato.Month, m, mes_vigente);
                            p._mes_Contable = SetConcatenarMesAnioContable();
                            p._cod_Ramo = ddl_ramo_ib.SelectedItem.Value;
                            p._cod_Producto = ddl_producto_ib.SelectedItem.Value;
                            p._mto_Abonado = 0.00m;
                            p._mto_Prima_Est = 0.00m;
                            p._estado = "A";
                            p._usu_reg = Session["username"].ToString();
                        }

                        listdm.Add(p);
                        mes_vigente++;
                    }
                    dm.SetInsertarDatoM(listdm);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox("ERROR => " +ex.Message.Replace(Environment.NewLine,""));
                    throw;
                }
            }
            else
                return false;
        }
        private void SetEliminarDatoM(Int32 tabIdex)
        {
            try
            {
                eDatoM edm = new eDatoM();
                if (tabIdex == 0)
                {
                    edm._nro_Contrato = ddl_contrato_p.SelectedItem.Value;
                    edm._tipo_info = "07";
                    edm._cod_Producto = ddl_producto_p.SelectedItem.Value;
                    edm._cod_Ramo = ddl_ramo_p.SelectedItem.Value;
                    edm._mes_Contable = SetConcatenarMesAnioContable();
                }
                else if (tabIdex == 1)
                {
                    edm._nro_Contrato = ddl_contrato_ib.SelectedItem.Value;
                    edm._tipo_info = "08";
                    edm._cod_Producto = ddl_producto_ib.SelectedItem.Value;
                    edm._cod_Ramo = ddl_ramo_ib.SelectedItem.Value;
                    edm._mes_Contable = SetConcatenarMesAnioContable();
                }

                bRegistroDatoVC brd = new bRegistroDatoVC();
                Int32 resp = brd.SetEliminarDatoM(edm);
                if (resp != 0)
                {
                    MessageBox(resp + " Registro (s) Eliminado (s) Correctamente");
                    if (tabIdex == 0)
                    {
                        gvPrima.DataSource = this.GetDatasourceGrid(edm, tabIdex);
                        gvPrima.DataBind();
                    }
                    else {
                        gvIbnr.DataSource = this.GetDatasourceGrid(edm, tabIdex);
                        gvIbnr.DataBind();
                    }
                }
                else
                {
                    MessageBox("No hay registros para realizar esta acción.");
                }
            }
            catch (Exception e)
            {
                MessageBox("ERROR =>" + e.Message);
            }
        }

        private Int32 SetCalculaMesDevengue(Int32 anio_devengue_ini, Int32 mes_devengue_ini, Int32 index_row, Int32 mes_actual)
        {
            Int32 devengue = 0;
            Int32 anio_devengue = (mes_devengue_ini + index_row) - 1;
            if (mes_actual < 10)
            {
                devengue = Convert.ToInt32(anio_devengue_ini + Convert.ToInt32(anio_devengue / 12) + "0" + mes_actual);
            }
            else
            {
                devengue = Convert.ToInt32(anio_devengue_ini + Convert.ToInt32(anio_devengue / 12) + "" + mes_actual);
            }
            return devengue;
        }

        private Int32 SetConcatenarMesAnioContable() {
            Int32 mes_contable_resp = 0;
            if (mes_vigente_session < 10)
                mes_contable_resp = Convert.ToInt32(anio_vigente_session + "0" + mes_vigente_session);
            else
                mes_contable_resp = Convert.ToInt32(anio_vigente_session + "" + mes_vigente_session);
            return mes_contable_resp;
        }

        private void GetDataGeneral()
        {
            mes_vigente_session = Convert.ToInt32(Session["mesvigente"]);
            anio_vigente_session = Convert.ToInt32(Session["aniovigente"]);
            formato_moneda = Session["formatomoneda"].ToString();
            txt_mesContable_p.Text = meses[mes_vigente_session] + "-" + anio_vigente_session;
            txt_mesContable_ib.Text = meses[mes_vigente_session] + "-" + anio_vigente_session;
        }
      
        private void clearScreenIbnr()
        {
            ddl_producto_ib.SelectedValue = "0";
            ddl_contrato_ib.SelectedValue = "0";
            ddl_ramo_ib.SelectedIndex = 0;
            ddl_producto_ib.SelectedIndex = 0;
            gvIbnr.DataSource = null;
            gvIbnr.DataBind();
        }

        private void clearScreenPrima()
        {
            ddl_producto_p.SelectedValue = "0";
            ddl_contrato_p.SelectedValue = "0";
            ddl_ramo_p.SelectedIndex = 0;
            lbl_totAbono.Text = "0";
            lbl_totDevengue.Text = "0";
            gvPrima.DataSource = null;
            gvPrima.DataBind();
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
    }
}