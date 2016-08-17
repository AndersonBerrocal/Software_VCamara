using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{

    public interface IGeneralVC
    {

        Int32 SetInsertarGeneral(eGeneral o);
        Int32 SetActualizarGeneral(eGeneral o);
        List<eGeneral> GetSelecionarGeneral();

    }
}

