
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace VidaCamara.DIS.Modelo
{

using System;
    using System.Collections.Generic;
    
public partial class NOMINA
{

    public int Id_Nomina { get; set; }

    public Nullable<int> Id_Empresa { get; set; }

    public Nullable<int> ArchivoId { get; set; }

    public int IDE_CONTRATO { get; set; }

    public string RUC_ORDE { get; set; }

    public string CTA_ORDE { get; set; }

    public string COD_TRAN { get; set; }

    public Nullable<int> TIP_MONE { get; set; }

    public Nullable<decimal> MON_TRAN { get; set; }

    public Nullable<System.DateTime> FEC_TRAN { get; set; }

    public string RUC_BENE { get; set; }

    public string NOM_BENE { get; set; }

    public Nullable<int> TIP_CTA { get; set; }

    public string CTA_BENE { get; set; }

    public string DET_TRAN { get; set; }

    public long CumpleValidacion { get; set; }

    public string Estado { get; set; }

    public Nullable<System.DateTime> FechaReg { get; set; }

    public string UsuReg { get; set; }

    public string ReglaObservada { get; set; }

    public string EstadoPago { get; set; }



    public virtual CONTRATO_SYS CONTRATO_SYS { get; set; }

    public virtual Archivo Archivo { get; set; }

}

}
