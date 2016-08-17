using System.Collections.Generic;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.data;
using System.Text;
using System.Linq;

namespace VidaCamara.DIS.Negocio
{
    public class nReglaArchivo
    {
        /// <summary>
        /// Retorna el listado de regla por nombre paginado
        /// </summary>
        /// <param name="regla"></param>
        /// <param name="jtStartIndex"></param>
        /// <param name="jtPageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ReglaArchivo> getListReglaArchivo(ReglaArchivo regla, int jtStartIndex, int jtPageSize,string jtSorting, out int total)
        {
            return new dReglaArchivo().getListReglaArchivo(regla, jtStartIndex, jtPageSize,jtSorting, out total);
        }
        /// <summary>
        /// Devuelve la las colunas para una grilla en formato de json acuerdo a un tipo de regla
        /// </summary>
        /// <param name="regla"></param>
        /// <returns></returns>
        public StringBuilder getColumnGridByArchivo(ReglaArchivo regla,string gridFor,string columnsAdd = null)
        {
            var total = 0;
            var listRegla = new dReglaArchivo().getListReglaArchivo(regla, 0, 1000, "CaracterInicial ASC", out total);
            //if (regla.Archivo.Equals("0"))
            //{
            //    listRegla = listRegla.GroupBy(x => new { x.NombreCampo, x.TituloColumna,x.TipoCampo })
            //                       .Select(y => new ReglaArchivo() { NombreCampo = y.Key.NombreCampo, TituloColumna = y.Key.TituloColumna,TipoCampo = y.Key.TipoCampo }).ToList();
            //}
            var sb = new StringBuilder();
            sb.Append("var fields = {");
            if (!gridFor.ToUpper().Equals("CARGA"))
            {
                sb.Append("Estado:{ title: 'Estado'},");
                sb.Append("NombreArchivo: { title: 'NombreArchivo'},");
                sb.Append("FechaInsert:{title:'Fecha_Carga',display: function (data) {return ConvertNumberToDateTime(data.record.FechaInsert);}},");
                sb.Append("FechaAprobacion:{title:'Fecha_Aprobación',display: function (data) {return ConvertNumberToDateTime(data.record.FechaAprobacion);}},");
            }
            for (int i = 1; i <= listRegla.Count; i++)
            {
                var type = listRegla[i - 1].TipoCampo.Trim() == "DATETIME" ? ",type: 'date', displayFormat: 'dd/mm/yy'" : "";
                sb.Append(listRegla[i - 1].NombreCampo + ":{");
                sb.Append("title:" + "'" + listRegla[i - 1].TituloColumna +"'"+type+ "}" + (i == listRegla.Count ? "" : ","));
            }
            sb.Append(columnsAdd);
            sb.Append("}");
            return sb;
        }

        public void grabarReglaArchivo(ReglaArchivo regla)
        {
            new dReglaArchivo().grabarReglaArchivo(regla);
        }

        public void actualizarReglaArchivo(ReglaArchivo regla)
        {
            new dReglaArchivo().actualizarReglaArchivo(regla);
        }

        public int validarExisteReglaByContrato(CONTRATO_SYS contratoSisEF)
        {
            return new dReglaArchivo().validarExisteReglaByContrato(contratoSisEF);
        }

        public string copiarUltimaReglaArchivo(CONTRATO_SYS contratoSisEF)
        {
            return new dReglaArchivo().copiarUltimaReglaArchivo(contratoSisEF);
        }
    }
}
