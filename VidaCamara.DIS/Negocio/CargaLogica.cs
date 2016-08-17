using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.Negocio
{
    public partial class CargaLogica
    {
        #region VARIABLES
        public string FullNombreArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public ObjectResult Resultado { get; set; }
        public bool ValidaNombre { get; set; }
        public StringCollection TipoLinea { get; set; }
        public int IdArchivo { get; set; }
        public int ContadorErrores { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Vigente { get; set; }
        public string ExtensionArchivo { get; set; }
        public List<int> LargoLinea { get; set; }
        public string MensageError { get; set; }
        public string MensajeExcepcion { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
        public string Correo { get; set; }
        private static string CampoActual { get; set; }
        private long CodigoCabecera { get; set; }

        private HistorialCargaArchivo_LinCab _lineaCabecera = new HistorialCargaArchivo_LinCab();
        private List<HistorialCargaArchivo_LinDet> _lineaDetalles = new List<HistorialCargaArchivo_LinDet>();
        private Dictionary<string, List<Regla>> _reglasLineaPorTipo = new Dictionary<string, List<Regla>>();
        //david choque 27 12 2015
        public int ContadorExito { get; set; }
        public string moneda { get; set; }
        public string importe { get; set; }
        public string formatoMoneda { get; set; }
        private static string ReglaObservada { get; set; }
        private string rootPath { get; set; }

        #endregion VARIABLES
        #region METODOS
        public CargaLogica(string archivo,string pathSave)
        {
            rootPath = pathSave;
            FullNombreArchivo = archivo;
            NombreArchivo = Path.GetFileName(archivo);
            string nombreArchivo = null;
            string extensionArchivo = null;
            nombreArchivo = Path.GetFileNameWithoutExtension(NombreArchivo);
            extensionArchivo = Path.GetExtension(NombreArchivo);

            if (extensionArchivo.Contains("CAM"))
            {
                if (NombreArchivo.Split('_')[0] == "NOMINA" | NombreArchivo.Split('_')[0] == "INOMINA")
                {
                    extensionArchivo = ".CSV";
                    // SE COMENTA ESTA LINEA PARA EVITAR QUE SE RENOMBRE EL ARCHIVO NOMINA
                    NombreArchivo = nombreArchivo + extensionArchivo;
                }
                ValidaNombre = ValidaNombreArchivo(NombreArchivo);
            }
            else
            {
                ValidaNombre = false;
            }
            MensajeExcepcion = "";
        }

        public void CargarArchivo(int contratoId)
        {
            if (!NombreArchivo.Distinct().Any()) return;

            if (ValidaNombre)
            {
                ObtieneTipoLinea(NombreArchivo.Split('_')[0]);
                var idestado = 0;
                idestado = LeeArchivo(NombreArchivo.Split('_')[0], TipoLinea, contratoId);
                //
                if (idestado > 2)
                {
                    MensageError = "No se puede procesar archivo por estar aprobado/pagado";
                    ContadorErrores = ContadorErrores + 1;
                }
                else
                {
                    if (idestado == 2)
                    {
                        MensageError = "No se puede procesar archivo por tener Checklists";
                        ContadorErrores = ContadorErrores + 1;
                    }
                }
            }
            else
            {
                MensageError = "Nombre de archivo no cumple formato";
                ContadorErrores = ContadorErrores + 1;

                NombreArchivo = string.Empty;
            }
        }

        public bool ValidaNombreArchivo(string nombreArchivo)
        {
            StringCollection archivo;
            string tipoArchivo = null;
            using (var context = new DISEntities())
            {
                var resultado = (object)context.pa_file_ObtieneTipoArchivos();
                archivo = ObtieneColeccion(resultado);
            }
            tipoArchivo = nombreArchivo.Split('_')[0];

            return archivo.Contains(tipoArchivo);
        }

        public void ObtieneTipoLinea(string tipoArchivo)
        {
            using (var context = new DISEntities())
            {
                var resultado = (object)context.pa_file_ObtienePrimerCaracterLineaPorTipoArchivo(tipoArchivo);
                TipoLinea = ObtieneColeccion(resultado);
            }
        }

        public string[] LineaArchivo()
        {
            ContadorErrores = 0;
            var sr = new StreamReader(FullNombreArchivo,System.Text.Encoding.Default);

            var texto = sr.ReadToEnd();
            var text = texto.Split('\n');
            sr.Close();
            return text;
        }

        public int LeeArchivo(string tipoArchivo, StringCollection tipoLinea, int contratoId)
        {
            var text = LineaArchivo();
            var eContratoSis = new nContratoSis().listContratoByID(new CONTRATO_SYS() { IDE_CONTRATO = contratoId });

            //Consultar en tabla el estado del archivo
            //entregara 0 si no existe
            //using (var context = new DISEntities())
            //{
            //    var estadoArchivo = context.pa_file_ConsultaEstadoArchivo(NombreArchivo).FirstOrDefault();
            //    if (estadoArchivo != null) Estado = estadoArchivo.Value;
            //}
            //InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), "Consulta estado archivo",
            //    "pa_file_ConsultaEstadoArchivo '" + NombreArchivo + "'", 0);

            //valor anterior = 3
            //Si el valor es menor que 2, significa 2 cosas:
            //1.- que la nomina no esta aprobada y puede ser cargado nuevos archivos.
            //2.- que no hay checklist sobre el o los cuspp de la liquidacion
            //if (Estado < 2)
            //{
                //Insertar en tabla
                //if (Estado == 1)
                //{
                //    MensageError = "Archivo ya cargado previamente";
                //}
                using (var context = new DISEntities())
                {
                    var archivo = context.pa_file_InsertaReferenciaArchivo(NombreArchivo, UsuarioModificacion).FirstOrDefault();
                    if (archivo != null) IdArchivo = archivo.Value;
                }
                //InsertaAuditoria(Me.UsuarioModificacion, "Inserta Referencia Archivo", "pa_file_InsertaReferenciaArchivo '" + Me.NombreArchivo + "'", Me.idArchivo)

                for (var indexLinea = 0; indexLinea <= text.Length - 1; indexLinea++)
                {
                    var caracterInicial = Mid(text[indexLinea].Trim(), 0, 1);
                    var carterInicialNumer = 0;
                    if (int.TryParse(caracterInicial, out carterInicialNumer))
                    {
                        caracterInicial = "*";
                    }
                    if (tipoLinea.Contains(caracterInicial))
                    {
                        using (var context = new DISEntities())
                        {
                            if (!_reglasLineaPorTipo.ContainsKey(caracterInicial))
                            {
                                _reglasLineaPorTipo.Add(caracterInicial,
                                ObtieneReglaLinea(context.pa_file_ObtieneReglasArchivoPorLinea(tipoArchivo,caracterInicial,Convert.ToInt32(eContratoSis.NRO_CONTRATO))));
                            }
                        }
                        //InsertaAuditoria(Me.UsuarioModificacion, "Obtiene Regla de archivo por línea", "pa_file_ObtieneReglasArchivoPorLinea '" + tipoLinea + "', " + CaracterInicial, Me.idArchivo)
                        try
                        {
                            var propertyValues = new Dictionary<string, object>();
                            var exitoLinea = 1; 
                            //if (!caracterInicial.Equals("T"))
                            //{
                                foreach (var regla in _reglasLineaPorTipo[caracterInicial])
                                {
                                    try
                                    {
                                        exitoLinea &= EvaluarRegla(tipoArchivo, regla, text, indexLinea, propertyValues);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                            //}
                            GrabarFilaArchivo(caracterInicial, IdArchivo, indexLinea + 1, propertyValues, contratoId, exitoLinea, tipoArchivo);
                        }
                        catch (Exception ex)
                        {
                            MensajeExcepcion = ex.Message;
                            return 0;
                        }
                    }
                    else
                    {
                        if (text[indexLinea].Trim().Any())
                        {
                            using (var context = new DISEntities())
                            {
                                context.pa_file_InsertaHistorialCarga(IdArchivo, 451, "#", indexLinea + 1, 1,text[indexLinea].Trim().Count(), text[indexLinea], 0);
                            }
                            //InsertaAuditoria(Me.UsuarioModificacion, "Inserta Historial de CargaLogica", "pa_file_InsertaHistorialCarga 451" + ", " + "'#'" + ", " + (x + 1).ToString() + ", " + "1" + ", " + text[x].Trim()().Count().ToString() + ", '" + Me.campoActual + "', " + "0", Me.idArchivo)
                            ContadorErrores = ContadorErrores + 1;
                        }
                    }
                }

                //aca se debe realizar el bolcado de archivo sin errores
                try
                {
                    //Antes de traspasar el archivo si verifica que  todas las filas se hayan cargado ccorrectamente
                    if(ContadorErrores > 0 && tipoArchivo != "NOMINA")
                    {
                        new dPagoCargado().setAatualizarEstadoArchivo(CodigoCabecera);
                    };
                    //actualizar la cabecera con el archivo id de la  nomina (NOMINA)
                    if (tipoArchivo == "NOMINA") {
                        var archivo = new nArchivo().getArchivoByNombre(new Archivo() {NombreArchivo = NombreArchivo });
                        //new dAprobacionCa
                        archivo.ArchivoId = IdArchivo;
                        new nAprobacionCarga().actulaizarArchivoIdNomina(archivo);
                        if (ContadorErrores > 0)
                        {
                            new nNomina().actualizarEstadoFallido(IdArchivo,contratoId);
                        }
                    }

                    TraspasaArchivo(tipoArchivo);
                    ProcesarErrores(tipoArchivo);
                    ContadorErrores = ContadorErrores > 0 ? ContadorErrores : 0;
                }
                catch (Exception ex)
                {
                    Observacion = ex.Message + "// TraspasaArchivo...!";
                }
            //}
            //else
            //{
            //    Observacion = "Ya está aprobado";
            //    return Estado;
            //}
            return 0;
        }

        private void GrabarFilaArchivo(string tipoLinea, int archivoId, int nroLinea, Dictionary<string, object> propertyValues, int contratoId, int exitoLinea,string tipoArchivo)
        {
            string session;
            try
            {
                session = System.Web.HttpContext.Current.Session["username"].ToString();
            }
            catch (Exception ex)
            {
                session = null;
        }
            if (tipoLinea == "C")
            {
                PopulateType(_lineaCabecera, propertyValues);
                _lineaCabecera.ArchivoId = archivoId;
                _lineaCabecera.USU_REG = string.IsNullOrEmpty(session) ? "jose.camara" : session;
                _lineaCabecera.FEC_REG = DateTime.Now;
                _lineaCabecera.ESTADO = "C";
                _lineaCabecera.CumpleValidacion = exitoLinea;
                _lineaCabecera.IDE_CONTRATO = contratoId;

                GrabarLineaCabecera();
            }

            if (tipoLinea == "D")
            {
                var detalle = new HistorialCargaArchivo_LinDet();
                PopulateType(detalle, propertyValues);
                detalle.IdHistorialCargaArchivoLinCab = CodigoCabecera;
                detalle.FechaInsert = DateTime.Now;
                detalle.CumpleValidacion = exitoLinea;
                detalle.TipoLinea = tipoLinea;
                detalle.NumeroLinea = nroLinea;
                detalle.ReglaObservada = string.IsNullOrEmpty(ReglaObservada) ? "OK" : ReglaObservada;

                //PopulateType(detalle, propertyValues);
                _lineaDetalles.Add(detalle);
            }

            if (tipoLinea == "T")
            {
                GrabarLineaDetalles();

                _lineaDetalles = new List<HistorialCargaArchivo_LinDet>();

                var eHistoriaLinDet = new HistorialCargaArchivo_LinDet();
                PopulateType(eHistoriaLinDet, propertyValues);
                eHistoriaLinDet.IdHistorialCargaArchivoLinCab = CodigoCabecera;
                eHistoriaLinDet.FechaInsert = DateTime.Now;
                eHistoriaLinDet.CumpleValidacion = exitoLinea;
                eHistoriaLinDet.TipoLinea = tipoLinea;
                eHistoriaLinDet.NumeroLinea = nroLinea;
                eHistoriaLinDet.ReglaObservada = string.IsNullOrEmpty(ReglaObservada) ? "OK" : ReglaObservada; ;
                eHistoriaLinDet.FEC_PAGO = string.IsNullOrWhiteSpace(eHistoriaLinDet.FEC_PAGO) ? string.Empty : eHistoriaLinDet.FEC_PAGO;
                eHistoriaLinDet.NUM_SINI = string.IsNullOrWhiteSpace(eHistoriaLinDet.NUM_SINI) ? string.Empty : eHistoriaLinDet.NUM_SINI;

                _lineaDetalles.Add(eHistoriaLinDet);

                GrabarLineaDetalles();

                _lineaDetalles = new List<HistorialCargaArchivo_LinDet>();
            }

            if (tipoLinea == "*")
            {
                var nomina = new NOMINA();
                PopulateType(nomina, propertyValues);
                nomina.ArchivoId = archivoId;
                nomina.Id_Empresa = 1;//observado
                nomina.IDE_CONTRATO = contratoId;
                nomina.Estado = "C";
                nomina.EstadoPago = "C";
                nomina.CumpleValidacion = exitoLinea;
                nomina.ReglaObservada = string.IsNullOrEmpty(ReglaObservada) ? "OK" : ReglaObservada;
                nomina.FechaReg = DateTime.Now;
                nomina.UsuReg = string.IsNullOrEmpty(session) ? "jose.camara" : session;

                //EVALUAR RETORNO
                var resp = new nNomina().setGrabarNomina(nomina);
            }
            ReglaObservada = string.Empty;
        }
        private void GrabarLineaCabecera()
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.HistorialCargaArchivo_LinCabs.Add(_lineaCabecera);
                    db.SaveChanges();
                    CodigoCabecera = _lineaCabecera.IdHistorialCargaArchivoLinCab;
                }
            }
            catch (DbEntityValidationException e)
            {
                var menssageException = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        menssageException += string.Format("{0} - {1} <br>", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw (new Exception(menssageException));
            }
        }

        private void GrabarLineaDetalles()
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.HistorialCargaArchivo_LinDets.AddRange(_lineaDetalles);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                var menssageException = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        menssageException += string.Format("{0} - {1} <br>", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw (new Exception(menssageException));
            }
        }

        private void ProcesarErrores(string tipoArchivo)
        {
            if (ValidacionesArchivo(tipoArchivo, 2) == false)
            {
                //COMENTADO ESTA VALIDACION SE USA EN EL ANTIGUO SISTEMA
                //using (var context = new DISEntities())
                //{
                //    Resultado = context.pa_file_ObtieneErrorArchivo(IdArchivo);
                //    var result = context.pa_file_ObtieneErrorArchivo(IdArchivo);

                //    var nombre = "";
                //    var largo = 0;
                //    foreach (var datoLoopVariable in result)
                //    {
                //        var dato = datoLoopVariable;
                //        if (dato.NumeroLinea.Value > 0)
                //        {
                //            nombre = dato.NombreArchivo;
                //            largo = dato.LargoCampo.Value;
                //        }
                //    }
                //    if (nombre != string.Empty & largo != null)
                //    {
                //        //If largo = 25 Then
                //        var valor1 = context.pa_valida_CodigoTransferenciaNomina(nombre, IdArchivo, largo);
                //        var resultado = 0;
                //        resultado = valor1.FirstOrDefault().Value;
                //        if (resultado == 0)
                //        {
                //            Resultado = null;
                //            Observacion =
                //                "No existe liquidación, debe cargar liquidación y despúes la nómina";
                //            //End If
                //        }
                //    }
                //}
            }

            //if (ContadorErrores == 0)
            //{
                using (var context = new DISEntities())
                {
                    var cantidadRes = context.pa_file_CantidadRegistroArchivo(IdArchivo).FirstOrDefault();
                    moneda = cantidadRes.Moneda;
                    importe = string.Format(formatoMoneda,cantidadRes.Importe);
                    ContadorExito = Convert.ToInt32(cantidadRes.cantidad);

                    Observacion = "<br> Registros procesados correctamente :" + ContadorExito.ToString() + " <br> Datos observados :"+ContadorErrores.ToString();
                    //insrtar auditoria
                } 
            //}

            //esto válida que los montos por cuspp no sean mayor a lo establecido
            //en la entidad: negocio.MontoAlto
            if (NombreArchivo.Substring(0, 3).ToLower() == "liq")
            {
                using (var context = new DISEntities())
                {
                    var monto = context.pa_valida_MontoAlto(IdArchivo, Convert.ToInt32(UsuarioModificacion));
                    string montoAlto = null;
                    montoAlto = monto.ToString();
                    if (montoAlto == "1")
                    {
                        dynamic monto1 = context.pa_devuelveresultado(IdArchivo);
                        var correo = "";
                        foreach (var registroLoopVariable in monto1)
                        {
                            var registro = registroLoopVariable;
                            correo = registro.correo;
                            Observacion = Observacion + "\\n Monto alto cargado al CUSPP: " +
                                          registro.Cuspp + ", por valor = " + registro.Valor.ToString;
                            InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), Observacion,
                                NombreArchivo, IdArchivo);
                        }
                        Correo = correo;
                    }
                }
            }
        }
        public string EnviarCorreo(Correo mail, string formatoCuerpo)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.pa_envioCorreo_Procesos(mail.Para, mail.CC, mail.CCO, mail.Asunto, mail.Cuerpo, formatoCuerpo, mail.Archivo).ToString();
            }
        }

        private static string Mid(string text, int startIndex, int length)
        {
            return text.Substring(startIndex, Math.Min(text.Length - startIndex, length));
        }

        private void InsertaAuditoria(int idUsuario, string descripcion, string comando, int idarchivo)
        {
            using (var context = new DISEntities())
            {
                context.pa_audit_InsertaBitacora(idUsuario, descripcion, comando, idarchivo);
            }
        }

        private void TraspasaArchivo(string tipoArchivo)
        {
            try
            {
                var destDirectory = string.Format(@"{0}\{1}\",rootPath, tipoArchivo);
                //InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), directorioArchivo, NombreArchivo,IdArchivo);

                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
                //InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), "despues de validar directorio", NombreArchivo, IdArchivo);

                var rutaArchivos = destDirectory + NombreArchivo;

                if (File.Exists(rutaArchivos))
                {
                    File.Delete(rutaArchivos);
                }
                //InsertaAuditoria(Convert.ToInt32(UsuarioModificacion),"despues de validar si el archivo existe en el directorio: " + rutaArchivos, NombreArchivo,IdArchivo);

                File.Copy(FullNombreArchivo, rutaArchivos);
                //InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), "despues de copiar archivo en directorio",NombreArchivo, IdArchivo);

                if (tipoArchivo == "PRIMAPAG")
                {
                    var nombre = FullNombreArchivo.Replace("PRIMAPAG", "PRIMPAGA");
                    dynamic nombreArch = Path.GetFileName(nombre);

                    destDirectory = string.Format(@"{0}\{1}\",rootPath, "PRIMPAGA");

                    if (!Directory.Exists(destDirectory))
                    {
                        Directory.CreateDirectory(destDirectory);
                    }

                    rutaArchivos = destDirectory + nombreArch;

                    if (File.Exists(rutaArchivos))
                    {
                        File.Delete(rutaArchivos);
                    }

                    ContadorErrores = 0;

                    using (var writer = new StreamWriter(rutaArchivos))
                    {
                        using (var sr = new StreamReader(FullNombreArchivo))
                        {
                            var texto = sr.ReadLine() + "\r";

                            while ((sr.Peek() >= 0))
                            {
                                if (texto.Contains("PAP"))
                                {
                                    var linea = texto.Substring(0, 1) + "PRE" + texto.Substring(4);
                                    texto = linea;
                                }
                                writer.Write(texto);
                                texto = sr.ReadLine() + "\r";
                            }
                            if (texto != string.Empty)
                            {
                                writer.Write(texto);
                            }
                        }
                    }
                    //Insertar en tabla
                    using (var context = new DISEntities())
                    {
                        var m = context.pa_file_InsertaReferenciaArchivo(nombreArch, UsuarioModificacion);
                        IdArchivo = m;

                        InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), "Se genero archivo(PrimPaga) en Servidor",nombreArch, IdArchivo);
                    }
                }

                File.Delete(FullNombreArchivo);

                InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), "Se guarda archivo en Servidor",NombreArchivo, IdArchivo);

                return;
            }
            catch (Exception ex)
            {
                InsertaAuditoria(Convert.ToInt32(UsuarioModificacion), ex.Message, NombreArchivo, IdArchivo);
                Observacion = ex.Message;
                return;
            }
        }

        public List<Regla> ObtieneReglaLinea(ObjectResult<pa_file_ObtieneReglasArchivoPorLinea_Result> dt)
        {
            var reglaLinea = new List<Regla>();
            foreach (var iLoopVariable in dt)
            {
                var i = iLoopVariable;
                var regla = new Regla
                {
                    idRegla = i.IdReglaArchivo,
                    CaracterInicial = i.CaracterInicial.Value,
                    LargoCampo = i.LargoCampo.Value,
                    TipoCampo = i.TipoCampo,
                    TipoValidacion = i.TipoValidacion,
                    ReglaValidacion = i.ReglaValidacion,
                    Tabladestino = i.TablaDestino,
                    NombreCampo = i.NombreCampo,
                    FormaValidacion = i.FormaValidacion
                };
                reglaLinea.Add(regla);
            }
            return reglaLinea;
        }

        private StringCollection ObtieneColeccion(object dt)
        {
            var coleccion = new StringCollection();

            foreach (var iLoopVariable in (IEnumerable)dt)
            {
                var i = iLoopVariable;
                coleccion.Add(i == null ? "" : i.ToString());
            }
            return coleccion;
        }
        
        private void PopulateType(object obj, Dictionary<String, Object> defaultValues)
        {
            foreach (var defaultValue in defaultValues)
            {
                var prop = obj.GetType().GetProperty(defaultValue.Key.ToString().Trim(), BindingFlags.Public | BindingFlags.Instance);
                if(null != prop && prop.CanWrite)
                {
                    var type = prop.PropertyType;
                    var underlyingType = Nullable.GetUnderlyingType(type);
                    var returnType = underlyingType ?? type;
                    var typeCode = Type.GetTypeCode(returnType);

                    var value = defaultValue.Value;

                    try
                    {
                        switch (typeCode)
                        {
                            case TypeCode.Int32:
                                prop.SetValue(obj, Convert.ToInt32(value), null);
                                break;
                            case TypeCode.DateTime:
                                if ((string) value == "00000000")
                                {
                                    prop.SetValue(obj, null, null);
                                    break;
                                }

                                var dateTimeValue = DateTime.ParseExact(value.ToString(), "yyyyMMdd",
                                    System.Globalization.CultureInfo.InvariantCulture);
                                prop.SetValue(obj, dateTimeValue, null);
                                break;
                            //ini 03 01 2016 david choque
                            case TypeCode.Decimal:
                                prop.SetValue(obj, Convert.ToDecimal(value), null);
                                break;
                            //fin 03 01 2016 david choque
                            default:
                                prop.SetValue(obj, value, null);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
        #endregion METODOS
    }
}
