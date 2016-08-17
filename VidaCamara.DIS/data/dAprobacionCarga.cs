using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.data
{
    public class dAprobacionCarga
    {
        public List<EAprobacionCarga> listApruebaCarga(CONTRATO_SYS contrato, int jtStartIndex, int jtPageSize,string sorting,object[] filters, out int total)
        {
            var listAprueba = new List<EAprobacionCarga>();
            try
            {
                #region VARIABLES
                var fecha_inicio = Convert.ToDateTime(filters[1]);
                var fecha_fin = Convert.ToDateTime(filters[2]);
                var sorter = sorting.Split(' ');
                if (sorter[0].ToUpper().Equals("MONEDA"))
                    sorter[0] = "Moneda";
                else if(sorter[0].ToUpper().Equals("TOTALIMPORTE"))
                    sorter[0] = "ImporteTotal";
                var propertyInfo = typeof(pa_sel_pagoNominaAprueba_Result).GetProperty(sorter[0].Trim());
                #endregion VARIABLES

                using (var db = new DISEntities())
                {
                    var query = db.pa_sel_pagoNominaAprueba(contrato.IDE_CONTRATO,filters[0].ToString(), fecha_inicio,fecha_fin).ToList();
                    total = query.Count();
                    if (sorter[1].Trim().ToUpper().Equals("ASC"))
                        query = query.OrderBy(x => propertyInfo.GetValue(x, null)).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    else
                        query = query.OrderByDescending(x => propertyInfo.GetValue(x, null)).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {
                        var eApruebaCarga = new EAprobacionCarga()
                        {
                            IdLinCab = item.IdLinCab,
                            IdArchivo = item.ArchivoId,
                            NombreArchivo = item.NombreArchivo,
                            FechaCarga = Convert.ToDateTime(item.FechaCarga),
                            moneda = item.Moneda,
                            TotalRegistros = Convert.ToInt64(item.TotalRegistros),
                            TotalImporte = string.Format(filters[3].ToString(), item.ImporteTotal),
                            PagoVc =  string.Format(filters[3].ToString(), item.PagoVC),
                            FechaInfo = item.FechaInfo,
                            UsuReg = item.UsuReg,
                            Aprobar = item.Aprobar,
                            Eliminar = item.Eliminar
                        };
                        listAprueba.Add(eApruebaCarga);
                    }
                }
                return listAprueba;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public void eliminarPagoYNomina(HistorialCargaArchivo_LinCab historialCargaArchivo_LinCab)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_upd_InactivaLinCabNomina(historialCargaArchivo_LinCab.IDE_CONTRATO, Convert.ToInt32(historialCargaArchivo_LinCab.IdHistorialCargaArchivoLinCab));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public void actualizaEstadoArchivo(Archivo archivo)
        //{
        //    try
        //    {
        //        using (var db = new DISEntities())
        //        {
        //            db.pa_upd_InactivaLinCabNomina(archivo.ArchivoId, Convert.ToInt32(archivo.EstadoArchivoId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public void actualizarEstado(HistorialCargaArchivo_LinCab historialCargaArchivo_LinCab)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_upd_ApruebaLinCabNomina(historialCargaArchivo_LinCab.IDE_CONTRATO, Convert.ToInt32(historialCargaArchivo_LinCab.IdHistorialCargaArchivoLinCab));
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void actualizarArchivoIdNomina(Archivo archivo)
        {
            try
            {
                var historiaLinCab  = archivo.HistorialCargaArchivo_LinCab.FirstOrDefault();
                using (var db = new DISEntities())
                {
                    var query = db.HistorialCargaArchivo_LinCabs.Find(historiaLinCab.IdHistorialCargaArchivoLinCab);
                    query.ArchivoIdNomina = archivo.ArchivoId;
                    db.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public List<eAprobacionCargaDetalle> listApruebaCargaDetalle(HistorialCargaArchivo_LinCab linCab, object[] filters)
        {
            var listDetalle = new List<eAprobacionCargaDetalle>();
            try
            {
                using (var db = new DISEntities())
                {
                    var query = db.pa_sel_pagoNominaApruebaDetalle(Convert.ToInt32(linCab.IdHistorialCargaArchivoLinCab)).ToList();
                    foreach (var item in query)
                    {
                        var eAprobacionDet = new eAprobacionCargaDetalle() { 
                            NombreArchivoNomina = item.NombreArchivo,
                            NombreAseguradora = item.COD_AFP,
                            TotalImporteNomina = string.Format(filters[0].ToString(),item.ImporteTotal),
                            PagoVcNomina = string.Format(filters[0].ToString(),(item.ImporteTotal * item.PagoVC))
                        };
                        listDetalle.Add(eAprobacionDet);
                    }
                }
                return listDetalle;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
