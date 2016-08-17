using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bContratoDetalleVC : IContratoDetalleVC
    {
        public Int32 SetInsertarContratoDetalle(eContratoDetalleVC o) {

            dSqlContratoDetalleVC cd = new dSqlContratoDetalleVC();
            return cd.SetInsertarContratoDetalle(o);
        }
        //actualizar contrato detalle
        public Int32 SetActualizarContratoDetalle(eContratoDetalleVC o) {
            dSqlContratoDetalleVC cd = new dSqlContratoDetalleVC();
            return cd.SetActualizarContratoDetalle(o);
        }
        public Int32 SetEliminarContratoDetalle(int indice)
        {
            dSqlContratoDetalleVC dc = new dSqlContratoDetalleVC();
            return dc.SetEliminarContratoDetalle(indice);
        }
        //listar contrato detalle
        public List<eContratoDetalleVC> GetSelecionarContratoDetalle(eContratoDetalleVC o,out int total) {
            dSqlContratoDetalleVC cd = new dSqlContratoDetalleVC();
            return  cd.GetSelecionarContratoDetalle(o,out total);
        }

    }
}