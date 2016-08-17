using System;
using System.Text;
using VidaCamara.DIS.Negocio;
using VidaCamara.DIS.Modelo;
using System.IO;
using System.Configuration;

namespace VidaCamara.Masiva.Logica
{
    public class FileReader
    {
        private Int32 IdContrato { get; set; }
        private Int32 IdMoneda { get; set; }
        private String FullNombreArchivo { get; set; }
        public StringBuilder lineMessageLog = new StringBuilder();
        public Boolean archivoValido { get; set; }
        public Int32 existeArchivo { get; set; }
        public String fileName { get; set; }
        public string pathSaveFile = String.Format("{0}/{1}",ConfigurationManager.AppSettings["PathSave"].ToString(), ConfigurationManager.AppSettings["CarpetaArchivos"].ToString());

        //instancias
        private static nContratoSis contratoSis = new nContratoSis();
        private static nLogOperacion nlog = new nLogOperacion();

        public Boolean loadFileAndSave(string path)
        {
            setAtributeClass(path);
            return true;
        }

        private void setAtributeClass(string path)
        {
            FullNombreArchivo = path;
            fileName = Path.GetFileName(FullNombreArchivo);
            var listNameFile = fileName.Split('_');
            var nroContrato = listNameFile[0].Equals("NOMINA") ? listNameFile[3].ToString() : listNameFile[2].ToString();
            IdContrato = contratoSis.listByNroContrato(new CONTRATO_SYS { NRO_CONTRATO = nroContrato }).IDE_CONTRATO;
            var archivo = new Archivo() { NombreArchivo = fileName };
            var existeArchivo = new nArchivo().listExisteArchivo(archivo);
            //validando que el archivo a un no se haya cargado anteriormente
            if (existeArchivo.Count > 0)
            {
                lineMessageLog.AppendLine("El archivo: " + fileName + " ya fue cargado correctamente, si desea reemplazar haga click en: <br> Permitir reemplazar archivo existente.");
                return;
            }
            //validando para la nomina se haya cargado la liquidacion correspondiente
            if (listNameFile[0].Equals("NOMINA"))
            {
                var existePagoNomina = new nArchivo().listExistePagoNomina(archivo);
                if (existePagoNomina == 0)
                {
                    lineMessageLog.AppendLine(string.Format("archivo {0} - {1}",fileName," Para cargar el archivo de nóminas debe cargar previamente los archivos de liquidaciones en forma correcta y sin errores"));
                    return;
                }
            }
            SaveFile(listNameFile);
        }

        private void SaveFile(string[] listNameFile)
        {
            var startTime = DateTime.Now;
            try
            {
                var cargaLogica = new CargaLogica(FullNombreArchivo, pathSaveFile) { UsuarioModificacion = /*Session["usernameId"].ToString() */   "2" };
                cargaLogica.formatoMoneda = ConfigurationManager.AppSettings.Get("Float").ToString();
                cargaLogica.CargarArchivo(IdContrato);
                //insertar log
                nlog.setLLenarEntidad(IdContrato, "I", (listNameFile[0].Equals("NOMINA") ? "I05" : "I04"), cargaLogica.IdArchivo.ToString(), "jose.camara"/*Session["username"].ToString()*/,"Archivo");
                var messageLog = string.Format("Archivo {0} ", fileName);
                messageLog += listNameFile[0].Equals("NOMINA") ? string.Format("Nomina procesada {0}", (cargaLogica.ContadorErrores > 0 ? "incorrectamente" : "correctamente")):
                                                                     cargaLogica.ContadorErrores > 0? "Archivo procesado incorrectamente": "Archivo procesado correctamente";
                var endTime = DateTime.Now;
                messageLog += string.Format("{0} - tiempo {1}", cargaLogica.Observacion,new TimeSpan(endTime.Ticks - startTime.Ticks));
                lineMessageLog.AppendLine(messageLog);
            }
            catch (Exception ex)
            {
                lineMessageLog.AppendLine("ERROR =>" + ex.Message);
            }
        }
    }
}
