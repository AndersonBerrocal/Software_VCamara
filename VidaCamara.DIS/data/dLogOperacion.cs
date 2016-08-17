using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.data
{

    public class dLogOperacion
    {
        public long setGuardarLogOperacion(LogOperacion log)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.LogOperacions.Add(log);
                    db.SaveChanges();
                    return log.CodiLogOper;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        public List<HLogOperacion> getListLogOperacion(HLogOperacion log, int jtStartIndex, int jtPageSize,object[] filters, out int total)
        {
            var listLogOperacion = new List<HLogOperacion>();
            try 
	        {
                DateTime fecha_ini = string.IsNullOrEmpty(filters[0].ToString())?new DateTime(1900,01,01):Convert.ToDateTime(filters[0].ToString());
                DateTime fecha_fin = string.IsNullOrEmpty(filters[1].ToString()) ? DateTime.Now : Convert.ToDateTime(filters[1].ToString());
                using (var db = new DISEntities())
                {
                    var query = db.pa_sel_LogOperacion(log.IDE_CONTRATO, log.TipoOper, fecha_ini, fecha_fin, log.Evento).ToList();
                    total = query.Count();

                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {

                        var logOperacion = new HLogOperacion()
                        {
                            IDE_CONTRATO = Convert.ToInt32(item.IDE_CONTRATO),
                            CONTRATO_SYS = new CONTRATO_SYS()
                            {
                                DES_CONTRATO = item.DES_CONTRATO
                            },
                            TipoOper     = item.TipoOper,
                            FechEven     = Convert.ToDateTime(item.FechEven),
                            Evento       = item.Evento,
                            TipoEvento   = item.TipoEvento,
                            CodiEven     = item.CodiEven,
                            CodiUsu = item.CodiUsu,
                            Columna = item.Columna,
                            Tabla = string.Format("{0} - {1}",item.tabla,item.CodiOper)
                        };

                        listLogOperacion.Add(logOperacion);
                    }

                }
                return listLogOperacion;
	        }
	        catch (Exception ex)
	        {
		
		        throw;
	        }
        }
    }
}
