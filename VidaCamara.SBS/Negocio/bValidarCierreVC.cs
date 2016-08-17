using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bValidarCierreVC
    {
        public List<eValidarCierreVC> GetSelecionarValidarCierre(eValidarCierreVC o) { 
            dSqlValidarCierreVC dv = new dSqlValidarCierreVC();
            return dv.GetSelectValidarCierre(o);
        }
    }
}