using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{

    public interface ITablaVC
    {
        Int32 SetInsertarConcepto(eTabla o);
        Int32 SetActualizarConcepto(eTabla o);
        Int32 SetEliminarConcepto(int indice);
        List<eTabla> GetSelectConcepto(eTabla o,out int total);
    }
}
