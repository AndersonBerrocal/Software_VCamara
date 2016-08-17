using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.data
{
    public class dNomina
    {
        public Int32 setGrabarNomina(NOMINA nomina) {
            try
            {
                using (var db = new DISEntities()) 
                {
                    db.NOMINAs.Add(nomina);
                    return db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public List<NOMINA> listNominaByArchivo(NOMINA nomina, object[] filters, int jtStartIndex, int jtPageSize, out int total)
        {
            var listNomina = new List<NOMINA>();
            try
            {
                using (var db = new DISEntities())
                {
                    var archivoId = filters[3].ToString();
                    var query = db.pa_sel_nominaXArchivo(nomina.IDE_CONTRATO, archivoId,nomina.CumpleValidacion).ToList();
                    total = query.Count();
                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {
                        var entity = new NOMINA()
                        {
                            Id_Nomina = item.Id_Nomina,
                            Id_Empresa = item.Id_Empresa,
                            ArchivoId = item.ArchivoId,
                            IDE_CONTRATO = item.IDE_CONTRATO,
                            RUC_ORDE = item.RUC_ORDE,
                            CTA_ORDE =  item.CTA_ORDE,
                            COD_TRAN = item.COD_TRAN,
                            TIP_MONE = item.TIP_MONE,
                            MON_TRAN = item.MON_TRAN,
                            FEC_TRAN = item.FEC_TRAN,
                            RUC_BENE = item.RUC_BENE,
                            NOM_BENE = item.NOM_BENE,
                            TIP_CTA = item.TIP_CTA,
                            CTA_BENE = item.CTA_BENE,
                            DET_TRAN = item.DET_TRAN,
                            ReglaObservada = item.ReglaObservada

                        };
                        listNomina.Add(entity);
                    }
                }
                return listNomina;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void actualizarEstadoFallido(int idArchivo, int contratoId)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_upd_cambiaEstadoNomina(idArchivo, contratoId);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<HNOMINA> listNominaConsulta(NOMINA nomina, object[] filters, int jtStartIndex, int jtPageSize, out int total)
        {
            var listNomina = new List<HNOMINA>();
            try
            {
                Nullable<DateTime> fechaCreacionInicio = null;
                Nullable<DateTime> fechaCreacionFin = null;

                Nullable<DateTime> fechaAprobacionInicio = null;
                Nullable<DateTime> fechaAprobacionFin = null;

                if(!string.IsNullOrEmpty(filters[1].ToString()))
                    fechaCreacionInicio = Convert.ToDateTime(filters[1].ToString());
                if(!string.IsNullOrEmpty(filters[2].ToString()))
                    fechaCreacionFin = Convert.ToDateTime(filters[2].ToString());

                if(!string.IsNullOrEmpty(filters[4].ToString()))
                    fechaAprobacionInicio = Convert.ToDateTime(filters[4].ToString());
                if(!string.IsNullOrEmpty(filters[5].ToString()))
                    fechaAprobacionFin = Convert.ToDateTime(filters[5].ToString());

                using (var db = new DISEntities())
                {
                    var nombreTipoArchivo = filters[0].ToString();
                    var query = db.pa_sel_nominaConsulta(nombreTipoArchivo, nomina.IDE_CONTRATO, nomina.NOM_BENE,(short)nomina.TIP_MONE,nomina.Estado, fechaCreacionInicio, fechaCreacionFin, fechaAprobacionInicio, fechaAprobacionFin).ToList();
                    total = query.Count();
                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {
                        var eNomina = new HNOMINA() {
                            FechaInsert = item.FechaInsert,
                            FechaAprobacion = item.FechaAprobacion,
                            Estado = item.Estado,
                            NombreArchivo = item.NombreArchivo,
                            Id_Nomina = item.Id_Nomina,
                            ArchivoId = item.ArchivoId,
                            IDE_CONTRATO = item.IDE_CONTRATO,
                            RUC_ORDE = item.RUC_ORDE,
                            CTA_ORDE = item.CTA_ORDE,
                            COD_TRAN = item.COD_TRAN,
                            TIP_MONE = item.TIP_MONE,
                            MON_TRAN = item.MON_TRAN,
                            FEC_TRAN = item.FEC_TRAN,
                            RUC_BENE = item.RUC_BENE,
                            NOM_BENE = item.NOM_BENE,
                            TIP_CTA = item.TIP_CTA,
                            CTA_BENE = item.CTA_BENE,
                            DET_TRAN = item.DET_TRAN
                        };
                        listNomina.Add(eNomina);
                    }
                }
                return listNomina;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal NOMINA getNominaByArchivoId(NOMINA nomina)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.NOMINAs.Include("Archivo").Where(n => n.ArchivoId == nomina.ArchivoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}
