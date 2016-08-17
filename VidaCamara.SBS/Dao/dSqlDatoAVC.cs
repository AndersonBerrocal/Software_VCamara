using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlDatoAVC:IDatoAVC
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        public Int32 SetInsertarDatoA(List<eDatoA> LstData)
        {
            Int32 _bool = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSinsertarDatoA;

                foreach(eDatoA o in LstData){


                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@IDE_EMPRESA", SqlDbType.Int).Value = o._Ide_Empresa;
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = o._Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = o._Mes_Vigente;
                    sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._Tipo_Dato;
                    sqlcmd.Parameters.Add("@COD_RAMO", SqlDbType.Char).Value = o._Cod_Ramo;
                    sqlcmd.Parameters.Add("@COD_PRODUCTO", SqlDbType.Int).Value = o._Cod_Producto;
                    sqlcmd.Parameters.Add("@COD_ASEGURADO", SqlDbType.Char).Value = o._Cod_Asegurado;
                    sqlcmd.Parameters.Add("@COD_MONEDA", SqlDbType.Char).Value = o._Cod_Moneda;
                    sqlcmd.Parameters.Add("@AP_MTNO_PENS", SqlDbType.VarChar).Value = o._Ap_Mtno_Pens;
                    sqlcmd.Parameters.Add("@AP_MTNO_SOLIC", SqlDbType.VarChar).Value = o._Ap_Mtno_Solic;
                    sqlcmd.Parameters.Add("@AP_PTNO_PENS", SqlDbType.VarChar).Value = o._Ap_Ptno_Pens;
                    sqlcmd.Parameters.Add("@AP_PTNO_SOLIC", SqlDbType.VarChar).Value = o._Ap_Ptno_Solic;
                    sqlcmd.Parameters.Add("@APORT_COMP", SqlDbType.Decimal).Value = o._Aport_Comp;
                    sqlcmd.Parameters.Add("@APORT_OBLIG", SqlDbType.Decimal).Value = o._Aport_Oblig;
                    sqlcmd.Parameters.Add("@CAP_REQ_PENS", SqlDbType.Decimal).Value = o._Cap_Req_Pens;
                    sqlcmd.Parameters.Add("@CAP_REQ_SEPE", SqlDbType.Decimal).Value = o._Cap_Req_Sepe;
                    sqlcmd.Parameters.Add("@CIC_MON_NSOL", SqlDbType.Decimal).Value = o._Cic_Mon_Nsol;
                    sqlcmd.Parameters.Add("@CIC_MON_ORIG", SqlDbType.Decimal).Value = o._Cic_Mon_Orig;
                    sqlcmd.Parameters.Add("@CRU_FAMILIAR", SqlDbType.Decimal).Value = o._Cru_Familiar;
                    sqlcmd.Parameters.Add("@CUSPP", SqlDbType.VarChar).Value = o._Cuspp;
                    sqlcmd.Parameters.Add("@FE_SIN", SqlDbType.VarChar).Value = o._Fe_Sin;
                    sqlcmd.Parameters.Add("@ID_AFP", SqlDbType.VarChar).Value = o._Id_Afp;
                    sqlcmd.Parameters.Add("@ID_COD_CSV1", SqlDbType.Int).Value = o._Id_Cod_Csv1;
                    sqlcmd.Parameters.Add("@ID_COD_CSV2", SqlDbType.Int).Value = o._Id_Cod_Csv2;
                    sqlcmd.Parameters.Add("@ID_COD_CSV3", SqlDbType.Int).Value = o._Id_Cod_Csv3;
                    sqlcmd.Parameters.Add("@ID_COD_CSV4", SqlDbType.Int).Value = o._Id_Cod_Csv4;
                    sqlcmd.Parameters.Add("@ID_COD_TRANSF", SqlDbType.Int).Value = o._Id_Cod_Transf;
                    sqlcmd.Parameters.Add("@ID_TIP_SOLI", SqlDbType.Int).Value = o._Id_Tip_Soli;
                    sqlcmd.Parameters.Add("@MTO_PAGO_CSV1", SqlDbType.Decimal).Value = o._Mto_Pago_Csv1;
                    sqlcmd.Parameters.Add("@MTO_PAGO_CSV2", SqlDbType.Decimal).Value = o._Mto_Pago_Csv2;
                    sqlcmd.Parameters.Add("@MTO_PAGO_CSV3", SqlDbType.Decimal).Value = o._Mto_Pago_Csv3;
                    sqlcmd.Parameters.Add("@MTO_PAGO_CSV4", SqlDbType.Decimal).Value = o._Mto_Pago_Csv4;
                    sqlcmd.Parameters.Add("@TI_MOV", SqlDbType.VarChar).Value = o._Ti_Mov;
                    sqlcmd.Parameters.Add("@FE_DEV", SqlDbType.VarChar).Value = o._Fe_Dev;
                    sqlcmd.Parameters.Add("@FE_FALLE", SqlDbType.VarChar).Value = o._Fe_Falle;
                    sqlcmd.Parameters.Add("@FE_INI_INV", SqlDbType.VarChar).Value = o._Fe_Ini_Inv;
                    sqlcmd.Parameters.Add("@FE_PAG_AA", SqlDbType.VarChar).Value = o._Fe_Pag_Aa;
                    sqlcmd.Parameters.Add("@ID_TIP_DOC", SqlDbType.Int).Value = o._Id_Tip_Doc;
                    sqlcmd.Parameters.Add("@IND_PENS_PRE", SqlDbType.VarChar).Value = o._Ind_Pens_Pre;
                    sqlcmd.Parameters.Add("@MTO_AA_MON_NSOL", SqlDbType.Decimal).Value = o._Mto_Aa_Mon_Nsol;
                    sqlcmd.Parameters.Add("@MTO_AA_MON_ORIG", SqlDbType.Decimal).Value = o._Mto_Aa_Mon_Orig;
                    sqlcmd.Parameters.Add("@MTO_EXC_MON_NSOL", SqlDbType.Decimal).Value = o._Mto_Exc_Mon_Nsol;
                    sqlcmd.Parameters.Add("@MTO_EXC_MON_ORIG", SqlDbType.Decimal).Value = o._Mto_Exc_Mon_Orig;
                    sqlcmd.Parameters.Add("@MTO_PENS_PRE_MON_NSOL", SqlDbType.Decimal).Value = o._Mto_Pens_Pre_Mon_Nsol;
                    sqlcmd.Parameters.Add("@MTO_PENS_PRE_MON_ORIG", SqlDbType.Decimal).Value = o._Mto_Pens_Pre_Mon_Orig;
                    sqlcmd.Parameters.Add("@NRO_BENEF", SqlDbType.VarChar).Value = o._Nro_Benef;
                    sqlcmd.Parameters.Add("@NRO_IDEN_SOLIC", SqlDbType.VarChar).Value = o._Nro_Iden_Solic;
                    sqlcmd.Parameters.Add("@NUM_CSV", SqlDbType.VarChar).Value = o._Num_Csv;
                    sqlcmd.Parameters.Add("@PRI_NOMB_SOLIC", SqlDbType.VarChar).Value = o._Pri_Nomb_Solic;
                    sqlcmd.Parameters.Add("@RAM_PROM_NSOL", SqlDbType.Decimal).Value = o._Ram_Prom_Nsol;
                    sqlcmd.Parameters.Add("@SEGU_NOMB_SOLIC", SqlDbType.VarChar).Value = o._Segu_Nomb_Solic;
                    sqlcmd.Parameters.Add("@TASA_INT", SqlDbType.Decimal).Value = o._Tasa_Int;
                    sqlcmd.Parameters.Add("@TI_CAMB_COMPRA", SqlDbType.Decimal).Value = o._Ti_Camb_Compra;
                    sqlcmd.Parameters.Add("@TI_CAMB_VTA", SqlDbType.Decimal).Value = o._Ti_Camb_Vta;
                    sqlcmd.Parameters.Add("@TI_REG", SqlDbType.VarChar).Value = o._Ti_Reg;
                    sqlcmd.Parameters.Add("@TIP_MON", SqlDbType.Int).Value = o._Tip_Mon;
                    sqlcmd.Parameters.Add("@TIP_PEN_EQUIV", SqlDbType.VarChar).Value = o._Tip_Pen_Equiv;
                    sqlcmd.Parameters.Add("@TOT_CAP_REQ_MON_NSOL", SqlDbType.Decimal).Value = o._Tot_Cap_Req_Mon_Nsol;
                    sqlcmd.Parameters.Add("@TOT_CAP_REQ_MON_ORIG", SqlDbType.Decimal).Value = o._Tot_Cap_Req_Mon_Orig;
                    sqlcmd.Parameters.Add("@DESCT_PENS", SqlDbType.Decimal).Value = o._Desct_Pens;
                    sqlcmd.Parameters.Add("@DESCT_PENS_CSV1", SqlDbType.Decimal).Value = o._Desct_Pens_Csv1;
                    sqlcmd.Parameters.Add("@DESCT_PENS_CSV2", SqlDbType.Decimal).Value = o._Desct_Pens_Csv2;
                    sqlcmd.Parameters.Add("@DESCT_PENS_CSV3", SqlDbType.Decimal).Value = o._Desct_Pens_Csv3;
                    sqlcmd.Parameters.Add("@DESCT_PENS_CSV4", SqlDbType.Decimal).Value = o._Desct_Pens_Csv4;
                    sqlcmd.Parameters.Add("@FE_DEV_ACT", SqlDbType.VarChar).Value = o._Fe_Dev_Act;
                    sqlcmd.Parameters.Add("@FE_DEV_INI", SqlDbType.VarChar).Value = o._Fe_Dev_Ini;
                    sqlcmd.Parameters.Add("@FE_FIN_SUBSI", SqlDbType.VarChar).Value = o._Fe_Fin_Subsi;
                    sqlcmd.Parameters.Add("@FE_NACI_PENS", SqlDbType.VarChar).Value = o._Fe_Naci_Pens;
                    sqlcmd.Parameters.Add("@FE_SECI", SqlDbType.VarChar).Value = o._Fe_Seci;
                    sqlcmd.Parameters.Add("@FEC_PAGO", SqlDbType.VarChar).Value = o._Fec_Pago;
                    sqlcmd.Parameters.Add("@FRAC_MES_DEV", SqlDbType.VarChar).Value = o._Frac_Mes_Dev;
                    sqlcmd.Parameters.Add("@ID_COD_BEN", SqlDbType.VarChar).Value = o._Id_Cod_Ben;
                    sqlcmd.Parameters.Add("@ID_PARENT", SqlDbType.Int).Value = o._Id_Parent;
                    sqlcmd.Parameters.Add("@ID_TIP_DOCU_PENS", SqlDbType.VarChar).Value = o._Id_Tip_Docu_Pens;
                    sqlcmd.Parameters.Add("@MES_DEV", SqlDbType.VarChar).Value = o._Mes_Dev;
                    sqlcmd.Parameters.Add("@MON_EQUI", SqlDbType.VarChar).Value = o._Mon_Equi;
                    sqlcmd.Parameters.Add("@MTO_APORT_COMP_CSV1", SqlDbType.Decimal).Value = o._Mto_Aport_Comp_Csv1;
                    sqlcmd.Parameters.Add("@MTO_APORT_COMP_CSV2", SqlDbType.Decimal).Value = o._Mto_Aport_Comp_Csv2;
                    sqlcmd.Parameters.Add("@MTO_APORT_COMP_CSV3", SqlDbType.Decimal).Value = o._Mto_Aport_Comp_Csv3;
                    sqlcmd.Parameters.Add("@MTO_APORT_COMP_CSV4", SqlDbType.Decimal).Value = o._Mto_Aport_Comp_Csv4;
                    sqlcmd.Parameters.Add("@MTO_APORT_OBLIG_CSV1", SqlDbType.Decimal).Value = o._Mto_Aport_Oblig_Csv1;
                    sqlcmd.Parameters.Add("@MTO_APORT_OBLIG_CSV2", SqlDbType.Decimal).Value = o._Mto_Aport_Oblig_Csv2;
                    sqlcmd.Parameters.Add("@MTO_APORT_OBLIG_CSV3", SqlDbType.Decimal).Value = o._Mto_Aport_Oblig_Csv3;
                    sqlcmd.Parameters.Add("@MTO_APORT_OBLIG_CSV4", SqlDbType.Decimal).Value = o._Mto_Aport_Oblig_Csv4;
                    sqlcmd.Parameters.Add("@MTO_PAGO", SqlDbType.Decimal).Value = o._Mto_Pago;
                    sqlcmd.Parameters.Add("@NRO_DOCU_PENS", SqlDbType.VarChar).Value = o._Nro_Docu_Pens;
                    sqlcmd.Parameters.Add("@NRO_EESS", SqlDbType.VarChar).Value = o._Nro_Eess;
                    sqlcmd.Parameters.Add("@NRO_MES_DEV", SqlDbType.VarChar).Value = o._Nro_Mes_Dev;
                    sqlcmd.Parameters.Add("@NRO_SIN", SqlDbType.Int).Value = o._Nro_Sin;
                    sqlcmd.Parameters.Add("@NRO_SOLI", SqlDbType.VarChar).Value = o._Nro_Soli;
                    sqlcmd.Parameters.Add("@PENS_BASE", SqlDbType.Decimal).Value = o._Pens_Base;
                    sqlcmd.Parameters.Add("@PENS_PAGAR_80", SqlDbType.Decimal).Value = o._Pens_Pagar_80;
                    sqlcmd.Parameters.Add("@PRI_NOMB_PENS", SqlDbType.VarChar).Value = o._Pri_Nomb_Pens;
                    sqlcmd.Parameters.Add("@PROC_BENE", SqlDbType.VarChar).Value = o._Proc_Bene;
                    sqlcmd.Parameters.Add("@PROM_ACT_6_MESES_APOCOMP", SqlDbType.Decimal).Value = o._Prom_Act_6_Meses_Apocomp;
                    sqlcmd.Parameters.Add("@REMPRO_ACT", SqlDbType.Decimal).Value = o._Rempro_Act;
                    sqlcmd.Parameters.Add("@SEGU_NOMB_PENS", SqlDbType.VarChar).Value = o._Segu_Nomb_Pens;
                    sqlcmd.Parameters.Add("@TIP_PEN", SqlDbType.VarChar).Value = o._Tip_Pen;
                    sqlcmd.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = o._Estado;
                    sqlcmd.Parameters.Add("@USU_REG", SqlDbType.VarChar).Value = o._Usu_Reg;

                    _bool = _bool + sqlcmd.ExecuteNonQuery();
                }
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
        public List<eDatoAString> GetSelecionarSepelio(eDatoAString o, int index, int size, String sort,out int totalRow)
        {
            List<eDatoAString> listSepelio = new List<eDatoAString>();
            int DBtotRow = 0;
            try
            {   
                Int32 mes_anio_vigente = 0;
                if (o._Mes_Vigente < 10) {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente+"0"+o._Mes_Vigente);
                } else {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente + "" + o._Mes_Vigente);
                }

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectSepelio;
                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IdeEmpresa", SqlDbType.Int).Value = o._Ide_Empresa;
                sqlcmd.Parameters.Add("@Nro_contrato", SqlDbType.Char).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@TipoInfo", SqlDbType.Char).Value = o._Tipo_Dato;
                sqlcmd.Parameters.Add("@Mes_Vigente", SqlDbType.Int).Value = mes_anio_vigente;
                sqlcmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = index;
                sqlcmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@OrderBy", SqlDbType.VarChar).Value = sort;
                sqlcmd.Parameters.Add("@TotalRows", SqlDbType.Int).Direction = ParameterDirection.Output;



                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ed = new eDatoAString();

                    ed._Ide_Empresa = dr.GetInt32(1);
                    ed._Nro_Contrato = dr.GetString(2);
                    ed._Anio_Vigente = o._Anio_Vigente;
                    ed._Mes_Vigente = dr.GetInt32(4);
                    ed._Cod_Ramo = dr.GetString(5);
                    ed._Cod_Producto = dr.GetInt32(6);
                    ed._Cod_Asegurado = dr.GetString(7);
                    ed._Cod_Moneda = dr.GetString(8);
                    ed._Ap_Mtno_Pens = dr.GetString(9);
                    ed._Ap_Mtno_Solic = dr.GetString(10);
                    ed._Ap_Ptno_Pens = dr.GetString(11);
                    ed._Ap_Ptno_Solic = dr.GetString(12);
                    ed._Aport_Comp = String.Format(o._Formato_Moneda, dr.GetDecimal(13));
                    ed._Aport_Oblig = String.Format(o._Formato_Moneda, dr.GetDecimal(14));
                    ed._Cap_Req_Pens = String.Format(o._Formato_Moneda, dr.GetDecimal(15));
                    ed._Cap_Req_Sepe = String.Format(o._Formato_Moneda, dr.GetDecimal(16));
                    ed._Cic_Mon_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(17));
                    ed._Cic_Mon_Orig = String.Format(o._Formato_Moneda, dr.GetDecimal(18));
                    ed._Cru_Familiar = String.Format(o._Formato_Moneda, dr.GetDecimal(19));
                    ed._Cuspp = dr.GetString(20);
                    ed._Fe_Sin = dr.GetString(21);
                    ed._Id_Afp = dr.GetString(22);
                    ed._Id_Cod_Csv1 = dr.GetInt32(23);
                    ed._Id_Cod_Csv2 = dr.GetInt32(24);
                    ed._Id_Cod_Csv3 = dr.GetInt32(25);
                    ed._Id_Cod_Csv4 = dr.GetInt32(26);
                    ed._Id_Cod_Transf = dr.GetInt32(27);
                    ed._Id_Tip_Soli = dr.GetInt32(28);
                    ed._Mto_Pago_Csv1 = String.Format(o._Formato_Moneda, dr.GetDecimal(29));
                    ed._Mto_Pago_Csv2 = String.Format(o._Formato_Moneda, dr.GetDecimal(30));
                    ed._Mto_Pago_Csv3 = String.Format(o._Formato_Moneda, dr.GetDecimal(31));
                    ed._Mto_Pago_Csv4 = String.Format(o._Formato_Moneda, dr.GetDecimal(32));
                    ed._Ti_Mov = dr.GetString(33);
                    ed._Fe_Dev = dr.GetString(34);
                    ed._Fe_Falle = dr.GetString(35);
                    ed._Fe_Ini_Inv = dr.GetString(36);
                    ed._Fe_Pag_Aa = dr.GetString(37);
                    ed._Id_Tip_Doc = dr.GetInt32(38);
                    ed._Ind_Pens_Pre = dr.GetString(39);
                    ed._Mto_Aa_Mon_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(40));
                    ed._Mto_Aa_Mon_Orig = String.Format(o._Formato_Moneda, dr.GetDecimal(41));
                    ed._Mto_Exc_Mon_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(42));
                    ed._Mto_Exc_Mon_Orig = String.Format(o._Formato_Moneda, dr.GetDecimal(43));
                    ed._Mto_Pens_Pre_Mon_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(44));
                    ed._Mto_Pens_Pre_Mon_Orig = String.Format(o._Formato_Moneda, dr.GetDecimal(45));
                    ed._Nro_Benef = dr.GetString(46);
                    ed._Nro_Iden_Solic = dr.GetString(47);
                    ed._Num_Csv = dr.GetString(48);
                    ed._Pri_Nomb_Solic = dr.GetString(49);
                    ed._Ram_Prom_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(50));
                    ed._Segu_Nomb_Solic = dr.GetString(51);
                    ed._Tasa_Int = String.Format(o._Formato_Moneda, dr.GetDecimal(52));
                    ed._Ti_Camb_Compra = String.Format(o._Formato_Moneda, dr.GetDecimal(53));
                    ed._Ti_Camb_Vta = String.Format(o._Formato_Moneda, dr.GetDecimal(54));
                    ed._Ti_Reg = dr.GetString(55);
                    ed._Tip_Mon = dr.GetInt32(56);
                    ed._Tip_Pen_Equiv = dr.GetString(57);
                    ed._Tot_Cap_Req_Mon_Nsol = String.Format(o._Formato_Moneda, dr.GetDecimal(58));
                    ed._Tot_Cap_Req_Mon_Orig = String.Format(o._Formato_Moneda, dr.GetDecimal(59));
                    ed._Desct_Pens = String.Format(o._Formato_Moneda, dr.GetDecimal(60));
                    ed._Desct_Pens_Csv1 = String.Format(o._Formato_Moneda, dr.GetDecimal(61));
                    ed._Desct_Pens_Csv2 = String.Format(o._Formato_Moneda, dr.GetDecimal(62));
                    ed._Desct_Pens_Csv3 = String.Format(o._Formato_Moneda, dr.GetDecimal(63));
                    ed._Desct_Pens_Csv4 = String.Format(o._Formato_Moneda, dr.GetDecimal(64));
                    ed._Fe_Dev_Act = dr.GetString(65);
                    ed._Fe_Dev_Ini = dr.GetString(66);
                    ed._Fe_Fin_Subsi = dr.GetString(67);
                    ed._Fe_Naci_Pens = dr.GetString(68);
                    ed._Fe_Seci = dr.GetString(69);
                    ed._Fec_Pago = dr.GetString(70);
                    ed._Frac_Mes_Dev = dr.GetString(71);
                    ed._Id_Cod_Ben = dr.GetString(72);
                    ed._Id_Parent = dr.GetInt32(73);
                    ed._Id_Tip_Docu_Pens = dr.GetString(74);
                    ed._Mes_Dev = dr.GetString(75);
                    ed._Mon_Equi = dr.GetString(76);
                    ed._Mto_Aport_Comp_Csv1 = String.Format(o._Formato_Moneda, dr.GetDecimal(77));
                    ed._Mto_Aport_Comp_Csv2 = String.Format(o._Formato_Moneda, dr.GetDecimal(78));
                    ed._Mto_Aport_Comp_Csv3 = String.Format(o._Formato_Moneda, dr.GetDecimal(79));
                    ed._Mto_Aport_Comp_Csv4 = String.Format(o._Formato_Moneda, dr.GetDecimal(80));
                    ed._Mto_Aport_Oblig_Csv1 = String.Format(o._Formato_Moneda, dr.GetDecimal(81));
                    ed._Mto_Aport_Oblig_Csv2 = String.Format(o._Formato_Moneda, dr.GetDecimal(82));
                    ed._Mto_Aport_Oblig_Csv3 = String.Format(o._Formato_Moneda, dr.GetDecimal(83));
                    ed._Mto_Aport_Oblig_Csv4 = String.Format(o._Formato_Moneda, dr.GetDecimal(84));
                    ed._Mto_Pago = String.Format(o._Formato_Moneda, dr.GetDecimal(85));
                    ed._Nro_Docu_Pens = dr.GetString(86);
                    ed._Nro_Eess = dr.GetString(87);
                    ed._Nro_Mes_Dev = dr.GetString(88);
                    ed._Nro_Sin = dr.GetInt32(89);
                    ed._Nro_Soli = dr.GetString(90);
                    ed._Pens_Base = String.Format(o._Formato_Moneda, dr.GetDecimal(91));
                    ed._Pens_Pagar_80 = String.Format(o._Formato_Moneda, dr.GetDecimal(92));
                    ed._Pri_Nomb_Pens = dr.GetString(93);
                    ed._Proc_Bene = dr.GetString(94);
                    ed._Prom_Act_6_Meses_Apocomp = String.Format(o._Formato_Moneda, dr.GetDecimal(95));
                    ed._Rempro_Act = String.Format(o._Formato_Moneda, dr.GetDecimal(96));
                    ed._Segu_Nomb_Pens = dr.GetString(97);
                    ed._Tip_Pen = dr.GetString(98);
                    ed._Estado = dr.GetString(99);
                    ed._Fec_Reg = dr.GetDateTime(100);
                    ed._Usu_Reg = dr.GetString(101);
                    listSepelio.Add(ed);
                }
                dr.Close();
                DBtotRow = (int)sqlcmd.Parameters["@TotalRows"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            totalRow = DBtotRow;
            return listSepelio;
        }

        public Int32 SetEliminarDatoA(eDatoA o) {
            Int32 _bool = 0;
            try
            {
                Int32 mes_anio_vigente = 0;
                if (o._Mes_Vigente < 10)
                {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente + "0" + o._Mes_Vigente);
                }
                else
                {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente + "" + o._Mes_Vigente);
                }
                String query = "DELETE FROM DATO_A WHERE NRO_CONTRATO ="+ o._Nro_Contrato+
                                " AND MES_VIGENTE = " + mes_anio_vigente +
                                " AND TIPO_INFO = " +o._Tipo_Dato;
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = query;

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
        //SELECT RSP
        public List<eDataRSP> GetSelectRSP(eDatoAString o, int index, int size, String sort, out int totalRows)
        {
            int DBTotal = 0;
            List<eDataRSP> list = new List<eDataRSP>();
            try
            {
                Int32 mes_anio_vigente = 0;
                if (o._Mes_Vigente < 10)
                {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente + "0" + o._Mes_Vigente);
                }
                else
                {
                    mes_anio_vigente = Convert.ToInt32(o._Anio_Vigente + "" + o._Mes_Vigente);
                }

                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectRsp;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@IdeEmpresa", SqlDbType.Int).Value = o._Ide_Empresa;
                sqlcmd.Parameters.Add("@Nro_contrato", SqlDbType.Char).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@TipoInfo", SqlDbType.Char).Value = o._Tipo_Dato;
                sqlcmd.Parameters.Add("@Mes_Vigente", SqlDbType.Int).Value = mes_anio_vigente;
                sqlcmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = index;
                sqlcmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
                sqlcmd.Parameters.Add("@OrderBy", SqlDbType.VarChar).Value = sort;
                sqlcmd.Parameters.Add("@TotalRows", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var rsp = new eDataRSP();

                    rsp.NRO_CONTRATO = dr.GetString(1);
                    rsp.ANIO_VIGENTE = o._Anio_Vigente;
                    rsp.MES_VIGENTE = dr.GetInt32(3);
                    rsp.COD_ASEGURADO = dr.GetString(4);
                    rsp.CUSPP = dr.GetString(5);
                    rsp.TI_MOV = dr.GetString(6);
                    rsp.TI_REG = dr.GetString(7);
                    rsp.TIP_MON = dr.GetInt32(8);
                    rsp.FE_DEV = dr.GetString(9);
                    rsp.FE_SIN = dr.GetString(10);
                    rsp.MTO_PAGO_S = String.Format(o._Formato_Moneda, dr.GetDecimal(11));
                    rsp.ID_AFP = dr.GetString(12);
                    rsp.FE_SECI = dr.GetString(13);
                    rsp.ESTADO = dr.GetString(14);
                    rsp.FEC_REG = dr.GetDateTime(15);
                    rsp.USU_REG = dr.GetString(16);

                    list.Add(rsp);
                }
                dr.Close();
                DBTotal = (int)sqlcmd.Parameters["@TotalRows"].Value;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            totalRows = DBTotal;
            return list;
        }
        //SELECT RSP
        public Int32 GetCountExisteDatoA(eDatoA o)
        {
            int DBTotal = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectCountDatoA;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = o._Nro_Contrato;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = o._Mes_Vigente;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = o._Tipo_Dato;

                DBTotal = (int)sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return DBTotal;
        }
    }
}