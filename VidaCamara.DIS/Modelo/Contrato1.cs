
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
    
public partial class Contrato1
{

    public Contrato1()
    {

        this.Siniestros = new HashSet<Siniestro>();

    }


    public int ContratoId { get; set; }

    public int Identificador { get; set; }

    public System.DateTime Periodo_FechaInicio { get; set; }

    public System.DateTime Periodo_FechaTermino { get; set; }

    public int TotalPartes { get; set; }



    public virtual ICollection<Siniestro> Siniestros { get; set; }

}

}