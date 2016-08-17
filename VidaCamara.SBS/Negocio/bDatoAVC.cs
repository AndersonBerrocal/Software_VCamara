using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bDatoAVC:IDatoAVC
    {
        public Int32 SetInsertarDatoA(List<eDatoA> LstData)
        {
            dSqlDatoAVC da = new dSqlDatoAVC();
            eDatoA datoatmp = new eDatoA();
            datoatmp._Nro_Contrato = LstData[0]._Nro_Contrato;
            datoatmp._Mes_Vigente = LstData[0]._Mes_Vigente;
            datoatmp._Tipo_Dato = LstData[0]._Tipo_Dato;
            Int32 resptmp = da.GetCountExisteDatoA(datoatmp);
            if (resptmp > 0)
                return -1;
            else
                return da.SetInsertarDatoA(LstData);
        }
        public List<eDatoAString> GetSelecionarSepelio(eDatoAString o,int index,int size ,String sort,out int totalRow)
        {
            dSqlDatoAVC da = new dSqlDatoAVC();
            return da.GetSelecionarSepelio(o,index,size,sort,out totalRow);
        }
        public Int32 SetEliminarDatoA(eDatoA o) {
            dSqlDatoAVC da = new dSqlDatoAVC();
            return da.SetEliminarDatoA(o);
        }
        public List<eDataRSP> GetSelectRSP(eDatoAString o,int index, int size, String sort ,out int totalRows)
        {
            dSqlDatoAVC da = new dSqlDatoAVC();
            return da.GetSelectRSP(o,index, size, sort, out totalRows);
        }
    }
}