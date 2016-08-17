using System;
using System.Web.Routing;

namespace VidaCamara.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RoutingData(RouteTable.Routes);
        }
        private void RoutingData(RouteCollection coleccion) 
        {
            coleccion.MapPageRoute("login", "Login", "~/WebPage/Inicio/frmLogin.aspx");
            coleccion.MapPageRoute("Inicio", "Inicio", "~/WebPage/Inicio/frmInicio.aspx");
            coleccion.MapPageRoute("Parametros", "Parametros", "~/WebPage/Mantenimiento/frmGeneral.aspx");
            coleccion.MapPageRoute("Error", "Error", "~/WebPage/Inicio/frmError.aspx");
            coleccion.MapPageRoute("CargaDatos", "CargaDatos", "~/WebPage/ModuloSBS/Operaciones/frmCargaDatos.aspx");
            coleccion.MapPageRoute("OperacionManual", "OperacionManual", "~/WebPage/ModuloSBS/Operaciones/frmOperacionManual.aspx");
            coleccion.MapPageRoute("RegistroDatos", "RegistroDatos", "~/WebPage/ModuloSBS/Operaciones/frmRegistroDatos.aspx");
            coleccion.MapPageRoute("ProcesoOperacion", "ProcesoOperacion", "~/WebPage/ModuloSBS/Operaciones/frmProcesaOperacion.aspx");
            coleccion.MapPageRoute("CierreOperacion", "CierreOperacion", "~/WebPage/ModuloSBS/Operaciones/frmCierreProceso.aspx");
            coleccion.MapPageRoute("InterfaceContable", "InterfaceContable", "~/WebPage/ModuloSBS/Operaciones/frmInterfaceContable.aspx");
            coleccion.MapPageRoute("Usuarios", "Usuarios", "~/WebPage/Mantenimiento/frmUsuario.aspx");
            coleccion.MapPageRoute("Tablas", "Tablas", "~/WebPage/Mantenimiento/frmTabla.aspx");
            coleccion.MapPageRoute("Operaciones", "Operaciones", "~/WebPage/ModuloSBS/Consultas/frmConsultaOperaciones.aspx");
            coleccion.MapPageRoute("Comprobantes", "Comprobantes", "~/WebPage/ModuloSBS/Consultas/frmConsultaComprobante.aspx");
            coleccion.MapPageRoute("Informes", "Informes", "~/WebPage/ModuloSBS/Consultas/frmInformes.aspx");

            coleccion.MapPageRoute("CargaDatosDIS", "CargaDatosDIS", "~/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx");
            coleccion.MapPageRoute("ApruebaCarga", "ApruebaCarga", "~/WebPage/ModuloDIS/Operaciones/frmCargaAprobacion.aspx");
            coleccion.MapPageRoute("Telebankig", "Telebankig", "~/WebPage/ModuloDIS/Operaciones/frmTelebankig.aspx");
            coleccion.MapPageRoute("InterfaceContbleSIS", "InterfaceContbleSIS", "~/WebPage/ModuloDIS/Operaciones/frmInterfaceContableSIS.aspx");
            coleccion.MapPageRoute("SegConsulta", "SegConsulta", "~/WebPage/ModuloDIS/Consultas/frmSegConsulta.aspx");
            coleccion.MapPageRoute("SegDescarga", "SegDescarga", "~/WebPage/ModuloDIS/Consultas/frmSegDescarga.aspx");
            coleccion.MapPageRoute("SegLogAdmin", "SegLogAdmin", "~/WebPage/ModuloDIS/Consultas/frmSegLogAdmin.aspx");

            coleccion.MapPageRoute("ReglaArchivo", "ReglaArchivo", "~/WebPage/Mantenimiento/frmReglaArchivo.aspx");
            coleccion.MapPageRoute("TipoCambio", "TipoCambio", "~/WebPage/Mantenimiento/frmTipoCambio.aspx");
            


        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}