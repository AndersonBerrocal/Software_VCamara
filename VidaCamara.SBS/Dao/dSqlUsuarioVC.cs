using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlUsuarioVC
    {
        private dbConexion _db = new dbConexion();


        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarUsuario(eUsuarioVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarUsuario;

                sqlcmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = o._Usuario;
                sqlcmd.Parameters.Add("@CONTRASENA", SqlDbType.VarChar).Value = o._Contrasena;
                sqlcmd.Parameters.Add("@ACCESO_PAGINA", SqlDbType.VarChar).Value = o._Aceso_Pagina;
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
        public Int32 SetValidarUsuario(eUsuarioVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSvalidarUsuario;

                sqlcmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = o._Usuario;
                sqlcmd.Parameters.Add("@CONTRASENA", SqlDbType.VarChar).Value = o._Contrasena;

                _bool = (int)sqlcmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return _bool;
        }
        public Int32 SetActualizarUsuario(eUsuarioVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarUsuario;

                sqlcmd.Parameters.Add("@IDE_USUARIO", SqlDbType.Int).Value = o._ide_Usuario;
                sqlcmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = o._Usuario;
                sqlcmd.Parameters.Add("@NOMBRES", SqlDbType.VarChar).Value = o._nombres;
                sqlcmd.Parameters.Add("@CARGO", SqlDbType.VarChar).Value = o._cargo;
                sqlcmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = o._email;
                sqlcmd.Parameters.Add("@TIPO_USUARIO", SqlDbType.VarChar).Value = o._tipo_Usuario;
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
        public List<eUsuarioVC> GetSelecionarUsuario(Int32 start,Int32 size,String orderby,out Int32 total)
        {
            List<eUsuarioVC> list = new List<eUsuarioVC>();
            Int32 DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelecionarUsuario;

                sqlcmd.Parameters.Add("@INDEX", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@SIZE", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = orderby;
                sqlcmd.Parameters.Add("@TOTAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    eUsuarioVC e = new eUsuarioVC();
                    e._ide_Usuario = dr.GetInt32(1);
                    e._Usuario = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                        e._nombres = dr.GetString(3);
                    if(!dr.IsDBNull(4))
                        e._cargo = dr.GetString(4);
                    if(!dr.IsDBNull(5))
                        e._email = dr.GetString(5);
                    if(!dr.IsDBNull(6))
                        e._tipo_Usuario = dr.GetString(6);
                    e._estado = dr.GetString(7);
                    e._fec_Reg = dr.GetDateTime(8);
                    e._usu_reg = dr.GetString(9);
                    if (!dr.IsDBNull(10))
                        e._fec_Mod = dr.GetDateTime(10);

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
        public List<eUsuarioVC> GetSelecionarAccesoUsuario(Int32 ide_usuario)
        {
            List<eUsuarioVC> list = new List<eUsuarioVC>();
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectAccesoUsuario;

                sqlcmd.Parameters.Add("@IDE_USUARIO", SqlDbType.Int).Value = ide_usuario;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    eUsuarioVC e = new eUsuarioVC();
                    e._ide_Usuario = dr.GetInt32(0);
                    e._Aceso_Pagina = dr.GetString(1);
                    list.Add(e);
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
        public Int32 SetActualizarAccesoUsuario(eUsuarioVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarAccesoUsuario;

                sqlcmd.Parameters.Add("@IDE_USUARIO", SqlDbType.Int).Value = o._ide_Usuario;
                sqlcmd.Parameters.Add("@ACCESO_PAGINA", SqlDbType.VarChar).Value = o._Aceso_Pagina;

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
        public List<eUsuarioVC> GetSelecionarAccesoSession(String usuario,String contrasena)
        {
            List<eUsuarioVC> list = new List<eUsuarioVC>();
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectAccesoUsuarioSession;

                sqlcmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = usuario;
                sqlcmd.Parameters.Add("@CONTRASENA", SqlDbType.VarChar).Value = contrasena;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    eUsuarioVC e = new eUsuarioVC();
                    e._ide_Usuario = dr.GetInt32(0);
                    e._Aceso_Pagina = dr.GetString(1);
                    list.Add(e);
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