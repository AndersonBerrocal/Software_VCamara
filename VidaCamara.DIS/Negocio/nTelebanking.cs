using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using VidaCamara.DIS.data;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;

namespace VidaCamara.DIS.Negocio
{
    public class nTelebanking
    {
        #region variables
        private string interfaceBanco = "I07";
        private string interfaceGeneral = "I06";
        private string aprobarInterfaProvision = "A03";
        private string aprobarInterfaBanco = "A04";
        #endregion variables
        public List<EGeneraTelebankig> listTelebanking(NOMINA nomina, int jtStartIndex, int jtPageSize,string sorting,string formatoMoneda, out int total)
        {
            return new dTelebanking().listTelebanking(nomina, jtStartIndex, jtPageSize, sorting, formatoMoneda,out total);
        }

        public List<EGeneraTelebankig> listTelebankingByArchivoId(NOMINA nomina,string formatoMoneda)
        {
            return new dTelebanking().listTelebankingByArchivoId(nomina, formatoMoneda);
        }

        public string descargarExcelTelebankig(NOMINA nomina, string formatoMoneda)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                int total;
                var listDescarga = new dTelebanking().listTelebanking(nomina, 0, 100000, "NombreArchivo ASC", formatoMoneda, out total);
                //atributos del file
                var nombreArchivo = string.Format("Nomina {0}_{1}", DateTime.Now.ToString("yyyyMMdd"),nomina.IDE_CONTRATO.ToString());
                var rutaTemporal = @HttpContext.Current.Server.MapPath(string.Format("~/Temp/Descargas/{0}.xlsx", nombreArchivo));
                var book = new XSSFWorkbook();
                string[] columns = { "NombreArchivo", "Fecha Operación", "Moneda", "Importe"};
                var sheet = book.CreateSheet(nombreArchivo);
                var rowBook = sheet.CreateRow(1);
                var headerStyle = helperStyle.setFontText(12, true, book);
                var bodyStyle = helperStyle.setFontText(11, false, book);
                ICell cellBook;
                for (int i = 0; i < columns.Length; i++)
                {
                    cellBook = rowBook.CreateCell(i + 1);
                    cellBook.SetCellValue(columns[i]);
                    cellBook.CellStyle = headerStyle;
                }
                for (int i = 0; i < listDescarga.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2 + i);

                    ICell cellArchivo = rowBody.CreateCell(1);
                    cellArchivo.SetCellValue(listDescarga[i].NombreArchivo);
                    cellArchivo.CellStyle = bodyStyle;

                    ICell cellFechaCarga = rowBody.CreateCell(2);
                    cellFechaCarga.SetCellValue(listDescarga[i].FechaOperacion.ToShortDateString());
                    cellFechaCarga.CellStyle = bodyStyle;

                    ICell cellMoneda = rowBody.CreateCell(3);
                    cellMoneda.SetCellValue(listDescarga[i].Moneda);
                    cellMoneda.CellStyle = bodyStyle;

                    ICell cellTotalRegistros = rowBody.CreateCell(4);
                    cellTotalRegistros.SetCellValue(listDescarga[i].Importe);
                    cellTotalRegistros.CellStyle = bodyStyle;
                }
                if (File.Exists(rutaTemporal))
                    File.Delete(rutaTemporal);
                using (var file = new FileStream(rutaTemporal, FileMode.Create, FileAccess.ReadWrite))
                {
                    book.Write(file);
                    file.Close();
                    book.Close();
                }

                return rutaTemporal;
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        public void aprobarFinalTelebanking(NOMINA nOMINA)
        {
            //llamada para generar los asientos contables respectivos
            new nInterfaceContable().createInterfaceContableExport(nOMINA);
            new dTelebanking().aprobarFinalTelebanking(nOMINA);
            ////crear log de operacion tanto para genera telebankig y interface contable
            var contratoFromNomina = new nNomina().getNominaByArchivoId(nOMINA);
            new nLogOperacion().setLLenarEntidad(contratoFromNomina.IDE_CONTRATO, "A", aprobarInterfaBanco, nOMINA.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(), "Archivo");
            new nLogOperacion().setLLenarEntidad(contratoFromNomina.IDE_CONTRATO, "I", interfaceBanco, nOMINA.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(), "Archivo");
        }

        public void aprobarTelebanking(NOMINA nOMINA)
        {
            //llamada para generar los asientos contables respectivos
            new nInterfaceContable().createInterfaceContable(nOMINA);
            new dTelebanking().aprobarTelebanking(nOMINA);
            //crear log de operacion tanto para genera telebankig y interface contable
            var contratoFromNomina = new nNomina().getNominaByArchivoId(nOMINA);
            new nLogOperacion().setLLenarEntidad(contratoFromNomina.IDE_CONTRATO, "A", aprobarInterfaProvision, nOMINA.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(), "Archivo");
            new nLogOperacion().setLLenarEntidad(contratoFromNomina.IDE_CONTRATO, "I", interfaceGeneral, nOMINA.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(), "Archivo");
        }
    }
}
