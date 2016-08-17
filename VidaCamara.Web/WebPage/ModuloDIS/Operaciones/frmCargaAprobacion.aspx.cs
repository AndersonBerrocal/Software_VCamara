using System;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Negocio;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.Web.WebPage.ModuloDIS.Operaciones
{
    public partial class CargaAprobacion : System.Web.UI.Page
    {
        #region VARIABLES
        static object[] filters = new object[1];//[1]fformato moneda
        static int total;
        static bValidarAcceso accesso = new bValidarAcceso();
        #endregion VARIABLES
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["pagina"] = "OTROS";
            if (Session["username"] == null)
                Response.Redirect("Login?go=0");
            else
            {
                if (!accesso.GetValidarAcceso(Request.QueryString["go"]))
                {
                    Response.Redirect("Error");
                }
            }
            if (!IsPostBack)
            {
                var concepto = new bTablaVC();
                SetLLenadoContrato();
                concepto.SetEstablecerDataSourceConcepto(ddl_tipo_archivo, "17");
                txt_fecha_inicio.Text = DateTime.Now.ToShortDateString();
                txt_fecha_fin.Text = DateTime.Now.ToShortDateString();
                filters[0] = ConfigurationManager.AppSettings.Get("Float").ToString();
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listApruebaCarga(int jtStartIndex, int jtPageSize, string jtSorting, CONTRATO_SYS contrato,object[] filtersP)
        {
            filtersP[3] = filters[0].ToString();
            var negocio = new nAprobacionCarga();
            return new { Result = "OK", Records = negocio.listApruebaCarga(contrato, jtStartIndex, jtPageSize,jtSorting, filtersP, out total), TotalRecordCount = total };
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object listApruebaCargaDetalle(int IdLinCab)
        {
            var negocio = new nAprobacionCarga();
            return new { Result = "OK", Records = negocio.listApruebaCargaDetalle(new HistorialCargaArchivo_LinCab() { IdHistorialCargaArchivoLinCab = IdLinCab },filters) };
        }
         
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object setAprobar(int linCabId, int IdeContrato,int ArchivoId)
        {
            try
            {
                new nAprobacionCarga().actualizarEstado(new HistorialCargaArchivo_LinCab() { IDE_CONTRATO = IdeContrato, IdHistorialCargaArchivoLinCab = linCabId },new Archivo() { ArchivoId = ArchivoId});
                return new { Result = true };
            }
            catch (Exception ex)
            {
                return new { Result = ex.Message };
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object setEliminar(int linCabId, int IdeContrato, int ArchivoId)
        {
            try
            {
                new nAprobacionCarga().eliminarPagoYNomina(new HistorialCargaArchivo_LinCab() { IDE_CONTRATO = IdeContrato, IdHistorialCargaArchivoLinCab = linCabId }, new Archivo() { ArchivoId = ArchivoId });
                return new { Result = true };
            }
            catch (Exception ex)
            {
                return new { Result = ex.Message };
            }
        }

        private void SetLLenadoContrato()
        {
            var list = new VidaCamara.SBS.Utils.Utility().getContratoSys(out total);
            ddl_contrato.DataSource = list;
            ddl_contrato.DataTextField = "_des_Contrato";
            ddl_contrato.DataValueField = "_ide_contrato";
            ddl_contrato.DataBind();
            ddl_contrato.Items.Insert(0, new ListItem("Seleccione ----", "0"));
        }

        protected void btn_exportar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var contratoSis = new CONTRATO_SYS() { IDE_CONTRATO = Convert.ToInt32(ddl_contrato.SelectedItem.Value) };
            var filtersNow = new object[4] {ddl_tipo_archivo.SelectedItem.Value,txt_fecha_inicio.Text,txt_fecha_inicio.Text,filters[0].ToString() };
            var filePath = new nAprobacionCarga().descargarExcelAprueba(contratoSis,filtersNow);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}",Path.GetFileName(filePath)));
            Response.TransmitFile(filePath);
            Response.End();
        }
    }
}