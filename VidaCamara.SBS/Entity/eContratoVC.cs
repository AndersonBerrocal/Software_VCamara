using System;

//using System.Data.Common;

namespace VidaCamara.SBS.Entity
{
    public class eContratoVC
    {
        public Int32 _id_Empresa { get; set; }
        public Int64 _ide_Contrato { get; set; }
        public String _nro_Contrato { get; set; }
        public String _cla_Contrato { get; set; }
        public String _cod_Ramo_Sin { get; set; }
        public String _cod_Ramo_pri { get; set; }
        public DateTime _fec_Ini_Vig { get; set; }
        public DateTime _fec_Fin_Vig { get; set; }
        public String _tip_Contrato { get; set; }
        public String _cod_Moneda { get; set; }
        public String _cod_Contratante { get; set; }
        public Decimal _por_Tasa_Riesgo { get; set; }
        public Decimal _por_Tasa_Reaseguro { get; set; }
        public Decimal _por_Impuesto { get; set; }
        public String _Centro_Costo { get; set; }
        public Decimal _por_Participa_Cia { get; set; }
        public String _des_Contrato { get; set; }
        public String _mod_Contrato { get; set; }
        public Decimal _por_Retencion { get; set; }
        public Decimal _por_Cesion { get; set; }
        public Decimal _mto_Max_Retencion { get;set;}
        public Decimal _mto_Max_Cesion { get; set; }
        public Decimal _mto_Pleno { get; set; }
        public Int32 _nro_Linea_Mult { get; set; }
        public Decimal _mto_Max_Cubertura { get; set; }
        public Int32 _nro_Capa_Xl1 { get; set; }
        public Decimal _Prioridad1 { get; set; }
        public Decimal _Cesion_Exc_Prioridad1 { get; set; }
        public Decimal _mto_Max_Cap_Lim_Sup1 { get; set; }
        public Decimal _prima_Min_Deposito1 { get; set; }
        public Int32 _nro_Capa_Xl2 { get; set; }
        public Decimal _Prioridad2 { get; set; }
        public Decimal _Cesion_Exc_Prioridad2 { get; set; }
        public Decimal _mto_Max_Cap_Lim_Sup2 { get; set; }
        public Decimal _prima_Min_Deposito2 { get; set; }
      
        public String _estado { get; set; }
        public DateTime _fec_reg { get; set; }
        public String _usu_reg { get; set; }
        public DateTime _fec_mod { get; set; }
        public String _usu_mod { get; set; }
        public Int32 _inicio { get; set; }
        public Int32 _fin { get; set; }
        public String _orderby { get; set; }
        public Int32 _total { get; set; }
    }
}
