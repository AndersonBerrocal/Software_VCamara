using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlRegistroDatoVC 
    {
        private dbConexion _db = new dbConexion();
        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarDatoM(List<eDatoM> m)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarDatoM;

                foreach (eDatoM o in m)
                {

                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@IDE_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                    sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._tipo_info;
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = o._mes_Vigente;
                    sqlcmd.Parameters.Add("@MES_CONTABLE", SqlDbType.Int).Value = o._mes_Contable;
                    sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._cod_Ramo;
                    sqlcmd.Parameters.Add("@COD_PRODUCTO", SqlDbType.Char).Value = o._cod_Producto;
                    sqlcmd.Parameters.Add("@MTO_ABONADO", SqlDbType.Decimal).Value = o._mto_Abonado;
                    sqlcmd.Parameters.Add("@MTO_PRIMA_EST", SqlDbType.Decimal).Value = o._mto_Prima_Est;
                    sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                    sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usu_reg;

                    _bool = _bool + sqlcmd.ExecuteNonQuery();
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
        public Int32 SetActualizarDatoM(eDatoM o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarDatoM;

                sqlcmd.Parameters.Add("@IDE_DATA_M", SqlDbType.Int).Value = o._ide_Data_M;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._tipo_info;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._anio_Vigente;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._cod_Ramo;
                sqlcmd.Parameters.Add("@COD_PRODUCTO", SqlDbType.Char).Value = o._cod_Producto;
                sqlcmd.Parameters.Add("@MTO_ABONADO", SqlDbType.Decimal).Value = o._mto_Abonado;
                sqlcmd.Parameters.Add("@MTO_PRIMA_EST", SqlDbType.Decimal).Value = o._mto_Prima_Est;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_MOD", SqlDbType.VarChar).Value = o._usu_mod;


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
        public Int32 SetEliminarDatoM(eDatoM o)
        {
            Int32 resp = 0;
            try
            {
                String query = "DELETE FROM DATO_M WHERE NRO_CONTRATO = " + o._nro_Contrato + " AND TIPO_INFO = " + o._tipo_info + " AND COD_RAMO = " + o._cod_Ramo + " AND MES_CONTABLE = " + o._mes_Contable;
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = query;

                resp = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return resp;

        }

        public DataTable GetSelectDatoMGrid(eDatoM o)
        {
            DataTable dt = new DataTable();
            String[] column = {"ID","MONTO","PRIMA"};
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
                sqlcmd.CommandText = _db.sSelectDatoMGrid;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._tipo_info;
                sqlcmd.Parameters.Add("@MES_CONTABLE", SqlDbType.Int).Value = o._mes_Contable;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._cod_Ramo;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    Object[] obj = new Object[3];
                    obj[0] = dr.GetInt32(0);
                    obj[1] = dr.GetDecimal(1);
                    obj[2] = dr.GetDecimal(2);
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
        public DataTable GetSelectDatoM(eDatoM o,out int total_column){

            int DBtotRow = 0;
            DataTable dt = new DataTable();
            String[] column = {"FEC_INI","FEC_FIN","ANIO","DEVENGUE","CONTABLE","ABONO","PRIMA"};
            for(int i= 0;i<column.Length;i++){
                dt.Columns.Add(column[i]);
            }
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectDatoM;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._tipo_info;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._cod_Ramo;
                sqlcmd.Parameters.Add("@COD_PRODUCTO", SqlDbType.Char).Value = o._cod_Producto;
                sqlcmd.Parameters.Add("@TOTAL_COLUMN", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    object[] obj = new object[7];
                    obj[0] = dr.GetDateTime(0);
                    obj[1] = dr.GetDateTime(1);
                    obj[2] = dr.GetInt32(2);
                    obj[3] = dr.GetInt32(3);
                    obj[4] = dr.GetInt32(4);
                    obj[5] = dr.GetDecimal(5);
                    obj[6] = dr.GetDecimal(6);
                    dt.Rows.Add(obj);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTAL_COLUMN"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total_column = DBtotRow;
            return dt;
        }

        public List<eDatoM> listMesDevengue(eDatoM datoM)
        {
            var listDatoM = new List<eDatoM>();
            try
            {
                conexion.Open();
                var sqlcmd = new SqlCommand()
                {
                    Connection = conexion,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = _db.sValidaRegistroManual
                };

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NroContrato", SqlDbType.VarChar).Value = datoM._nro_Contrato;
                sqlcmd.Parameters.Add("@TipoInfo", SqlDbType.Char).Value = datoM._tipo_info;
                sqlcmd.Parameters.Add("@CodRamo", SqlDbType.Char).Value = datoM._cod_Ramo;
                sqlcmd.Parameters.Add("@CodProducto", SqlDbType.Char).Value = datoM._cod_Producto;

                var  reader = sqlcmd.ExecuteReader();
                while (reader.Read())
                {
                    var eDatoM = new eDatoM()
                    {
                        _mes_Contable = Convert.ToInt32(reader["MES_CONTABLE"].ToString())
                    };
                    listDatoM.Add(eDatoM);
                }
                return listDatoM;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}