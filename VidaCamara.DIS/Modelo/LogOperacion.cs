
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
    
public partial class LogOperacion
{

    public long CodiLogOper { get; set; }

    public int IDE_CONTRATO { get; set; }

    public System.DateTime FechEven { get; set; }

    public string TipoOper { get; set; }

    public string CodiOper { get; set; }

    public string CodiEven { get; set; }

    public string CodiUsu { get; set; }

    public string CodiCnx { get; set; }

    public string Entidad { get; set; }



    public virtual CONTRATO_SYS CONTRATO_SYS { get; set; }

}

}
