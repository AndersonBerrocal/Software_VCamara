using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Dao
{
    public class dSqlInterfaceContable
    {
        private dbConexion _db = new dbConexion();

        static SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBD").ToString());
        static String[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviebre", "Diciembre" };

        //RSP Y PAGO
        public Int32 SetIntertarAsientoSeniestroRSPyPAGO(eInterfaceContableVC e)
        {
            String[] asiento_rsp = {"+52","-26","+26","-14"};
            Int32 _bool = 0;
            String asiento;
            DateTime fecha_registro;
            try
            {
                SetIntertarExactusCabecera(e, out asiento, out fecha_registro);
                String mensaje = "";
                String descripcion_ope = "";
                if (e.Tipo_Info.Trim().Equals("RP"))
                {
                    mensaje = "PAGO DE SINIESTROS";
                    descripcion_ope = "PAGOS";
                }
                else
                {
                    mensaje = "RESERVA DE SINIESTROS PENDIENTES";
                    descripcion_ope = "RSP";
                }
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaContabilidad;
                for (int r = 0; r < asiento_rsp.Length; r++)
                {
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_OPERACION", SqlDbType.VarChar).Value = e.Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.VarChar).Value = e.Mes_Vigente;
                    sqlcmd.Parameters.Add("@FECHA_REGISTRO", SqlDbType.DateTime).Value = fecha_registro;
                    sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = e.Tipo_Info;
                    sqlcmd.Parameters.Add("@DESCRIPCION_OPE", SqlDbType.VarChar).Value =  descripcion_ope;
                    sqlcmd.Parameters.Add("@ASIENTO_CONTABLE", SqlDbType.VarChar).Value = asiento;
                    switch (asiento_rsp[r]) { 
                        case "+52" :
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento52;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 1; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.ope_mercado_ext52;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.siniestros_pend52;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = mensaje;
                            break;
                        case "-26":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento26;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta credito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.reserva_cedida26;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.Siniestro_pendiente26;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = mensaje;
                            break;
                        case "+26":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento26;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 1; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.reserva_cedida26;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.Siniestro_pendiente26;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = mensaje;
                            break;
                        case "-14":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento14;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.mercado_exterior14;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = "00";//parametro sin accion
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = mensaje;
                            break;
                    }
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
        //IBNR
        public Int32 SetIntertarAsientoSeniestroIBNR(eInterfaceContableVC e)
        {
            String[] asiento_rsp = { "+52", "-26", "+26", "-14" };
            Int32 _bool = 0;
            String asiento;
            DateTime fecha_registro;
            try
            {
                SetIntertarExactusCabecera(e, out asiento, out fecha_registro);
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaContabilidad;
                for (int r = 0; r < asiento_rsp.Length; r++)
                {
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_OPERACION", SqlDbType.VarChar).Value = e.Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.VarChar).Value = e.Mes_Vigente;
                    sqlcmd.Parameters.Add("@FECHA_REGISTRO", SqlDbType.DateTime).Value = fecha_registro;
                    sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = e.Tipo_Info;
                    sqlcmd.Parameters.Add("@DESCRIPCION_OPE", SqlDbType.VarChar).Value = "IBNR";
                    sqlcmd.Parameters.Add("@ASIENTO_CONTABLE", SqlDbType.VarChar).Value = asiento;
                    switch (asiento_rsp[r])
                    {
                        case "+52":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento52;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 1; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.ope_mercado_ext52;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.IBNR52;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "RESERVA DE SINIESTROS OCURRIDOS Y NO REPORTADOS";
                            break;
                        case "-26":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento26;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta credito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.reserva_cedida26;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.IBNR26;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "RESERVA DE SINIESTROS OCURRIDOS Y NO REPORTADOS";
                            break;
                        case "+26":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento26;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 1; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.reserva_cedida26;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.IBNR26;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "RESERVA DE SINIESTROS OCURRIDOS Y NO REPORTADOS";
                            break;
                        case "-14":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.asiento14;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.mercado_exterior14;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = "00";//parametro sin accion
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "RESERVA DE SINIESTROS OCURRIDOS Y NO REPORTADOS";
                            break;
                    }
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
        //PRIMA
        public Int32 SetIntertarAsientoSeniestroPRIMA(eInterfaceContableVC e)
        {
            String[] asiento_prima = { "+40", "-20", "-24"};
            Int32 _bool = 0;
            String asiento;
            DateTime fecha_registro;
            try
            {
                SetIntertarExactusCabecera(e, out asiento, out fecha_registro);
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaContabilidadPrima;
                for (int r = 0; r < asiento_prima.Length; r++)
                {
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_OPERACION", SqlDbType.VarChar).Value = e.Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_OPERACION", SqlDbType.VarChar).Value = e.Mes_Vigente;
                    sqlcmd.Parameters.Add("@FECHA_REGISTRO", SqlDbType.DateTime).Value = fecha_registro;
                    sqlcmd.Parameters.Add("@TIPO_OPERACION", SqlDbType.Char).Value = e.Tipo_Info;
                    sqlcmd.Parameters.Add("@ASIENTO_CONTABLE", SqlDbType.VarChar).Value = asiento;
                    switch (asiento_prima[r])
                    {
                        case "+40":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.cuenta40;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 1; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.mercado_exterior40;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.seguro_provisional40;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "PRIMA SISCO";
                            break;
                        case "-20":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.cuenta20;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta credito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.cuenta_tercero20;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = this.cuenta_xpag_diversa20;
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "PRIMA SISCO";
                            break;
                        case "-24":
                            sqlcmd.Parameters.Add("@NRO_ASIENTO", SqlDbType.Char).Value = this.cuenta24;
                            sqlcmd.Parameters.Add("@DEBITO_O_CREDITO", SqlDbType.Int).Value = 0; //cuenta debito
                            sqlcmd.Parameters.Add("@INTERMEDIO1", SqlDbType.VarChar).Value = this.mercado_exterior24;
                            sqlcmd.Parameters.Add("@INTERMEDIO2", SqlDbType.Char).Value = "01";//VALOR POR DEFECTO
                            sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = "PRIMA SISCO";
                            break;
                    }
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

        //CABECERA
        public void SetIntertarExactusCabecera(eInterfaceContableVC e,out String asiento,out DateTime fecha_registro)
        {
            String asiento_res = "";
            DateTime fecha_creacion_res = System.DateTime.Today;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sProcesaExactusCabecera;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.VarChar).Value = e.Tipo_Info;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.VarChar).Value = e.Anio_Vigente;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Char).Value = e.Mes_Vigente;
                sqlcmd.Parameters.Add("@CONTABILIDAD", SqlDbType.VarChar).Value = "C";
                sqlcmd.Parameters.Add("@NOTAS", SqlDbType.VarChar).Value = "PRUEBA CONTABILIDAD";
                sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = 2;
                sqlcmd.Parameters.Add("@PERMITIR_DESCUADRADO", SqlDbType.Char).Value = "N";
                sqlcmd.Parameters.Add("@CONSERVAR_NUMERACION", SqlDbType.Char).Value = "S";
                sqlcmd.Parameters.Add("@ACTUALIZAR_CONSECUTIVO", SqlDbType.Char).Value = "N";
                sqlcmd.Parameters.Add("@USUARIO_REGISTRO", SqlDbType.VarChar).Value = e.Usuario_Registro;

                //asiento = sqlcmd.ExecuteScalar().ToString();
                SqlDataReader dr = sqlcmd.ExecuteReader();
                while(dr.Read()){
                    asiento_res = dr.GetString(0);
                    fecha_creacion_res = dr.GetDateTime(1);
                }
                dr.Close();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            asiento = asiento_res;
            fecha_registro = fecha_creacion_res;
        }

        //ELIMIANR INTERFACE CONTABLE DATOS
        public Int32 SetEliminarCabeceraDetalle(eInterfaceContableVC e)
        {
            Int32 resp = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sDeleteExactus;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.VarChar).Value = e.Tipo_Info;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = e.Anio_Vigente;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = e.Mes_Vigente;

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
    
        //PLAN CONTABLE
    //--------------------------------------------------------------
    //variables para procesar asientos acuerdo al tipo de asiento   !
    //______________________________________________________________!

    //cuenta 52----------------------------------------------------------------------
        public String asiento52 = "52"; //Cuenta Siniestros de Primas Cedidas       !
        //1: Nuevos Soles   --se evalua si es soles o dolares                       !
        //2: Dólares        --se evalua si es soles o dolares                       !
        public String ope_mercado_ext52 = "2"; //Operaciones Mercado Exterior       !
        //91: Invalidez                                                             !
        //92: Sobrevivencia                                                         !
        //93: Gasto Sepelio                                                         !
        public String siniestros_pend52 = "01"; //Siniestros Pendientes             !
        public String IBNR52 = "02";//IBNR                                          !
    //______________________________________________________________________________!_



    //cuenta 26------------------------------------------------------------------
        public String asiento26 = "26"; //Cuenta Reservas                       !
        //1: Nuevos Soles   --se evalua si es soles o dolares                   !
        //2: Dólares        --se evalua si es soles o dolares                   !
        public String reserva_cedida26 = "5"; //Reservas Cedidas                !
        //91: Invalidez                         --se evalua acuerdo al ramo     !
        //92: Sobrevivencia                     --se evalua acuerdo al ramo     !
        //93: Gasto Sepelio                     --se evalua acuerdo al ramo     !
        public String Siniestro_pendiente26 = "01"; //Siniestros Pendientes     !
        public String IBNR26 = "02"; //IBNR                                     !
        //                                                                      !
        //-----------------------------------------------------------------------

    //cuenta 14------------------------------------------------------------------------
        public String asiento14 = "14";//: Cuentas por Cobrar Reaseguradores          !
        //1: Nuevos Soles   --se evalua si es soles o dolares                         !
        //2: Dólares        --se evalua si es soles o dolares                         !
        public String mercado_exterior14 = "2";//Reaseguros Mercado Exterior          !
        //01: Proporcionales Automáticos	--se evalua acuerdo al tipo de contrato   !
        //02: Proporcionales Facultativos   --se evalua acuerdo al tipo de contrato   !
	    //01: Invalidez	                    --se evalua acuerdo al ramo               !
        //02: Sobrevivencia	                --se evalua acuerdo al ramo               !
        //03: Gasto Sepelio	                --se evalua acuerdo al ramo               !
    //                                                                                !
    //---------------------------------------------------------------------------------

    //cuenta 40-----------------------------------------------------------------------!--
        public String cuenta40 = "40"; //Egresos                                        !
        //1: Nuevos Soles   --se evalua si es soles o dolares                           !
        //2: Dólares        --se evalua si es soles o dolares                           !
        public String mercado_exterior40 = "2"; //Mercado Exterior                      !
        public String seguro_provisional40 = "91"; //Seguros Previsionales              !
        //01: Proporcionales Automáticos	--se evalua acuerdo al tipo de contrato 	!
        //02: Proporcionales Facultativos   --se evalua acuerdo al tipo de contrato     !
    //                                                                                  !
    //__________________________________________________________________________________!


    //cuenta 20-------------------------------------------------------------------!-
        public String cuenta20 = "20"; //Tributos                                   !
        //1: Nuevos Soles   --se evalua si es soles o dolares                       !
        //2: Dólares        --se evalua si es soles o dolares                       !
        public String cuenta_tercero20 = "2"; //Cuentas con Terceros                !
        public String cuenta_xpag_diversa20 = "09"; //Cuentas por pagar diversas    !
        //** Tal cual                                                               !
    //______________________________________________________________________________!_

        
    //cuenta----------------------------------------------------------------------!- 
        public string cuenta24 = "24"; // Cuentas por Pagar Reaseguradores          !
        //1: Nuevos Soles   --se evalua si es soles o dolares                       !
        //2: Dólares        --se evalua si es soles o dolares                       !
        public string mercado_exterior24 = "2"; //Reaseguros Mercado Exterior       !
        //** Tal cual                                                               !
    //______________________________________________________________________________!

     //-----------------------------------------------------------------------------!
     //ejemplo composicion de asiento contable 52 con sus  variables                  !
     //   //  --52.1.2.91.03.00                                                       !
     //_______________________________________________________________________________! 

        //LISTA DE ASIENTOS CONTABLE
        public List<eInterfaceContableVC> GetSelectContable(eInterfaceContableVC e)
        {
            List<eInterfaceContableVC> list = new List<eInterfaceContableVC>();
            //int DBtotRow = 0;
            try
            {
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sSelectExactus;

                sqlcmd.Parameters.Clear();
                sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = e.Tipo_Info;
                sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = e.Anio_Vigente;
                sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = e.Mes_Vigente;

                SqlDataReader dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    var ic = new eInterfaceContableVC();
                    ic.Nro_Contrato = dr.GetString(0);
                    ic.Tipo_Info = dr.GetString(1);
                    ic.Anio_Vigente = dr.GetInt32(2);
                    ic.Mes_Vigente = meses[dr.GetInt32(3)];
                    ic.Paquete = dr.GetString(4);
                    ic.Asiento = dr.GetString(5);
                    ic.Fecha = dr.GetDateTime(6);
                    ic.Tipo_Asiento = dr.GetString(7);
                    ic.Contabilidad = dr.GetString(8);
                    ic.Fuente = dr.GetString(10);
                    ic.Referencia = dr.GetString(11);
                    ic.Centro_Costo = dr.GetString(13);
                    ic.Cuenta_Contable = dr.GetString(14);
                    //ic.Consecutivo = dr.GetInt32(9);
                    if (dr.GetDecimal(15) > 0)
                        ic.debito_Local = String.Format(e.Formato_Moneda, dr.GetDecimal(15));
                    else if(dr.GetDecimal(15) < 0)
                        ic.credito_Local = String.Format(e.Formato_Moneda, dr.GetDecimal(15));
                    if(dr.GetDecimal(16) > 0)
                        ic.debito_dolar = String.Format(e.Formato_Moneda, dr.GetDecimal(16));
                    else if (dr.GetDecimal(16) < 0)
                        ic.credito_dolar= String.Format(e.Formato_Moneda, dr.GetDecimal(16));
                    ic.Monto_Unidades = String.Format(e.Formato_Moneda, dr.GetDecimal(17));
                    ic.Usuario_Registro = dr.GetString(18);
                    ic.Estado_Transferencia = dr.GetString(19);
                    list.Add(ic);
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

        //LISTA DE ASIENTOS CONTABLE EXPORT
        public DataTable GetSelectContableExport(eInterfaceContableVC e)
        {
            DataTable dtlist = new DataTable();
            String[] columns = { "NRO_CONTRATO", "TIPO_INFORMACION", "AÑO", "MES_VIGENTE", "PAQUETE", "ASIENTO", "FECHA", "TIPO_ASIENTO", 
                                 "TIPO CONTABILIDAD", "CLASE ASIENTO", "FUENTE", "REFERENCIA", "CONTRIBUYENTE", "CENTRO_COSTO", "CUENTA_CONTABLE", 
                                 "DEBITO_LOCAL", "CREDITO_LOCAL", "DEBITO_DOLAR", "CREDITO_DOLAR", "MONTO UNIDADES","USUARIO_REGISTRO" };
            for (int c = 0; c < columns.Length; c++) {
                dtlist.Columns.Add(columns[c]);
            }
                //int DBtotRow = 0;
                try
                {
                    conexion.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = conexion;
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = _db.sSelectExactus;

                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                    sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = e.Tipo_Info;
                    sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = e.Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = e.Mes_Vigente;

                    SqlDataReader dr = sqlcmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Object[] obj = new Object[columns.Length];

                        obj[0] = dr.GetString(0);
                        obj[1] = dr.GetString(1);
                        obj[2] = dr.GetInt32(2);
                        obj[3] = meses[dr.GetInt32(3)];
                        obj[4] = dr.GetString(4);
                        obj[5] = dr.GetString(5);
                        obj[6] = dr.GetDateTime(6).ToShortDateString();
                        obj[7] = dr.GetString(7);
                        obj[8] = dr.GetString(8);
                        obj[9] = dr.GetString(9);
                        obj[10] = dr.GetString(10);
                        obj[11] = dr.GetString(11);
                        obj[12] = dr.GetString(12);
                        obj[13] = dr.GetString(13);
                        obj[14] = dr.GetString(14);
                        if (dr.GetDecimal(15) > 0)
                            obj[15] = String.Format(e.Formato_Moneda, dr.GetDecimal(15));
                        else if (dr.GetDecimal(15) < 0)
                            obj[16] = String.Format(e.Formato_Moneda, dr.GetDecimal(15));

                        if (dr.GetDecimal(16) > 0)
                            obj[17] = String.Format(e.Formato_Moneda, dr.GetDecimal(16));
                        else if (dr.GetDecimal(16) < 0)
                            obj[18] = String.Format(e.Formato_Moneda, dr.GetDecimal(16));

                        obj[19] = String.Format(e.Formato_Moneda, dr.GetDecimal(17));
                        obj[20] = dr.GetString(18);

                        dtlist.Rows.Add(obj);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conexion.Close();
                }
            return dtlist;
        }
        public Int32 GetExisteInterface(eInterfaceContableVC e) {
            Int32 _bool = 0;
            try
            {
                String DeleteQuery = "SELECT COUNT(*) FROM EXACTUS_CABECERA WHERE NRO_CONTRATO =" + e.Nro_Contrato + " AND TIPO_INFO = " + e.Tipo_Info + " AND ANIO_VIGENTE = " + e.Anio_Vigente + " AND MES_VIGENTE = " + e.Mes_Vigente;
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = DeleteQuery;


                _bool = (int)sqlcmd.ExecuteScalar();
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
        public Int32 GetExisteCierreOperacion(eInterfaceContableVC e) {
            Int32 _bool = 0;
            try
            {
                String SelectQuery = "SELECT COUNT(*) FROM CIERRE_PROCESO WHERE NRO_CONTRATO = '"+e.Nro_Contrato+"' AND TIPO_INFO = '"+e.Tipo_Info+"' AND ANIO_CIERRE = "+e.Anio_Vigente+" AND MES_CIERRE = "+e.Mes_Vigente;
                conexion.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = SelectQuery;


                _bool = (int)sqlcmd.ExecuteScalar();
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

        public Int32 GetTransferExactus(eInterfaceContableVC e) {
            SqlConnection connection_exactus = new SqlConnection(ConfigurationManager.AppSettings.Get("CnnBDEX").ToString());
            String[] tipo = {"0","1"};
            Int32 resp = 0;
            Int32 validacion = 0;
            try
                {
                conexion.Open();
                connection_exactus.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = _db.sTransferExactus;

                for(int r = 0;r<tipo.Length;r++){
                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@NRO_CONTRATO", SqlDbType.VarChar).Value = e.Nro_Contrato;
                    sqlcmd.Parameters.Add("@ANIO_VIGENTE", SqlDbType.Int).Value = e.Anio_Vigente;
                    sqlcmd.Parameters.Add("@MES_VIGENTE", SqlDbType.Int).Value = e.Mes_Vigente;
                    sqlcmd.Parameters.Add("@TIPO_INFO", SqlDbType.Char).Value = e.Tipo_Info;
                    sqlcmd.Parameters.Add("@TOKEN", SqlDbType.Int).Value = tipo[r];
                    

                    SqlDataReader dr = sqlcmd.ExecuteReader();
                    Int32 correlativo = 1;
                    while (dr.Read())
                    {
                        var exc = new eInterfaceContableVC();
                        if (tipo[r].Equals("0"))
                        {
                            exc.Asiento = dr.GetString(0);
                            exc.Paquete = dr.GetString(1);
                            exc.Tipo_Asiento = dr.GetString(2);
                            exc.Fecha = dr.GetDateTime(3);
                            exc.Contabilidad = dr.GetString(4);
                            exc.Notas = dr.GetString(5);
                            exc.Estado = dr.GetInt32(6);
                            exc.Permitir_Descuadrado = dr.GetString(7);
                            exc.Conservar_Numeracion = dr.GetString(8);
                            exc.Actualizar_Consecutivo = dr.GetString(9);
                            exc.Fecha_Auditoria = dr.GetDateTime(10);
                            validacion = this.SetInsertTransferExactus(exc, tipo[r], connection_exactus);
                        }
                        else {
                            exc.Asiento = dr.GetString(0);
                            exc.Consecutivo = correlativo;
                            exc.Centro_Costo = dr.GetString(2);
                            exc.Cuenta_Contable = dr.GetString(3);
                            exc.Fuente = dr.GetString(4);
                            exc.Referencia = dr.GetString(5);
                            exc.Monto_Local = dr.GetDecimal(6);
                            exc.Monto_Dolar = dr.GetDecimal(7);
                            exc.Monto_Unidades = dr.GetDecimal(8).ToString();
                            if (!dr.IsDBNull(9))
                                exc.Nit = dr.GetString(9);
                            else exc.Nit = "NULL";
                            if (!dr.IsDBNull(10))
                                exc.Dimension1 = dr.GetString(10);
                            else exc.Dimension1 = "NULL";
                            if (!dr.IsDBNull(11))
                                exc.Dimension2 = dr.GetString(11);
                            else exc.Dimension2 = "NULL";
                            if (!dr.IsDBNull(12))
                                exc.Dimension3 = dr.GetString(12);
                            else exc.Dimension3 = "NULL";
                            if (!dr.IsDBNull(13))
                                exc.Dimension4 = dr.GetString(13);
                            else exc.Dimension4 = "NULL";
                            resp += this.SetInsertTransferExactus(exc, tipo[r], connection_exactus);
                            correlativo++;
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection_exactus.Close();
                if (resp > 0 && validacion > 0)
                    SetUpdateEstadoExactusTransfer(e);
                conexion.Close();
            }
            return resp;
        }
        private Int32 SetInsertTransferExactus(eInterfaceContableVC e,String token,SqlConnection conexion) {
            Int32 resp = 0;
            try
            {
                SqlCommand sqlcmd = new SqlCommand();
                if (token.Equals("0"))
                {

                    String InsertHeader = "INSERT INTO VCAMARA.EXACTUS_ASIENTO_DE_DIARIO(ASIENTO,PAQUETE,TIPO_ASIENTO,FECHA,CONTABILIDAD,NOTAS,ESTADO,PERMITIR_DESCUADRADO,CONSERVAR_NUMERACION,ACTUALIZAR_CONSECUTIVO,FECHA_AUDITORIA)";
                    InsertHeader += "VALUES(@ASIENTO,@PAQUETE,@TIPO_ASIENTO,@FECHA,@CONTABILIDAD,@NOTAS,@ESTADO,@PERMITIR_DESCUADRADO,@CONSERVAR_NUMERACION,@ACTUALIZAR_CONSECUTIVO,@FECHA_AUDITORIA)";
                    sqlcmd.Connection = conexion;
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.CommandText = InsertHeader;

                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.AddWithValue("@ASIENTO", SqlDbType.VarChar).Value = e.Asiento;
                    sqlcmd.Parameters.AddWithValue("@PAQUETE", SqlDbType.VarChar).Value = e.Paquete;
                    sqlcmd.Parameters.AddWithValue("@TIPO_ASIENTO", SqlDbType.VarChar).Value = e.Tipo_Asiento;
                    sqlcmd.Parameters.AddWithValue("@FECHA", SqlDbType.DateTime).Value = e.Fecha;
                    sqlcmd.Parameters.AddWithValue("@CONTABILIDAD", SqlDbType.VarChar).Value = e.Contabilidad;
                    sqlcmd.Parameters.AddWithValue("@NOTAS", SqlDbType.VarChar).Value = e.Notas;
                    sqlcmd.Parameters.AddWithValue("@ESTADO", SqlDbType.Int).Value = e.Estado;
                    sqlcmd.Parameters.AddWithValue("@PERMITIR_DESCUADRADO", SqlDbType.Char).Value = e.Permitir_Descuadrado;
                    sqlcmd.Parameters.AddWithValue("@CONSERVAR_NUMERACION", SqlDbType.Char).Value = e.Conservar_Numeracion;
                    sqlcmd.Parameters.AddWithValue("@ACTUALIZAR_CONSECUTIVO", SqlDbType.Char).Value = e.Actualizar_Consecutivo;
                    sqlcmd.Parameters.AddWithValue("@FECHA_AUDITORIA", SqlDbType.DateTime).Value = e.Fecha_Auditoria;
                }
                else {

                    String InertDetail = "INSERT INTO VCAMARA.EXACTUS_DIARIO(ASIENTO,CONSECUTIVO,CENTRO_COSTO,CUENTA_CONTABLE,FUENTE,REFERENCIA,MONTO_LOCAL,MONTO_DOLAR,MONTO_UNIDADES,NIT,DIMENSION1,DIMENSION2,DIMENSION3,DIMENSION4)";
                    InertDetail += "VALUES(@ASIENTO,@CONSECUTIVO,@CENTRO_COSTO,@CUENTA_CONTABLE,@FUENTE,@REFERENCIA,@MONTO_LOCAL,@MONTO_DOLAR,@MONTO_UNIDADES,@NIT,@DIMENSION1,@DIMENSION2,@DIMENSION3,@DIMENSION4)";

                    sqlcmd.Connection = conexion;
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.CommandText = InertDetail;

                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.AddWithValue("@ASIENTO", SqlDbType.VarChar).Value = e.Asiento;
                    sqlcmd.Parameters.AddWithValue("@CONSECUTIVO", SqlDbType.Int).Value = e.Consecutivo;
                    sqlcmd.Parameters.AddWithValue("@CENTRO_COSTO", SqlDbType.VarChar).Value = e.Centro_Costo;
                    sqlcmd.Parameters.AddWithValue("@CUENTA_CONTABLE", SqlDbType.VarChar).Value = e.Cuenta_Contable;
                    sqlcmd.Parameters.AddWithValue("@FUENTE", SqlDbType.VarChar).Value = e.Fuente;
                    sqlcmd.Parameters.AddWithValue("@REFERENCIA", SqlDbType.VarChar).Value = e.Referencia;
                    sqlcmd.Parameters.AddWithValue("@MONTO_LOCAL", SqlDbType.Decimal).Value = e.Monto_Local;
                    sqlcmd.Parameters.AddWithValue("@MONTO_DOLAR", SqlDbType.Decimal).Value = e.Monto_Dolar;
                    sqlcmd.Parameters.AddWithValue("@MONTO_UNIDADES", SqlDbType.Decimal).Value = e.Monto_Unidades;
                    sqlcmd.Parameters.AddWithValue("@NIT", SqlDbType.Char).Value = e.Nit;
                    sqlcmd.Parameters.AddWithValue("@DIMENSION1", SqlDbType.VarChar).Value = e.Dimension1;
                    sqlcmd.Parameters.AddWithValue("@DIMENSION2", SqlDbType.VarChar).Value = e.Dimension2;
                    sqlcmd.Parameters.AddWithValue("@DIMENSION3", SqlDbType.VarChar).Value = e.Dimension3;
                    sqlcmd.Parameters.AddWithValue("@DIMENSION4", SqlDbType.VarChar).Value = e.Dimension4;
                }
                resp = sqlcmd.ExecuteNonQuery();
            }catch (Exception ex)
            {

            }
            return resp/2;
        }
        private void SetUpdateEstadoExactusTransfer(eInterfaceContableVC e) { 
            Int32 resp = 0;
            try
            {
                String QueryUpdate = "UPDATE EXACTUS_CABECERA SET ESTADO_TRANSFERENCIA = 'T' WHERE NRO_CONTRATO = '"+e.Nro_Contrato+"' AND TIPO_INFO = '"+e.Tipo_Info+"' AND ANIO_VIGENTE = "+e.Anio_Vigente+" AND MES_VIGENTE = "+e.Mes_Vigente;
                       QueryUpdate += "UPDATE EXACTUS_DETALLE SET ESTADO_TRANSFERENCIA = 'T' WHERE NRO_CONTRATO = '" + e.Nro_Contrato + "' AND TIPO_INFO = '" + e.Tipo_Info + "' AND ANIO_VIGENTE = " + e.Anio_Vigente + " AND MES_VIGENTE = " + e.Mes_Vigente;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = conexion;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = QueryUpdate;

                resp = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally {
            }
        }
    }
}