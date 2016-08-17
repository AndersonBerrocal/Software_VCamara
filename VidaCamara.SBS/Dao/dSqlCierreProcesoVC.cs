using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlCierreProcesoVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviebre", "Diciembre" };

        public Int32 SetInsertarCierreProceso(eCierreProceso o) {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                if(o._Tipo_Info.ToUpper() == "RP")
                    sqlcmd.CommandText = _db.sSinsertarCierrePago;
                else if(o._Tipo_Info.ToUpper() == "RI")
                    sqlcmd.CommandText = _db.sSinsertarCierreIbnr;
                else if(o._Tipo_Info.ToUpper() == "RR")
                    sqlcmd.CommandText = _db.sSinsertarCierreRsp;
                else
                    sqlcmd.CommandText = _db.sSinsertarCierrePrima;
                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tipo_Info;
                sqlcmd.Parameters.Add("@ANIO_OPERACION", SqlDbType.Int).Value = o._Anio_Cierre;
                sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.Int).Value = o._Mes_Cierre;
                sqlcmd.Parameters.Add("@TIPO_CIERRE", SqlDbType.Char).Value = o._Tipo_Cierre;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._Usu_Reg;

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
        public List<eCierreProceso> GetSelectCierreOperacion(eCierreProceso o,out int total,int start,int size,String orderBy) { 
            List<eCierreProceso> list = new List<eCierreProceso>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectCierreProceso;

                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._Tipo_Info;
                sqlcmd.Parameters.Add("@START ", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@SIZE ", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@ORDERBY ", SqlDbType.VarChar).Value = orderBy;
                sqlcmd.Parameters.Add("@TOTAL ", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ec = new eCierreProceso();
                    ec._Id_Cierre = dr.GetInt32(1);
                    ec._Nro_Contrato = dr.GetString(2);
                    ec._Tipo_Info = dr.GetString(3);
                    ec._Fec_Cie = dr.GetDateTime(4);
                    ec._Anio_Cierre = dr.GetInt32(5);
                    ec._Mes_Cierre = meses[dr.GetInt32(6)];
                    ec._Nro_Registro = dr.GetInt32(7);
                    ec._Tipo_Cierre = dr.GetString(8);
                    ec._Total_Monto = String.Format(o._Formato_Moneda, dr.GetDecimal(9));


                    ec._Estado = dr.GetString(10);
                    ec._Fec_Reg = dr.GetDateTime(11);
                    ec._Usu_Reg = dr.GetString(12);
                    list.Add(ec);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTAL "].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBtotRow;
            return list;
        }
        public Int32 SetEliminarCierreProceso(Int32 ide_cierre)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sDeleteCierreProceso;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IDE_CIERRE", SqlDbType.Int).Value = ide_cierre;

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
    }
}