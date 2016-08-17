using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlOperacionVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] meses = {"","Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public List<eOperacionVC> GetSelectOperacion(eOperacionVC o, int start, int size, String orderBy, out int total,out String monedaS)
        {
            List<eOperacionVC> list = new List<eOperacionVC>();
            int DBtotRow = 0;
            try
            {
                conexion.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectOperacion;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.Char).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                sqlcmd.Parameters.Add("@NRO_OPERACION", SqlDbType.Int).Value = o._Nro_Operacion;
                sqlcmd.Parameters.Add("@FECHA_INI", SqlDbType.Date).Value = o._fecha_ini;
                sqlcmd.Parameters.Add("@FECHA_HASTA", SqlDbType.Date).Value = o._fecha_fin;
                sqlcmd.Parameters.Add("@TOKEN_DATE", SqlDbType.Char).Value = o._token_fecha;
                sqlcmd.Parameters.Add("@ORDERBY", SqlDbType.VarChar).Value = orderBy;
                sqlcmd.Parameters.Add("@START", SqlDbType.Int).Value = start;
                sqlcmd.Parameters.Add("@FIN", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@TOTALROW", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ec = new eOperacionVC();
                    ec._Nro_Operacion = dr.GetInt32(1);
                    ec._Tip_Operacion = dr.GetString(2);
                    ec._Cod_Reasegurador = dr.GetString(3);
                    ec._Fec_Operacion = dr.GetDateTime(4);
                    ec._Ano_Operacion = dr.GetInt32(5);
                    ec._Mes_Operacion = meses[dr.GetInt32(6)];
                    ec._Cod_Asegurado = dr.GetString(7);
                    ec._Ide_Contrato = dr.GetString(8);
                    ec._Cod_Ramo = dr.GetString(9);
                    ec._Cod_Moneda = dr.GetString(10);
                    ec._Entorno_Prima_T_2 = String.Format(o._Formato_Moneda, dr.GetDecimal(11));

                    ec._Entorno_Prima_T_1 = String.Format(o._Formato_Moneda, dr.GetDecimal(12));
                    ec._Prima_Abonada = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                    ec._Prima_Reg_Contab = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                    ec._Pri_Ced_Mes = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                    ec._Des_Reg_Mes = dr.GetString(16);
                    ec._Imp_Impuesto_Mes = String.Format(o._Formato_Moneda,dr.GetDecimal(17));
                    ec._Pri_Xpag_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                    ec._Pri_Xcob_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                    ec._Sin_Directo = String.Format(o._Formato_Moneda, dr.GetDecimal(20));
                    ec._Sin_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(21));
                    ec._Sin_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(22));
                    ec._Otr_Cta_Xcob_Rea_Ced = String.Format(o._Formato_Moneda, dr.GetDecimal(23));
                    ec._Otr_Cta_Xpag_Rea_Ace = String.Format(o._Formato_Moneda, dr.GetDecimal(24));
                    ec._Dscto_Comis_Rea = String.Format(o._Formato_Moneda, dr.GetDecimal(25));
                    ec._Saldo_Deudor = String.Format(o._Formato_Moneda, dr.GetDecimal(26));
                    ec._Saldo_Acreedor = String.Format(o._Formato_Moneda, dr.GetDecimal(27));
                    ec._Saldo_Deudor_Comp = String.Format(o._Formato_Moneda, dr.GetDecimal(28));
                    ec._Saldo_Acreedor_Comp = String.Format(o._Formato_Moneda, dr.GetDecimal(29));
                    ec._Estado = dr.GetString(30);
                    ec._Fec_Reg = dr.GetDateTime(31);
                    ec._Usu_Reg = dr.GetString(32);

                    list.Add(ec);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TOTALROW"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            total = DBtotRow;
            monedaS = o._Formato_Moneda;
            return list;
        }
        public Int32 SetProcesaPago(eOperacionVC o) {
            Int32 _bool = 0;
            try
            {
                Int32 mes_vigente = 0;
                if (Convert.ToInt32(o._Mes_Operacion) < 10)
                    mes_vigente = Convert.ToInt32(o._Ano_Operacion + "0" + o._Mes_Operacion);
                else
                    mes_vigente = Convert.ToInt32(o._Ano_Operacion + "" + o._Mes_Operacion);

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaPago;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = mes_vigente;
                sqlcmd.Parameters.Add("@DESCRIPCION_REG", SqlDbType.VarChar).Value = o._Des_Reg_Mes;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.Char).Value = o._Usu_Reg;

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

        public Int32 SetProcesaPrima(eOperacionVC o)
        {
            Int32 _bool = 0;
            try
            {
                int mes_devengue1 = 0;
                int mes_devengue2 = 0;
                int mes_contable = 0;
                if (Convert.ToInt32(o._Mes_Operacion) < 10)
                {
                    if (Convert.ToInt32(o._Mes_Operacion) == 1)
                    {
                        mes_devengue2 = Convert.ToInt32((Convert.ToInt32(o._Ano_Operacion)) - 1 + "" + (Convert.ToInt32(o._Mes_Operacion) + 10));
                        mes_devengue1 = Convert.ToInt32((Convert.ToInt32(o._Ano_Operacion)) - 1 + "" + (Convert.ToInt32(o._Mes_Operacion) + 11));
                        mes_contable = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                    else if (Convert.ToInt32(o._Mes_Operacion) == 2)
                    {
                        mes_devengue2 = Convert.ToInt32((Convert.ToInt32(o._Ano_Operacion)) - 1 + "" + (Convert.ToInt32(o._Mes_Operacion) + 10));
                        mes_devengue1 = Convert.ToInt32((o._Ano_Operacion) + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                        mes_contable = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                    else
                    {
                        mes_devengue2 = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 2));
                        mes_devengue1 = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                        mes_contable = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                }
                else
                {
                    if (Convert.ToInt32(o._Mes_Operacion) - 2 < 10)
                        mes_devengue2 = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 2));
                    else
                        mes_devengue2 = Convert.ToInt32(o._Ano_Operacion + "" + (Convert.ToInt32(o._Mes_Operacion) - 2));

                    if (Convert.ToInt32(o._Mes_Operacion) - 1 < 10)
                        mes_devengue1 = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    else
                        mes_devengue1 = Convert.ToInt32(o._Ano_Operacion + "" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    mes_contable = Convert.ToInt32(o._Ano_Operacion + "" + (o._Mes_Operacion));
                }
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaPrima;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@MES_DEVENGUE2", SqlDbType.VarChar).Value = mes_devengue2;
                sqlcmd.Parameters.Add("@MES_DEVENGUE1", SqlDbType.Char).Value = mes_devengue1;
                sqlcmd.Parameters.Add("@MES_CONTABLE", SqlDbType.Char).Value = mes_contable;
                sqlcmd.Parameters.Add("@DESCRIPCION_REG", SqlDbType.VarChar).Value = o._Des_Reg_Mes;
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

        public Int32 SetProcesaIBNR(eOperacionVC o)
        {
            Int32 _bool = 0;
            try
            {
                int mes_anterior = 0;
                int mes_actual = 0;
                if (Convert.ToInt32(o._Mes_Operacion) < 10)
                {
                    if (Convert.ToInt32(o._Mes_Operacion) == 1)
                    {
                        mes_anterior = Convert.ToInt32((Convert.ToInt32(o._Ano_Operacion)) - 1 + "" + (Convert.ToInt32(o._Mes_Operacion) +11));
                        mes_actual = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                    else
                    {
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                        mes_actual = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                }
                else
                {
                    if (Convert.ToInt32(o._Mes_Operacion) - 1 < 10)
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    else
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    mes_actual = Convert.ToInt32(o._Ano_Operacion + "" + (o._Mes_Operacion));
                }

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaIbnr;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@MES_CONTABLE_1", SqlDbType.Int).Value = mes_anterior;
                sqlcmd.Parameters.Add("@MES_CONTABLE", SqlDbType.Decimal).Value = mes_actual;
                sqlcmd.Parameters.Add("@DESCRIPCION_MES", SqlDbType.VarChar).Value = o._Des_Reg_Mes;
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
            return _bool/3;
        }
        //PROCESO DE RESP
        public Int32 SetProcesaRsp(eOperacionVC o)
        {
            Int32 _bool = 0;
            try
            {
                int mes_anterior = 0;
                int mes_actual = 0;
                if (Convert.ToInt32(o._Mes_Operacion) < 10)
                {
                    if (Convert.ToInt32(o._Mes_Operacion) == 1)
                    {
                        mes_anterior = Convert.ToInt32((Convert.ToInt32(o._Ano_Operacion)) - 1 + "" + (Convert.ToInt32(o._Mes_Operacion) + 11));
                        mes_actual = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                    else
                    {
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                        mes_actual = Convert.ToInt32(o._Ano_Operacion + "0" + (o._Mes_Operacion));
                    }
                }
                else
                {
                    if (Convert.ToInt32(o._Mes_Operacion) - 1 < 10)
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "0" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    else
                        mes_anterior = Convert.ToInt32(o._Ano_Operacion + "" + (Convert.ToInt32(o._Mes_Operacion) - 1));
                    mes_actual = Convert.ToInt32(o._Ano_Operacion + "" + (o._Mes_Operacion));
                }

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaRsp;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@MES_ANTERIOR", SqlDbType.Int).Value = mes_anterior;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = mes_actual;
                sqlcmd.Parameters.Add("@DESCRIPCION_REG", SqlDbType.VarChar).Value = o._Des_Reg_Mes;
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
        public Int32 SetGuardarOperacionManual(eOperacionVC o)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarOperacionManual;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@TIP_OPERACION", SqlDbType.Char).Value = o._Tip_Operacion;
                sqlcmd.Parameters.Add("@TIPO_REGISTRO", SqlDbType.Char).Value = o._Tipo_Registro;
                sqlcmd.Parameters.Add("@COD_REASEGURADOR", SqlDbType.Char).Value = o._Cod_Reasegurador;
                sqlcmd.Parameters.Add("@ANO_OPERACION", SqlDbType.Int).Value = o._Ano_Operacion;
                sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.Int).Value = o._Mes_Operacion;
                sqlcmd.Parameters.Add("@COD_ASEGURADO", SqlDbType.Char).Value = o._Cod_Asegurado;
                sqlcmd.Parameters.Add("@IDE_CONTRATO", SqlDbType.VarChar).Value = o._Ide_Contrato;
                sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                sqlcmd.Parameters.Add("@COD_MONEDA", SqlDbType.Char).Value = o._Cod_Moneda;
                sqlcmd.Parameters.Add("@PRI_CED_MES", SqlDbType.Decimal).Value = o._Pri_Ced_Mes;
                sqlcmd.Parameters.Add("@DES_REG_MES", SqlDbType.VarChar).Value = o._Des_Reg_Mes;
                sqlcmd.Parameters.Add("@IMP_IMPUESTO_MES", SqlDbType.Decimal).Value = o._Imp_Impuesto_Mes;
                sqlcmd.Parameters.Add("@PRI_XPAG_REA_CED", SqlDbType.Decimal).Value = o._Pri_Xpag_Rea_Ced;
                sqlcmd.Parameters.Add("@PRI_XCOB_REA_ACE", SqlDbType.Decimal).Value = o._Pri_Xcob_Rea_Ace;
                sqlcmd.Parameters.Add("@SIN_DIRECTO", SqlDbType.Decimal).Value = o._Sin_Directo;
                sqlcmd.Parameters.Add("@SIN_XCOB_REA_CED", SqlDbType.Decimal).Value = o._Sin_Xcob_Rea_Ced;
                sqlcmd.Parameters.Add("@SIN_XPAG_REA_ACE", SqlDbType.Decimal).Value = o._Sin_Xpag_Rea_Ace;
                sqlcmd.Parameters.Add("@OTR_CTA_XCOB_REA_CED", SqlDbType.Decimal).Value = o._Otr_Cta_Xcob_Rea_Ced;
                sqlcmd.Parameters.Add("@OTR_CTA_XPAG_REA_ACE", SqlDbType.Decimal).Value = o._Otr_Cta_Xpag_Rea_Ace;
                sqlcmd.Parameters.Add("@DSCTO_COMIS_REA", SqlDbType.Decimal).Value = o._Dscto_Comis_Rea;
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Char).Value = o._Estado;
                sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._Usu_Reg;
                sqlcmd.Parameters.Add("@TIP_COMPROBANTE", SqlDbType.VarChar).Value = o._Tip_Comprobante;
                sqlcmd.Parameters.Add("@ORIGEN_OPERACION", SqlDbType.SmallInt).Value = o._Origen_Operacion;

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