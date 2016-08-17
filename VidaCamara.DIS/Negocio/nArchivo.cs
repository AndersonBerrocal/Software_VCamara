using System;
using System.Collections.Generic;
using System.IO;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.Negocio
{
    public class nArchivo
    {
        /// <summary>
        /// Devuelve la validacion de un archivo si ya fue cargado anteriormente
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public List<Archivo> listExisteArchivo(Archivo archivo)
        {
            string[] collectionArchivo = archivo.NombreArchivo.Split('_');
            if (collectionArchivo[0].ToString().Equals("NOMINA"))
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension(archivo.NombreArchivo) + ".CSV";
            //archivo.NombreArchivo = collectionArchivo[0] + "_" + collectionArchivo[1] + "_" + collectionArchivo[2] + "_" + collectionArchivo[3];
            var tamanoNombre = archivo.NombreArchivo.Length;
            return new dArchivo().listExisteArchivo(archivo, tamanoNombre);
        }

        public Int32 listExistePagoNomina(Archivo archivo)
        {
            var nombreNomina = archivo.NombreArchivo.Split('_');
            if (nombreNomina[1].Equals("AAD"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "AADIC" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }else if (nombreNomina[1].Equals("RGS"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "PSEP" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }else {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + nombreNomina[1] + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            //archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + nombreNomina[1] + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1))+".CAM";
            return new dArchivo().listExistePagoNomina(archivo);
        }

        public Archivo getArchivoByNombre(Archivo archivo) {
            var nombreNomina = archivo.NombreArchivo.Split('_');
            if (nombreNomina[1].Equals("AAD"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "AADIC" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            else if (nombreNomina[1].Equals("RGS"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "PSEP" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            else
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + nombreNomina[1] + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            
            return new dArchivo().getArchivoByNombre(archivo);
        }

        public void actualizarEstadoArchivo(Archivo archivo)
        {
            new dArchivo().actualizarEstadoArchivo(archivo);
        }

        public Archivo getArchivoByNomina(Archivo archivo)
        {
            var nombreNomina = archivo.NombreArchivo.Split('_');
            if (nombreNomina[1].Equals("AAD"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "AADIC" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            else if (nombreNomina[1].Equals("RGS"))
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + "PSEP" + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            else
            {
                archivo.NombreArchivo = Path.GetFileNameWithoutExtension("LIQ" + nombreNomina[1] + archivo.NombreArchivo.Substring(nombreNomina[0].Length + nombreNomina[1].Length + 1)) + ".CAM";
            }
            return new dArchivo().getArchivoByNomina(archivo);
        }
    }
}
