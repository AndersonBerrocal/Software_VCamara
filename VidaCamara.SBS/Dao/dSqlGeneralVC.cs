using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlGeneralVC :IGeneralVC
    {

        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarGeneral(eGeneral o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertGeneral;
                 

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = o._descripcion;
                sqlcmd.Parameters.Add("@RUC_EMPRESA", SqlDbType.VarChar).Value = o._rucEmpresa;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._anoVigente;
                sqlcmd.Parameters.Add("@MES_VEGENTE", SqlDbType.Int).Value = o._mesVigente;
                sqlcmd.Parameters.Add("@RUTA_ARCHIVO", SqlDbType.VarChar).Value = o._Ruta_Archivo;
                sqlcmd.Parameters.Add("@NRO_DECIMAL", SqlDbType.Int).Value = o._Nro_Decimal;
                sqlcmd.Parameters.Add("@TCA_MES", SqlDbType.Decimal).Value = o._tcaMes;
                sqlcmd.Parameters.Add("@TCA_ANIO", SqlDbType.Decimal).Value = o._tcaAno;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usureg;

                 _bool  = Int32.Parse(sqlcmd.ExecuteScalar().ToString());
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

        public Int32 SetActualizarGeneral(eGeneral o) {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarGeneral;


                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IDE_EMPRESA", SqlDbType.Int).Value = o._idEmpresa;
                sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = o._descripcion;
                sqlcmd.Parameters.Add("@RUC_EMPRESA", SqlDbType.VarChar).Value = o._rucEmpresa;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._anoVigente;
                sqlcmd.Parameters.Add("@MES_VEGENTE", SqlDbType.Int).Value = o._mesVigente;
                sqlcmd.Parameters.Add("@RUTA_ARCHIVO", SqlDbType.VarChar).Value = o._Ruta_Archivo;
                sqlcmd.Parameters.Add("@NRO_DECIMAL", SqlDbType.Int).Value = o._Nro_Decimal;
                sqlcmd.Parameters.Add("@TCA_MES", SqlDbType.Decimal).Value = o._tcaMes;
                sqlcmd.Parameters.Add("@TCA_ANIO", SqlDbType.Decimal).Value = o._tcaAno;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_MOD", SqlDbType.VarChar).Value = o._usumod;

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
        public List<eGeneral> GetSelecionarGeneral()
        {
            List<eGeneral> list = new List<eGeneral>();
            try
            {
                conexion.Open();
                String query = "SELECT TOP(1)* FROM GENERAL";
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = query;
                sqlcmd.Connection = conexion;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var o = new eGeneral();
                    o._idEmpresa = dr.GetInt32(0);
                    o._descripcion = dr.GetString(1);
                    o._rucEmpresa = dr.GetString(2);
                    o._anoVigente = dr.GetInt32(3);
                    o._mesVigente = dr.GetInt32(4);
                    o._Ruta_Archivo = dr.GetString(5);
                    o._Nro_Decimal = dr.GetInt32(6);
                    o._tcaMes = dr.GetDecimal(7);
                    o._tcaAno = dr.GetDecimal(8);
                    list.Add(o);
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
