using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidaCamara.DIS.Modelo.EEntidad
{
    public class EGeneraTelebankig
    {
        public string NombreArchivo { get; set; }
        public int ArchivoId { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string Moneda { get; set; }
        public string Importe { get; set; }
        public string RUC_BENE { get; set; }
        public string NOM_BENE { get; set; }
        public string TIP_CTA { get; set; }
        public string CTA_BENE { get; set; }
        public string RutaNomina { get; set; }
        public string Estado { get; set; }
        public string EstadoPago { get; set; }
    }
}
