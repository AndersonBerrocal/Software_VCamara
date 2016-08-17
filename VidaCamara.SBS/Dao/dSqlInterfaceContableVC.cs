using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlInterfaceContableVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] cabecera = {"PAQUETE","ASIENTO","FECHA","TIPO_ASIENTO","TIPO CONTABILIDAD","CLASE ASIENTO","FUENTE",
                                    "REFERENCIA","CONTRIBUYENTE","CENTRO_COSTO","CUENTA_CONTABLE","DEBITO","CREDITO"};
        static String[] cuenta = {"52","26","14","40","20","24"};

        public DataTable GetSelectIBNRContable(eInterfaceContableVC o) {
            DataTable dt = new DataTable();

            for (int c = 0; c < cabecera.Length; c++)
            {
                dt.Columns.Add(cabecera[c]);
            }
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectComprobante;

                for (int c = 0; c < (6 - 3); c++)
                {
                    sqlcmd.Parameters.Clear();
                   /* sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._Ide_Contrato;
                    sqlcmd.Parameters.Add("@TIPO_COMPROBANTE", SqlDbType.Char).Value = o._Tip_Comprobante;
                    sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                    sqlcmd.Parameters.Add("@NRO_COMPROBANTE", SqlDbType.Int).Value = o._Nro_Comprobante;*/

                    SqlDataReader dr = sqlcmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Object[] obj = new Object[cabecera.Length];
                        obj[0] = dr.GetInt32(1);
                        obj[1] = dr.GetString(2);
                        obj[2] = dr.GetString(3);
                        obj[3] = dr.GetDateTime(4);
                        obj[4] = dr.GetInt32(5);
                        obj[5] = dr.GetInt32(6);
                        obj[6] = dr.GetString(7);
                        obj[7] = dr.GetString(8);
                        obj[8] = dr.GetString(9);
                        obj[9] = dr.GetString(10);
                        obj[10] = dr.GetString(11);
                        obj[11] = dr.GetDecimal(12);
                        obj[12] = dr.GetDecimal(13);

                        dt.Rows.Add(obj);
                    }
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