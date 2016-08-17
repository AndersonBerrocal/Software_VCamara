using System;

//using System.Data.Common;

namespace VidaCamara.SBS.Entity
{
    public class eGeneral
    {
        public Int32 _idEmpresa { get; set; }
        public String _descripcion { get; set; }
        public String  _rucEmpresa { get; set; }
        public Int32 _anoVigente { get; set; }
        public Int32 _mesVigente { get; set; }
        public String _Ruta_Archivo {get;set;}
        public Int32 _Nro_Decimal { get; set; }
        public Decimal _tcaMes { get; set; }
        public Decimal _tcaAno { get; set; }
        public String _estado { get; set; }
        public DateTime _fecreg { get; set; }
        public String _usureg { get; set; }
        public DateTime _fecmod { get; set; }
        public String _usumod { get; set; }

    }
}
