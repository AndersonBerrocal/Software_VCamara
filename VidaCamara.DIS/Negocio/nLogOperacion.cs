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
    public class nLogOperacion
    {
        private DateTime FechEven = DateTime.Now;
        private String CodiCnx = String.Empty;
        //este el el metodo generico para llamar desde cualquier parte de la aplicacion
        //ejemplo
        //llena la entidad
        //var operacion = LogOperacion(){agregar sus propiedades menos (FechEven,CodiCnx)}
        //luego instacia la clase nLogOperacion
        //var negocio = nLogOperacion().setGuardarLogOperacion(operacion);
        public long setGuardarLogOperacion(LogOperacion log)
        { 
            log.FechEven = FechEven;
            log.CodiCnx = CodiCnx;
            return new dLogOperacion().setGuardarLogOperacion(log);
        }

        public void setLLenarEntidad(int IdeContrato,string TipoOper,string CodiEven, string CodiOper,string CodiUsu,string Entidad)
        {
            var entity = new LogOperacion()
            {
                IDE_CONTRATO = IdeContrato,
                TipoOper = TipoOper,
                CodiEven = CodiEven,
                CodiOper = CodiOper,
                CodiUsu = CodiUsu,
                Entidad = Entidad
            };
            this.setGuardarLogOperacion(entity);
        }

        public List<HLogOperacion> getListLogOperacion(HLogOperacion log, int jtStartIndex, int jtPageSize,object[] filters, out int total) 
        {
            return new dLogOperacion().getListLogOperacion(log, jtStartIndex, jtPageSize, filters, out total);
        }
        public string descargarConsultaExcel(HLogOperacion log, object[] filters)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                var nombreArchivo = string.Format("Log {0}_{1}",log.IDE_CONTRATO,DateTime.Now.ToString("yyyyMMdd"));
                var rutaTemporal = @HttpContext.Current.Server.MapPath("~/Temp/Descargas/" + nombreArchivo + ".xlsx");
                int total;
                var book = new XSSFWorkbook();
                string[] columns = { "Contrato","Tabla - ID" ,"Tipo evento", "Fecha  evento", "Evento","Columna - Dato", "Usuario" };
                var sheet = book.CreateSheet();
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
                var listLogOperacion = getListLogOperacion(log, 0, 100000, filters, out total);
                for (int i = 0; i < listLogOperacion.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2 + i);

                    ICell cellContrato = rowBody.CreateCell(1);
                    cellContrato.SetCellValue(listLogOperacion[i].CONTRATO_SYS.DES_CONTRATO);
                    cellContrato.CellStyle = bodyStyle;

                    ICell cellTablaId = rowBody.CreateCell(2);
                    cellTablaId.SetCellValue(listLogOperacion[i].Tabla);
                    cellTablaId.CellStyle = bodyStyle;

                    ICell cellTipoEvento = rowBody.CreateCell(3);
                    cellTipoEvento.SetCellValue(listLogOperacion[i].TipoEvento);
                    cellTipoEvento.CellStyle = bodyStyle;

                    ICell cellFechaEvento = rowBody.CreateCell(4);
                    cellFechaEvento.SetCellValue(listLogOperacion[i].FechEven.ToString());
                    cellFechaEvento.CellStyle = bodyStyle;

                    ICell cellEvento = rowBody.CreateCell(5);
                    cellEvento.SetCellValue(listLogOperacion[i].Evento);
                    cellEvento.CellStyle = bodyStyle;

                    ICell cellColDato = rowBody.CreateCell(6);
                    cellColDato.SetCellValue(listLogOperacion[i].Columna);
                    cellColDato.CellStyle = bodyStyle;

                    ICell cellUsuario = rowBody.CreateCell(7);
                    cellUsuario.SetCellValue(listLogOperacion[i].CodiUsu);
                    cellUsuario.CellStyle = bodyStyle;
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
