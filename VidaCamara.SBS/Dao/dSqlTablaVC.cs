using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlTablaVC : ITablaVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        public Int32 SetInsertarConcepto(eTabla o) {
             Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarConcepto;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IDE_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@TIPO_TABLA", SqlDbType.VarChar).Value = o._tipo_Tabla;
                sqlcmd.Parameters.Add("@CODIGO", SqlDbType.VarChar).Value = o._codigo;
                sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = o._descripcion;
                sqlcmd.Parameters.Add("@VALOR", SqlDbType.Char).Value = o._valor;
                sqlcmd.Parameters.Add("@CLASE", SqlDbType.VarChar).Value = o._clase;
                sqlcmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = o._tipo;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usu_reg;

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
        //actualizar concepto
        public Int32 SetActualizarConcepto(eTabla o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarConcepto;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@ID_CONCEPTO", SqlDbType.Int).Value = o._id_Concepto;
                sqlcmd.Parameters.Add("@IDE_EMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@TIPO_TABLA", SqlDbType.VarChar).Value = o._tipo_Tabla;
                sqlcmd.Parameters.Add("@CODIGO", SqlDbType.VarChar).Value = o._codigo;
                sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = o._descripcion;
                sqlcmd.Parameters.Add("@VALOR", SqlDbType.Char).Value = o._valor;
                sqlcmd.Parameters.Add("@CLASE", SqlDbType.VarChar).Value = o._clase;
                sqlcmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = o._tipo;
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
        public Int32 SetEliminarConcepto(int indice)
        {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery = "DELETE FROM CONCEPTO WHERE ID_CONCEPTO = " + indice;
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

        public List<eTabla> GetSelectConcepto(eTabla o,out int total)
        {
            int DBTotal = 0;
            List<eTabla> list = new List<eTabla>();
            int tokenlist = 0;
            try
            {
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSselectConcepto;
                conexion.Open();

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IDE_IMPRESA", SqlDbType.Int).Value = o._id_Empresa;
                sqlcmd.Parameters.Add("@TIPO_TABLA", SqlDbType.VarChar).Value = o._tipo_Tabla;
                sqlcmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = o._descripcion;
                sqlcmd.Parameters.Add("@VALOR", SqlDbType.Char).Value = o._valor;
                if (o._tipo == null)
                {
                    sqlcmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = "N";
                }
                else {
                    sqlcmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = o._tipo;
                }
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@START", SqlDbType.Int).Value = o._inicio;
                sqlcmd.Parameters.Add("@FIN", SqlDbType.Int).Value = o._fin;
                sqlcmd.Parameters.Add("@ORDER", SqlDbType.VarChar).Value = o._order.ToUpper();
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    eTabla t = new eTabla();
                    t._id_Concepto = dr.GetInt32(1);
                    t._id_Empresa =dr.GetInt32(2);
                    t._tipo_Tabla = dr.GetString(3).Trim();
                    t._codigo = dr.GetString(4).Trim();
                    if(o._estado.ToUpper() == "S")
                        t._descripcion = dr.GetString(5);
                    else
                        t._descripcion = dr.GetString(5) + " (" + dr.GetString(4).Trim() + ")";

                    t._valor = dr.GetString(6);
                    t._clase = dr.GetString(7);
                    t._tipo = dr.GetString(8);
                    t._estado = dr.GetString(9);
                    t._fe_creg = dr.GetDateTime(10);
                    t._usu_reg = dr.GetString(11);
                    list.Add(t);
                }
                dr.Close();
                DBTotal = (int)sqlcmd.Parameters["@TOTALROW"].Value;
            }

            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBTotal;
            return list;
        }

        public StringCollection GetConceptoByCodigo(String codigo) {
            var resp = new StringCollection();
            try
            {
                String query = string.Format("SELECT TIPO,IS_EDITABLE  FROM CONCEPTO WHERE TIPO_TABLA = '9999' AND CODIGO = '{0}'",codigo);
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = query;
                conexion.Open();

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] fields = new string[] { dr["TIPO"].ToString(), dr["IS_EDITABLE"].ToString() };
                    resp.AddRange(fields);
                }
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
    }
}