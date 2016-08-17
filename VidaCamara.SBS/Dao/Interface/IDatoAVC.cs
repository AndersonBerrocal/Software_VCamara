using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{
    public interface IDatoAVC
    {
        Int32 SetInsertarDatoA(List<eDatoA> o);
        List<eDatoAString> GetSelecionarSepelio(eDatoAString o, int index, int size, String sort, out int totalRow);
        Int32 SetEliminarDatoA(eDatoA o);
        List<eDataRSP> GetSelectRSP(eDatoAString o, int index, int size, String sort,out int totalRows);
    }
}
