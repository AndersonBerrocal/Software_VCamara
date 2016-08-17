using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bOperacionVC
    {
        static String moneda;
        public Int32 SetProcesaPago(eOperacionVC o) {
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.SetProcesaPago(o);
        }
        public Int32 SetProcesaIbnr(eOperacionVC o) {
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.SetProcesaIBNR(o);
        }
        public Int32 SetProcesaPrima(eOperacionVC o) {
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.SetProcesaPrima(o);
        }
        public Int32 SetProcesoRsp(eOperacionVC o) {
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.SetProcesaRsp(o);
        }
        public List<eOperacionVC>GetSelectOperacion(eOperacionVC o,int start,int size,String orderby,out int total){
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.GetSelectOperacion(o,start,size,orderby,out total,out moneda);
        }
        public Int32 SetGuardarOperacionManual(eOperacionVC o) {
            dSqlOperacionVC ope = new dSqlOperacionVC();
            return ope.SetGuardarOperacionManual(o);
        }
    }
}