
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
    
public partial class TipoSolicitude
{

    public TipoSolicitude()
    {

        this.Siniestros = new HashSet<Siniestro>();

    }


    public int TipoSolicitudId { get; set; }

    public string Nombre { get; set; }

    public string Clasificacion { get; set; }



    public virtual ICollection<Siniestro> Siniestros { get; set; }

}

}