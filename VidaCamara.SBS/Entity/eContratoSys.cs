using System;

//using System.Data.Common;

namespace VidaCamara.SBS.Entity
{
    public class eContratoSys
    {
        public Int32 _id_Empresa { get; set; }
        public Int64 _ide_Contrato { get; set; }
        public String _nro_Contrato { get; set; }
        public String _cla_Contrato { get; set; }
        public DateTime _fec_Ini_Vig { get; set; }
        public DateTime _fec_Fin_Vig { get; set; }
        public String _des_Contrato { get; set; }
        public String _estado { get; set; }
        public DateTime _fec_reg { get; set; }
        public String _usu_reg { get; set; }
        public DateTime _fec_mod { get; set; }
        public String _usu_mod { get; set; }
        public Int32 _inicio { get; set; }
        public Int32 _fin { get; set; }
        public String _orderby { get; set; }
        public Int32 _total { get; set; }
        public Int32 _nro_empresa { get; set; }
        public string _centro_costo { get; set; }
    }
}
