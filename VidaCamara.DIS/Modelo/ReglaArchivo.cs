
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
    
public partial class ReglaArchivo
{

    public ReglaArchivo()
    {

        this.HistorialCargaArchivoes = new HashSet<HistorialCargaArchivo>();

    }


    public int IdReglaArchivo { get; set; }

    public string Archivo { get; set; }

    public string TipoLinea { get; set; }

    public Nullable<int> CaracterInicial { get; set; }

    public Nullable<int> LargoCampo { get; set; }

    public string TipoCampo { get; set; }

    public string InformacionCampo { get; set; }

    public string FormatoContenido { get; set; }

    public string TipoValidacion { get; set; }

    public string ReglaValidacion { get; set; }

    public string ReglaValidacionInterna { get; set; }

    public string TablaDestino { get; set; }

    public Nullable<short> vigente { get; set; }

    public string CampoNegocio { get; set; }

    public Nullable<System.DateTime> VigenciaReglaDesde { get; set; }

    public Nullable<System.DateTime> VigenciaReglaHasta { get; set; }

    public string NombreCampo { get; set; }

    public string TituloColumna { get; set; }

    public Nullable<short> FormaValidacion { get; set; }

    public Nullable<int> NUM_CONT_LIC { get; set; }



    public virtual ICollection<HistorialCargaArchivo> HistorialCargaArchivoes { get; set; }

}

}
