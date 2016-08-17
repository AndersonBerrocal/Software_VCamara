using System;
using System.Collections.Generic;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{
    public interface IContratoDetalleVC
    {
        Int32 SetInsertarContratoDetalle(eContratoDetalleVC o);
        Int32 SetActualizarContratoDetalle(eContratoDetalleVC o);
        List<eContratoDetalleVC> GetSelecionarContratoDetalle(eContratoDetalleVC o,out int total);
    }
}
