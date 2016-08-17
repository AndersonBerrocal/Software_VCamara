using System;
using System.Collections.Generic;
using System.Data;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bOperacionSelectVC
    {
        public List<eOperacionVC> GetSelectOperacionDetalle(eOperacionVC o,int start, int size, String orderBy, out int total){
            dSqlOperacionSelectVC ope = new dSqlOperacionSelectVC();
            return ope.GetSelectOperacionDetalle(o,start,size,orderBy,out total);
        }
        public Int32 SetEliminarOperacion(int nro_operacion) {
            dSqlOperacionSelectVC ope = new dSqlOperacionSelectVC();
            return ope.SetEliminarOperacion(nro_operacion);
        }
        public void GetSelectTotalOperacion(eOperacionVC o,String[] dataparam, out int registro_procesado, out int registro_creado, out String total_suma) {
            dSqlOperacionSelectVC ope = new dSqlOperacionSelectVC();
            ope.GetSelectTotalOperacion(o,dataparam,out registro_procesado,out registro_creado,out total_suma);
        }
        public DataTable GetSelectOperacionExport(eOperacionVC o, int start, int size, String orderBy, out int total) {
            dSqlOperacionSelectVC ope = new dSqlOperacionSelectVC();
            return ope.GetSelectOperacionExport(o,start,size,orderBy ,out total);
        }
    }
}