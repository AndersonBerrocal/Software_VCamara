using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.data
{
    public class dTipoCambio
    {
        public void updateTipoCambio(TipoCambio cambio)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var entity = db.TipoCambios.Find(cambio.IdTipoCambio);
                    entity.Vigente = cambio.Vigente;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message,ex.InnerException));
            }
        }
        public void saveTipoCambio(TipoCambio cambio)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.TipoCambios.Add(cambio);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message == null ? e.Message : e.InnerException.InnerException.Message;
                throw (new Exception(message));
            }
        }

        public List<TipoCambio> listTipoCambio(TipoCambio cambio, int jtStartIndex, int jtPageSize, string jtSorting, out int total)
        {
            try
            {
                var sort = jtSorting.Split(' ');
                using (var db = new DISEntities())
                {
                    var query =  db.TipoCambios.Where(a=>(a.Periodo == cambio.Periodo || cambio.Periodo == "") &&
                                                    (a.Monto == cambio.Monto || cambio.Monto == 0)).ToList();
                    total = query.Count;
                    switch (sort[0])
                    {
                        case "Monto":
                            query =  sort[1].Equals("ASC") ? query.OrderBy(a=>a.Monto).Skip(jtStartIndex).Take(jtPageSize).ToList() : query.OrderByDescending(a=>a.Monto).Skip(jtStartIndex).Take(jtPageSize).ToList();
                            break;
                        case "Periodo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.Periodo).Skip(jtStartIndex).Take(jtPageSize).ToList() : query.OrderByDescending(a => a.Periodo).Skip(jtStartIndex).Take(jtPageSize).ToList();
                            break;
                        default:
                            query = query.OrderBy(a => a.IdTipoCambio).Skip(jtStartIndex).Take(jtPageSize).ToList();
                            break;
                    }
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message, ex.InnerException));
            }
        }
    }
}
