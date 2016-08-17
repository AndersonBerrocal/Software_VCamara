using System;

namespace VidaCamara.DIS.Modelo.EEntidad
{
    public class HHistorialCargaArchivo_LinDet: HistorialCargaArchivo_LinDet
    {
        public string NombreArchivo { get;set;}
        public string Estado { get; set; }
        public Nullable<DateTime> FechaAprobacion { get; set; }
    }
}
