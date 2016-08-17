using System;

//using System.Data.Common;


namespace VidaCamara.SBS.Entity
{
    public class eUsuarioVC
    {
        public Int64 _idEmpresa { get; set; }
        public Int64 _ide_Usuario { get; set; }
        public String _nombres { get; set; }
        public String _cargo { get; set; }
        public String _email { get; set; }
        public String _tipo_Usuario { get; set; }
        public String _Aceso_Pagina { get; set; }
        public String _estado {get;set;}
        public DateTime _fec_Reg { get; set; }
        public String _usu_reg { get; set; }
        public String _usu_mod { get; set; }
        public DateTime _fec_Mod { get; set; }
        public String _Usuario { get; set; }
        public String _Contrasena { get; set; }
        public Boolean _Permiso { get; set; }
    }
    public class eAccesoPagina
    {
        public String ide_Pagina  { get; set; }
        public String Descripcion { get; set; }
        public Boolean permiso { get; set; }
    }
}
