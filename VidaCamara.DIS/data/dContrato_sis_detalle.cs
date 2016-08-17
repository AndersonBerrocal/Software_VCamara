using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.data
{
    public class dContrato_sis_detalle
    {
        public List<HCONTRATO_SIS_DET> getlistContratoDetalle(CONTRATO_SIS_DET contratoDetalle, object[] filterOptions, out int total)
        {
            var listDetalle = new List<HCONTRATO_SIS_DET>();
            try
            {
                var rowIndex = Int32.Parse(filterOptions[0].ToString());
                var rowEnd = Int32.Parse(filterOptions[1].ToString());
                using (var db = new DISEntities())
                {
                   //total = db.CONTRATO_SIS_DETs.Where(a=>a.IDE_CONTRATO == contratoDetalle.IDE_CONTRATO).Count();
                   //var query = db.CONTRATO_SIS_DETs.Include("CONTRATO_SYS")
                   //    .Where(a => a.IDE_CONTRATO == contratoDetalle.IDE_CONTRATO)
                   //    .OrderBy(a => a.IDE_CONTRATO_DET).Skip(rowIndex).Take(rowEnd).ToList();
                   var query = (from a in db.CONTRATO_SIS_DETs
                                join b in db.CONTRATO_SYSs on a.IDE_CONTRATO equals b.IDE_CONTRATO
                                join c in db.CONCEPTOs on a.COD_CSV.ToString() equals c.CODIGO
                                where
                                   a.IDE_CONTRATO == contratoDetalle.IDE_CONTRATO
                               && c.TIPO_TABLA.Equals("29")
                                orderby a.NRO_ORDEN
                                select new { a, b, c }).ToList();
                   total = query.Count;
                    foreach (var item in query)
                    {
                        var entity = new HCONTRATO_SIS_DET()
                        {
                            IDE_CONTRATO_DET = item.a.IDE_CONTRATO_DET,
                            IDE_CONTRATO = item.a.IDE_CONTRATO,
                            COD_CSV = item.a.COD_CSV,
                            PRC_PARTICIACION = item.a.PRC_PARTICIACION,
                            NRO_ORDEN = item.a.NRO_ORDEN,
                            ESTADO = item.a.ESTADO,
                            FEC_REG = item.a.FEC_REG,
                            USU_REG = item.a.USU_REG,
                            CONTRATO_SYS = new CONTRATO_SYS()
                            {
                                DES_CONTRATO = item.b.DES_CONTRATO,
                                NRO_CONTRATO = item.b.NRO_CONTRATO,
                                NRO_EMPRESAS = item.b.NRO_EMPRESAS
                            },
                            nombreCompania = item.c.DESCRIPCION
                        };
                        listDetalle.Add(entity);
                    }
                }
                return listDetalle;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public Int32 setGuardarContratoDetalle(CONTRATO_SIS_DET det)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.CONTRATO_SIS_DETs.Add(det);
                    db.SaveChanges();
                    return det.IDE_CONTRATO_DET;
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
        public Int32 setActualizarContratoDetalle(CONTRATO_SIS_DET det)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var entity = db.CONTRATO_SIS_DETs.Find(det.IDE_CONTRATO_DET);
                    entity.IDE_CONTRATO = det.IDE_CONTRATO;
                    entity.NRO_ORDEN = det.NRO_ORDEN;
                    entity.PRC_PARTICIACION = det.PRC_PARTICIACION;
                    entity.USU_MOD = det.USU_MOD;
                    entity.FEC_MOD = det.FEC_MOD;

                    return db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Int32 setEliminarContratoDetalle(int primary_key)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var entity = db.CONTRATO_SIS_DETs.Find(primary_key);
                    db.CONTRATO_SIS_DETs.Remove(entity);
                    return db.SaveChanges();
                }
            }
            catch (Exception)
            {
                    
                throw;
            }
        }
    }
}
