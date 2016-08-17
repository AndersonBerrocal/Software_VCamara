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
    public class nInterfaceContable
    {
        public void createInterfaceContable(NOMINA nomina)
        {
            nomina = new nNomina().getNominaByArchivoId(nomina);
            var nombreLiqByNomina = new nArchivo().getArchivoByNomina(new Archivo() { NombreArchivo = nomina.Archivo.NombreArchivo });
            var cabecera = new dInterfaceContable().createInterfaceContableCabecera(nomina, nombreLiqByNomina);

            var asiento = new List<int>(){ 42, 26 };
            for (int i = 0; i < asiento.Count; i++)
            {
                new dInterfaceContable().createInterfaceContableDetalle(nomina, cabecera, asiento[i]);
            }
        }
        public List<HEXACTUS_DETALLE_SIS> listInterfaceContable(EXACTUS_CABECERA_SIS cabecera,TipoArchivo archivo, int index, int size, out int total)
        {
            return new dInterfaceContable().listInterfaceContable(cabecera,archivo,index,size,out total);
        }
        public string descargarExcel(EXACTUS_CABECERA_SIS cabecera,TipoArchivo archivo)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                int total;
                var listInterface = new dInterfaceContable().listInterfaceContable(cabecera, archivo,0, 100000, out total);
                //atributos del file
                var nombreArchivo = string.Format("Interface Provision_{0}_{1}",cabecera.IDE_CONTRATO,DateTime.Now.ToString("yyyyMMdd"));
                var rutaTemporal = @HttpContext.Current.Server.MapPath(string.Format("~/Temp/Descargas/{0}.xlsx", nombreArchivo));
                var book = new XSSFWorkbook();
                string[] columns = { "PAQUETE", "ASIENTO", "FECHA_REGISTRO", "TIPO_ASIENTO", "CONTABILIDAD", "FUENTE", "REFERENCIA", "CONTRIBUYENTE",
                                   "CENTRO_COSTO","CUENTA_CONTABLE","DebitoSoles","CreditoSoles","DebitoDolar","CreditoDolar","MONTO_UNIDADES","NIT","DIMENSION1","DIMENSION2","DIMENSION3","DIMENSION4","DIMENSION5"};
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
                for (int i = 0; i < listInterface.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2 + i);

                    ICell cellPaquete = rowBody.CreateCell(1);
                    cellPaquete.SetCellValue(listInterface[i].EXACTUS_CABECERA_SIS.PAQUETE);
                    cellPaquete.CellStyle = bodyStyle;

                    ICell cellAsiento = rowBody.CreateCell(2);
                    cellAsiento.SetCellValue(listInterface[i].EXACTUS_CABECERA_SIS.ASIENTO);
                    cellAsiento.CellStyle = bodyStyle;

                    ICell cellFechaReg = rowBody.CreateCell(3);
                    cellFechaReg.SetCellValue(listInterface[i].EXACTUS_CABECERA_SIS.FECHA.ToShortDateString());
                    cellFechaReg.CellStyle = bodyStyle;

                    ICell cellTipoAsiento = rowBody.CreateCell(4);
                    cellTipoAsiento.SetCellValue(listInterface[i].EXACTUS_CABECERA_SIS.TIPO_ASIENTO);
                    cellTipoAsiento.CellStyle = bodyStyle;

                    ICell cellContabilidad = rowBody.CreateCell(5);
                    cellContabilidad.SetCellValue(listInterface[i].EXACTUS_CABECERA_SIS.CONTABILIDAD);
                    cellContabilidad.CellStyle = bodyStyle;

                    ICell cellFuente = rowBody.CreateCell(6);
                    cellFuente.SetCellValue(listInterface[i].FUENTE);
                    cellFuente.CellStyle = bodyStyle;

                    ICell cellReferencia = rowBody.CreateCell(7);
                    cellReferencia.SetCellValue(listInterface[i].REFERENCIA);
                    cellReferencia.CellStyle = bodyStyle;

                    ICell cellContribuyente = rowBody.CreateCell(8);
                    cellContribuyente.SetCellValue(listInterface[i].CONTRIBUYENTE);
                    cellContribuyente.CellStyle = bodyStyle;

                    ICell cellCentroCosto = rowBody.CreateCell(9);
                    cellCentroCosto.SetCellValue(listInterface[i].CENTRO_COSTO);
                    cellCentroCosto.CellStyle = bodyStyle;

                    ICell cellCuentaCont = rowBody.CreateCell(10);
                    cellCuentaCont.SetCellValue(listInterface[i].CUENTA_CONTABLE);
                    cellCuentaCont.CellStyle = bodyStyle;

                    ICell cellDebitoSol = rowBody.CreateCell(11);
                    cellDebitoSol.SetCellValue(listInterface[i].DebitoSoles);
                    cellDebitoSol.CellStyle = bodyStyle;

                    ICell cellCreditoSol = rowBody.CreateCell(12);
                    cellCreditoSol.SetCellValue(listInterface[i].CreditoSoles);
                    cellCreditoSol.CellStyle = bodyStyle;

                    ICell cellDebitoDol = rowBody.CreateCell(13);
                    cellDebitoDol.SetCellValue(listInterface[i].DebitoDolar);
                    cellDebitoDol.CellStyle = bodyStyle;

                    ICell cellCreditoDol = rowBody.CreateCell(14);
                    cellCreditoDol.SetCellValue(listInterface[i].CreditoDolar);
                    cellCreditoDol.CellStyle = bodyStyle;

                    ICell cellMontoUnid = rowBody.CreateCell(15);
                    cellMontoUnid.SetCellValue(listInterface[i].MONTO_UNIDADES.ToString());
                    cellMontoUnid.CellStyle = bodyStyle;

                    ICell cellNit = rowBody.CreateCell(16);
                    cellNit.SetCellValue(listInterface[i].NIT);
                    cellNit.CellStyle = bodyStyle;

                    ICell cellDIMENSION1 = rowBody.CreateCell(17);
                    cellDIMENSION1.SetCellValue(listInterface[i].DIMENSION1);
                    cellDIMENSION1.CellStyle = bodyStyle;

                    ICell cellDIMENSION2 = rowBody.CreateCell(18);
                    cellDIMENSION2.SetCellValue(listInterface[i].DIMENSION2);
                    cellDIMENSION2.CellStyle = bodyStyle;

                    ICell cellDIMENSION3 = rowBody.CreateCell(19);
                    cellDIMENSION3.SetCellValue(listInterface[i].DIMENSION3);
                    cellDIMENSION3.CellStyle = bodyStyle;

                    ICell cellDIMENSION4 = rowBody.CreateCell(20);
                    cellDIMENSION4.SetCellValue(listInterface[i].DIMENSION4);
                    cellDIMENSION4.CellStyle = bodyStyle;

                    ICell cellDIMENSION5 = rowBody.CreateCell(21);
                    cellDIMENSION5.SetCellValue(listInterface[i].DIMENSION5);
                    cellDIMENSION5.CellStyle = bodyStyle;

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

        public string descargarExcelExport(EXACTUS_CABECERA_SIS cabecera, TipoArchivo tipoArchivo)
        {
            var helperStyle = new Helpers.excelStyle();
            try
            {
                int total;
                var listInterfaceParcial = new dInterfaceContable().listInterfaceContableParcial(cabecera, tipoArchivo, 0, 100000, out total);
                //atributos del file
                var nombreArchivo = string.Format("Interface Banco_{0}_{1}", cabecera.IDE_CONTRATO, DateTime.Now.ToString("yyyyMMdd"));
                var rutaTemporal = @HttpContext.Current.Server.MapPath(string.Format("~/Temp/Descargas/{0}.xlsx", nombreArchivo));
                var book = new XSSFWorkbook();
                string[] columns = {"CUENTA_BANCARIA","NUMERO","TIPO_DOCUMENTO","FECHA_DOCUMENTO","CONCEPTO","BENEFICIARIO","CONTRIBUYENTE",
                                    "MONTO","DETALLE","SUBTIPO","CENTRO_COSTO","CUENTA_CONTABLE","RUBRO_1","RUBRO_2","RUBRO_3","RUBRO_4","RUBRO_5","PAQUETE"};
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
                for (int i = 0; i < listInterfaceParcial.Count; i++)
                {
                    var rowBody = sheet.CreateRow(2 + i);

                    var numberInterface = listInterfaceParcial[i].NUMERO;
                    ICell cellCuentaBan = rowBody.CreateCell(1);
                    cellCuentaBan.SetCellValue(listInterfaceParcial[i].CUENTA_BANCARIA);
                    cellCuentaBan.CellStyle = bodyStyle;

                    ICell cellNumero = rowBody.CreateCell(2);
                    cellNumero.SetCellValue(string.Format("CB{0}{1}",new string('0',8-numberInterface.ToString().Length), numberInterface.ToString()));
                    cellNumero.CellStyle = bodyStyle;

                    ICell cellTipDoc= rowBody.CreateCell(3);
                    cellTipDoc.SetCellValue(listInterfaceParcial[i].TIPO_DOCUMENTO);
                    cellTipDoc.CellStyle = bodyStyle;

                    ICell cellFecDoc = rowBody.CreateCell(4);
                    cellFecDoc.SetCellValue(listInterfaceParcial[i].FECHA_DOCUMENTO.ToShortDateString());
                    cellFecDoc.CellStyle = bodyStyle;

                    ICell cellConcepto = rowBody.CreateCell(5);
                    cellConcepto.SetCellValue(listInterfaceParcial[i].CONCEPTO);
                    cellConcepto.CellStyle = bodyStyle;

                    ICell cellBenificiario = rowBody.CreateCell(6);
                    cellBenificiario.SetCellValue(listInterfaceParcial[i].BENEFICIARIO);
                    cellBenificiario.CellStyle = bodyStyle;


                    ICell cellContribuyente = rowBody.CreateCell(7);
                    cellContribuyente.SetCellValue(listInterfaceParcial[i].CONTRIBUYENTE);
                    cellContribuyente.CellStyle = bodyStyle;

                    ICell cellMonto = rowBody.CreateCell(8);
                    cellMonto.SetCellValue(listInterfaceParcial[i].MONTOSTR);
                    cellMonto.CellStyle = bodyStyle;

                    ICell cellDetalle = rowBody.CreateCell(9);
                    cellDetalle.SetCellValue(listInterfaceParcial[i].DETALLE);
                    cellDetalle.CellStyle = bodyStyle;

                    ICell cellSubTipo = rowBody.CreateCell(10);
                    cellSubTipo.SetCellValue(listInterfaceParcial[i].SUBTIPO.ToString());
                    cellSubTipo.CellStyle = bodyStyle;


                    ICell cellCentroCosto = rowBody.CreateCell(11);
                    cellCentroCosto.SetCellValue(listInterfaceParcial[i].CENTRO_COSTO);
                    cellCentroCosto.CellStyle = bodyStyle;

                    ICell cellCuentaCont = rowBody.CreateCell(12);
                    cellCuentaCont.SetCellValue(listInterfaceParcial[i].CUENTA_CONTABLE);
                    cellCuentaCont.CellStyle = bodyStyle;

                    ICell cellRubro1 = rowBody.CreateCell(13);
                    cellRubro1.SetCellValue(listInterfaceParcial[i].RUBRO_1);
                    cellRubro1.CellStyle = bodyStyle;

                    ICell cellRubro2 = rowBody.CreateCell(14);
                    cellRubro2.SetCellValue(listInterfaceParcial[i].RUBRO_2);
                    cellRubro2.CellStyle = bodyStyle;

                    ICell cellRubro3 = rowBody.CreateCell(15);
                    cellRubro3.SetCellValue(listInterfaceParcial[i].RUBRO_3);
                    cellRubro3.CellStyle = bodyStyle;

                    ICell cellRubro4 = rowBody.CreateCell(16);
                    cellRubro4.SetCellValue(listInterfaceParcial[i].RUBRO_4);
                    cellRubro4.CellStyle = bodyStyle;

                    ICell cellRubro5 = rowBody.CreateCell(17);
                    cellRubro5.SetCellValue(listInterfaceParcial[i].RUBRO_5);
                    cellRubro5.CellStyle = bodyStyle;

                    ICell cellPaquete = rowBody.CreateCell(18);
                    cellPaquete.SetCellValue(listInterfaceParcial[i].PAQUETE);
                    cellPaquete.CellStyle = bodyStyle;

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
                throw (new Exception(ex.Message));
            }
        }

        public List<HEXACTUS_DETALLE_EXPORT_SIS> listInterfaceContableParcial(EXACTUS_CABECERA_SIS cabecera, TipoArchivo tipoArchivo, int index, int size, out int total)
        {
            return new dInterfaceContable().listInterfaceContableParcial(cabecera, tipoArchivo, index, size, out total);
        }

        public void createInterfaceContableExport(NOMINA nOMINA)
        {
            nOMINA = new nNomina().getNominaByArchivoId(nOMINA);
            var nombreLiqByNomina = new nArchivo().getArchivoByNomina(new Archivo() { NombreArchivo = nOMINA.Archivo.NombreArchivo });
            new dInterfaceContable().createInterfaceContableExport(nOMINA, nombreLiqByNomina);
        }

        public bool transferirInterfaceContable(EXACTUS_CABECERA_SIS contrato, TipoArchivo tipoArchivo)
        {
            var exactusCabecera = new dInterfaceContable().getExactusCabecera(contrato,tipoArchivo);
            var interfaceContable = new dInterfaceContable();
            var response = true;
            try
            {
                foreach (var item in exactusCabecera)
                {
                    if (interfaceContable.createCabeceraInRemoteExactus(item))
                    {
                        interfaceContable.createDetalleInRemote(item);
                        interfaceContable.actualizarEstadoTransferido(item);
                    }
                    else
                    {
                        response = false;
                        break;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}
