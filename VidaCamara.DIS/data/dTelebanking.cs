using System;
using System.Collections.Generic;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;
using System.Linq;

namespace VidaCamara.DIS.data
{
    public class dTelebanking
    {
        public List<EGeneraTelebankig> listTelebanking(NOMINA nomina, int jtStartIndex, int jtPageSize,string sorting, string formatoMoneda, out int total)
        {
            var folder = System.Configuration.ConfigurationManager.AppSettings["CarpetaArchivos"].ToString();
            var listTelebankig = new List<EGeneraTelebankig>();
            try
            {
                var sorter = sorting.Split(' ');
                var propertyInfo = typeof(pa_sel_NominaForTelebanking_Result).GetProperty(sorter[0].Trim());

                using (var db = new DISEntities())
                {
                    var query = db.pa_sel_NominaForTelebanking(nomina.IDE_CONTRATO, nomina.FechaReg,nomina.Estado).ToList();
                    total = query.Count;
                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {
                        var telebankig = new EGeneraTelebankig()
                        {
                            ArchivoId = item.ArchivoId,
                            NombreArchivo = item.NombreArchivo,
                            FechaOperacion = Convert.ToDateTime(item.FechaOperacion),
                            Moneda = item.Moneda,
                            Importe = string.Format(formatoMoneda,item.Importe),
                            RutaNomina = string.Format("{0}/NOMINA/{1}",folder,item.NombreArchivo),
                            Estado = item.Estado,
                            EstadoPago = item.EstadoPago
                            
                        };
                        listTelebankig.Add(telebankig);
                    }
                }
                return listTelebankig;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<EGeneraTelebankig> listTelebankingByArchivoId(NOMINA nomina,string formatoMoneda)
        {
            var listTelebankig = new List<EGeneraTelebankig>();
            try
            {
                using (var db = new DISEntities())
                {
                    var query = db.pa_sel_NominaForTelebankinByArchivoId(nomina.ArchivoId).ToList();
                    foreach (var item in query)
                    {
                        var telebankig = new EGeneraTelebankig()
                        {
                            RUC_BENE = item.RUC_BENE,
                            NOM_BENE = item.NOM_BENE,
                            TIP_CTA = item.TIP_CTA,
                            CTA_BENE = item.CTA_BENE,
                            Importe = string.Format(formatoMoneda,item.IMPORTE)
                        };
                        listTelebankig.Add(telebankig);
                    }
                }
                return listTelebankig;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void aprobarTelebanking(NOMINA nOMINA)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_confirmaPagoNomina(nOMINA.ArchivoId,1);
                }
            }
            catch (Exception ex)
            { 
                throw(new Exception(ex.Message));
            }
        }

        public void aprobarFinalTelebanking(NOMINA nOMINA)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_confirmaPagoNomina(nOMINA.ArchivoId,2);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
    }
}
