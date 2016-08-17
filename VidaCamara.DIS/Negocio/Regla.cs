namespace VidaCamara.DIS.Negocio
{
    public class Regla
    {
        public int idRegla { get; set; }
        public int CaracterInicial { get; set; }
        public int LargoCampo { get; set; }
        public string TipoCampo { get; set; }
        public string TipoValidacion { get; set; }
        public string ReglaValidacion { get; set; }
        public string Tabladestino { get; set; }
        public string NombreCampo { get; set; }
        public short? FormaValidacion { get; set; }
    }
}