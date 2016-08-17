using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloSBS.Operaciones
{
    public partial class frmProcesaOperacion : System.Web.UI.Page
    {
        static String[] meses = {"","Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"};
        static String[] trimestre = {"Primero","Segundo","Tercero","Cuarto"};
        static int anio_vigente,mes_vigente;
        static String formato_moneda;
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
                bContratoVC contrato = new bContratoVC();
                bTablaVC concepto = new bTablaVC();
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_o);
                contrato.SetEstablecerDataSourceContrato(ddl_contrato_det);
                concepto.SetEstablecerDataSourceConcepto(ddl_tipinfo,"12");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipinfo_det, "12");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_registro,"15");
                GetDataGeneral();
                SetCalcularTrimestre("SI");
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static object SetEliminarOperacion(Int32 _Nro_Operacion)
        {
            bOperacionSelectVC ope = new bOperacionSelectVC();
            ope.SetEliminarOperacion(_Nro_Operacion);
            return new { Result = "OK" };
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetSelectOperacionDetalle(int jtStartIndex, int jtPageSize, string jtSorting, String[] data)
        {
            int totalI;
            int indexPage;
            if (jtStartIndex != 0)
            {
                indexPage = jtStartIndex / jtPageSize;
            }
            else
            {
                indexPage = jtStartIndex;
            }
            eOperacionVC ope = new eOperacionVC();
            ope._Ide_Contrato = data[0];
            ope._Tip_Operacion = data[1];
            ope._Mes_Operacion = mes_vigente.ToString();
            ope._Ano_Operacion = anio_vigente;
            ope._Formato_Moneda = formato_moneda;

            bOperacionSelectVC bos = new bOperacionSelectVC();
            List<eOperacionVC> listI = bos.GetSelectOperacionDetalle(ope, indexPage, jtPageSize, jtSorting.Substring(1), out totalI);
            return new { Result = "OK", Records = listI, TotalRecordCount = totalI };
        }
        /*protected void ddl_contrato_o_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCalcularTrimestre(ddl_contrato_o.SelectedItem.Value);
        }*/

        protected void btn_procesa_ope_Click(object sender, ImageClickEventArgs e)
        {
            SetProcesaOperacion();
        }

        private void SetProcesaOperacion() {
            Int32 existe_mes_actual = 0;
            Int32 existe_mes_anterior = 0;
            Int32 existe_x_triemstre = 0;

            eValidarCierreVC ev = new eValidarCierreVC();
            ev._Nro_Contrato = ddl_contrato_o.SelectedItem.Value;
            ev._Tipo_Info = ddl_tipinfo.SelectedItem.Value;
            ev._Tipo_Registro = ddl_tipo_registro.SelectedItem.Value;
            ev._Mes_Actual = mes_vigente;
            ev._Anio_Operacion = anio_vigente;

            bValidarCierreVC bv = new bValidarCierreVC();
            List<eValidarCierreVC> validar = bv.GetSelecionarValidarCierre(ev);

            if (ev._Tipo_Registro.ToUpper() == "MEN") {
                existe_mes_actual = validar[0]._Mes_Actual;
                existe_mes_anterior = validar[0]._Mes_Anterior;
            }else{
                existe_mes_actual = validar[0]._Mes_Actual;
                existe_x_triemstre = validar[0]._Mes_x_Trimestral;
            }
            SetValidarCierreXTipoRegistro(ev._Tipo_Registro,existe_mes_actual,existe_mes_anterior,existe_x_triemstre);
        }
        private void SetValidarCierreXTipoRegistro(String tiporegistro,Int32 mes_actual,Int32 mes_anterior,Int32 mes_x_trimestre){

            int registro_procesado, registro_creado;
            String monto_acumulado;

            Int32 resp = 0;
            eOperacionVC ope = new eOperacionVC();
            ope._Ide_Contrato = ddl_contrato_o.SelectedItem.Value;
            ope._Tip_Operacion = ddl_tipinfo.SelectedItem.Value;
            ope._Tipo_Registro = ddl_tipo_registro.SelectedItem.Value;
            ope._Ano_Operacion = anio_vigente;
            ope._Mes_Operacion = mes_vigente.ToString();
            ope._Formato_Moneda = formato_moneda;
            ope._Des_Reg_Mes = "OPERACION " + ope._Tipo_Registro + " (" + ope._Tip_Operacion + ")";
            ope._Usu_Reg = Session["username"].ToString();

            if (/*existe_mes_anterior > 0 &&mes_actual == 0 &&*/ ope._Tipo_Registro.ToUpper() == "MEN")
            {
                bOperacionVC bo = new bOperacionVC();
                if (ope._Tip_Operacion == "RP")
                {
                    resp = bo.SetProcesaPago(ope);
                }
                else if (ope._Tip_Operacion == "RI")
                {
                    resp = bo.SetProcesaIbnr(ope);
                }
                else if (ope._Tip_Operacion == "RR")
                {
                    resp = bo.SetProcesoRsp(ope);
                }
                else
                {
                    resp = bo.SetProcesaPrima(ope);
                }

                if (resp != 0)
                {
                    String[] tabla = {"1","NO"};
                    bOperacionSelectVC bos = new bOperacionSelectVC();
                    bos.GetSelectTotalOperacion(ope, tabla, out registro_procesado, out registro_creado, out monto_acumulado);

                    lbl_regprocesado.Text = registro_procesado.ToString();
                    lbl_opecreada.Text = registro_creado.ToString();
                    lbl_totimporte.Text = monto_acumulado.ToString();
                    MessageBox(registro_creado + " Registros Creados");
                }
                else
                {
                    MessageBox("Aviso => No hay registros y/o ya fueron procesados verifique.");
                }
            }
            else if (mes_actual == 0 && mes_x_trimestre == 0 && ope._Tipo_Registro.ToUpper() == "TRI")
            {

                bOperacionVC bo = new bOperacionVC();
                if (ope._Tip_Operacion == "RP")
                {
                    resp = bo.SetProcesaPago(ope);
                }
                else if (ope._Tip_Operacion == "RI")
                {
                    resp = bo.SetProcesaIbnr(ope);
                }
                else if (ope._Tip_Operacion == "RR")
                {
                    resp = bo.SetProcesoRsp(ope);
                }
                else
                {
                    resp = bo.SetProcesaPrima(ope);
                }

                if (resp != 0)
                {
                    String[] tabla = { "2", "C"+ddl_tipinfo.SelectedItem.Value.Substring(1)};
                    bOperacionSelectVC bos = new bOperacionSelectVC();
                    bos.GetSelectTotalOperacion(ope, tabla, out registro_procesado, out registro_creado, out monto_acumulado);

                    lbl_regprocesado.Text = registro_procesado.ToString();
                    lbl_opecreada.Text = registro_creado.ToString();
                    lbl_totimporte.Text = monto_acumulado.ToString();
                    MessageBox(registro_creado + " Registros Creados");
                }
                else
                {
                    MessageBox("Aviso => No hay registros y/o ya fueron procesados verifique.");
                }
            }
            else {
                MessageBox("El Tipo de Operación "+ddl_tipinfo.SelectedItem.Value+" ya fue Procesado");
            }
        }
        private void SetCalcularTrimestre(String contrato) {
            if (mes_vigente < 4)
                lbl_trimestre.Text = trimestre[0];
            else if (mes_vigente < 7)
                lbl_trimestre.Text = trimestre[1];
            else if (mes_vigente < 10)
                lbl_trimestre.Text = trimestre[2];
            else if (mes_vigente < 13)
                lbl_trimestre.Text = trimestre[3];
            lbl_mes.Text = meses[mes_vigente] + "-" + anio_vigente;
        }
        private void GetDataGeneral() {
            mes_vigente = Convert.ToInt32(Session["mesvigente"]);
            anio_vigente = Convert.ToInt32(Session["aniovigente"]);
            formato_moneda = Session["formatomoneda"].ToString();
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:170,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }
    }
}