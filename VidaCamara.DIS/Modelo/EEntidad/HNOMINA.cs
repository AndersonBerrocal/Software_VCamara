using System;

namespace VidaCamara.DIS.Modelo.EEntidad
{
    public class HNOMINA:NOMINA
    {
        public string NombreArchivo { get; set; }
        public Nullable<DateTime> FechaInsert { get; set; }
        public Nullable<DateTime> FechaAprobacion { get; set; }
    }
}
