using System;

namespace VidaCamara.SBS.Entity
{
    public class eContratoDetalleVC
    {
        public Int32 _id_Empresa { get; set; }
        public Int32 _ide_Contrato_Det { get;set; }
        public String _nro_Contrato { get; set; }
        public String _ide_Reasegurador { get; set; }
        public String _cod_Reasegurador { get; set; }
        public String _cal_Crediticia { get; set; }
        public String _cod_Empresa_Califica { get; set; }
        public String _mod_Contrato { get; set; } 
        public Decimal _prc_Retencion { get; set; }
        public Decimal _prc_Cesion { get; set; }
        public Decimal _prc_participacion_rea { get; set; }
        public String _nombre_Rea { get; set; }
        public Int32 _nro_Registro_Rea { get; set; }
        public String _estado { get; set; }
        public String _usu_reg { get; set; }
        public DateTime _fec_reg { get; set; }
        public DateTime _fec_mod { get; set; }
        public String _usu_mod { get; set; }
        public Int32 _inicio { get; set; }
        public Int32 _fin { get; set; }
        public String _orderby { get; set; }
        public Int32 _total { get; set; }
    }
}
