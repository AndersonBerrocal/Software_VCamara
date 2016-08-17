using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlOperacionSelectVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] meses = {"","Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public List<eOperacionVC> GetSelectOperacionDetalle(eOperacionVC o, int start, int size, String orderBy, out int total)
        {
            List<eOperacionVC> list = new List<eOperacionVC>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectOperacionDetalle;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@ANIO_OPERACION", SqlDbType.Int).Value = o._Ano_Operacion;
                sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.Int).Value = o._Mes_Operacion;
                sqlcmd.Parameters.Add("@INDEX", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@SIZE", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@ORDER", SqlDbType.VarChar).Value = orderBy;
                sqlcmd.Parameters.Add("@TOTAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ec = new eOperacionVC();
                    ec._Nro_Operacion = dr.GetInt32(1);
                    ec._Tip_Operacion = dr.GetString(2);
                    ec._Cod_Reasegurador = dr.GetString(3);
                    ec._Fec_Operacion = dr.GetDateTime(4);
                    ec._Ano_Operacion = dr.GetInt32(5);
                    ec._Mes_Operacion = meses[dr.GetInt32(6)];
                    ec._Cod_Asegurado = dr.GetString(7);
                    ec._Ide_Contrato = dr.GetString(8);
                    ec._Cod_Ramo = dr.GetString(9);
                    ec._Cod_Moneda = dr.GetString(10);
                    ec._Entorno_Prima_T_2 = String.Format(o._Formato_Moneda, dr.GetDecimal(11));
                    ec._Entorno_Prima_T_1 = String.Format(o._Formato_Moneda, dr.GetDecimal(12));
                    ec._Prima_Abonada = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                    ec._Prima_Reg_Contab = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                    ec._Pri_Ced_Mes = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                    ec._Des_Reg_Mes = dr.GetString(16);
                    ec._Imp_Impuesto_Mes = String.Format(o._Formato_Moneda, dr.GetDecimal(17));
                    ec._Pri_Xpag_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                    ec._Pri_Xcob_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                    ec._Sin_Directo = String.Format(o._Formato_Moneda, dr.GetDecimal(20));
                    ec._Sin_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                    ec._Sin_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                    ec._Otr_Cta_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(23));
                    ec._Otr_Cta_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(24));
                    ec._Dscto_Comis_Rea = String.Format(o._Formato_Moneda, dr.GetDecimal(25));
                    ec._Saldo_Deudor = String.Format(o._Formato_Moneda, dr.GetDecimal(26));
                    ec._Saldo_Acreedor = String.Format(o._Formato_Moneda, dr.GetDecimal(27));
                    ec._Estado = dr.GetString(28);
                    ec._Fec_Reg = dr.GetDateTime(29);
                    ec._Usu_Reg = dr.GetString(30);

                    list.Add(ec);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTAL"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBtotRow;
            //monedaS = moneda;
            return list;
        }

        public Int32 SetEliminarOperacion(int nro_opercion) {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery = "DELETE FROM OPERACION WHERE NRO_OPERACION = " + nro_opercion +" AND ESTADO <> 'C'";
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = DeleteQuery;


                _bool = sqlcmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return _bool;
        }
        public void GetSelectTotalOperacion(eOperacionVC o,String[] dataparam, out int registro_procesado, out int registro_creado, out String total_suma) {
            Int32 conteo_total = 0;
            Int32 conteo_procesado = 0;
            Decimal suma_total = 0.00m;
            try{
                Int32 mes_vigente = 0;
                String tipo_info = "";
                switch (o._Tip_Operacion.ToUpper()){
                    case "RR":
                         tipo_info = "01"; //cuando es rsp
                        break;
                    case "RI":
                        tipo_info = "08";//cuando es ibnr
                        break;
                    case "RM":
                        tipo_info = "07"; //cuando es primas
                        break;
                    default:
                        tipo_info = "PG"; //son pagos
                        break;
                }
                if (Convert.ToInt32(o._Mes_Operacion) < 10) 
                    mes_vigente = Convert.ToInt32(o._Ano_Operacion+"0"+Convert.ToInt32(o._Mes_Operacion));
                else
                    mes_vigente = Convert.ToInt32(o._Ano_Operacion + "" + Convert.ToInt32(o._Mes_Operacion));

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectTotalOperacion;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO ", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_TABLA", SqlDbType.Int).Value = dataparam[0];
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = tipo_info;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@TIPO_COMPROBANTE", SqlDbType.Char).Value = dataparam[1];
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = mes_vigente;
             
                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    conteo_procesado = dr.GetInt32(0);
                    conteo_total = dr.GetInt32(1);
                    suma_total = dr.GetDecimal(2);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            registro_procesado = conteo_procesado;
            registro_creado = conteo_total;
            total_suma = String.Format(o._Formato_Moneda,suma_total);
        }
        public DataTable GetSelectOperacionExport(eOperacionVC o, int start, int size, String orderBy, out int total)
        {
            String[] cabeceraPrima = {"N° Operación","Tipo Operación","Reasegurador","Fecha de Operacón","Año","Mes","Asegurado","N° Contrato","Ramo","Descripción","Moneda","Extorno de Prima t-2",
                                       "Extorno de Prima t-1","Prima Abonada","Prima Registrada Contablemente","Prima Cedida","Impuesto","Prima por Pagar","Estado","Fecha Registro","usuario Registro"};
            String[] cabeceraIbnr = {"N° Operación","Tipo Operación","Reasegurador","Fecha de Operacón","Año","Mes","Asegurado","N° Contrato","Ramo","Descripción","Moneda","Siniestros Directos",
                                    "Siniestros Cedidos","Comisiones / Descuentos","Siniestros por Cobrar","Estado","Fecha Registro","usuario Registro"};
            String[] cabeceraPago = { "N° Operación", "Tipo Operación", "Reasegurador", "Fecha de Operacón", "Año", "Mes", "Asegurado", "N° Contrato", "Ramo","Moneda", "Descripción", "Primas por \n pagar Reaseguros \n Cedidos",
                                    "Primas por Cobrar \n Reaseguros \n Aceptados","Siniestros por \n Cobrar Reaseguros \n Cedidos","Siniestros por \n Pagar Reaseguros \n Aceptados","Otras Cuentas por \n Cobrar Reaseguros \n Cedidos",
                                    "Otras Cuentas por \n  Pagar Reaseguros \n Aceptados","Descuento y \n Comisiones de \nReaseguros","Saldo Deudor","Saldo Acreedor","Saldo Deudor \n (COMPENSADOS)","Saldo Acreedor \n (COMPENSADOS)","Estado","Fecha Registro","usuario Registro"};

            DataTable dt = new DataTable();
            if (o._Tip_Operacion.ToUpper().Equals("RM"))
            {
                for (int c = 0; c < cabeceraPrima.Length; c++)
                {
                    dt.Columns.Add(cabeceraPrima[c]);
                }
            }
            else if (o._Tip_Operacion.ToUpper().Equals("RI") || o._Tip_Operacion.ToUpper().Equals("RR"))
            {
                for (int c = 0; c < cabeceraIbnr.Length; c++)
                {
                    dt.Columns.Add(cabeceraIbnr[c]);
                }
            }
            else {
                for (int c = 0; c < cabeceraPago.Length; c++)
                {
                    dt.Columns.Add(cabeceraPago[c]);
                }
            }

            int DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectOperacion;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                sqlcmd.Parameters.Add("@NRO_OPERACION", SqlDbType.Int).Value = o._Nro_Operacion;
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
                    if (o._Tip_Operacion.ToUpper().Equals("RM")){
                        Object[] obj = new Object[cabeceraPrima.Length];
                        obj[0] = dr.GetInt32(1);
                        obj[1] = dr.GetString(2);
                        obj[2] = dr.GetString(3);
                        obj[3] = dr.GetDateTime(4).ToShortDateString();
                        obj[4] = dr.GetInt32(5);
                        obj[5] = meses[dr.GetInt32(6)];
                        obj[6] = dr.GetString(7);
                        obj[7] = dr.GetString(8);
                        obj[8] = dr.GetString(9);
                        obj[9] = dr.GetString(16);
                        obj[10] = dr.GetString(10);
                        obj[11] = String.Format(o._Formato_Moneda, dr.GetDecimal(11));
                        obj[12] = String.Format(o._Formato_Moneda, dr.GetDecimal(12));
                        obj[13] = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                        obj[14] = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                        obj[15] = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                        obj[16] = String.Format(o._Formato_Moneda, dr.GetDecimal(17));
                        obj[17] = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                        obj[18] = dr.GetString(30);
                        obj[19] = dr.GetDateTime(31);
                        obj[20] = dr.GetString(32);

                        dt.Rows.Add(obj);
                    }
                    else if (o._Tip_Operacion.ToUpper().Equals("RI") || o._Tip_Operacion.ToUpper().Equals("RR"))
                    {
                        Object[] obj = new Object[cabeceraIbnr.Length];
                        obj[0] = dr.GetInt32(1);
                        obj[1] = dr.GetString(2);
                        obj[2] = dr.GetString(3);
                        obj[3] = dr.GetDateTime(4).ToShortDateString();
                        obj[4] = dr.GetInt32(5);
                        obj[5] = meses[dr.GetInt32(6)];
                        obj[6] = dr.GetString(7);
                        obj[7] = dr.GetString(8);
                        obj[8] = dr.GetString(9);
                        obj[9] = dr.GetString(16);
                        obj[10] = dr.GetString(10);
                        obj[11] = String.Format(o._Formato_Moneda, dr.GetDecimal(20));
                        obj[12] = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                        obj[13] = String.Format(o._Formato_Moneda, dr.GetDecimal(25));
                        obj[14] = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                        obj[15] = dr.GetString(30);
                        obj[16] = dr.GetDateTime(31);
                        obj[17] = dr.GetString(32);

                        dt.Rows.Add(obj);
                    }
                    else
                    {
                        Object[] obj = new Object[cabeceraPago.Length];
                        obj[0] = dr.GetInt32(1);
                        obj[1] = dr.GetString(2);
                        obj[2] = dr.GetString(3);
                        obj[3] = dr.GetDateTime(4).ToShortDateString();
                        obj[4] = dr.GetInt32(5);
                        obj[5] = meses[dr.GetInt32(6)];
                        obj[6] = dr.GetString(7);
                        obj[7] = dr.GetString(8);
                        obj[8] = dr.GetString(9);
                        obj[9] = dr.GetString(10);
                        obj[10] = dr.GetString(16);
                        obj[11] = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                        obj[12] = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                        obj[13] = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                        obj[14] = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                        obj[15] = String.Format(o._Formato_Moneda, dr.GetDecimal(23));
                        obj[16] = String.Format(o._Formato_Moneda, dr.GetDecimal(24));
                        obj[17] = String.Format(o._Formato_Moneda, dr.GetDecimal(25));
                        obj[18] = String.Format(o._Formato_Moneda, dr.GetDecimal(26));
                        obj[19] = String.Format(o._Formato_Moneda, dr.GetDecimal(27));
                        obj[22] = String.Format(o._Formato_Moneda, dr.GetDecimal(28));
                        obj[21] = String.Format(o._Formato_Moneda, dr.GetDecimal(29));
                        obj[22] = dr.GetString(30);
                        obj[23] = dr.GetDateTime(31);
                        obj[24] = dr.GetString(32);

                        dt.Rows.Add(obj);
                    }
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