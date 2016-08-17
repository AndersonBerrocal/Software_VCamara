using System;

namespace VidaCamara.DIS.Modelo.EEntidad
{
    public partial class EAprobacionCarga
    {
        public long IdLinCab { get; set; }
        public long IdArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaCarga { get; set; }
        public string moneda { get; set; }
        public long TotalRegistros { get; set; }
        public string TotalImporte { get; set; }
        public string PagoVc { get; set; }
        public DateTime FechaInfo { get; set; }
        public string UsuReg { get; set; }
        public string Aprobar { get; set; }
        public string Eliminar { get; set; }
    }

    public partial class eAprobacionCargaDetalle { 
        public string NombreArchivoNomina { get; set; }
        public string NombreAseguradora { get; set; }
        public string TotalImporteNomina { get; set; }
        public string PagoVcNomina { get; set; }
    }
}
