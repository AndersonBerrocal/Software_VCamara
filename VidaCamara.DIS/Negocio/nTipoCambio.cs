using System.Collections.Generic;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.data;
using System.Text;

namespace VidaCamara.DIS.Negocio
{
    public class nTipoCambio
    {
        public void updateTipoCambio(TipoCambio cambio)
        {
            new dTipoCambio().updateTipoCambio(cambio);
        }
        public void saveTipoCambio(TipoCambio cambio)
        {
            new dTipoCambio().saveTipoCambio(cambio);
        }

        public List<TipoCambio> listTipoCambio(TipoCambio cambio, int jtStartIndex, int jtPageSize, string jtSorting, out int total)
        {
            return new dTipoCambio().listTipoCambio(cambio,jtStartIndex,jtPageSize,jtSorting,out total);
        }
    }
}
