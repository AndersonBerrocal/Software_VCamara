using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{
    public interface IContratoSys
    {
        Int32 SetInsertarContratoSys(eContratoSys o);
        Int32 SetEliminarContratoSys(int indice);
        List<eContratoSys> GetSelecionarContratoSys(eContratoSys o, out int total);
    }
}
