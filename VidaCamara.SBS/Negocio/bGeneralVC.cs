using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

//dos
namespace VidaCamara.SBS.Negocio
{
    public class bGeneralVC : System.Web.UI.Page
    {
        public Int32 SetInsertarGeneral(eGeneral o)
        {
            dSqlGeneralVC dg = new dSqlGeneralVC();
            return dg.SetInsertarGeneral(o);
        }
        public Int32 SetActualizarGeneral(eGeneral o) {
            dSqlGeneralVC dg = new dSqlGeneralVC();
            return dg.SetActualizarGeneral(o);
        }
        public List<eGeneral> GetSelecionarGeneral() {
            dSqlGeneralVC dg = new dSqlGeneralVC();
            return dg.GetSelecionarGeneral();
        }
        public void ParametroListSession()
        {
            List<eGeneral> list = this.GetSelecionarGeneral();
            if (list.Count > 0)
            {
                Session["idempresa"] = list[0]._idEmpresa.ToString();
                Session["aniovigente"] = list[0]._anoVigente;
                Session["mesvigente"] = list[0]._mesVigente;
                Session["rutaexcel"] = list[0]._Ruta_Archivo;
                Session["formatomoneda"] = "{0:n" + list[0]._Nro_Decimal + "}";
            }
        }
    }
}
