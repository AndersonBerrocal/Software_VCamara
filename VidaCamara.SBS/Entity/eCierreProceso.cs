using System;

namespace VidaCamara.SBS.Entity
{
    public class eCierreProceso
    {
        public Int32 _Id_Cierre { get; set; }
        public String _Nro_Contrato { get; set; }
        public String _Tipo_Info { get; set; }
        public DateTime _Fec_Cie { get; set; }
        public Int32 _Anio_Cierre { get; set; }
        public String _Mes_Cierre { get; set; }
        public Int32 _Nro_Registro { get; set; }
        public String _Tipo_Cierre { get; set; }
        public String _Total_Monto { get; set; }
        public String _Estado { get; set; }
        public DateTime _Fec_Reg { get; set; }
        public String _Usu_Reg { get; set; }
        public String _Formato_Moneda { get; set; }
    }
}