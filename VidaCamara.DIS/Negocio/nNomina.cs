using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.Negocio
{
    public class nNomina
    {
        public Int32 setGrabarNomina(NOMINA nomina) 
        {
            return new dNomina().setGrabarNomina(nomina);
        }

        public List<NOMINA> listNominaByArchivo(NOMINA nomina, object[] filters, int jtStartIndex, int jtPageSize, out int total)
        {
            filters[0] = Path.GetFileNameWithoutExtension(filters[0].ToString())+".CSV";
            return new dNomina().listNominaByArchivo(nomina,filters,jtStartIndex,jtPageSize,out total);
        }
        public List<HNOMINA> listNominaConsulta(NOMINA nomina, object[] filters, int jtStartIndex, int jtPageSize, out int total)
        {
            return new dNomina().listNominaConsulta(nomina,filters,jtStartIndex,jtPageSize,out total);
        }

        public void actualizarEstadoFallido(int idArchivo, int contratoId)
        {
            new dNomina().actualizarEstadoFallido(idArchivo, contratoId);
        }

        public NOMINA getNominaByArchivoId(NOMINA nomina)
        {
            return new dNomina().getNominaByArchivoId(nomina);
        }
    }
}
