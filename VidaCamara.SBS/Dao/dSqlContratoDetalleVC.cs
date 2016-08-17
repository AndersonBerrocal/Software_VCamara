using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlContratoDetalleVC : IContratoDetalleVC
    {
        private dbConexion _db = new dbConexion();

        SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarContratoDetalle(eContratoDetalleVC o)
        {

         Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarContratoDet;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@IDE_REASEGURADOR", SqlDbType.Char).Value = o._ide_Reasegurador;
                sqlcmd.Parameters.Add("@COD_REASEGURADOR", SqlDbType.Char).Value = o._cod_Reasegurador;
                sqlcmd.Parameters.Add("@CAL_CREDITICIA", SqlDbType.Char).Value = o._cal_Crediticia;
                sqlcmd.Parameters.Add("@COD_EMPRESA_CALIFICA", SqlDbType.Char).Value = o._cod_Empresa_Califica;
                sqlcmd.Parameters.Add("@MOD_CONTRATO", SqlDbType.Char).Value = o._mod_Contrato;
                sqlcmd.Parameters.Add("@PRC_RETENCION", SqlDbType.Decimal).Value = o._prc_Retencion;
                sqlcmd.Parameters.Add("@PRC_PARTICIPACION_REA", SqlDbType.Decimal).Value = o._prc_participacion_rea;
                sqlcmd.Parameters.Add("@PRC_CESION", SqlDbType.Decimal).Value = o._prc_Cesion;
                sqlcmd.Parameters.Add("@NOMBRE_REA", SqlDbType.VarChar).Value = o._nombre_Rea;
                sqlcmd.Parameters.Add("@NRO_REGISTRO_REA", SqlDbType.Int).Value = o._nro_Registro_Rea;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usu_reg;


                _bool = Int32.Parse(sqlcmd.ExecuteScalar().ToString());
                if (_bool != 0) {
                    String query = "UPDATE CONTRATO SET ESTADO = 'A' WHERE NRO_CONTRATO = "+o._nro_Contrato;
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.CommandText = query;
                    sqlcmd.ExecuteNonQuery();
                }
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

        //actualizar contrato detalle
        public Int32 SetActualizarContratoDetalle(eContratoDetalleVC o) { 

            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarContratoDet;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@IDE_CONTRATO_DET", SqlDbType.Int).Value = o._ide_Contrato_Det;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@IDE_REASEGURADOR", SqlDbType.Char).Value = o._ide_Reasegurador;
                sqlcmd.Parameters.Add("@COD_REASEGURADOR", SqlDbType.Char).Value = o._cod_Reasegurador;
                sqlcmd.Parameters.Add("@CAL_CREDITICIA", SqlDbType.Char).Value = o._cal_Crediticia;
                sqlcmd.Parameters.Add("@COD_EMPRESA_CALIFICA", SqlDbType.Char).Value = o._cod_Empresa_Califica;
                sqlcmd.Parameters.Add("@MOD_CONTRATO", SqlDbType.Char).Value = o._mod_Contrato;
                sqlcmd.Parameters.Add("@PRC_RETENCION", SqlDbType.Decimal).Value = o._prc_Retencion;
                sqlcmd.Parameters.Add("@PRC_CESION", SqlDbType.Decimal).Value = o._prc_Cesion;
                sqlcmd.Parameters.Add("@PRC_PARTICIPACION_REA", SqlDbType.Decimal).Value = o._prc_participacion_rea;
                sqlcmd.Parameters.Add("@NOMBRE_REA", SqlDbType.VarChar).Value = o._nombre_Rea;
                sqlcmd.Parameters.Add("@NRO_REGISTRO_REA", SqlDbType.Int).Value = o._nro_Registro_Rea;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_MOD", SqlDbType.VarChar).Value = o._usu_reg;


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

        public Int32 SetEliminarContratoDetalle(int indice)
        {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery = "DELETE FROM CONTRATO_DETALLE WHERE IDE_CONTRATO_DET = " + indice;
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
        //listar contrato detalle
        public List<eContratoDetalleVC> GetSelecionarContratoDetalle(eContratoDetalleVC o, out int total)
        { 
            List<eContratoDetalleVC> list = new List<eContratoDetalleVC>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = _db.sSelectContratoDetalle;
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;


                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@PAGE_INDEX", SqlDbType.Int).Value = o._inicio;
                sqlcmd.Parameters.Add("@PAGE_SIZE", SqlDbType.Int).Value = o._fin;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = o._orderby;
                sqlcmd.Parameters.Add("@TOTAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while(dr.Read())
                {
                    eContratoDetalleVC e = new eContratoDetalleVC();
                    e._ide_Contrato_Det = dr.GetInt32(1);
                    e._nro_Contrato = dr.GetString(2);
                    e._ide_Reasegurador = dr.GetString(3).Trim();
                    e._cod_Reasegurador = dr.GetString(4).Trim();
                    e._cal_Crediticia = dr.GetString(5).Trim();
                    e._cod_Empresa_Califica = dr.GetString(6).Trim();
                    e._mod_Contrato = dr.GetString(7).Trim();
                    e._prc_Retencion = dr.GetDecimal(8);
                    e._prc_Cesion = dr.GetDecimal(9);
                    e._prc_participacion_rea = dr.GetDecimal(10);
                    e._nombre_Rea = dr.GetString(11);
                    e._nro_Registro_Rea = dr.GetInt32(12);
                    e._estado = dr.GetString(13);
                    e._usu_reg = dr.GetString(14);
                    e._fec_reg = dr.GetDateTime(15);

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