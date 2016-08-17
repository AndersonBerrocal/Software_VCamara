using System;
using System.Linq;
using VidaCamara.DIS.Modelo;
namespace VidaCamara.DIS.data
{
    public class dContratoSis
    {
        //INVOCAR STORE PROCEDURE
        //var date1 = DateTime.Now;
        //var date2 = DateTime.Now;
        //
        public CONTRATO_SYS listContratoByID(CONTRATO_SYS contrato)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.CONTRATO_SYSs.OrderBy(a => a.IDE_CONTRATO).Where(a => a.IDE_CONTRATO == contrato.IDE_CONTRATO).FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                
                throw;
            }
        }

        public int existeFecha(CONTRATO_SYS contratoSis,int paso)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    if(paso == 0)
                        return int.Parse(db.pa_Valida_RangoFechaXContrato(contratoSis.FEC_INI_VIG, contratoSis.FEC_FIN_VIG).FirstOrDefault().Value.ToString());
                    else
                        return int.Parse(db.pa_Valida_RangoFechaXContratoV2(contratoSis.FEC_INI_VIG, contratoSis.FEC_FIN_VIG).FirstOrDefault().Value.ToString());
                }
            }
            catch (System.Exception ex)
            {
                
                throw;
            }
        }

        internal CONTRATO_SYS listByNroContrato(CONTRATO_SYS contrato)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.CONTRATO_SYSs.Where(x => x.NRO_CONTRATO == contrato.NRO_CONTRATO && x.ESTADO == "A").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
