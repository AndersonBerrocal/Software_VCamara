using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlContratoSys : IContratoSys
    {
        private dbConexion _db = new dbConexion();
        
        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarContratoSys(eContratoSys o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarContratoSys;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@CLA_CONTRATO", SqlDbType.Char).Value = o._cla_Contrato;
                sqlcmd.Parameters.Add("@FEC_INI_VIG", SqlDbType.Date).Value = o._fec_Ini_Vig;
                sqlcmd.Parameters.Add("@FEC_FIN_VIG", SqlDbType.Date).Value = o._fec_Fin_Vig;
                sqlcmd.Parameters.Add("@DES_CONTRATO", SqlDbType.VarChar).Value = o._des_Contrato;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usu_reg;
                sqlcmd.Parameters.Add("@NRO_EMPRESA", SqlDbType.Int).Value = o._nro_empresa;
                sqlcmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar).Value = o._centro_costo;

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
        //actualizar contrato
        public Int32 SetActualizarContratoSys(eContratoSys o)
        { 
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarContratoSys;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@IDE_CONTRATO", SqlDbType.Int).Value = o._ide_Contrato;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@CLA_CONTRATO", SqlDbType.Char).Value = o._cla_Contrato;
                sqlcmd.Parameters.Add("@FEC_INI_VIG", SqlDbType.Date).Value = o._fec_Ini_Vig;
                sqlcmd.Parameters.Add("@FEC_FIN_VIG", SqlDbType.Date).Value = o._fec_Fin_Vig;
                sqlcmd.Parameters.Add("@DES_CONTRATO", SqlDbType.VarChar).Value = o._des_Contrato;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_MOD", SqlDbType.VarChar).Value = o._usu_reg;
                sqlcmd.Parameters.Add("@NRO_EMPRESA", SqlDbType.Int).Value = o._nro_empresa;
                sqlcmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar).Value = o._centro_costo;

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
        public Int32 SetEliminarContratoSys(int indice) {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery = "DELETE FROM CONTRATO_SYS WHERE IDE_CONTRATO = " + indice;
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = DeleteQuery;
                
                _bool = sqlcmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                conexion.Close();
            }
            return _bool;
        }
        //listar contrato
        public List<eContratoSys> GetSelecionarContratoSys(eContratoSys o, out int total)
        {
            List<eContratoSys> list = new List<eContratoSys>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectContratoSys;
                sqlcmd.Connection = conexion;

                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@PAGE_INDEX", SqlDbType.Int).Value = o._inicio;
                sqlcmd.Parameters.Add("@PAGE_SIZE", SqlDbType.Int).Value = o._fin;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = o._orderby;
                sqlcmd.Parameters.Add("@TOTAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    eContratoSys e = new eContratoSys();

                    e._ide_Contrato = dr.GetInt32(1);
                    e._nro_Contrato = dr.GetString(2).Trim();
                    e._cla_Contrato = dr.GetString(3).Trim();
                    e._fec_Ini_Vig = dr.GetDateTime(4);
                    e._fec_Fin_Vig = dr.GetDateTime(5);
                    e._des_Contrato = dr.GetString(6).Trim();
                    e._estado = dr.GetString(7);
                    e._fec_reg = dr.GetDateTime(8);
                    e._usu_reg = dr.GetString(9);
                    e._nro_empresa = dr.GetInt32(10);
                    e._centro_costo = dr.GetString(11);

                    list.Add(e);
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
            return list;
        }
    }
}