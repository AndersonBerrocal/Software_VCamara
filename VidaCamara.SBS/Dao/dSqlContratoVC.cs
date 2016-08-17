using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlContratoVC : IContratoVC
    {
        private dbConexion _db = new dbConexion();


        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());

        public Int32 SetInsertarContrato(eContratoVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarContrato;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@COD_RAMO_PRI", SqlDbType.Char).Value = o._cod_Ramo_pri;
                sqlcmd.Parameters.Add("@COD_RAMO_SIN", SqlDbType.Char).Value = o._cod_Ramo_Sin;
                sqlcmd.Parameters.Add("@CLA_CONTRATO", SqlDbType.Char).Value = o._cla_Contrato;
                sqlcmd.Parameters.Add("@FEC_INI_VIG", SqlDbType.Date).Value = o._fec_Ini_Vig;
                sqlcmd.Parameters.Add("@FEC_FIN_VIG", SqlDbType.Date).Value = o._fec_Fin_Vig;
                sqlcmd.Parameters.Add("@TIP_CONTRATO", SqlDbType.Char).Value = o._tip_Contrato;
                sqlcmd.Parameters.Add("@COD_MONEDA", SqlDbType.Char).Value = o._cod_Moneda;
                sqlcmd.Parameters.Add("@COD_CONTRATANTE", SqlDbType.Char).Value = o._cod_Contratante;
                sqlcmd.Parameters.Add("@POR_PARTICIPA_CIA", SqlDbType.Decimal).Value = o._por_Participa_Cia;
                sqlcmd.Parameters.Add("@POR_TASA_RIESGO", SqlDbType.Decimal).Value = o._por_Tasa_Riesgo;
                sqlcmd.Parameters.Add("@POR_TASA_REASEGURO", SqlDbType.Decimal).Value = o._por_Tasa_Reaseguro;
                sqlcmd.Parameters.Add("@POR_IMPUESTO", SqlDbType.Decimal).Value = o._por_Impuesto;
                sqlcmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar).Value = o._Centro_Costo;
                sqlcmd.Parameters.Add("@DES_CONTRATO", SqlDbType.VarChar).Value = o._des_Contrato;
                sqlcmd.Parameters.Add("@MOD_CONTRATO", SqlDbType.Char).Value = o._mod_Contrato;
                sqlcmd.Parameters.Add("@POR_RETENCION", SqlDbType.Decimal).Value = o._por_Retencion;
                sqlcmd.Parameters.Add("@POR_CESION", SqlDbType.Decimal).Value = o._por_Cesion;
                sqlcmd.Parameters.Add("@MTO_MAX_RETENCION", SqlDbType.Decimal).Value = o._mto_Max_Retencion;
                sqlcmd.Parameters.Add("@MTO_MAX_CESION", SqlDbType.Decimal).Value = o._mto_Max_Cesion;
                sqlcmd.Parameters.Add("@MTO_PLENO", SqlDbType.Decimal).Value = o._mto_Pleno;
                sqlcmd.Parameters.Add("@NRO_LINEA_MULT", SqlDbType.Int).Value = o._nro_Linea_Mult;
                sqlcmd.Parameters.Add("@MTO_MAX_CUBERTURA", SqlDbType.Decimal).Value = o._mto_Max_Cubertura;
                sqlcmd.Parameters.Add("@NRO_CAPA_XL1", SqlDbType.Int).Value = o._nro_Capa_Xl1;
                sqlcmd.Parameters.Add("@PRIORIDAD1", SqlDbType.Decimal).Value = o._Prioridad1;
                sqlcmd.Parameters.Add("@CESION_EXC_PRIORIDAD1", SqlDbType.Decimal).Value = o._Cesion_Exc_Prioridad1;
                sqlcmd.Parameters.Add("@MTO_MAX_CAP_LIM_SUP1", SqlDbType.Decimal).Value = o._mto_Max_Cap_Lim_Sup1;
                sqlcmd.Parameters.Add("@PRIMA_MIN_DEPOSITO1", SqlDbType.Decimal).Value = o._prima_Min_Deposito1;
                sqlcmd.Parameters.Add("@NRO_CAPA_XL2", SqlDbType.Int).Value = o._nro_Capa_Xl2;
                sqlcmd.Parameters.Add("@PRIORIDAD2", SqlDbType.Decimal).Value = o._Prioridad2;
                sqlcmd.Parameters.Add("@CESION_EXC_PRIORIDAD2", SqlDbType.Decimal).Value = o._Cesion_Exc_Prioridad2;
                sqlcmd.Parameters.Add("@MTO_MAX_CAP_LIM_SUP2", SqlDbType.Decimal).Value = o._mto_Max_Cap_Lim_Sup2;
                sqlcmd.Parameters.Add("@PRIMA_MIN_DEPOSITO2", SqlDbType.Decimal).Value = o._prima_Min_Deposito2;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._usu_reg;

                _bool = sqlcmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
            finally
            {
                conexion.Close();
            }
            return _bool;
        }
        //actualizar contrato
        public Int32 SetActualizarContrato(eContratoVC o) { 
            Int32 _bool = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSactualizarContrato;

                sqlcmd.Parameters.Add("@ID_EMPRESA", SqlDbType.Int).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@IDE_CONTRATO", SqlDbType.Int).Value = o._ide_Contrato;
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._nro_Contrato;
                sqlcmd.Parameters.Add("@COD_RAMO_PRI", SqlDbType.Char).Value = o._cod_Ramo_pri;
                sqlcmd.Parameters.Add("@COD_RAMO_SIN", SqlDbType.Char).Value = o._cod_Ramo_Sin;
                sqlcmd.Parameters.Add("@CLA_CONTRATO", SqlDbType.Char).Value = o._cla_Contrato;
                sqlcmd.Parameters.Add("@FEC_INI_VIG", SqlDbType.Date).Value = o._fec_Ini_Vig;
                sqlcmd.Parameters.Add("@FEC_FIN_VIG", SqlDbType.Date).Value = o._fec_Fin_Vig;
                sqlcmd.Parameters.Add("@TIP_CONTRATO", SqlDbType.Char).Value = o._tip_Contrato;
                sqlcmd.Parameters.Add("@COD_MONEDA", SqlDbType.Char).Value = o._cod_Moneda;
                sqlcmd.Parameters.Add("@COD_CONTRATANTE", SqlDbType.Char).Value = o._cod_Contratante;
                sqlcmd.Parameters.Add("@POR_PARTICIPA_CIA", SqlDbType.Decimal).Value = o._por_Participa_Cia;
                sqlcmd.Parameters.Add("@POR_TASA_RIESGO", SqlDbType.Decimal).Value = o._por_Tasa_Riesgo;
                sqlcmd.Parameters.Add("@POR_TASA_REASEGURO", SqlDbType.Decimal).Value = o._por_Tasa_Reaseguro;
                sqlcmd.Parameters.Add("@POR_IMPUESTO", SqlDbType.Decimal).Value = o._por_Impuesto;
                sqlcmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar).Value = o._Centro_Costo;
                sqlcmd.Parameters.Add("@DES_CONTRATO", SqlDbType.VarChar).Value = o._des_Contrato;
                sqlcmd.Parameters.Add("@MOD_CONTRATO", SqlDbType.Char).Value = o._mod_Contrato;
                sqlcmd.Parameters.Add("@POR_RETENCION", SqlDbType.Decimal).Value = o._por_Retencion;
                sqlcmd.Parameters.Add("@POR_CESION", SqlDbType.Decimal).Value = o._por_Cesion;
                sqlcmd.Parameters.Add("@MTO_MAX_RETENCION", SqlDbType.Decimal).Value = o._mto_Max_Retencion;
                sqlcmd.Parameters.Add("@MTO_MAX_CESION", SqlDbType.Decimal).Value = o._mto_Max_Cesion;
                sqlcmd.Parameters.Add("@MTO_PLENO", SqlDbType.Decimal).Value = o._mto_Pleno;
                sqlcmd.Parameters.Add("@NRO_LINEA_MULT", SqlDbType.Int).Value = o._nro_Linea_Mult;
                sqlcmd.Parameters.Add("@MTO_MAX_CUBERTURA", SqlDbType.Decimal).Value = o._mto_Max_Cubertura;
                sqlcmd.Parameters.Add("@NRO_CAPA_XL1", SqlDbType.Int).Value = o._nro_Capa_Xl1;
                sqlcmd.Parameters.Add("@PRIORIDAD1", SqlDbType.Decimal).Value = o._Prioridad1;
                sqlcmd.Parameters.Add("@CESION_EXC_PRIORIDAD1", SqlDbType.Decimal).Value = o._Cesion_Exc_Prioridad1;
                sqlcmd.Parameters.Add("@MTO_MAX_CAP_LIM_SUP1", SqlDbType.Decimal).Value = o._mto_Max_Cap_Lim_Sup1;
                sqlcmd.Parameters.Add("@PRIMA_MIN_DEPOSITO1", SqlDbType.Decimal).Value = o._prima_Min_Deposito1;
                sqlcmd.Parameters.Add("@NRO_CAPA_XL2", SqlDbType.Int).Value = o._nro_Capa_Xl2;
                sqlcmd.Parameters.Add("@PRIORIDAD2", SqlDbType.Decimal).Value = o._Prioridad2;
                sqlcmd.Parameters.Add("@CESION_EXC_PRIORIDAD2", SqlDbType.Decimal).Value = o._Cesion_Exc_Prioridad2;
                sqlcmd.Parameters.Add("@MTO_MAX_CAP_LIM_SUP2", SqlDbType.Decimal).Value = o._mto_Max_Cap_Lim_Sup2;
                sqlcmd.Parameters.Add("@PRIMA_MIN_DEPOSITO2", SqlDbType.Decimal).Value = o._prima_Min_Deposito2;
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
        public Int32 SetEliminarContrato(int indice) {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery  = "DELETE FROM CONTRATO WHERE IDE_CONTRATO = " + indice;
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
        //listar contrato
        public List<eContratoVC> GetSelecionarContrato(eContratoVC o,out int total) {
            List<eContratoVC> list = new List<eContratoVC>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectContrato;
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
                    eContratoVC e = new eContratoVC();

                    e._ide_Contrato = dr.GetInt32(1);
                    e._nro_Contrato = dr.GetString(2).Trim();
                    e._cod_Ramo_pri = dr.GetString(3).Trim();
                    e._cod_Ramo_Sin = dr.GetString(4).Trim();
                    e._cla_Contrato = dr.GetString(5).Trim();
                    e._fec_Ini_Vig = dr.GetDateTime(6);
                    e._fec_Fin_Vig = dr.GetDateTime(7);
                    e._tip_Contrato = dr.GetString(8).Trim();
                    e._cod_Moneda = dr.GetString(9).Trim();
                    e._cod_Contratante = dr.GetString(10).Trim();
                    e._por_Participa_Cia = dr.GetDecimal(11);
                    e._por_Tasa_Riesgo = dr.GetDecimal(12);
                    e._por_Tasa_Reaseguro = dr.GetDecimal(13);
                    e._por_Impuesto = dr.GetDecimal(14);
                    e._des_Contrato = dr.GetString(15);
                    e._mod_Contrato = dr.GetString(16).Trim();
                    e._por_Retencion = dr.GetDecimal(17);
                    e._por_Cesion = dr.GetDecimal(18);
                    e._mto_Max_Retencion = dr.GetDecimal(19);
                    e._mto_Max_Cesion = dr.GetDecimal(20);
                    e._mto_Pleno = dr.GetDecimal(21);
                    e._nro_Linea_Mult = dr.GetInt32(22);
                    e._mto_Max_Cubertura = dr.GetDecimal(23);
                    e._nro_Capa_Xl1 = dr.GetInt32(24);
                    e._Prioridad1 = dr.GetDecimal(25);
                    e._Cesion_Exc_Prioridad1 = dr.GetDecimal(26);
                    e._mto_Max_Cap_Lim_Sup1 = dr.GetDecimal(27);
                    e._prima_Min_Deposito1 = dr.GetDecimal(28);
                    e._nro_Capa_Xl2 = dr.GetInt32(29);
                    e._Prioridad2 = dr.GetDecimal(30);
                    e._Cesion_Exc_Prioridad2 = dr.GetDecimal(31);
                    e._mto_Max_Cap_Lim_Sup2 = dr.GetDecimal(32);
                    e._prima_Min_Deposito2 = dr.GetDecimal(33);
                    e._estado = dr.GetString(34);
                    e._fec_reg = dr.GetDateTime(35);
                    e._usu_reg = dr.GetString(36);
                    e._Centro_Costo = dr.GetString(37);

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
