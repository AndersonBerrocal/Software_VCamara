
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
    
public partial class HistorialCargaArchivo
{

    public long IdHistorialCargaArchivo { get; set; }

    public int IdArchivo { get; set; }

    public Nullable<System.DateTime> FechaInsert { get; set; }

    public Nullable<int> IdReglaArchivo { get; set; }

    public string TipoLinea { get; set; }

    public Nullable<int> NumeroLinea { get; set; }

    public Nullable<int> CampoInicial { get; set; }

    public Nullable<int> LargoCampo { get; set; }

    public string ValorCampo { get; set; }

    public Nullable<int> CumpleValidacion { get; set; }

    public bool EsVC { get; set; }



    public virtual Archivo Archivo { get; set; }

    public virtual ReglaArchivo ReglaArchivo { get; set; }

}

}
