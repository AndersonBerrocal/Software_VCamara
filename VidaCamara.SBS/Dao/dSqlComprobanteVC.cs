using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlComprobanteVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public List<eComprobanteVC> GetSelectComprobante(eComprobanteVC o, int start, int size, String orderBy, out int total,out String monedaS,String excel)
        {
            List<eComprobanteVC> list = new List<eComprobanteVC>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectComprobante;

                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_COMPROBANTE", SqlDbType.Char).Value = o._Tip_Comprobante;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                sqlcmd.Parameters.Add("@NRO_COMPROBANTE", SqlDbType.Int).Value = o._Nro_Comprobante;
                sqlcmd.Parameters.Add("@FECHA_INI", SqlDbType.Date).Value = o._fecha_ini;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = o._fecha_fin;
                sqlcmd.Parameters.Add("@TOKEN_DATE", SqlDbType.Char).Value = o._token_fecha;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = orderBy;
                sqlcmd.Parameters.Add("@START", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@FIN", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ec = new eComprobanteVC();
                    ec._Nro_Comprobante = dr.GetInt32(1);
                    ec._Tip_Comprobante = dr.GetString(2);
                    ec._Cod_Reasegurador = dr.GetString(3);
                    ec._Fec_Comprobante = dr.GetDateTime(4);
                    ec._Ano_Comprobante = dr.GetInt32(5);
                    ec._Mes_Comprobante = meses[dr.GetInt32(6)];
                    ec._Cod_Asegurado = dr.GetString(7);
                    ec._Ide_Contrato = dr.GetString(8);
                    ec._Cod_Ramo = dr.GetString(9);
                    ec._Cod_Moneda = dr.GetString(10);
                    ec._Des_Reg_Trimestre = dr.GetString(11);
                    ec._Pri_Xpag_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(12));

                    ec._Pri_Xcob_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                    ec._Sin_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                    ec._Sin_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                    ec._Otr_Cta_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(16));
                    ec._Otr_Cta_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(17));
                    ec._Dscto_Comis_Rea = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                    ec._Saldo_Deudor = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                    ec._Saldo_Acreedor = String.Format(o._Formato_Moneda, dr.GetDecimal(20));
                    ec._Saldo_Deudor_Comp = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                    ec._Saldo_Acreedor_Comp = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                    ec._Estado = dr.GetString(23);
                    ec._Fec_Reg = dr.GetDateTime(24);
                    ec._Usu_Reg = dr.GetString(25);
                    list.Add(ec);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTALROW"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBtotRow;
            monedaS = o._Formato_Moneda;
            return list;
        }
        public DataTable GetSelectComprobanteExport(eComprobanteVC o, int start, int size, String orderBy, out int total)
        {
            String[] cabecera = { "N° Comprobante", "Tipo \n Comprobante", "Reasegurador", "Fecha de \n Comprobante", "Año", "Mes", "Asegurado", "N° Contrato", "Ramo","Moneda", "Descripción", "Primas por pagar \n Reaseguros \n Cedidos",
                                    "Primas por Cobrar \n Reaseguros \n Aceptados","Siniestros por \n Cobrar \n Reaseguros \n Cedidos","Siniestros por \n Pagar Reaseguros \n Aceptados","Otras Cuentas por \n Cobrar \n Reaseguros \n Cedidos",
                                    "Otras Cuentas por \n Pagar Reaseguros \n Aceptados","Descuento y \n Comisiones de \n Reaseguros","Saldo Deudor","Saldo Acreedor","Saldo Deudor \n (COMPENSADOS)","Saldo Acreedor \n (COMPENSADOS)","Estado","Fecha Registro","Usuario \n Registro"};
            DataTable dt = new DataTable();
            for (int c = 0; c < cabecera.Length; c++)
            {
                dt.Columns.Add(cabecera[c]);
            }
            int DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectComprobante;

                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_COMPROBANTE", SqlDbType.Char).Value = o._Tip_Comprobante;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                sqlcmd.Parameters.Add("@NRO_COMPROBANTE", SqlDbType.Int).Value = o._Nro_Comprobante;
                sqlcmd.Parameters.Add("@FECHA_INI", SqlDbType.Date).Value = o._fecha_ini;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = o._fecha_fin;
                sqlcmd.Parameters.Add("@TOKEN_DATE", SqlDbType.Char).Value = o._token_fecha;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = orderBy;
                sqlcmd.Parameters.Add("@START", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@FIN", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    Object[] obj = new Object[cabecera.Length];
                    obj[0] = dr.GetInt32(1);
                    obj[1] = dr.GetString(2);
                    obj[2] = dr.GetString(3);
                    obj[3] = dr.GetDateTime(4);
                    obj[4] = dr.GetInt32(5);
                    obj[5] = meses[dr.GetInt32(6)];
                    obj[6] = dr.GetString(7);
                    obj[7] = dr.GetString(8);
                    obj[8] = dr.GetString(9);
                    obj[9] = dr.GetString(10);
                    obj[10] = dr.GetString(11);
                    obj[11] = String.Format(o._Formato_Moneda, dr.GetDecimal(12));
                    obj[12] = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                    obj[13] = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                    obj[14] = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                    obj[15] = String.Format(o._Formato_Moneda, dr.GetDecimal(16));
                    obj[16] = String.Format(o._Formato_Moneda, dr.GetDecimal(17));
                    obj[17] = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                    obj[18] = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                    obj[19] = String.Format(o._Formato_Moneda, dr.GetDecimal(20));
                    obj[20] = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                    obj[21] = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                    obj[22] = dr.GetString(23);
                    obj[23] = dr.GetDateTime(24);
                    obj[24] = dr.GetString(25);

                    dt.Rows.Add(obj);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTALROW"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBtotRow;
            return dt;
        }
    }
}