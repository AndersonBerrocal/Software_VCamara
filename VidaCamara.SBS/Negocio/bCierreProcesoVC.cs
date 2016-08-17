using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bCierreProcesoVC
    {
        public Int32 SetInsertarCierreProceso(eCierreProceso o) {
            dSqlCierreProcesoVC dc = new dSqlCierreProcesoVC();
            return dc.SetInsertarCierreProceso(o);
        }
       public List<eCierreProceso> GetSelectCierreOperacion(eCierreProceso o, out int total, int start, int size, String orderBy) {
            dSqlCierreProcesoVC dco = new dSqlCierreProcesoVC();
            return dco.GetSelectCierreOperacion(o,out total,start,size,orderBy);
        }
       public Int32 SetEliminarCierreProceso(Int32 ide_cierre)
       {
           dSqlCierreProcesoVC dco = new dSqlCierreProcesoVC();
           return dco.SetEliminarCierreProceso(ide_cierre);
       }
    }
}