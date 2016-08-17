using System;
using System.Linq;

namespace VidaCamara.SBS.Negocio
{
    public class bValidarAcceso : System.Web.UI.Page
    {
        public Boolean GetValidarAcceso(String QueryParam)
        {
            var listaPagina = Session["accesos"].ToString().Split(',');
            var encontro = false;
            foreach (var item in listaPagina)
            {
                if (item.Equals(QueryParam))
                {
                    encontro = true;
                    break;
                }
            }
            return encontro;
        }
    }
}