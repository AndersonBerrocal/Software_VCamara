using System;
using System.Collections.Generic;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.Negocio
{
    public class nContratoSisDetalle
    {
        public Int32 setGuardarContratoDetalle(CONTRATO_SIS_DET det)
        {
            return new dContrato_sis_detalle().setGuardarContratoDetalle(det);
        }
        public Int32 setActualizarContratoDetalle(CONTRATO_SIS_DET det)
        {
            return new dContrato_sis_detalle().setActualizarContratoDetalle(det);
        }

        public List<HCONTRATO_SIS_DET> getlistContratoDetalle(CONTRATO_SIS_DET contratoDetalle, object[] filterOptions, out int total)
        {
            return new dContrato_sis_detalle().getlistContratoDetalle(contratoDetalle, filterOptions,out total);
        }

        public Int32 setEliminarContratoDetalle(int primary_key)
        {
            return new dContrato_sis_detalle().setEliminarContratoDetalle(primary_key);
        }
    }
}
