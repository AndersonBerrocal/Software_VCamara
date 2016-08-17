using System.Collections.Generic;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.data;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Web;
using System;
using System.Reflection;
using NPOI.HSSF.Util;
using VidaCamara.DIS.Modelo.EEntidad;
using System.Linq;

namespace VidaCamara.DIS.Negocio
{
    public class nArchivoCargado
    {
        /// <summary>
        /// Devuelve la lista de historia lin det utilizando los parametros endicados por el usuario
        /// </summary>
        /// <param name="historiaLinCab"></param>
        /// <param name="historiaLinDet"></param>
        /// <param name="filterParam"></param>
        /// <param name="jtStartIndex"></param>
        /// <param name="jtPageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<HHistorialCargaArchivo_LinDet> listArchivoCargado(HistorialCargaArchivo_LinCab historiaLinCab,HistorialCargaArchivo_LinDet historiaLinDet, object[] filterParam,int jtStartIndex, int jtPageSize,string jtSorting, out int total)
        {
            return new dPagoCargado().listArchivoCargado(historiaLinCab,historiaLinDet, filterParam, jtStartIndex, jtPageSize, jtSorting, out total);
        }
        /// <summary>
        /// Crea un archivo excel acuerdo a los filtros especificados
        /// </summary>
        /// <param name="cab"></param>
        /// <param name="det"></param>
        /// <param name="filterParam"></param>
        /// <returns></returns>
        public string getDescargarConsulta(HistorialCargaArchivo_LinCab cab, NOMINA nomina, HistorialCargaArchivo_LinDet det, object[] filterParam)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                var nombreArchivo = "Archivo " + filterParam[0].ToString()+" "+DateTime.Now.ToString("yyyyMMdd");
                var rutaTemporal = @HttpContext.Current.Server.MapPath("~/Temp/Descargas/" + nombreArchivo + ".xlsx");
                int total;
                var tipoLinea = filterParam[0].ToString() == "NOMINA" ? "*" : "D";
                //new Utils.DeleteFile().deleteFile(HttpContext.Current.Server.MapPath(@"~/Utils/xlsxs/"));
                XSSFWorkbook book = new XSSFWorkbook();
                var contratoSis = new nContratoSis().listContratoByID(new CONTRATO_SYS() { IDE_CONTRATO = cab.IDE_CONTRATO});
                var reglaArchivo = new ReglaArchivo() { Archivo = filterParam[0].ToString(), TipoLinea = tipoLinea,NUM_CONT_LIC = Convert.ToInt32(contratoSis.NRO_CONTRATO),vigente = 1 };
                var listReglaArchivo = new nReglaArchivo().getListReglaArchivo(reglaArchivo, 0, 1000,"IdReglaArchivo ASC", out total);
                if (reglaArchivo.Archivo.Equals("0"))
                {
                    listReglaArchivo = listReglaArchivo.GroupBy(x => new { x.NombreCampo, x.TituloColumna })
                                   .Select(y=>new ReglaArchivo() {NombreCampo = y.Key.NombreCampo,TituloColumna = y.Key.TituloColumna }).ToList();
                }
                //crear el libro
                var sheet = book.CreateSheet(nombreArchivo);
                var rowCabecera = sheet.CreateRow(1);
                var headerStyle = helperStyle.setFontText(12, true, book);
                var bodyStyle = helperStyle.setFontText(11, false, book);
                //construir cabecera
                ICell cellCabecera;
                for (int i = 0; i < listReglaArchivo.Count; i++)
                {
                    cellCabecera = rowCabecera.CreateCell(i+1);
                    cellCabecera.SetCellValue(listReglaArchivo[i].TituloColumna);
                    cellCabecera.CellStyle = headerStyle;
                }
                //consultar datos segun los filtros especificados
                if (filterParam[0].ToString() == "NOMINA")
                {
                    var listNomina = new nNomina().listNominaConsulta(nomina, filterParam, 0, 100000, out total);
                    for (int i = 0; i < listNomina.Count; i++)
                    {
                        IRow rowBody = sheet.CreateRow(i + 2);
                        ICell cellBody;
                        for (int c = 0; c < listReglaArchivo.Count; c++)
                        {
                            cellBody = rowBody.CreateCell(c + 1);
                            var property = listNomina[i].GetType().GetProperty(listReglaArchivo[c].NombreCampo.ToString().Trim(), BindingFlags.Public | BindingFlags.Instance);
                            cellBody.SetCellValue(property.GetValue(listNomina[i],null) == null ? "" : property.GetValue(listNomina[i],null).ToString());
                            cellBody.CellStyle = bodyStyle;
                        }
                    }
                }
                else {
                    var listHistoriaLinDet = new dPagoCargado().listArchivoCargado(cab, det, filterParam, 0, 100000, "TipoLinea ASC", out total);
                    for (int i = 0; i < listHistoriaLinDet.Count; i++)
                    {
                        IRow rowBody = sheet.CreateRow(i + 2);
                        ICell cellBody;
                        for (int c = 0; c < listReglaArchivo.Count; c++)
                        {
                            cellBody = rowBody.CreateCell(c + 1);
                            var property = listHistoriaLinDet[i].GetType().GetProperty(listReglaArchivo[c].NombreCampo.ToString().Trim(), BindingFlags.Public | BindingFlags.Instance);
                            cellBody.SetCellValue(property.GetValue(listHistoriaLinDet[i],null) == null ? "" : property.GetValue(listHistoriaLinDet[i],null).ToString());
                            cellBody.CellStyle = bodyStyle;
                        }
                    }
                }
                if (File.Exists(rutaTemporal))
                    File.Delete(rutaTemporal);
                //guardar el archivo creado en memoria
                using (var file = new FileStream(rutaTemporal,FileMode.Create,FileAccess.ReadWrite))
                {
                    book.Write(file);
                    file.Close();
                    book.Close();
                }
                return rutaTemporal;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        public List<HistorialCargaArchivo_LinDet> listArchivoCargadoByArchivo(HistorialCargaArchivo_LinCab cab, object[] filterParam, int jtStartIndex, int jtPageSize, out int total)
        {
            return new dPagoCargado().listArchivoCargadoByArchivo(cab,filterParam,jtStartIndex,jtPageSize,out total);
        }
        private ICellStyle setFontText(short point, bool color, XSSFWorkbook book)
        {
            var font = book.CreateFont();
            font.FontName = "Calibri";
            font.Color = (IndexedColors.Black.Index);
            font.FontHeightInPoints = point;

            var style = book.CreateCellStyle();
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            if (color)
            {
                style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                style.FillPattern = FillPattern.SolidForeground;
            }
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            return style;
        }
    }
}
