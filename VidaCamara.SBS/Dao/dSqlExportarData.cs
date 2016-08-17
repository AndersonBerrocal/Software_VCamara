using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VidaCamara.SBS.Dao
{
    public class dSqlExportarData
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public DataTable GetSelecionarAnexo(String contrato, String formato_moneda, DateTime fecha_inicio, DateTime fecha_hasta)
        {
            DataTable dtResponse = new DataTable();
            String[] column = { "COD_REASEGURADOR", "DES_REASEGURADOR", "PRI_XPAG_REA_CED", "PRI_XCOD_REA_ACE", "SIN_XCOB_REA_CED", "SIN_XPAG_REA_ced", "OTRA_CUENTAS_XCOB", "OTRA_CUENTAS_XPAG", "DESC_COMISIONES", "SALDO_DEUDOR", "SALDO_ACREEDOR", "SALDO_DEUDOR_COM", "SALDO_ACREEDOR_COM", "TIENE_DETALLE" , "IS_TOTALS" };
            for (int i = 0; i < column.Length; i++)
            {
                dtResponse.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectAnexo;

                sqlcmd.Parameters.Clear();
                //sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;
                sqlcmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = fecha_inicio;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = fecha_hasta;
                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow row = dtResponse.NewRow();

                    row["COD_REASEGURADOR"] = dr["COD_REASEGURADOR"].ToString();
                    row["DES_REASEGURADOR"] = dr["DES_REASEGURADOR"].ToString();
                    row["PRI_XPAG_REA_CED"] = String.Format(formato_moneda, Convert.ToDecimal(dr["PRI_XPAG_REA_CED"]));
                    row["PRI_XCOD_REA_ACE"] = String.Format(formato_moneda, Convert.ToDecimal(dr["PRI_XCOD_REA_ACE"]));
                    row["SIN_XCOB_REA_CED"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SIN_XCOB_REA_CED"]));
                    row["SIN_XPAG_REA_ced"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SIN_XPAG_REA_ced"]));
                    row["OTRA_CUENTAS_XCOB"] = String.Format(formato_moneda, Convert.ToDecimal(dr["OTRA_CUENTAS_XCOB"]));
                    row["OTRA_CUENTAS_XPAG"] = String.Format(formato_moneda, Convert.ToDecimal(dr["OTRA_CUENTAS_XPAG"]));
                    row["DESC_COMISIONES"] = String.Format(formato_moneda, Convert.ToDecimal(dr["DESC_COMISIONES"]));
                    row["SALDO_DEUDOR"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_DEUDOR"]));
                    row["SALDO_ACREEDOR"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_ACREEDOR"]));
                    row["SALDO_DEUDOR_COM"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_DEUDOR_COM"]));
                    row["SALDO_ACREEDOR_COM"] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_ACREEDOR_COM"]));
                    row["TIENE_DETALLE"] = dr["TIENE_DETALLE"].ToString();
                    row["IS_TOTALS"] = dr["IS_TOTALS"].ToString();
                    dtResponse.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dtResponse;
        }
        public DataTable GetSelecionarEs18A(String contrato, DateTime fecha_inicio, DateTime fecha_hasta,String formato_moneda) {
 
            DataTable dt = new DataTable();
            String[] column = {"NRO","CODIGO","NOMBRE","PAIS","CALIFICADORA","CLASIFICACION","N_REGISTRO","NOMBRE_1","TIP_CONTRATO","RAMO","INI_VIG","P_CEDITAS_B","P_CEDITAS_N","OBSERVACION"};
            for(int i= 0;i<column.Length;i++){
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18A;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;
                sqlcmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = fecha_inicio;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = fecha_hasta;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[14];

                    obj[0] = dr.GetString(0);
                    obj[1] = dr.GetString(1);
                    obj[2] = dr.GetString(2);
                    obj[3] = dr.GetString(3);
                    obj[4] = dr.GetString(4);
                    obj[5] = dr.GetString(5);
                    obj[6] = dr.GetInt32(6);
                    obj[7] = dr.GetString(7);
                    obj[8] = dr.GetString(8);
                    obj[9] = dr.GetString(9);
                    obj[10] = dr.GetDateTime(10).ToShortDateString();
                    obj[11] = String.Format(formato_moneda, dr.IsDBNull(11)? 0.00m:dr.GetDecimal(11));
                    obj[12] = String.Format(formato_moneda, dr.IsDBNull(12)? 0.00m : dr.GetDecimal(12));
                    obj[13] = dr.GetString(13);
                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarEs18B(String contrato) {

            DataTable dt = new DataTable();
            String[] column = {"TIPO","RAMO","REASEGURADOR", "MODALIDAD","CODIGO","PORCENTAJE"};
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18B;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[6];

                    obj[0] = dr.GetString(0);
                    obj[1] = dr.GetString(1);
                    obj[2] = dr.GetString(2);
                    obj[3] = dr.GetString(3);
                    obj[4] = dr.GetString(4);
                    obj[5] = dr.GetDecimal(5);

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarEs18C(String contrato, String formato_moneda){
            DataTable dt = new DataTable();
            String[] column = { "","","RAMO", "INICIO", "FIN", "CIENTO" ,"MTO_MAX_SEC","MTO_PLE_A","NRO_L_MUL","MTO_MAX_S2","CIENTO_RET","CIENTO_SEC","MTO_MX_COB"};
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18C;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[13];

                    obj[0] = dr.GetString(0);
                    obj[1] = dr.GetDateTime(1).ToShortDateString();
                    obj[2] = dr.GetDateTime(2).ToShortDateString();
                    obj[3] = dr.GetDecimal(3);
                    obj[4] = String.Format(formato_moneda,dr.GetDecimal(4));
                    obj[5] = dr.GetDecimal(5);
                    obj[6] = String.Format(formato_moneda,dr.GetDecimal(6));
                    obj[7] = String.Format(formato_moneda,dr.GetDecimal(7));
                    obj[8] = dr.GetInt32(8);
                    obj[9] = String.Format(formato_moneda,dr.GetDecimal(9));
                    obj[10] = String.Format(formato_moneda,dr.GetDecimal(10));
                    obj[11] = String.Format(formato_moneda, dr.GetDecimal(11));
                    obj[12] = String.Format(formato_moneda, dr.GetDecimal(12));

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarEs18D(String contrato, String formato_moneda)
        {
            DataTable dt = new DataTable();
            String[] column = { "NRO", "TIP_CONT", "RAMO", "NOMBRE", "CIENTO", "SBS", "INI_VIG", "FIN_VIG", "CAPA_XL", "PRIORIDAD", "SEC_EX_PRIO", "MTO_MAX_CAP_LIM_SUP", "PRIMA MIN" };
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18D;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[13];

                    obj[0] = dr.GetString(0);
                    obj[1] = dr.GetString(1);
                    obj[2] = dr.GetString(2);
                    obj[3] = dr.GetString(3);
                    obj[4] = String.Format(formato_moneda,dr.GetDecimal(4));
                    obj[5] = dr.GetString(5);
                    obj[6] = dr.GetDateTime(6).ToShortDateString();
                    obj[7] = dr.GetDateTime(7).ToShortDateString();
                    obj[8] = dr.GetInt32(8);
                    obj[9] = String.Format(formato_moneda,dr.GetDecimal(9));
                    obj[10] = String.Format(formato_moneda,dr.GetDecimal(10));
                    obj[11] = String.Format(formato_moneda,dr.GetDecimal(11));
                    obj[12] = String.Format(formato_moneda,dr.GetDecimal(12));

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;

        }
        public DataTable GetSelecionarEs18E(String contrato,DateTime fecha_inicio,DateTime fecha_hasta,String formato_menada)
        {
            DataTable dt = new DataTable();
            String[] column = { "NRO", "CODIGO", "NOMBRE", "RAMO", "ASEGURADO", "INI_VIG", "MERCADO_L", "MERCADO _EXT", "TOTAL_0", "PEN_LIQ", "LIQUIDAC", "TOTAL_1" };
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18E;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;
                sqlcmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = fecha_inicio;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = fecha_hasta;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[12];

                    obj[0] = dr[0];
                    obj[1] = dr[1];
                    obj[2] = dr[2];
                    obj[3] = dr[3];
                    obj[4] = dr[4];
                    obj[5] = Convert.ToDateTime(dr[5]).ToShortDateString();
                    obj[6] = String.Format(formato_menada,dr[6]);
                    obj[7] = String.Format(formato_menada,dr[7]);
                    obj[8] = String.Format(formato_menada,dr[8]);
                    obj[9] = String.Format(formato_menada,dr[9]);
                    obj[10] = String.Format(formato_menada,dr[10]);
                    obj[11] = String.Format(formato_menada, dr[11]);

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarEs18F(String contrato,DateTime fecha_inicio,DateTime fecha_hasta,String formato_moneda)
        {
            DataTable dt = new DataTable();
            String[] column = {"REASEGURADOR","PRIMA_CED", "SENIESTRO", "COMISION", "PRIMA_REASG", "SIN_REA_CED", "COM_REA_CED", "SALDO" };
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.ePselectEs18F;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;
                sqlcmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = fecha_inicio;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = fecha_hasta;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[column.Length];

                    obj[0] = dr[0];
                    obj[1] = String.Format(formato_moneda, dr.GetDecimal(1));
                    obj[2] = String.Format(formato_moneda,dr.GetDecimal(2));
                    obj[3] = String.Format(formato_moneda,dr.GetDecimal(3));
                    obj[4] = String.Format(formato_moneda,dr.GetDecimal(4));
                    obj[5] = String.Format(formato_moneda,dr.GetDecimal(5));
                    obj[6] = String.Format(formato_moneda,dr.GetDecimal(6));
                    obj[7] = String.Format(formato_moneda, dr.GetDecimal(7));

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarModelo(String contrato, DateTime fecha_inicio, DateTime fecha_hasta, String formato_moneda, Int32 token, string reasegurador)
        {
            DataTable dt = new DataTable();
            //String[] column = { "FECHA","REASEGURADOR","TIPO","NRO","ASEGURADO","CONTRATO","RAMO","MONEDA","PRI_XPAG_REA_CED", "PRI_XCOB_REA_ACE", "SIN_XCOB_REA_CED", "SIN_XPAG_REA_ACE", "OTR_CTA_XCOB_REA_CED", "OTR_CTA_XPAG_REA_ACE", "DSCTO_COMIS_REA", "SALDO_DEUDOR", "SALDO_ACREEDOR","COMP_1","COMP_2" };
            for (int i = 0; i < 21; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                if(token == 1)
                    sqlcmd.CommandText = _db.ePselectModelo1;
                else
                    sqlcmd.CommandText = _db.ePselectModelo2;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = contrato;
                sqlcmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = fecha_inicio;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = fecha_hasta;
                sqlcmd.Parameters.Add("@COD_REASEGURADOR",SqlDbType.VarChar).Value = reasegurador;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    Object[] obj = new Object[dt.Columns.Count];
                    obj[0] = Convert.ToDateTime(dr["FEC_OPERACION"]).ToShortDateString();
                    obj[1] = Convert.ToInt32(dr["LEVEL_TRIMESTRE"].ToString());
                    obj[2] = "CT";
                    obj[3] = dr["NRO_OPERACION"];
                    obj[4] = dr["REASEGURADOR"];
                    obj[5] = dr["ASEGURADOR"];
                    obj[6] = dr["IDE_CONTRATO"];
                    obj[7] = dr["TIPO_COMPROBANTE"];
                    obj[8] = dr["RAMO"];
                    obj[9] = dr["MONEDA"];
                    obj[10] = String.Format(formato_moneda, Convert.ToDecimal(dr["PRI_XPAG_REA_CED"].ToString()));
                    obj[11] = String.Format(formato_moneda, Convert.ToDecimal(dr["PRI_XCOB_REA_ACE"].ToString()));
                    obj[12] = String.Format(formato_moneda, Convert.ToDecimal(dr["SIN_XCOB_REA_CED"].ToString()));
                    obj[13] = String.Format(formato_moneda, Convert.ToDecimal(dr["SIN_XPAG_REA_ACE"].ToString()));
                    obj[14] = String.Format(formato_moneda, Convert.ToDecimal(dr["OTR_CTA_XCOB_REA_CED"].ToString()));
                    obj[15] = String.Format(formato_moneda, Convert.ToDecimal(dr["OTR_CTA_XPAG_REA_ACE"].ToString()));
                    obj[16] = String.Format(formato_moneda, Convert.ToDecimal(dr["DSCTO_COMIS_REA"].ToString()));
                    obj[17] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_DEUDOR"].ToString()));
                    obj[18] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_ACREEDOR"].ToString()));
                    obj[19] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_DEUDOR_COMP"].ToString()));
                    obj[20] = String.Format(formato_moneda, Convert.ToDecimal(dr["SALDO_ACREEDOR_COMP"].ToString()));

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarAnexoEs2A(String contrato)
        {
            DataTable dt = new DataTable();
            String[] column = { "COD_SBS", "REASEGURADOR", "PRIMAS_X_PAG", "PRIMAS_X_COB", "SIN_X_COB", "SIN_X_PAGAR", "CUENTAS_X_PAG", "DES_COM_REASG", "SALDO_DEU", "SALDO_ACREED" };
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            /*DataSet ds = new DataSet();
            ds.Tables.Add(dt);*/
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectDatoM;

                sqlcmd.Parameters.Clear();
                /*sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = tipo;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = contrato;
                sqlcmd.Parameters.Add("@PAGE_INDEX", SqlDbType.Char).Value = index;
                sqlcmd.Parameters.Add("@PAGE_SIZE", SqlDbType.Char).Value = size;
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;*/

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[11];

                    obj[0] = dr.GetDateTime(0);
                    obj[1] = dr.GetDateTime(1);
                    obj[2] = dr.GetInt32(2);
                    obj[3] = dr.GetInt32(3);
                    obj[4] = dr.GetDateTime(0);
                    obj[5] = dr.GetDateTime(1);
                    obj[6] = dr.GetInt32(2);
                    obj[7] = dr.GetInt32(3);
                    obj[8] = dr.GetDateTime(0);
                    obj[9] = dr.GetDateTime(1);
                    obj[10] = dr.GetInt32(2);

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
        public DataTable GetSelecionarAnexoEs2B(String contrato)
        {
            DataTable dt = new DataTable();
            String[] column = { "COD_SBS", "NOMBRE_EMP/DET_CONT", "CUENTAS_X_COB/REASG_COASEG", "6_+_MES_ANTIG", "12_+_MES_ANTIG", "TOTAL" };
            for (int i = 0; i < column.Length; i++)
            {
                dt.Columns.Add(column[i]);
            }
            /*DataSet ds = new DataSet();
            ds.Tables.Add(dt);*/
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectDatoM;

                sqlcmd.Parameters.Clear();
                /*sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = tipo;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = contrato;
                sqlcmd.Parameters.Add("@PAGE_INDEX", SqlDbType.Char).Value = index;
                sqlcmd.Parameters.Add("@PAGE_SIZE", SqlDbType.Char).Value = size;
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;*/

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[6];

                    obj[0] = dr.GetDateTime(0);
                    obj[1] = dr.GetDateTime(1);
                    obj[2] = dr.GetInt32(2);
                    obj[3] = dr.GetInt32(3);
                    obj[4] = dr.GetDateTime(0);
                    obj[5] = dr.GetDateTime(1);

                    dt.Rows.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }
    }
}