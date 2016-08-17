using System;
using System.Data;
using VidaCamara.SBS.Dao;

namespace VidaCamara.SBS.Negocio
{
    public class bExportarData
    {
        public DataTable GetSelecionarAnexo(String contrato,String formato_moneda,DateTime fecha_inicio,DateTime fecha_hasta) {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarAnexo(contrato,formato_moneda,fecha_inicio,fecha_hasta);
        }
        public DataTable GetSelecionarEs18A(String contrato,DateTime fecha_inicio, DateTime fecha_hasta,String formato_moneda)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18A(contrato,fecha_inicio, fecha_hasta,formato_moneda);
        }
        public DataTable GetSelecionarEs18B(String contrato) {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18B(contrato);
        }
        public DataTable GetSelecionarEs18C(String contrato, String formato_moneda)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18C(contrato, formato_moneda);
        }
        public DataTable GetSelecionarEs18D(String contrato, String formato_moneda)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18D(contrato, formato_moneda);
        }
        public DataTable GetSelecionarEs18E(String contrato,DateTime fecha_inicio,DateTime fecha_hasta,String formato_moneda)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18E(contrato,fecha_inicio,fecha_hasta,formato_moneda);
        }
        public DataTable GetSelecionarEs18F(String contrato, DateTime fecha_inicio, DateTime fecha_hasta, String formato_moneda)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarEs18F(contrato,fecha_inicio,fecha_hasta,formato_moneda);
        }
        public DataTable GetSelecionarModelo(String contrato, DateTime fecha_inicio, DateTime fecha_hasta, String formato_moneda,Int32 token,string reasegurador)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarModelo(contrato, fecha_inicio, fecha_hasta, formato_moneda, token, reasegurador);

        }
        public DataTable GetSelecionarAnexoEs2A(String contrato)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarAnexoEs2A(contrato);
        }
        public DataTable GetSelecionarAnexoEs2B(String contrato)
        {
            dSqlExportarData ed = new dSqlExportarData();
            return ed.GetSelecionarAnexoEs2B(contrato);
        }
    }
}