using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{

    public interface IContratoVC
    {
        Int32 SetInsertarContrato(eContratoVC o);
        Int32 SetEliminarContrato(int indice);
        List<eContratoVC> GetSelecionarContrato(eContratoVC o,out int total);
    }
}
