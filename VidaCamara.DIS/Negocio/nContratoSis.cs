using System.Collections.Generic;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.Negocio
{
    public class nContratoSis
    {
        /// <summary>
        /// Lista los contratos por id
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public CONTRATO_SYS listContratoByID(CONTRATO_SYS contrato) {
            return new dContratoSis().listContratoByID(contrato);
        }

        public int existeFecha(CONTRATO_SYS contratoSis,int paso)
        {
            return new dContratoSis().existeFecha(contratoSis,paso);
        }
        public CONTRATO_SYS listByNroContrato(CONTRATO_SYS contrato)
        {
            return new dContratoSis().listByNroContrato(contrato);
        }
    }
}
