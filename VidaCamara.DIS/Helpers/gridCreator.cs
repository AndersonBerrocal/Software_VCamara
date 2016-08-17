using System.Text;

namespace VidaCamara.DIS.Helpers
{
    public class gridCreator
    {
        /// <summary>
        /// Develvuelve la estructura de la grilla jtable
        /// </summary>
        /// <param name="id"></param>
        /// <param name="size"></param>
        /// <param name="action"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public StringBuilder getGrid(string id,string size,string action,string sorting)
        {
            var sb = new StringBuilder();
            sb.Append("$(document).ready(function () {");
            sb.Append("$('#"+id+"').jtable({");
            sb.Append("paging: true,");
            sb.Append("sorting: true,");
            sb.Append("pageSize: 13,");
            sb.Append("defaultSorting: '"+sorting+"',");
            sb.Append("actions: {listAction: '" + action + "'},");
            sb.Append("fields:fields");
            sb.Append("});");
            sb.Append("$('#"+id+".jtable-main-container').css({'width':'"+size+"px'});");
            sb.Append("$('#"+id+"').jtable('load');");
            sb.Append("});");
            return sb;
        }
    }
}
