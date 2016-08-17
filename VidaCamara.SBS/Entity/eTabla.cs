using System;

//using System.Data.Common;

namespace VidaCamara.SBS.Entity
{
    public class eTabla
    {
        public Int32 _id_Concepto { get; set; }
        public Int32 _id_Empresa { get; set; }
        public String _tipo_Tabla { get; set; }
        public String _codigo { get; set; }
        public String _descripcion { get; set; }
        public String _valor { get; set; }
        public String _clase { get; set; }
        public String _tipo { get; set; }
        public String _estado { get; set; }
        public DateTime _fe_creg { get; set; }
        public String _usu_reg { get; set; }
        public DateTime _fe_cmod { get; set; }
        public String _usu_mod { get; set; }
        public Int32 _inicio { get; set; }
        public Int32 _fin { get; set; }
        public String _order { get; set; }
        public int _is_editable { get; set; }
    }
}
