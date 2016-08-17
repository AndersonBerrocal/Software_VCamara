using System;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao.Interface
{
    public interface IRegistroDatosVC
    {
        Int32 SetInsertarDatoM(eDatoM o);
        //List<eDatoM> GetSelectDatoM(int empresa, String contrato, int contable, int devengue, int index, int size, out int totalRows);
       
    }
}
