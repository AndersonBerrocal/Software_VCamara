using System;
using System.Collections.Generic;
using System.Data;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bComprobanteVC
    {
        String moneda;
        int total;
        public List<eComprobanteVC> GetSelectComprobante(eComprobanteVC o, int start, int size, String orderBy, out int total,String excel) {
            dSqlComprobanteVC dc = new dSqlComprobanteVC();
            return dc.GetSelectComprobante(o, start, size, orderBy, out total, out moneda,excel);
        }
        public DataTable GetSelectComprobanteExport(eComprobanteVC o, int start, int size, String orderBy, out int total) {
            dSqlComprobanteVC dc = new dSqlComprobanteVC();
            return dc.GetSelectComprobanteExport(o,start,size,orderBy,out total);
        }
        public List<eComprobanteVC> GetSelectTotalComprobante(eComprobanteVC o, String orderBy){
            dSqlComprobanteVC dc = new dSqlComprobanteVC();
            var dataList = dc.GetSelectComprobante(o, 0, 1000, orderBy, out total, out moneda,"FORM");

            List<eComprobanteVC> list = new List<eComprobanteVC>();
            eComprobanteVC ec = new eComprobanteVC();
            Decimal _Otr_Cta_Xcob_Rea_Ced = 0.00m;
            Decimal _Pri_Xpag_Rea_Ced = 0.00m;
            Decimal _Pri_Xcob_Rea_Ace = 0.00m;
            Decimal _Sin_Xcob_Rea_Ced = 0.00m;
            Decimal _Sin_Xpag_Rea_Ace = 0.00m;
            Decimal _Otr_Cta_Xpag_Rea_Ace = 0.00m;
            Decimal _Dscto_Comis_Rea = 0.00m;
            Decimal _Saldo_Deudor = 0.00m;
            Decimal _Saldo_Acreedor = 0.00m;

            for (int i = 0; i < dataList.Count; i++) {
               _Otr_Cta_Xcob_Rea_Ced += Convert.ToDecimal(dataList[i]._Otr_Cta_Xcob_Rea_Ced);
               _Pri_Xpag_Rea_Ced += Convert.ToDecimal(dataList[i]._Pri_Xpag_Rea_Ced);
               _Pri_Xcob_Rea_Ace += Convert.ToDecimal(dataList[i]._Pri_Xcob_Rea_Ace);
               _Sin_Xcob_Rea_Ced += Convert.ToDecimal(dataList[i]._Sin_Xcob_Rea_Ced);
               _Sin_Xpag_Rea_Ace += Convert.ToDecimal(dataList[i]._Sin_Xpag_Rea_Ace);
               _Otr_Cta_Xpag_Rea_Ace += Convert.ToDecimal(dataList[i]._Otr_Cta_Xpag_Rea_Ace);
               _Dscto_Comis_Rea += Convert.ToDecimal(dataList[i]._Dscto_Comis_Rea);
               _Saldo_Deudor += Convert.ToDecimal(dataList[i]._Saldo_Deudor);
               _Saldo_Acreedor += Convert.ToDecimal(dataList[i]._Saldo_Acreedor);
            }

            ec._Otr_Cta_Xcob_Rea_Ced = String.Format(moneda, _Otr_Cta_Xcob_Rea_Ced);
            ec._Pri_Xpag_Rea_Ced = String.Format(moneda,_Pri_Xpag_Rea_Ced);
            ec._Pri_Xcob_Rea_Ace = String.Format(moneda,_Pri_Xcob_Rea_Ace);
            ec._Sin_Xcob_Rea_Ced = String.Format(moneda,_Sin_Xcob_Rea_Ced);
            ec._Sin_Xpag_Rea_Ace = String.Format(moneda,_Sin_Xpag_Rea_Ace);
            ec._Otr_Cta_Xpag_Rea_Ace = String.Format(moneda,_Otr_Cta_Xpag_Rea_Ace);
            ec._Dscto_Comis_Rea = String.Format(moneda,_Dscto_Comis_Rea);
            ec._Saldo_Deudor = String.Format(moneda,_Saldo_Deudor);
            ec._Saldo_Acreedor = String.Format(moneda,_Saldo_Acreedor);

            list.Add(ec);
          return list;
        }
    }
}