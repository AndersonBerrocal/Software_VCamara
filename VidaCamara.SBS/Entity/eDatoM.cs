using System;

//using System.Data.Common;

namespace VidaCamara.SBS.Entity
{
    public class eDatoM
    {
        public Int32 _id_Empresa { get; set; }
        public Int64 _ide_Data_M { get; set; }
        public String _tipo_info { get; set; }
        public String _nro_Contrato { get; set; }
        public Int32 _anio_Vigente { get; set; }
        public Int32 _mes_Vigente { get; set; }
        public Int32 _mes_Contable { get;set;}
        public String _cod_Ramo { get; set; }
        public String _cod_Producto { get; set; }
        public Decimal _mto_Abonado { get; set; }
        public Decimal _mto_Prima_Est { get; set; }
        public String _estado { get; set; }
        public DateTime _fe_creg { get; set; }
        public String _usu_reg { get; set; }
        public DateTime _fec_mod { get; set; }
        public String _usu_mod { get; set; }
        public String _Formato_Moneda { get; set; }
    }
    public class eDatoMGrid {

        public Int64 _ide_Data_M { get; set; }
        public String _tipo_info { get; set; }
        public String _nro_Contrato { get; set; }
        public Int32 _anio_Vigente { get; set; }
        public Int32 _mes_Vigente { get; set; }
        public Int32 _mes_Contable { get; set; }
        public String _cod_Ramo { get; set; }
        public String _cod_Producto { get; set; }
        public String _mto_Abonado { get; set; }
        public String _mto_Prima_Est { get; set; }
        public String _estado { get; set; }
        public DateTime _fe_creg { get; set; }
        public String _usu_reg { get; set; }
        public Int32 _inicio { get; set; }
        public Int32 _fin { get; set; }
        public String _orderby { get; set; }
        public String _Formato_Moneda { get; set; }
    }
}
