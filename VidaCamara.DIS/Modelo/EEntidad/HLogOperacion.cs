namespace VidaCamara.DIS.Modelo.EEntidad
{
    public class HLogOperacion:LogOperacion
    {
        public string TipoEvento { get; set; }
        public string Evento { get; set; }
        public string Columna { get; set; }
        public string Tabla { get; set; }
    }
}
