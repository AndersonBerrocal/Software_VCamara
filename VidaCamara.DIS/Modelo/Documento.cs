
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
    
public partial class Documento
{

    public Documento()
    {

        this.Checkeados = new HashSet<Checkeado>();

        this.DocumentoSolicitados = new HashSet<DocumentoSolicitado>();

    }


    public int DocumentoId { get; set; }

    public string Descripcion { get; set; }



    public virtual ICollection<Checkeado> Checkeados { get; set; }

    public virtual ICollection<DocumentoSolicitado> DocumentoSolicitados { get; set; }

}

}
