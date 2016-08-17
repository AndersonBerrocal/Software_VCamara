using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlValidarCierreVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public List<eValidarCierreVC>GetSelectValidarCierre(eValidarCierreVC o)
        {
            List<eValidarCierreVC> list = new List<eValidarCierreVC>();
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectValidarCierre;

                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._Tipo_Info;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@MES_ANTERIOR1", SqlDbType.Int).Value = o._Mes_Actual - 2;
                sqlcmd.Parameters.Add("@MES_ANTERIOR", SqlDbType.Int).Value = o._Mes_Actual- 1;
                sqlcmd.Parameters.Add("@MES_ACTUAL", SqlDbType.Int).Value = o._Mes_Actual;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._Anio_Operacion; ;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ev = new eValidarCierreVC();
                    if (o._Tipo_Registro.ToUpper() == "MEN")
                    {
                        ev._Mes_Anterior = dr.GetInt32(0);
                        ev._Mes_Actual = dr.GetInt32(1);
                    }
                    else
                    {
                        ev._Mes_x_Trimestral = dr.GetInt32(0);
                        ev._Mes_Actual = dr.GetInt32(1);
                    }
                    list.Add(ev);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return list;
        }
    }
}