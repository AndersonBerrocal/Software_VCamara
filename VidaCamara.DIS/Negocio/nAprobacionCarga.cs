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
    public class nAprobacionCarga
    {
        public List<EAprobacionCarga> listApruebaCarga(CONTRATO_SYS contrato, int jtStartIndex, int jtPageSize,string sorting,object[] filters, out int total)
        {
            return new dAprobacionCarga().listApruebaCarga(contrato,jtStartIndex,jtPageSize,sorting, filters, out total);
        }

        public void actualizarEstado(HistorialCargaArchivo_LinCab historialCargaArchivo_LinCab,Archivo archivo)
        {
            new nLogOperacion().setLLenarEntidad(Convert.ToInt32(historialCargaArchivo_LinCab.IDE_CONTRATO), "A", "A06", archivo.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(),"Archivo");
            new dAprobacionCarga().actualizarEstado(historialCargaArchivo_LinCab);
        }

        public void actulaizarArchivoIdNomina(Archivo archivo)
        {
            new dAprobacionCarga().actualizarArchivoIdNomina(archivo);
        }

        public void eliminarPagoYNomina(HistorialCargaArchivo_LinCab historialCargaArchivo_LinCab,Archivo archivo)
        {
            new nLogOperacion().setLLenarEntidad(Convert.ToInt32(historialCargaArchivo_LinCab.IDE_CONTRATO), "E", "E01", archivo.ArchivoId.ToString(), HttpContext.Current.Session["username"].ToString(),"Archivo");
            new dAprobacionCarga().eliminarPagoYNomina(historialCargaArchivo_LinCab);
        }

        //public void actualizaEstadoArchivo(Archivo archivo)
        //{
        //    new dAprobacionCarga().actualizaEstadoArchivo(archivo);
        //}

        public List<eAprobacionCargaDetalle> listApruebaCargaDetalle(HistorialCargaArchivo_LinCab linCab, object[] filters)
        {
            return new dAprobacionCarga().listApruebaCargaDetalle(linCab, filters);
        }

        public string descargarExcelAprueba(CONTRATO_SYS contratoSis, object[] filtersNow)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                int total;
                var listDescarga = new dAprobacionCarga().listApruebaCarga(contratoSis, 0, 100000, "IdLinCab ASC", filtersNow, out total);
                //atributos del file
                var nombreArchivo = string.Format("Aprueba {0}_{1}_{2}",filtersNow[0].ToString(),DateTime.Now.ToString("yyyyMMdd"),contratoSis.IDE_CONTRATO.ToString());
                var rutaTemporal = @HttpContext.Current.Server.MapPath(string.Format("~/Temp/Descargas/{0}.xlsx",nombreArchivo));
                var book = new XSSFWorkbook();
                string[] columns = {"NombreArchivo","Fecha Carga","Mondeda","TotalRegistros","TotalImporte","FechaCreacción","Usuario"};
                var sheet = book.CreateSheet(nombreArchivo);
                var rowBook = sheet.CreateRow(1);
                var headerStyle = helperStyle.setFontText(12, true, book);
                var bodyStyle = helperStyle.setFontText(11, false, book);
                ICell cellBook;
                for (int i = 0; i < columns.Length; i++)
                {
                    cellBook = rowBook.CreateCell(i+1);
                    cellBook.SetCellValue(columns[i]);
                    cellBook.CellStyle = headerStyle;
                }
                for (int i = 0; i < listDescarga.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2+i);

                    ICell cellArchivo = rowBody.CreateCell(1);
                    cellArchivo.SetCellValue(listDescarga[i].NombreArchivo);
                    cellArchivo.CellStyle = bodyStyle;

                    ICell cellFechaCarga = rowBody.CreateCell(2);
                    cellFechaCarga.SetCellValue(listDescarga[i].FechaCarga.ToShortDateString());
                    cellFechaCarga.CellStyle = bodyStyle;

                    ICell cellMoneda = rowBody.CreateCell(3);
                    cellMoneda.SetCellValue(listDescarga[i].moneda);
                    cellMoneda.CellStyle = bodyStyle;

                    ICell cellTotalRegistros = rowBody.CreateCell(4);
                    cellTotalRegistros.SetCellValue(listDescarga[i].TotalRegistros);
                    cellTotalRegistros.CellStyle = bodyStyle;

                    ICell cellTotalImporte = rowBody.CreateCell(5);
                    cellTotalImporte.SetCellValue(listDescarga[i].TotalImporte);
                    cellTotalImporte.CellStyle = bodyStyle;

                    ICell cellFechaInfo = rowBody.CreateCell(6);
                    cellFechaInfo.SetCellValue(listDescarga[i].FechaInfo.ToShortDateString());
                    cellFechaInfo.CellStyle = bodyStyle;

                    ICell cellUsuReg = rowBody.CreateCell(7);
                    cellUsuReg.SetCellValue(listDescarga[i].UsuReg);
                    cellUsuReg.CellStyle = bodyStyle;
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
                
                throw;
            }
           
            
        }
    }
}
