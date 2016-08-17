using System;
using System.Collections.Generic;
using System.Data;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bInterfaceContableVC
    {
        public Int32 SetInsertarInterfaceContable(eInterfaceContableVC e) {
            dSqlInterfaceContable interfaceC = new dSqlInterfaceContable();
            Int32 existe_interface = interfaceC.GetExisteInterface(e);
            Int32 existe_cierre = interfaceC.GetExisteCierreOperacion(e);
            if (existe_cierre > 0)
            {
                if (existe_interface == 0)
                {
                    if (e.Tipo_Info.Trim().Equals("RR") || e.Tipo_Info.Trim().Equals("RP"))
                        return interfaceC.SetIntertarAsientoSeniestroRSPyPAGO(e);
                    else if (e.Tipo_Info.Trim().Equals("RI"))
                        return interfaceC.SetIntertarAsientoSeniestroIBNR(e);
                    else if (e.Tipo_Info.Trim().Equals("RM"))
                        return interfaceC.SetIntertarAsientoSeniestroPRIMA(e);
                    else
                        return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
                return -2;
        }

        public List<eInterfaceContableVC> GetSelectContable(eInterfaceContableVC e) {
            dSqlInterfaceContable contable = new dSqlInterfaceContable();
            return contable.GetSelectContable(e);
        }

        public DataTable GetSelectContableExport(eInterfaceContableVC e) {
            dSqlInterfaceContable contable = new dSqlInterfaceContable();
            return contable.GetSelectContableExport(e);
        }
        public Int32 SetEliminarExactusDetalle(eInterfaceContableVC e) {
            dSqlInterfaceContable contable = new dSqlInterfaceContable();
            return contable.SetEliminarCabeceraDetalle(e);
        }
        public Int32 SetTransferExactus(eInterfaceContableVC e) {
            dSqlInterfaceContable contable = new dSqlInterfaceContable();
            return contable.GetTransferExactus(e);
        }
    }
}