using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using VidaCamara.DIS.Helpers;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Negocio;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloDIS.Operaciones
{
    public partial class CargaDatos : System.Web.UI.Page
    {
        #region variables
        static int total;
        readonly bValidarAcceso _accesso = new bValidarAcceso();
        static string nombreArchivo = string.Empty;
        static string tipoArchivo = string.Empty;
        static HistorialCargaArchivo_LinCab historiaCab = new HistorialCargaArchivo_LinCab();
        static NOMINA nomina = new NOMINA();
        static object[] filters = new object[4];//[0]NombreArchivo,[1]tipo moneda [2]cumpleValidacion,[3]ArchivoId
        static nContratoSis contratoSis = new nContratoSis();
        static nLogOperacion nlog = new nLogOperacion();
        #endregion variables

        #region eventos control
        protected void Page_Load(object sender, EventArgs e)
        {
            var tabIndex = multiTabs.ActiveViewIndex;
           Session["pagina"] = "OTROS";
            if (Session["username"] == null)
                Response.Redirect("Login?go=0");
            else
            {
                if (!_accesso.GetValidarAcceso(Request.QueryString["go"]))
                {
                    Response.Redirect("Error");
                }
            }
            if (!IsPostBack)
            {
                bTablaVC concepto = new bTablaVC();
                SetLLenadoContrato();
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_archivo, "17");
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_linea,"18");
                filters[1] = ConfigurationManager.AppSettings.Get("Float").ToString();
            }
            //david choque 27 12 2015
            if (!control_grid.Value.Equals("0"))
            {
                setMostrarRegistroCargadosOK(tipoArchivo);
                setMostrarRegistroCargadosObservado(tipoArchivo);
            }
            //fin david choque
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            if (!fileUpload.HasFile) return;
            cargarValidarArchivo();
        }

        private void cargarValidarArchivo()
        {
            var pathRoot = Server.MapPath(string.Format(@"~\{0}\", ConfigurationManager.AppSettings["CarpetaArchivos"].ToString()));
            try
            {
                //david choque 27 12 2015
                nombreArchivo = fileUpload.FileName.ToString().ToUpper().Replace(" ","").Replace("  ","");
                //VALIDA SI EL NUMERO CONTRATO SELECCIONADO COICEDE CON EL ARCHIVO A CARGAR
                if (validarNumeroContratoXArchivo(nombreArchivo, ddl_conrato1.SelectedItem.Value) == 0)
                {
                    MessageBox("El archivo que se está intentando cargar pertenece a un número de contrato diferente.");
                    return;
                }
                //validar que el archivo seleccionado corresponde al mismo tipo al que esta seleccionado el combo combo
                string[] nombreArchivoValido = nombreArchivo.Split('_');
                if (!nombreArchivoValido[0].ToString().ToUpper().Equals(ddl_tipo_archivo.SelectedItem.Value.ToUpper()))
                {
                    MessageBox("El archivo seleccionado no corresponde al Tipo de Archivo eligido");
                    return;
                }
                var archivo = new Archivo() { NombreArchivo = nombreArchivo };

                //aqui se verifica que, el archivo que se está cargandose ya fue cargado previamente con el mismo nombre.
                var existeArchivo = new nArchivo().listExisteArchivo(archivo);
                if (existeArchivo.Count > 0)
                {
                    if (chk_permitir.Checked)
                    {
                        new nArchivo().actualizarEstadoArchivo(existeArchivo[0]);
                        chk_permitir.Checked = false;
                        chk_permitir.Visible = false;
                    }
                    else
                    {
                        MessageBox("El archivo: " + nombreArchivo + " ya fue cargado correctamente, si desea reemplazar haga click en: <br> Permitir reemplazar archivo existente.");
                        chk_permitir.Visible = true;
                        return;
                    }
                }
                //si el tipo de archivo es nomina validar  que se haya cargado un equivalente de  pago y que se haya cargado correctamente.
                if (ddl_tipo_archivo.SelectedItem.Value.Equals("NOMINA"))
                {
                    var existePagoNomina = new nArchivo().listExistePagoNomina(archivo);
                    if (existePagoNomina == 0)
                    {
                        MessageBox("Para cargar el archivo de nóminas debe cargar previamente los archivos de liquidaciones en forma correcta y sin errores");
                        return;
                    }
                }

                //fin david choque 27 12 2015
                var fileName = string.Format("{0}/{1}",Server.MapPath("~/Temp/Archivos"), nombreArchivo);
                fileUpload.SaveAs(fileName);
                var cargaLogica = new CargaLogica(fileName, pathRoot) { UsuarioModificacion = /*Session["usernameId"].ToString() */   "2" };
                cargaLogica.formatoMoneda = ConfigurationManager.AppSettings.Get("Float").ToString();
                cargaLogica.CargarArchivo(Convert.ToInt32(ddl_conrato1.SelectedValue));
                //david choque 27 12 2015
                tipoArchivo = ddl_tipo_archivo.SelectedItem.Value;
                setCargarReglaArchivo();
                //insertar log
                nlog.setLLenarEntidad(Convert.ToInt32(ddl_conrato1.SelectedItem.Value), "I", (tipoArchivo.Equals("NOMINA") ? "I05" : "I04"), cargaLogica.IdArchivo.ToString(), Session["username"].ToString(),"Archivo");

                txt_moneda.Text = cargaLogica.moneda;
                txt_registro_procesado.Text = cargaLogica.ContadorExito.ToString();
                txt_total_importe.Text = string.IsNullOrEmpty(cargaLogica.importe) ? "0.00" : cargaLogica.importe;
                txt_registro_observado.Text = cargaLogica.ContadorErrores.ToString();
                filters[3] = cargaLogica.IdArchivo;
                //fin david choque 27 12 2015

                if ((cargaLogica.MensajeExcepcion != ""))
                {
                    var mensaje = "Se produjo un error al cargar el archivo.<br> " + cargaLogica.MensajeExcepcion;
                    MessageBox(mensaje.Replace(Environment.NewLine, "").Replace("'", ""));
                }
                else
                {
                    string nombre;
                    if ((cargaLogica.NombreArchivo.Split('_')[0] == "NOMINA"))
                    {
                        nombre = string.Format("Nomina cargada {0} <br> {1}",(cargaLogica.ContadorErrores > 0 ? "incorrectamente" : "correctamente"), cargaLogica.Observacion);
                        if ((cargaLogica.MensageError != String.Empty))
                        {
                            nombre = (nombre + (", " + cargaLogica.MensageError));
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                        else
                        {
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                    }
                    else if ((cargaLogica.NombreArchivo.Split('_')[0] == "INOMINA"))
                    {
                        nombre = string.Format("Nomina procesada {0}", (cargaLogica.ContadorErrores > 0 ? "incorrectamente" : "correctamente"));
                        if ((cargaLogica.MensageError != String.Empty))
                        {
                            nombre = (nombre + (", " + cargaLogica.MensageError));
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                        else
                        {
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                    }
                    else
                    {
                        nombre = cargaLogica.ContadorErrores > 0? "Archivo procesado incorrectamente": "Archivo procesado correctamente";
                        if ((cargaLogica.Observacion != String.Empty))
                        {
                            nombre = (nombre + (", " + cargaLogica.Observacion));
                            MessageBox(nombre.ToString().Replace(Environment.NewLine, ""));
                            if (cargaLogica.Observacion.Contains("alto"))
                            {
                                string respuesta = setEnviarCorreo(cargaLogica);
                            }
                        }
                        else if ((cargaLogica.MensageError != String.Empty))
                        {
                            nombre = (nombre + (", " + cargaLogica.MensageError));
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                        else
                        {
                            MessageBox(nombre.Replace(Environment.NewLine, ""));
                        }
                    }
                }
                //david choque 27 12 2015
                setMostrarRegistroCargadosOK(tipoArchivo);
                setMostrarRegistroCargadosObservado(tipoArchivo);
                multiTabs.ActiveViewIndex = 1;
                menuTabs.Items[1].Selected = true;
                control_grid.Value = "1";
                //fin david choque
            }
            catch (Exception s)
            {
                MessageBox("ERROR =>" + s.Message.Replace("'", "-"));
            }
        }

        private int validarNumeroContratoXArchivo(string nombreArchivo, string contratoId)
        {

            var nameCollection = nombreArchivo.Split('_');
            var eContratoSis = contratoSis.listContratoByID(new CONTRATO_SYS() { IDE_CONTRATO = Convert.ToInt32(contratoId)});
            var numeroContrato = nameCollection[0] == "NOMINA" ? nameCollection[3] : nameCollection[2];
            if (Convert.ToInt32(numeroContrato) == Convert.ToInt32(eContratoSis.NRO_CONTRATO))
                return 1;
            else
                return 0;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listReglaArchivo(int jtStartIndex, int jtPageSize, string jtSorting, ReglaArchivo regla)
        {
            var contratoSis = new nContratoSis().listContratoByID(new CONTRATO_SYS() { IDE_CONTRATO = tipoArchivo.Equals("NOMINA")? nomina.IDE_CONTRATO:historiaCab.IDE_CONTRATO });
            regla.NUM_CONT_LIC = Convert.ToInt32(contratoSis.NRO_CONTRATO);
            var negocio = new nReglaArchivo();
            return new { Result = "OK", Records = negocio.getListReglaArchivo(regla, jtStartIndex, jtPageSize,jtSorting,out total), TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listHistoriaDetalleByArchivoOK(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            filters[2] = 1;//cumple validacion exitoso.
            var negocio = new nArchivoCargado();
            return new { Result = "OK", Records = negocio.listArchivoCargadoByArchivo(historiaCab, filters, jtStartIndex, jtPageSize, out total), TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listHistoriaDetalleByArchivoObservado(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            filters[2] = 0;//no cumple validacion exitoso.
            var negocio = new nArchivoCargado();
            return new { Result = "OK", Records = negocio.listArchivoCargadoByArchivo(historiaCab, filters, jtStartIndex, jtPageSize, out total), TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listNominaByArchivoOK(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            var negocio = new nNomina();
            nomina.CumpleValidacion = 1;
            return new { Result = "OK", Records = negocio.listNominaByArchivo(nomina, filters, jtStartIndex, jtPageSize, out total), TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listNominaByArchivoObservada(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            var negocio = new nNomina();
            nomina.CumpleValidacion = 0;
            return new { Result = "OK", Records = negocio.listNominaByArchivo(nomina, filters, jtStartIndex, jtPageSize, out total), TotalRecordCount = total };
        }

        #endregion eventos control

        #region metodos usuario
        private string setEnviarCorreo(CargaLogica cargaLogica)
        {
            string FormatoCuerpo = "";
            Correo mail = new Correo()
            {
                Para = cargaLogica.Correo,
                CC = "",
                CCO = "",
                Asunto = ("Monto alto a reembolsar: " + cargaLogica.NombreArchivo),
                Cuerpo = ("Estimado Usuario (a),</BR> liquidacion cargada contiene monto superior al establecido, el dia " + DateTime.Now) + "</BR> " + cargaLogica.Observacion,
                Archivo = ""
            };
            return cargaLogica.EnviarCorreo(mail, FormatoCuerpo);
        }

        private void setCargarReglaArchivo()
        {
            txt_nombre_archivo_inf.Text = nombreArchivo;
            txt_tipo_archivo_inf.Text = ddl_tipo_archivo.SelectedItem.Text;
            hdf_tipo_archivo.Value = tipoArchivo;
        }

        private void setMostrarRegistroCargadosOK(string tipoArchivo)
        {
            filters[0] = nombreArchivo;
            txt_nombre_archivo_det.Text = nombreArchivo;
            txt_tipo_informacion_det.Text = ddl_tipo_archivo.SelectedItem.Text;

            var columns = ",ReglaObservada:{title:'Observaciones'}";

            if (tipoArchivo == "NOMINA")
                nomina.IDE_CONTRATO = Convert.ToInt32(ddl_conrato1.SelectedItem.Value);
            else
                historiaCab.IDE_CONTRATO = Convert.ToInt32(ddl_conrato1.SelectedItem.Value);

            var action = tipoArchivo == "NOMINA"? "/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx/listNominaByArchivoOK" : "/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx/listHistoriaDetalleByArchivoOK";
            var tipoLinea = tipoArchivo == "NOMINA" ? "*" : "D";
            var sorter = tipoArchivo == "NOMINA" ? "RUC_ORDE ASC" : "TIP_REGI ASC";
            var contratoSis = new nContratoSis().listContratoByID(new CONTRATO_SYS() { IDE_CONTRATO = Convert.ToInt32(ddl_conrato1.SelectedItem.Value) });
            var regla = new ReglaArchivo() { Archivo = ddl_tipo_archivo.SelectedItem.Value, TipoLinea = tipoLinea,NUM_CONT_LIC = Convert.ToInt32(contratoSis.NRO_CONTRATO),vigente = 1 };
            var fields = new nReglaArchivo().getColumnGridByArchivo(regla,"Carga", columns).ToString();
            var grid = new gridCreator().getGrid("frmCargaExito", "5000", action, sorter).ToString();

            Page.ClientScript.RegisterStartupScript(GetType(), "Grid", string.Format("{0};{1}",fields,grid), true);
        }
        private void setMostrarRegistroCargadosObservado(string tipoArchivo)
        {
            var action = tipoArchivo == "NOMINA" ? "/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx/listNominaByArchivoObservada" : "/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx/listHistoriaDetalleByArchivoObservado";
            var sorter = tipoArchivo == "NOMINA" ? "RUC_ORDE ASC" : "TIP_REGI ASC";
            var grid = new gridCreator().getGrid("frmCargaObservado", "5000", action, sorter).ToString();
            Page.ClientScript.RegisterStartupScript(GetType(), "Grid1", grid, true);
        }

        private void SetLLenadoContrato()
        {
            var list = new VidaCamara.SBS.Utils.Utility().getContratoSys(out total);
            ddl_conrato1.DataSource = list;
            ddl_conrato1.DataTextField = "_des_Contrato";
            ddl_conrato1.DataValueField = "_ide_Contrato";
            ddl_conrato1.DataBind();
            ddl_conrato1.Items.Insert(0, new ListItem("Seleccione ----", "0"));
        }

        private void MessageBox(string text)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "$('<div style=\"font-size:14px;text-align:center;\">"+ text +"</div>').dialog({title:'Confirmación',modal:true,width:400,height:240,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]});", true);
        }
        #endregion metodos usuario
    }
}