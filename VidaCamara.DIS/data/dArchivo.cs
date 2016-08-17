using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.data
{
    public class dArchivo
    {
        public List<Archivo> listExisteArchivo(Archivo archivo,int tamanoNombre)
        {
            // Valida que el archivo exista y el estado de del archivo este validado | Validado = 2
            try
            {
                using (var db = new DISEntities())
                {
                    return db.Archivos.Where(a => a.NombreArchivo.Equals(archivo.NombreArchivo) && a.Vigente == true && a.EstadoArchivoId == 2).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Int32 listExistePagoNomina(Archivo archivo)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return (from a in db.Archivos join b in db.HistorialCargaArchivo_LinCabs on
                                 a.ArchivoId equals b.ArchivoId
                                 where
                                    a.NombreArchivo == archivo.NombreArchivo
                                 && b.ESTADO == "C"
                                 select a).Count();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Archivo getArchivoByNombre(Archivo archivo)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.Archivos.Include("HistorialCargaArchivo_LinCab").Where(a => a.NombreArchivo == archivo.NombreArchivo && a.EstadoArchivoId == 2).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void actualizarEstadoArchivo(Archivo archivo)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var entity = db.Archivos.Find(archivo.ArchivoId);
                    entity.EstadoArchivoId = 1;
                    entity.Vigente = false;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        internal Archivo getArchivoByNomina(Archivo archivo)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.Archivos.Where(x => x.NombreArchivo == archivo.NombreArchivo && x.Vigente == true && x.EstadoArchivoId == 2).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}
