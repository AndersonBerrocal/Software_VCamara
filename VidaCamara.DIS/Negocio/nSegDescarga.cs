using NPOI.HSSF.Util;
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
    public class nSegDescarga
    {
        public List<ESegDescarga> listSegDescarga(CONTRATO_SYS contrato, object[] filters, int jtStartIndex, int jtPageSize,string jtSorting, out int total)
        {
            return new dSegDescarga().listSegDescarga(contrato,filters,jtStartIndex,jtPageSize, jtSorting, out total);
        }

        public string descargarConsultaExcel(CONTRATO_SYS contrato, object[] filters)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                var nombreArchivo = "Descarga " + filters[0].ToString() + " " + DateTime.Now.ToString("yyyyMMdd");
                var rutaTemporal = @HttpContext.Current.Server.MapPath("~/Temp/Descargas/" + nombreArchivo + ".xlsx");
                int total;
                var book = new XSSFWorkbook();
                string[] columns = {"Archivo","Fecha Carga","Usuario","Nro Lineas","Estado","Moneda","Importe" };
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

                var listSegDescarga = new nSegDescarga().listSegDescarga(contrato, filters, 0, 100000, "Estado ASC", out total);
                for (int i = 0; i < listSegDescarga.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2+i);

                    ICell cellNombre = rowBody.CreateCell(1);
                    cellNombre.SetCellValue(listSegDescarga[i].NombreArchivo);
                    cellNombre.CellStyle = bodyStyle;

                    ICell cellFecCarga = rowBody.CreateCell(2);
                    cellFecCarga.SetCellValue(listSegDescarga[i].FechaCarga.ToShortDateString());
                    cellFecCarga.CellStyle = bodyStyle;

                    ICell cellUsuario = rowBody.CreateCell(3);
                    cellUsuario.SetCellValue(listSegDescarga[i].Usuario);
                    cellUsuario.CellStyle = bodyStyle;

                    ICell cellNroLinea = rowBody.CreateCell(4);
                    cellNroLinea.SetCellValue(listSegDescarga[i].NroLineas);
                    cellNroLinea.CellStyle = bodyStyle;

                    ICell cellEstado = rowBody.CreateCell(5);
                    cellEstado.SetCellValue(listSegDescarga[i].Estado);
                    cellEstado.CellStyle = bodyStyle;

                    ICell cellMoneda = rowBody.CreateCell(6);
                    cellMoneda.SetCellValue(listSegDescarga[i].Moneda);
                    cellMoneda.CellStyle = bodyStyle;

                    ICell cellImporte = rowBody.CreateCell(7);
                    cellImporte.SetCellValue(listSegDescarga[i].Importe);
                    cellImporte.CellStyle = bodyStyle;

                }
                if(File.Exists(rutaTemporal))
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
