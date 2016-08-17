using System;

namespace VidaCamara.SBS.Entity
{
    public class eOperacionVC
    {
        public Int32 _Nro_Operacion { get; set; }
        public String _Tip_Operacion { get; set; }
        public String _Tipo_Registro {get; set;}
        public String _Cod_Reasegurador { get; set; }
        public DateTime _Fec_Operacion { get; set; }
        public Int32 _Ano_Operacion { get; set; }
        public String _Mes_Operacion { get; set; }
        public String _Cod_Asegurado { get; set; }
        public String _Ide_Contrato { get; set; }
        public String _Cod_Ramo { get; set; }
        public String _Cod_Moneda { get; set; }
        public String _Entorno_Prima_T_2 { get; set; }
		public String _Entorno_Prima_T_1 { get; set; }
		public String _Prima_Abonada { get; set; }
        public String _Prima_Reg_Contab { get; set; }
        public String _Pri_Ced_Mes { get; set; }
        public String _Des_Reg_Mes { get; set; }
        public String _Imp_Impuesto_Mes { get; set; }
        public String _Pri_Xpag_Rea_Ced { get; set; }
        public String _Pri_Xcob_Rea_Ace { get; set; }
        public String _Sin_Directo { get; set; }
        public String _Sin_Xcob_Rea_Ced { get; set; }
        public String _Sin_Xpag_Rea_Ace { get; set; }
        public String _Otr_Cta_Xcob_Rea_Ced { get; set; }
        public String _Otr_Cta_Xpag_Rea_Ace { get; set; }
        public String _Dscto_Comis_Rea { get; set; }
        public String _Saldo_Deudor { get; set; }
        public String _Saldo_Acreedor { get; set; }
        public String _Saldo_Deudor_Comp { get; set; }
        public String _Saldo_Acreedor_Comp { get; set; }
        public String _Estado { get; set; }
        public DateTime _Fec_Reg { get; set; }
        public String _Usu_Reg { get; set; }
        public DateTime _Fec_Mod { get; set; }
        public String _Usu_Mod { get; set; }
        public DateTime _fecha_ini { get; set; }
        public DateTime _fecha_fin { get; set; }
        public String _token_fecha { get; set; }
        public String _Formato_Moneda { get; set; }
        public String _Tip_Comprobante { get; set; }
        public short _Origen_Operacion { get; set; }
    }
    public class eValidarCierreVC {

        public String _Nro_Contrato { get; set; }
        public String _Tipo_Info { get; set; }
        public String _Tipo_Registro { get; set; }
        public Int32 _Mes_x_Trimestral { get; set; }
        public Int32 _Mes_Anterior1 { get; set; }
        public Int32 _Mes_Anterior { get; set; }
        public Int32 _Mes_Actual { get; set; }
        public Int32 _Anio_Operacion { get; set; }
    }
}