using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.DIS.Negocio;
using VidaCamara.DIS.Modelo;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.Mantenimiento
{
    public partial class General : System.Web.UI.Page
    {
        #region VARIABLES
        static int total;
        static String[] mes = { "-", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        static int totalContrato;
        bValidarAcceso accesso = new bValidarAcceso();
        static int numero_empresa = 0;
        static nLogOperacion nlog = new nLogOperacion();
        #endregion VARIABLES
        #region EVENTOS
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
                else
                {
                    //if (!accesso.GetValidarAcceso("100"))
                    //    if (accesso.GetValidarAcceso("103"))
                    //    {
                    //        menuTabs.Items[2].Selected = true;
                    //        multiTabs.ActiveViewIndex = 2;
                    //    }

                }
            }

            if (!IsPostBack){
                bTablaVC concepto = new bTablaVC();
                ParametroList();
                concepto.SetEstablecerDataSourceConcepto(ddl_ramo_prima_c,"05");
                concepto.SetEstablecerDataSourceConcepto(ddl_seniestro_c,"04");
                concepto.SetEstablecerDataSourceConcepto(ddl_moneda_c,"10");
                concepto.SetEstablecerDataSourceConcepto(ddl_contratante_c,"14");
                SetLLenadoContrato();
                concepto.SetEstablecerDataSourceConcepto(ddl_reasegurador_r,"01");
                concepto.SetEstablecerDataSourceConcepto(ddl_modalidad_c,"06");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipcont_det_r,"16");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipcon_c,"07");
                concepto.SetEstablecerDataSourceConcepto(ddl_calificadora_r,"02");
                concepto.SetEstablecerDataSourceConcepto(ddl_crediticia_r,"11");
                concepto.SetEstablecerDataSourceConcepto(ddl_compania_seg_vida,"29");
                concepto.SetEstablecerDataSourceConcepto(ddl_estado_sys,"21");

                //contrato sys
                var bContrato = new bContratoSys();
                bContrato.SetEstablecerDataSourceContratoSys(ddl_contrato_sis);

                llenarEstado("09","U");


                var list = new bTablaVC().getConceptoByTipo("69");
                ddl_clasecontrato_c.DataSource = list.FindAll(o => o._tipo.Trim().Equals("SBS"));
                ddl_clasecontrato_c.DataTextField = "_descripcion";
                ddl_clasecontrato_c.DataValueField = "_codigo";
                ddl_clasecontrato_c.DataBind();
                ddl_clasecontrato_c.Items.Insert(0, new ListItem("Seleccione ----", "0"));

                //ddl_clase_contrato_sys.DataSource = list.FindAll(o => o._tipo.Trim().Equals("SIS"));
                //ddl_clase_contrato_sys.DataTextField = "_descripcion";
                //ddl_clase_contrato_sys.DataValueField = "_codigo";
                //ddl_clase_contrato_sys.DataBind();
                //ddl_clase_contrato_sys.Items.Insert(0, new ListItem("Seleccione ----", "0"));
            }
        }

        //LLENADO DE PARAMETROS
        private void ParametroList() {
            bGeneralVC bg = new bGeneralVC();
            List<eGeneral> list = bg.GetSelecionarGeneral();
            if (list.Count > 0)
            {
                txt_idempresa.Value = list[0]._idEmpresa.ToString();
                txt_empresa.Text = list[0]._descripcion.ToString();
                txt_ruc.Text = list[0]._rucEmpresa.ToString();
                txt_vigente.Text = list[0]._anoVigente.ToString();
                txt_mes_vig.Text = list[0]._mesVigente.ToString();
                txt_ruta_archivo.Text = list[0]._Ruta_Archivo;
                txt_cantidad_decimal.Text = list[0]._Nro_Decimal.ToString();
                txt_tcamesCont.Text = list[0]._tcaMes.ToString();
                txt_tcaCierre.Text = list[0]._tcaAno.ToString();
                bg.ParametroListSession();
            }
        }
        //LLENADO DE LISTA CONTRATO DETALLE
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ContratoDetalleList(int jtStartIndex, int jtPageSize, string jtSorting, String WhereBy)
        {
            int total;
            int indexPage = jtStartIndex != 0 ? jtStartIndex / jtPageSize : jtStartIndex;
            eContratoDetalleVC o = new eContratoDetalleVC();
            o._inicio = indexPage;
            o._fin = jtPageSize;
            o._orderby = jtSorting.Substring(1).ToUpper();
            o._nro_Contrato = WhereBy.Trim();

            var list = new bContratoDetalleVC().GetSelecionarContratoDetalle(o, out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ContratoSisDetalleList(int jtStartIndex, int jtPageSize, string jtSorting, String WhereBy)
        {
            var contratoDetalle = new CONTRATO_SIS_DET() { IDE_CONTRATO = Convert.ToInt32(WhereBy.Trim()) };
            var filterOptions = new Object[3] { jtStartIndex, jtPageSize, jtSorting };
            var listDetalle = new nContratoSisDetalle().getlistContratoDetalle(contratoDetalle, filterOptions,out total);
            return new { Result = "OK", Records = listDetalle, TotalRecordCount = total };
        }
        //LLENADO DE CONTRATOO
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ContratoList(int jtStartIndex, int jtPageSize, string jtSorting, String WhereBy)
        {
            int total;
            int indexPage = jtStartIndex != 0 ? jtStartIndex / jtPageSize : jtStartIndex;
            eContratoVC o = new eContratoVC();
            o._inicio = indexPage;
            o._fin = jtPageSize;
            o._orderby = jtSorting.Substring(1).ToUpper();
            o._nro_Contrato = WhereBy.Trim();
            o._estado = "R";

            var list = new bContratoVC().GetSelecionarContrato(o, out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            int tab = Convert.ToInt16(menuTabs.SelectedValue);
            if (tab == 0)
                SetInsertarGeneral();
            else if (tab == 1)
                SetInsertarActualizarContrato();
            else if (tab == 2)
                SetInsertarActualizarContratoDetalle();
            else if (tab == 3)
                SetInsertarActualizarContratoSys();
            else if (tab == 4)
                setInsertarActualizarContratoSisDetalle();
        }

        //botton de borrar
        protected void btn_borrar_Click(object sender, ImageClickEventArgs e)
        {
            int tab = Convert.ToInt16(menuTabs.SelectedValue);
            if (tab == 0)
                MessageBox("Formulario no Borrable");
            else if (tab == 1)
                SetEliminarParamentro("CONTRATO", txt_idContrato_c.Value);
            else if (tab == 2)
                SetEliminarParamentro("CONTRATO_DETALLE", txt_idContratoDetalle_c.Value);
            else if (tab == 3)
                SetEliminarParamentro("CONTRATO_SYS", txt_ide_contrato_sis.Value);
            else if (tab == 4)
                SetEliminarParamentro("CONTRATO_SIS_DETALLE", txt_ide_contrato_sis_det.Value);

        }
        #endregion EVENTOS
        #region METODOS
        private void setInsertarActualizarContratoSisDetalle()
        {
            try
            {
                var ContratoSisDetalle = new CONTRATO_SIS_DET()
                {
                    IDE_CONTRATO_DET = Convert.ToInt32(txt_ide_contrato_sis_det.Value),
                    IDE_CONTRATO = Convert.ToInt32(ddl_contrato_sis.SelectedItem.Value),
                    COD_CSV = Convert.ToInt32(ddl_compania_seg_vida.SelectedItem.Value),
                    PRC_PARTICIACION = Convert.ToDecimal(txt_participacion_sis.Text),
                    NRO_ORDEN = Convert.ToInt32(txt_orden_empresa_sis.Text),
                    ESTADO = "A",
                    FEC_REG = DateTime.Now,
                    FEC_MOD = DateTime.Now,
                    USU_REG = Session["username"].ToString(),
                    USU_MOD = Session["username"].ToString()
                };
                    
                if (ContratoSisDetalle.IDE_CONTRATO_DET == 0) {
                    //validar el numero maximo de empresas permitido
                    
                    if (validaNumeroMaximoEmpresa(ContratoSisDetalle) == 1) {
                        MessageBox("El número de máximo empresas CSV es de "+numero_empresa.ToString());
                        return;
                    }
                    //CREAR UN METODOD  RECIBE COMO PARAMETRO LA ENTIDAD DE CONTRATOSISDETALLE
                    //navegar al metodo con f12
                    //aki lo evaluasa si viene mayor a 0 , quere decir que si encontrato si no pasa.
                    if (verificarExisteCompaniaSeguro(ContratoSisDetalle) > 0)
                    {
                        MessageBox("La CSV " + ddl_compania_seg_vida.SelectedItem.Text + " ya esta en la lista : ");
                        return;
                    }
                    //listo termina la valadacion
                    if (verificarSiExisteNroOrden(ContratoSisDetalle) == 1)
                    {
                        MessageBox("El numero de orden ya existe.");
                        return;
                    }


                    if (verificarSumaTotalPorcentaje(ContratoSisDetalle) > 100.00m)
                    {
                        MessageBox("La suma de los porcentajes ingresados supera el limite maximo de 100");
                        return;
                    };


                    // Validar que la suma de los % de participacion esten correctos
                    var valorPorc = sumPorcentaje(ContratoSisDetalle);
                    var restaRes = 100 - valorPorc;

                    if (Math.Abs(restaRes) > 0.001m)
                    {
                        var resp = new nContratoSisDetalle().setGuardarContratoDetalle(ContratoSisDetalle);
                        nlog.setLLenarEntidad(Convert.ToInt32(ContratoSisDetalle.IDE_CONTRATO), "I", "I02", resp.ToString(), Session["username"].ToString(),"ContratoSisDetalle");
                        MessageBox("Registro  grabado corretamente");
                    }
                    else
                    {
                        MessageBox("La suma de los porcentajes parciales no es equivalente al 100%");
                        return;
                    }

                }
                else
                {
                    if (verificarSumaTotalPorcentaje(ContratoSisDetalle) > 100.00m)
                    {
                        MessageBox("La suma de los porcentajes ingresados supera el limite maximo de 100");
                        return;
                    }
                    else
                    {
                        var resp = new nContratoSisDetalle().setActualizarContratoDetalle(ContratoSisDetalle);
                        nlog.setLLenarEntidad(Convert.ToInt32(ContratoSisDetalle.IDE_CONTRATO), "A", "A05", ContratoSisDetalle.IDE_CONTRATO_DET.ToString(), Session["username"].ToString(), "ContratoSisDetalle");
                        MessageBox(resp + " Registro actualizado corretamente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox("Ocurrio el siguiente error : "+ex.Message.ToString());
            }
        }

        private int validaNumeroMaximoEmpresa(CONTRATO_SIS_DET ContratoSisDetalle)
        {
            var filterOptions = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            var contratosis = new CONTRATO_SYS()
            {
                IDE_CONTRATO = Convert.ToInt32(ContratoSisDetalle.IDE_CONTRATO)
            };
            var nContratoSis = new nContratoSis().listContratoByID(contratosis);
            var nContratoDetalle = new nContratoSisDetalle().getlistContratoDetalle(ContratoSisDetalle, filterOptions, out total);
            numero_empresa = Convert.ToInt32(nContratoSis.NRO_EMPRESAS);
            if (nContratoDetalle.Count == nContratoSis.NRO_EMPRESAS)
                return 1;
            else
                return 0;
        }

        private int verificarExisteCompaniaSeguro(CONTRATO_SIS_DET ContratoSisDetalle)
        {
            //seteas los filtros del procedure
            var filterOptions = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            //llamas la clase de negocio contrato sisdetalle donde se encuentra el metodo de listar
            var listCompania = new nContratoSisDetalle().getlistContratoDetalle(ContratoSisDetalle, filterOptions, out total);
            // el metodo de listar trae todas los registros de contrato sis detalle de un  contrato seleccionado
            //declaras tu variable para buscar si el codigo de compania ya existe
            var existeCompania = listCompania.FindAll(a=>a.COD_CSV == ContratoSisDetalle.COD_CSV);
            //esta variable va filtrar en esa lista atodos los registros cuyo COD_CSV coniceda con la que se selecciono en la panatalla
            //finalmente evaluas 
            return existeCompania.Count;

        }

        private decimal verificarSumaTotalPorcentaje(CONTRATO_SIS_DET contratoSisDetalle)
        {
            var filterOptions = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            var listEmpresa = new nContratoSisDetalle().getlistContratoDetalle(contratoSisDetalle, filterOptions, out total);
            var sumaPorcentaje = 0.00m;
            var eContratoSisDet = listEmpresa.Find(a => a.IDE_CONTRATO_DET == contratoSisDetalle.IDE_CONTRATO_DET); 
            foreach (var item in listEmpresa)
            {
                sumaPorcentaje += Convert.ToDecimal(item.PRC_PARTICIACION);
            }
            var residuo = eContratoSisDet != null ? Convert.ToDecimal(sumaPorcentaje - eContratoSisDet.PRC_PARTICIACION) : sumaPorcentaje;
            return residuo + Convert.ToDecimal(contratoSisDetalle.PRC_PARTICIACION);
        }


        //Solo la suma de porcentajes
        private decimal sumPorcentaje(CONTRATO_SIS_DET contratoSisDetalle)
        {
            var filterOptions = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            var listEmpresa = new nContratoSisDetalle().getlistContratoDetalle(contratoSisDetalle, filterOptions, out total);
            var sumaPorcentaje = 0.00m;
            foreach (var item in listEmpresa)
            {
                sumaPorcentaje += Convert.ToDecimal(item.PRC_PARTICIACION);
            }
            return sumaPorcentaje;
        }

        private int verificarSiExisteNroOrden(CONTRATO_SIS_DET contratoSisDetalle)
        {
            var filterOptions = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            var listEmpresa = new nContratoSisDetalle().getlistContratoDetalle(contratoSisDetalle, filterOptions, out total);
            var listOrdenExiste = listEmpresa.FindAll(a => a.NRO_ORDEN == contratoSisDetalle.NRO_ORDEN);
            if (listOrdenExiste.Count > 0)
                return 1;
            else
                return 0;
        }

        //#region funciones
        private void SetInsertarGeneral() {
            try
            {
                if (Int32.Parse(txt_idempresa.Value) == 0)
                {
                    eGeneral o = new eGeneral();
                    o._descripcion = txt_empresa.Text;
                    o._rucEmpresa = txt_ruc.Text;
                    o._anoVigente = Convert.ToInt32(txt_vigente.Text);
                    o._mesVigente = Convert.ToInt32(txt_mes_vig.Text);
                    o._Ruta_Archivo = txt_ruta_archivo.Text;
                    o._Nro_Decimal = Convert.ToInt32(txt_cantidad_decimal.Text);
                    o._tcaMes = Decimal.Parse(txt_tcamesCont.Text);
                    o._tcaAno = Decimal.Parse(txt_tcaCierre.Text);
                    o._estado = "A";
                    o._usureg = Session["username"].ToString();

                    bGeneralVC control = new bGeneralVC();
                    Int32 resp = control.SetInsertarGeneral(o);
                    if (resp != 0)
                    {
                        MessageBox("Registro Grabado Correctamente!");
                        ParametroList();
                    }
                    else
                    {
                        MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
                else
                {
                    SetActualizarGeneral();
                }
            }
            catch (Exception e) {
                MessageBoxcCatch("ERROR =>" + e.Message);
            }
        }
        //funcion de insertar contrato
        private void SetActualizarGeneral() {
            try
            {
                eGeneral o = new eGeneral();
                o._idEmpresa = Int32.Parse(txt_idempresa.Value);
                o._descripcion = txt_empresa.Text;
                o._rucEmpresa = txt_ruc.Text;
                o._anoVigente = Convert.ToInt32(txt_vigente.Text);
                o._mesVigente = Convert.ToInt32(txt_mes_vig.Text);
                o._Ruta_Archivo = txt_ruta_archivo.Text;
                o._Nro_Decimal = Convert.ToInt32(txt_cantidad_decimal.Text);
                o._tcaMes = Decimal.Parse(txt_tcamesCont.Text);
                o._tcaAno = Decimal.Parse(txt_tcaCierre.Text);
                o._estado = "A";
                o._usumod = Session["username"].ToString();

                bGeneralVC control = new bGeneralVC();
                Int32 resp = control.SetActualizarGeneral(o);
                if (resp != 0)
                {
                    MessageBox("Registro Actualizado Correctamente!");
                    ParametroList();
                }
                else
                {
                    MessageBox("Ocurrio un Error en el Servidor!");
                }
            }
            catch (Exception e) { 
                MessageBoxcCatch("ERROR =>" +e.Message);
            }
        }
        //actualiza y inserta contrato
        private void SetInsertarActualizarContrato() {

            try
            {
                Int32 resp = 0;
                eContratoVC c = new eContratoVC();
                c._id_Empresa = Convert.ToInt32(Session["idempresa"]);
                c._ide_Contrato = Convert.ToInt32(txt_idContrato_c.Value);
                c._nro_Contrato = txt_nrocont_c.Text;
                c._cod_Ramo_Sin = ddl_seniestro_c.SelectedItem.Value;
                c._cod_Ramo_pri = ddl_ramo_prima_c.SelectedItem.Value;
                c._cla_Contrato = ddl_clasecontrato_c.SelectedItem.Value;
                c._fec_Ini_Vig = DateTime.Parse(txt_fecini_c.Text);
                c._fec_Fin_Vig = DateTime.Parse(txt_fecfin_c.Text);
                c._tip_Contrato = ddl_tipcon_c.SelectedItem.Value;
                c._cod_Moneda = ddl_moneda_c.SelectedItem.Value;
                c._cod_Contratante = ddl_contratante_c.SelectedItem.Value;
                c._por_Participa_Cia = Convert.ToDecimal(txt_participacion_cia_c.Text);
                c._por_Tasa_Riesgo = Convert.ToDecimal(txt_tasariesgo_c.Text);
                c._por_Tasa_Reaseguro = Convert.ToDecimal(txt_tasareaseguro_c.Text);
                c._por_Impuesto = Convert.ToDecimal(txt_impuesto_c.Text);
                c._Centro_Costo = txt_centro_costo.Text;
                c._des_Contrato = txt_descrip_contrato.Text;
                c._estado = ddl_estado_c.SelectedItem.Value;
                c._mod_Contrato = ddl_modalidad_c.SelectedItem.Value;
                c._por_Retencion = Convert.ToDecimal(txt_retencion_c.Text);
                c._por_Cesion = Convert.ToDecimal(txt_cesion_c.Text);
                c._mto_Max_Retencion = Convert.ToDecimal(txt_montomax_retenc_c.Text);
                c._mto_Max_Cesion = Convert.ToDecimal(txt_montomax_cesion_c.Text);
                c._mto_Pleno = Convert.ToDecimal(txt_montopleno_c.Text);
                c._nro_Linea_Mult = Convert.ToInt32(txt_multiplo_c.Text);
                c._mto_Max_Cubertura = Convert.ToDecimal(txt_mto_max_cubert_c.Text);
                c._nro_Capa_Xl1 = Convert.ToInt32(txt_nrocapaxl_c1.Text);
                c._Prioridad1 = Convert.ToDecimal(txt_prioridad_c1.Text);
                c._Cesion_Exc_Prioridad1 = Convert.ToDecimal(txt_excesoprio_c1.Text);
                c._mto_Max_Cap_Lim_Sup1 = Convert.ToDecimal(txt_mto_max_lim_sup_c1.Text);
                c._prima_Min_Deposito1 = Convert.ToDecimal(txt_primaminima_deposit_c1.Text);

                c._nro_Capa_Xl2 = Convert.ToInt32(txt_nrocapaxl_c2.Text);
                c._Prioridad2 = Convert.ToDecimal(txt_prioridad_c2.Text);
                c._Cesion_Exc_Prioridad2 = Convert.ToDecimal(txt_excesoprio_c2.Text);
                c._mto_Max_Cap_Lim_Sup2 = Convert.ToDecimal(txt_mto_max_lim_sup_c2.Text);
                c._prima_Min_Deposito2 = Convert.ToDecimal(txt_primaminima_deposit_c2.Text);

                c._usu_reg = Session["username"].ToString();
                c._usu_mod = Session["username"].ToString();
                
                bContratoVC control = new bContratoVC();
                if (c._ide_Contrato == 0)
                {
                    resp = control.SetInsertarContrato(c);
                    MessageBox("Registro Grabado Correctamente");
                    SetLLenadoContrato();
                    
                }
                else {
                    resp = control.SetActualizarContrato(c);
                    MessageBox("Registro Actualizado Correctamente");
                    SetLLenadoContrato();
                }
            }
            catch (Exception e) {
                MessageBoxcCatch("ERROR =>" + e.Message.Replace(Environment.NewLine,""));
            }
        }
        //funcion de insertar  reaseguradores
        private void SetInsertarActualizarContratoDetalle(){
            try
            {
                Int32 resp = 0;
                eContratoDetalleVC d = new eContratoDetalleVC();
                d._ide_Contrato_Det = Convert.ToInt32(txt_idContratoDetalle_c.Value);
                d._id_Empresa = Convert.ToInt32(Session["idempresa"]);
                d._nro_Contrato = ddl_contrato_r.SelectedItem.Value;
                d._ide_Reasegurador = ddl_reasegurador_r.SelectedItem.Value;
                d._cod_Reasegurador = ddl_reasegurador_r.SelectedItem.Value;
                d._cod_Empresa_Califica = ddl_calificadora_r.SelectedItem.Value;
                d._cal_Crediticia = ddl_crediticia_r.SelectedItem.Value;
                d._mod_Contrato = ddl_tipcont_det_r.SelectedItem.Value;
                d._prc_Retencion = Convert.ToDecimal(txt_retencion_r.Text);
                d._prc_Cesion = Convert.ToDecimal(txt_cesion_r.Text);
                d._prc_participacion_rea = Convert.ToDecimal(txt_participacion_cesion.Text);
                d._nombre_Rea = txt_nombre_rea.Text;
                d._nro_Registro_Rea = Convert.ToInt32(txt_nro_registro_rea.Text);
                d._estado = "A";
                d._usu_reg = Session["username"].ToString();
                d._usu_mod = Session["username"].ToString();

                bContratoDetalleVC icd = new bContratoDetalleVC();
                if(d._ide_Contrato_Det == 0){
                    resp = icd.SetInsertarContratoDetalle(d);
                    MessageBox("Registro Grabado Correctamente!");
                }
                else
                {
                    resp = icd.SetActualizarContratoDetalle(d);
                    MessageBox("Registro Actualizado Correctamente");
                }
            }
            catch (Exception e) {
                MessageBoxcCatch("ERROR =>" + e.Message);
            }
        }
        public void SetEliminarParamentro(String tabla, String indice)
        {
            try
            {
                if (tabla.Equals("CONTRATO") && indice != "0")
                {
                    bContratoVC bc = new bContratoVC();
                    Int32 resp = bc.SetEliminarContrato(Int32.Parse(indice));
                    if (resp != 0)
                    {
                        MessageBox(resp + "Registro Eliminado Correctamente!");
                        SetLLenadoContrato();
                    }
                    else
                    {
                        MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
                else if (tabla.Equals("CONTRATO_DETALLE") && indice != "0")
                {
                    bContratoDetalleVC bcd = new bContratoDetalleVC();
                    Int32 resp = bcd.SetEliminarContratoDetalle(Int32.Parse(indice));
                    if (resp != 0)
                    {
                        MessageBox(resp + "Registro Eliminado Correctamente!");
                    }
                    else
                    {
                        MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
                else if (tabla.Equals("CONTRATO_SYS") && indice != "0")
                {
                    var  resp = new bContratoSys().SetEliminarContratoSys(Int32.Parse(indice));
                    MessageBox(resp + "Registro Eliminado Correctamente!");
  
                }
                else if(tabla.Equals("CONTRATO_SIS_DETALLE") && indice != "0")
                {
                    var resp = new nContratoSisDetalle().setEliminarContratoDetalle(Int32.Parse(indice));
                    MessageBox(resp + " Registros (s) eliminado (s) correctamente");
                }
            }
            catch (Exception ex) {
                MessageBoxcCatch("Ocurrio el siguiente error "+ex.Message.ToString());
            }
        }

        private void SetLLenadoContrato() {
            eContratoVC o = new eContratoVC();
            o._inicio = 0;
            o._fin = 10000;
            o._orderby = "IDE_CONTRATO ASC";
            o._estado = "R";
            o._nro_Contrato = "NO";

            bContratoVC tb = new bContratoVC();

            ddl_contrato_r.DataSource = tb.GetSelecionarContrato(o, out totalContrato);
            ddl_contrato_r.DataTextField = "_des_Contrato";
            ddl_contrato_r.DataValueField = "_nro_Contrato";
            ddl_contrato_r.DataBind();
            ddl_contrato_r.Items.Insert(0, new ListItem("Selecione ----", "0"));
        }
        private void llenarEstado(String tipo_tabla,String tipo)
        {

            var concepto = new eTabla()
            {
                _id_Empresa = 0,
                _tipo_Tabla = tipo_tabla,
                _descripcion = "NULL",
                _valor = "N",
                _estado = "A",
                _tipo = tipo,
                _inicio = 0,
                _fin = 10000000,
                _order = "DESCRIPCION ASC"
            };
            var listConcepto = new bTablaVC();
            ddl_estado_c.DataSource = listConcepto.GetSelectConcepto(concepto, out total);
            ddl_estado_c.DataTextField = "_descripcion";
            ddl_estado_c.DataValueField = "_codigo";
            ddl_estado_c.DataBind();
        }
        private void MessageBox(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text + "</div>').dialog({title:'Confirmación',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
        private void MessageBoxcCatch(String text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">" + text.Replace("'","").Replace(Environment.NewLine,"") + "</div>').dialog({title:'Error',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]})", true);
        }
        //LLENADO DE CONTRATO_SYS
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object ContratoSysList(int jtStartIndex, int jtPageSize, string jtSorting, String WhereBy)
        {
            int total;
            int indexPage = jtStartIndex != 0? jtStartIndex / jtPageSize: jtStartIndex;
            eContratoSys o = new eContratoSys();
            o._inicio = indexPage;
            o._fin = jtPageSize;
            o._orderby = jtSorting.Substring(1).ToUpper();
            o._nro_Contrato = WhereBy.Trim();
            o._estado = "R";

            bContratoSys tb = new bContratoSys();
            List<eContratoSys> list = tb.GetSelecionarContratoSys(o, out total);
            return new { Result = "OK", Records = list, TotalRecordCount = total };
        }

        private void SetInsertarActualizarContratoSys()
        {
            var mensajeConfirmn = string.Empty;
            try
            {
                Int32 resp = 0;
                var contratoSis = new eContratoSys()
                {
                    _id_Empresa = Convert.ToInt32(Session["idempresa"]),
                    _ide_Contrato = Convert.ToInt32(txt_ide_contrato_sis.Value),
                    _nro_Contrato = txt_nrocont_sys.Text,
                    _cla_Contrato = string.Empty,
                    _fec_Ini_Vig = DateTime.Parse(txtFechaInicio_sys.Text),
                    _fec_Fin_Vig = DateTime.Parse(txtFechaFin_sys.Text),
                    _des_Contrato = txtdescripcion_sys.Text,
                    _estado = ddl_estado_sys.SelectedItem.Value,
                    _usu_reg = Session["username"].ToString(),
                    _usu_mod = Session["username"].ToString(),
                    _nro_empresa = int.Parse(txt_numero_empresa.Text),
                    _centro_costo = txt_centro_costo_sys.Text
                };

                if(contratoSis._fec_Ini_Vig >= contratoSis._fec_Fin_Vig)
                {
                    MessageBox("La fecha de inicio no debe ser mayor al de fin");
                    return;
                }

                if (contratoSis._nro_empresa < 4 || contratoSis._nro_empresa > 7)
                {
                    MessageBox("El rango de N° de empresas por contrato puede ser entre 4 y 7");
                    return;
                }

                //valida rango de fecha para un contrato 
                var contratoSisEF = new CONTRATO_SYS() {
                    FEC_INI_VIG = DateTime.Parse(txtFechaInicio_sys.Text),
                    FEC_FIN_VIG = DateTime.Parse(txtFechaFin_sys.Text),
                    NRO_CONTRATO = txt_nrocont_sys.Text
                };
                var siExisteFecha = new nContratoSis().existeFecha(contratoSisEF,0);

                bContratoSys control = new bContratoSys();
                if (contratoSis._ide_Contrato == 0)
                {
                    if (contratoSis._estado.Equals("A"))
                    {
                        MessageBox("Para crear la empresa como activa, debe completar la infomación de las CSV SIS.");
                        return;
                    }
                    if (siExisteFecha > 0)
                    {
                        MessageBox("La fecha ingresada se sobrepone al rango de fechas de otro contrato.");
                        return;
                    }
                    else {
                        var siExisteFechaV2 = new nContratoSis().existeFecha(contratoSisEF, 1);
                        if (siExisteFechaV2 > 0)
                        {
                            MessageBox("La fecha ingresada se sobrepone al rango de fechas de otro contrato.");
                            return;
                        }
                    }
                    resp = control.SetInsertarContratoSys(contratoSis);
                    //var elog = nlog.setLLenarEntidad(resp, "C001", "CONSIS_C", resp.ToString(), Session["username"].ToString());
                    //nlog.setGuardarLogOperacion(elog);
                    if (resp != 0)
                    {
                        //contrato sys
                        var bContrato = new bContratoSys();
                        bContrato.SetEstablecerDataSourceContratoSys(ddl_contrato_sis);
                        MessageBox("Registro Grabado Correctamente");
                    }
                    else
                        MessageBox("Ocurrio un Error en el Servidor!");
                }
                else
                {
                    if (!validaContratoSisDetalle(contratoSis) && contratoSis._estado.Equals("A"))
                    {
                        MessageBox("Para pasar la empresa como activa, debe completar la infomación de las CSV SIS.");
                    }
                    else {
                        //validar si existe o no las reglas para el numero de contrato selecciondo
                        var existeReglaArchivo = new nReglaArchivo().validarExisteReglaByContrato(contratoSisEF);
                        if (existeReglaArchivo == 0)
                        {
                            var numeroContrato = new nReglaArchivo().copiarUltimaReglaArchivo(contratoSisEF);
                            mensajeConfirmn = string.Format(", Se copió las reglas del contrato Nro: {0}", numeroContrato);
                        }
                        resp = control.SetActualizarContratoSys(contratoSis);
                        if (resp != 0)
                        {
                            nlog.setLLenarEntidad(Convert.ToInt32(txt_ide_contrato_sis.Value), "A", "A01", contratoSis._ide_Contrato.ToString(), Session["username"].ToString(),"Contrato");
                            //contrato sys
                            var bContrato = new bContratoSys();
                            bContrato.SetEstablecerDataSourceContratoSys(ddl_contrato_sis);
                            MessageBox(string.Format("Registro Actualizado Correctamente {0}",mensajeConfirmn));
                        }
                        else
                            MessageBox("Ocurrio un Error en el Servidor!");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBoxcCatch("ERROR =>" + e.Message);
            }
        }

        private bool validaContratoSisDetalle(eContratoSys contratoSis)
        {
            var filters = new Object[3] { 0, 10000, "IDE_CONTRATO ASC" };
            var contratoDetSis = new CONTRATO_SIS_DET()
            {
                IDE_CONTRATO = Convert.ToInt32(contratoSis._ide_Contrato)
            };
            var listContratoSisDet = new nContratoSisDetalle().getlistContratoDetalle(contratoDetSis, filters,out total);
            var totalPorcenteje = 0.00m;
            foreach (var item in listContratoSisDet)
            {
                totalPorcenteje += Convert.ToDecimal(item.PRC_PARTICIACION);
            }
            if (contratoSis._nro_empresa == listContratoSisDet.Count && Math.Round(totalPorcenteje) == 100)
                return true;
            else
                return false;
        }

        #endregion "METODOS"
       
    }
}