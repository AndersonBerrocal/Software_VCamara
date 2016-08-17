using System;

namespace VidaCamara.SBS.Entity
{
    public class eInterfaceContableVC
    {

        public String Nro_Contrato {get;set;}
        public Int32 Anio_Vigente { get; set; }
        public String Mes_Vigente { get; set; }
        public String Tipo_Info { get; set; }
        public String Asiento { get; set; }
        public String Paquete { get; set; }
        public String Tipo_Asiento { get; set; }
        public DateTime Fecha { get; set; }
        public String Contabilidad { get; set; }
        public String Clase_Contrato { get; set; }
        public String Notas { get; set; }
        public Int32 Estado { get; set; }
        public String Estado_Transferencia {get;set;}
        public String Permitir_Descuadrado { get; set; }
        public String Conservar_Numeracion { get; set; }
        public String Actualizar_Consecutivo { get; set; }
        public DateTime Fecha_Auditoria { get; set; }
        public String Usuario_Registro { get; set; }

        public Int32 Consecutivo { get; set; }
        public String Contribuyente { get; set; }
        public String Centro_Costo { get; set; }
        public String Cuenta_Contable { get; set; }
        public String Fuente { get; set; }
        public String Referencia { get; set; }
        public String debito_Local { get; set; }
        public Decimal Monto_Local { get; set; }
        public Decimal Monto_Dolar { get; set; }
        public String credito_Local { get; set; }
        public String debito_dolar { get; set; }
        public String credito_dolar { get; set; }
        public String Monto_Unidades { get; set; }
        public String Nit { get; set; }
        public String Dimension1 { get; set; }
        public String Dimension2 { get; set; }
        public String Dimension3 { get; set; }
        public String Dimension4 { get; set; }
        public String Dimension5 { get; set; }

        public String Formato_Moneda { get; set; }

    }
}