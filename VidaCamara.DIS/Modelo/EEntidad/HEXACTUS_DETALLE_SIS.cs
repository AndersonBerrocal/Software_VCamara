namespace VidaCamara.DIS.Modelo.EEntidad
{
    public class HEXACTUS_DETALLE_SIS:EXACTUS_DETALLE_SIS
    {
        public string CreditoSoles { get; set; }
        public string CreditoDolar { get; set; }
        public string DebitoSoles { get; set; }
        public string DebitoDolar { get; set; }

        public string EstadoTransferenciaDetalle { get; set; }
    }
}
