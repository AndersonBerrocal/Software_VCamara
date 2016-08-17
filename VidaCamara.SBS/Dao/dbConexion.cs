using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VidaCamara.SBS.Dao
{
    public class dbConexion
    {
    #region "Objeto de Conexion"
        private SqlConnection _dataBase;

        public SqlConnection miconexion
        {
            get
            {
                Cargar();
                return _dataBase;
            }
        }

        private void Cargar()
        {
            try
            {
                if (_dataBase != null)
                {
                    if (_dataBase.State != ConnectionState.Closed)
                    {
                        _dataBase.Close();
                        _dataBase.Dispose();
                    }
                }

                _dataBase = new SqlConnection();
                _dataBase.ConnectionString = ConfigurationManager.AppSettings.Get("CnnBD").ToString();
                _dataBase.Open();
            }
            catch (Exception ex)
            { }
        }

#endregion


    #region "Store Procedures"

            /*seccion sql  procedures VC*/
        public String sSinsertGeneral = "SP_INS_GENERAL";
        public String sSinsertarContrato = "SP_INS_CONTRATO";
        public String sSinsertarContratoDet = "SP_INS_CONTRATO_DETALLE";
        public String sSinsertarDatoM = "SP_INS_DATO_M";
        public String sSinsertarDatoA = "SP_INS_DATO_A";
        public string sValidaRegistroManual = "SP_VALIDA_REGISTRO_MANUAL";
        public String sSinsertarConcepto = "SP_INS_CONCEPTO";
        public String sSinsertarOperacionManual = "SP_INS_OPERACION_MANUAL";
        public String sSinsertarUsuario = "SP_INS_USUARIO";

        public String sSvalidarUsuario = "SP_VAL_USUARIO";

        public String sProcesaPago = "SP_PRC_PAGO";
        public String sProcesaPrima = "SP_PRC_PRIMA";
        public String sProcesaIbnr = "SP_PRC_IBNR";
        public String sProcesaRsp = "SP_PRC_RSP";
        //procedure para cierre de operaciones
        public String sSinsertarCierrePago = "SP_CIE_PAGO";
        public String sSinsertarCierreRsp = "SP_CIE_RSP";
        public String sSinsertarCierreIbnr = "SP_CIE_IBNR";
        public String sSinsertarCierrePrima = "SP_CIE_PRIMA";
        //interface contable
        public String sProcesaContabilidad = "SP_CONT_RSP_Y_IBNR_Y_PAGO";
        public String sProcesaExactusCabecera = "SP_INS_EXACTUS_CABECERA";
        public String sProcesaContabilidadPrima = "SP_CONT_PRIMA";
        //update
        public String sSactualizarGeneral = "SP_UPD_GENERAL";
        public String sSactualizarContratoDet = "SP_UPD_CONTRATO_DETALLE";
        public String sSactualizarContrato = "SP_UPD_CONTRATO";
        public String sSactualizarConcepto = "SP_UPD_CONCEPTO";
        public String sSactualizarDatoM = "SP_UPD_DATO_M";
        public String sSactualizarUsuario = "SP_UPD_USUARIO";
        public String sSactualizarAccesoUsuario = "SP_UPD_ACCESO_USUARIO";
        //select
        public String sSelectContrato = "SP_SEL_CONTRATO";
        public String sSelectContratoDetalle = "SP_SEL_CONTRATO_DETALLE";
        public String sSselectConcepto = "SP_SEL_CONCEPTO";
        public String sSelectSepelio = "SP_SEL_SEPELIO";
        public String sSelectSepelioTmp = "SP_SEL_SEPELIO_TMP";
        public String sSelectDatoM = "SP_SEL_DATO_M";
        public String sSelectDatoMGrid = "SP_SEL_DATO_M_GRID";
        public String sSelectRspTmp = "SP_SEL_RSP_TMP";
        public String sSelectRsp = "SP_SEL_RSP";
        public String sSelectComprobante = "SP_SEL_COMPROBANTE";
        public String sSelectOperacion = "SP_SEL_OPERACION";
        public String sSelectCierreProceso = "SP_SEL_CIERRE_PROCESO";
        public String sSelecionarUsuario = "SP_SEL_USUARIO";
        public String sSelectAccesoUsuario = "SP_SEL_ACCESO_USUARIO";
        public String sSelectAccesoUsuarioSession = "SP_SEL_ACCESO_PAGINA_SESSION";
        public String sSelectCountDatoA = "SP_CON_DATO_A_EXISTE";

        //PROCEDURE DE PROCESA OPERACION
        public String sSelectOperacionDetalle = "SP_SEL_OPERACION_DETALLE";
        public String sSelectValidarCierre = "SP_SEL_VALIDAR_CIERRE";
        public String sSelectExactus = "SP_SEL_EXACTUS";

        //PROCEDURE TRANSFER EXACTUS
        public String sTransferExactus = "SP_TRANSFER_EXACTUS";


        //PROCEDURE DE SALIDA DE DATOS
        public String sSelectTotalOperacion = "SP_SEL_TOTAL_REGISTRO";

        //PROCEDURE DE EXPORT DATA
        public String ePselectAnexo = "SP_SEL_ANEXO";
        public String ePselectEs18A = "SP_SEL_ES18A";
        public String ePselectEs18B = "SP_SEL_ES18B";
        public String ePselectEs18C = "SP_SEL_ES18C";
        public String ePselectEs18D = "SP_SEL_ES18D";
        public String ePselectEs18E = "SP_SEL_ES18E";
        public String ePselectEs18F = "SP_SEL_ES18F";
        public String ePselectModelo1 = "SP_SEL_MODELO1";
        public String ePselectModelo2 = "SP_SEL_MODELO2";

        public String sDeleteCierreProceso = "SP_DEL_CIERRE_PROCESO";

        //delte
        public String sDeleteExactus = "SP_DEL_EXACTUS_CONTABLE";

        /*--DRIVERA Ini--*/
        public String sSelectContratoSys = "SP_SEL_CONTRATO_SYS";
        public String sSinsertarContratoSys = "SP_INS_CONTRATO_SYS";
        public String sSactualizarContratoSys = "SP_UPD_CONTRATO_SYS";
        /*--DRIVERA Fin--*/

    #endregion

    }
}

