using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using VidaCamara.DIS.Modelo;
using VidaCamara.DIS.Modelo.EEntidad;
using System.Data;

namespace VidaCamara.DIS.data
{
    public class dInterfaceContable
    {
        internal void createInterfaceContableDetalle(NOMINA nomina, EXACTUS_CABECERA_SIS cabecera, int asiento)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var respuesta = db.pa_create_cuenta_42_26_sis(cabecera.IDE_EXACTUS_CABECERA_SIS,cabecera.ASIENTO,nomina.ArchivoId, asiento).FirstOrDefault();
                    if (respuesta.Equals("ERROR"))
                    {
                        throw(new ArgumentException("Ocurrió un error cuando intentaba buscar el tipo de cambio."));

                    }
                }
            }
            catch (System.Exception ex)
            {
                throw(new System.Exception(ex.Message));
            }
        }

        internal EXACTUS_CABECERA_SIS createInterfaceContableCabecera(NOMINA nomina, Archivo archivo)
        {
            try
            {
                var cabecera = new EXACTUS_CABECERA_SIS()
                {
                    IDE_CONTRATO = nomina.IDE_CONTRATO,
                    ArchivoId = (int)nomina.ArchivoId,
                    TIPO_ARCHIVO = archivo.NombreArchivo.Split('_')[0].ToString(),
                    IDE_MONEDA = nomina.TIP_MONE,
                    ASIENTO = "SIN", //string.Format("SIN{0}{1}",DateTime.Now.ToString("ddMMyy"),nomina.TIP_MONE.ToString()),
                    PAQUETE = "SIN",
                    TIPO_ASIENTO = "RS",
                    FECHA = DateTime.Now,
                    CONTABILIDAD = "A",
                    NOTAS = "CONTABLE SIS",
                    ESTADO = 1,
                    ESTADO_TRANSFERENCIA = "C",
                    PERMITIR_DESCUADRADO = "N",
                    CONSERVAR_NUMERACION = "N",
                    ACTUALIZAR_CONSECUTIVO = "N",
                    FECHA_AUDITORIA = DateTime.Now,
                    FECHA_CREACION = DateTime.Now,
                    USUARIO_REGISTRO = System.Web.HttpContext.Current.Session["username"].ToString()
                };
                using (var db = new DISEntities())
                {
                    //CREA SIN NUMERO ASIENTO
                    db.EXACTUS_CABECERA_SISs.Add(cabecera);
                    db.SaveChanges();
                    //ACTRUALIZA EL NUMERO ASIENTO
                    cabecera.ASIENTO = string.Format("SIN{0}{1}", new string('0', 7 - cabecera.IDE_EXACTUS_CABECERA_SIS.ToString().Length), cabecera.IDE_EXACTUS_CABECERA_SIS.ToString());
                    db.Entry(cabecera).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return cabecera;
            }
            catch (DbEntityValidationException ex)
            {
                var menssageException = string.Empty;
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        menssageException += string.Format("{0} - {1} <br>", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw(new System.Exception(ex.Message));
            }
        }

        internal List<HEXACTUS_DETALLE_SIS> listInterfaceContable(EXACTUS_CABECERA_SIS cabecera, TipoArchivo archivo, int index, int size, out int total)
        {
            var listInterface = new List<HEXACTUS_DETALLE_SIS>();
            var formatoMoneda = System.Configuration.ConfigurationManager.AppSettings["Float"].ToString();
            try
            {
                using (var db = new DISEntities())
                {
                    var fechaHasta = cabecera.FECHA_CREACION.AddDays(1);
                    var query = (from xd in db.EXACTUS_DETALLE_SISs
                                 join x in db.EXACTUS_CABECERA_SISs on xd.IDE_EXACTUS_CABECERA_SIS equals x.IDE_EXACTUS_CABECERA_SIS
                                 where x.IDE_CONTRATO == cabecera.IDE_CONTRATO &&
                                       (x.ESTADO_TRANSFERENCIA == cabecera.ESTADO_TRANSFERENCIA || cabecera.ESTADO_TRANSFERENCIA == "0") &&
                                       x.FECHA >= cabecera.FECHA &&
                                       x.FECHA < fechaHasta &&
                                       (x.TIPO_ARCHIVO == archivo.NombreTipoArchivo || archivo.NombreTipoArchivo == "0") &&
                                       (x.IDE_MONEDA == cabecera.IDE_MONEDA || cabecera.IDE_MONEDA == 0)
                                 select new {xd,x}).ToList();
                    total = query.Count;
                    foreach (var item in query.Skip(index).Take(size))
                    {
                        var detalle = new HEXACTUS_DETALLE_SIS(){
                            FUENTE = item.xd.FUENTE,
                            REFERENCIA = item.xd.REFERENCIA,
                            CONTRIBUYENTE = item.xd.CONTRIBUYENTE,
                            CENTRO_COSTO = item.xd.CENTRO_COSTO,
                            CUENTA_CONTABLE = item.xd.CUENTA_CONTABLE,
                            DebitoSoles = string.Format(formatoMoneda,item.xd.MONTO_LOCAL>=0?item.xd.MONTO_LOCAL:0.00M),
                            CreditoSoles = string.Format(formatoMoneda,item.xd.MONTO_LOCAL < 0? Math.Abs(item.xd.MONTO_LOCAL):0.00M),
                            DebitoDolar = string.Format(formatoMoneda,item.xd.MONTO_DOLAR>=0?item.xd.MONTO_DOLAR:0.00M),
                            CreditoDolar = string.Format(formatoMoneda,item.xd.MONTO_DOLAR < 0? Math.Abs(item.xd.MONTO_DOLAR):0.00M),
                            MONTO_UNIDADES = item.xd.MONTO_UNIDADES,
                            NIT = item.xd.NIT,
                            EstadoTransferenciaDetalle = item.xd.ESTADO_TRANSFERENCIA,
                            EXACTUS_CABECERA_SIS = new EXACTUS_CABECERA_SIS(){
                                PAQUETE = item.x.PAQUETE,
                                ASIENTO = item.x.ASIENTO,
                                FECHA = item.x.FECHA,
                                TIPO_ASIENTO = item.x.TIPO_ASIENTO,
                                CONTABILIDAD = item.x.CONTABILIDAD,
                                ESTADO_TRANSFERENCIA = item.x.ESTADO_TRANSFERENCIA
                            }
                        };
                        listInterface.Add(detalle);
                    }
                }
                return listInterface;
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        internal List<EXACTUS_CABECERA_SIS> getExactusCabecera(EXACTUS_CABECERA_SIS cabecera, TipoArchivo tipoArchivo)
        {
            var listCabecera = new List<EXACTUS_CABECERA_SIS>();
            try
            {
                using (var db = new DISEntities())
                {
                    var fechaHasta = cabecera.FECHA_CREACION.AddDays(1);
                    return (from x in db.EXACTUS_CABECERA_SISs 
                                 where x.IDE_CONTRATO == cabecera.IDE_CONTRATO &&
                                       x.ESTADO_TRANSFERENCIA == "C" &&
                                       x.FECHA >= cabecera.FECHA &&
                                       x.FECHA < fechaHasta &&
                                       (x.TIPO_ARCHIVO == tipoArchivo.NombreTipoArchivo || tipoArchivo.NombreTipoArchivo == "0") &&
                                       (x.IDE_MONEDA == cabecera.IDE_MONEDA || cabecera.IDE_MONEDA == 0)
                                 select x).ToList();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        internal bool createCabeceraInRemoteExactus(EXACTUS_CABECERA_SIS item)
        {
            var connectionString = ConfigurationManager.AppSettings.Get("CnnBDEX").ToString(); 
            try
            {
                //VCAMARA.EXACTUS_ASIENTO_DE_DIARIO
                var queryInsert = @"INSERT VCAMARA.EXACTUS_ASIENTO_DE_DIARIO(ASIENTO,PAQUETE,TIPO_ASIENTO,FECHA,CONTABILIDAD,NOTAS,ESTADO,PERMITIR_DESCUADRADO,CONSERVAR_NUMERACION,ACTUALIZAR_CONSECUTIVO,FECHA_AUDITORIA)
                                    VALUES(@ASIENTO,@PAQUETE,@TIPO_ASIENTO,@FECHA,@CONTABILIDAD,@NOTAS,@ESTADO,@PERMITIR_DESCUADRADO,@CONSERVAR_NUMERACION,@ACTUALIZAR_CONSECUTIVO,@FECHA_AUDITORIA)";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlcmd = connection.CreateCommand();
                    sqlcmd.CommandText = queryInsert;

                    sqlcmd.Parameters.Clear();
                    sqlcmd.Parameters.Add("@ASIENTO", SqlDbType.VarChar).Value = item.ASIENTO;
                    sqlcmd.Parameters.Add("@PAQUETE", SqlDbType.VarChar).Value = item.PAQUETE;
                    sqlcmd.Parameters.Add("@TIPO_ASIENTO", SqlDbType.VarChar).Value = item.TIPO_ASIENTO;
                    sqlcmd.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = item.FECHA;
                    sqlcmd.Parameters.Add("@CONTABILIDAD", SqlDbType.VarChar).Value = item.CONTABILIDAD;
                    sqlcmd.Parameters.Add("@NOTAS", SqlDbType.VarChar).Value = item.NOTAS;
                    sqlcmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = item.ESTADO;
                    sqlcmd.Parameters.Add("@PERMITIR_DESCUADRADO", SqlDbType.Char).Value = item.PERMITIR_DESCUADRADO;
                    sqlcmd.Parameters.Add("@CONSERVAR_NUMERACION", SqlDbType.Char).Value = item.CONSERVAR_NUMERACION;
                    sqlcmd.Parameters.Add("@ACTUALIZAR_CONSECUTIVO", SqlDbType.Char).Value = item.ACTUALIZAR_CONSECUTIVO;
                    sqlcmd.Parameters.Add("@FECHA_AUDITORIA", SqlDbType.DateTime).Value = item.FECHA_AUDITORIA;

                    if (sqlcmd.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message,ex.InnerException));
            }
        }

        internal List<HEXACTUS_DETALLE_EXPORT_SIS> listInterfaceContableParcial(EXACTUS_CABECERA_SIS cabecera, TipoArchivo tipoArchivo, int index, int size, out int total)
        {
            var listInterfaceExport = new List<HEXACTUS_DETALLE_EXPORT_SIS>();
            var formatoMoneda = System.Configuration.ConfigurationManager.AppSettings["Float"].ToString();
            try
            {
                using (var db = new DISEntities())
                {
                    var fechaHasta = cabecera.FECHA_CREACION.AddDays(1);
                    var query = db.EXACTUS_DETALLE_EXPORT_SISs
                                .Where(x => x.IDE_CONTRATO == cabecera.IDE_CONTRATO &&
                                       x.FECHA_DOCUMENTO >= cabecera.FECHA &&
                                       x.FECHA_DOCUMENTO < fechaHasta &&
                                       (x.TIPO_ARHIVO == tipoArchivo.NombreTipoArchivo || tipoArchivo.NombreTipoArchivo == "0") &&
                                       (x.IDE_MONEDA == cabecera.IDE_MONEDA || cabecera.IDE_MONEDA == 0)
                                ).ToList();
                    total = query.Count;
                    foreach (var item in query.Skip(index).Take(size))
                    {
                        var v_interfaceExport = new HEXACTUS_DETALLE_EXPORT_SIS()
                        {
                            CUENTA_BANCARIA = item.CUENTA_BANCARIA,
                            NUMERO = item.NUMERO,
                            NUMEROSTR = string.Format("CB{0}{1}", new string('0', 8 - item.NUMERO.ToString().Length), item.NUMERO.ToString()),
                            TIPO_DOCUMENTO = item.TIPO_DOCUMENTO,
                            FECHA_DOCUMENTO = item.FECHA_DOCUMENTO,
                            CONCEPTO = item.CONCEPTO,
                            BENEFICIARIO = item.BENEFICIARIO,
                            CONTRIBUYENTE = item.CONTRIBUYENTE,
                            MONTOSTR = string.Format(formatoMoneda, item.MONTO),
                            DETALLE = item.DETALLE,
                            SUBTIPO = item.SUBTIPO,
                            CENTRO_COSTO = item.CENTRO_COSTO,
                            CUENTA_CONTABLE = item.CUENTA_CONTABLE,
                            RUBRO_1 = item.RUBRO_1,
                            RUBRO_2 = item.RUBRO_2,
                            RUBRO_3 = item.RUBRO_3,
                            RUBRO_4 = item.RUBRO_4,
                            RUBRO_5 = item.RUBRO_5,
                            PAQUETE = item.PAQUETE
                        };
                        listInterfaceExport.Add(v_interfaceExport);
                    }
                }
                return listInterfaceExport;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        internal void createInterfaceContableExport(NOMINA nomina, Archivo archivo)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.pa_create_cuenta_26_export_sis(nomina.ArchivoId, 26, archivo.NombreArchivo.Split('_')[0].ToString());
                }
            }
            catch (System.Exception ex)
            {
                throw (new System.Exception(ex.Message));
            }
        }

        internal void createDetalleInRemote(EXACTUS_CABECERA_SIS item)
        {
            //VCAMARA.EXACTUS_DIARIO
            var connectionString = ConfigurationManager.AppSettings.Get("CnnBDEX").ToString();
            var queryInsertDet = @"INSERT INTO VCAMARA.EXACTUS_DIARIO(ASIENTO,CONSECUTIVO,CENTRO_COSTO,CUENTA_CONTABLE,FUENTE,REFERENCIA,MONTO_LOCAL,MONTO_DOLAR,MONTO_UNIDADES,NIT,DIMENSION1,DIMENSION2,DIMENSION3,DIMENSION4)
                                                VALUES(@ASIENTO,@CONSECUTIVO,@CENTRO_COSTO,@CUENTA_CONTABLE,@FUENTE,@REFERENCIA,@MONTO_LOCAL,@MONTO_DOLAR,@MONTO_UNIDADES,@NIT,@DIMENSION1,@DIMENSION2,@DIMENSION3,@DIMENSION4)";
            try
            {
                using (var db = new DISEntities())
                {

                    var detalle = db.EXACTUS_DETALLE_SISs.Where(x => x.IDE_EXACTUS_CABECERA_SIS == item.IDE_EXACTUS_CABECERA_SIS).ToList();
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand sqlcmd = connection.CreateCommand();
                        sqlcmd.CommandText = queryInsertDet;
                        var consecutivo = 1;
                        foreach (var det in detalle)
                        {
                            sqlcmd.Parameters.Clear();
                            if (det.MONTO_LOCAL != 0.00m && det.MONTO_DOLAR != 0.00m)
                            {
                                sqlcmd.Parameters.Add("@ASIENTO", SqlDbType.VarChar).Value = det.ASIENTO;
                                sqlcmd.Parameters.Add("@CONSECUTIVO", SqlDbType.Int).Value = consecutivo;
                                sqlcmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar).Value = det.CENTRO_COSTO;
                                sqlcmd.Parameters.Add("@CUENTA_CONTABLE", SqlDbType.VarChar).Value = det.CUENTA_CONTABLE;
                                sqlcmd.Parameters.Add("@FUENTE", SqlDbType.VarChar).Value = det.FUENTE;
                                sqlcmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = det.REFERENCIA;
                                sqlcmd.Parameters.Add("@MONTO_LOCAL", SqlDbType.Decimal).Value = det.MONTO_LOCAL;
                                sqlcmd.Parameters.Add("@MONTO_DOLAR", SqlDbType.Decimal).Value = det.MONTO_DOLAR;
                                sqlcmd.Parameters.Add("@MONTO_UNIDADES", SqlDbType.Decimal).Value = det.MONTO_UNIDADES;
                                sqlcmd.Parameters.Add("@NIT", SqlDbType.VarChar).Value = det.NIT;
                                sqlcmd.Parameters.Add("@DIMENSION1", SqlDbType.VarChar).Value = det.DIMENSION1 == null ? string.Empty : det.DIMENSION1;
                                sqlcmd.Parameters.Add("@DIMENSION2", SqlDbType.VarChar).Value = det.DIMENSION2 == null ? string.Empty : det.DIMENSION2;
                                sqlcmd.Parameters.Add("@DIMENSION3", SqlDbType.VarChar).Value = det.DIMENSION3 == null ? string.Empty : det.DIMENSION3;
                                sqlcmd.Parameters.Add("@DIMENSION4", SqlDbType.VarChar).Value = det.DIMENSION4 == null ? string.Empty : det.DIMENSION4;
                                sqlcmd.ExecuteNonQuery();
                                consecutivo++;
                                det.ESTADO_TRANSFERENCIA = "T";//Transferido
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message,ex.InnerException));
            }
        }

        internal void actualizarEstadoTransferido(EXACTUS_CABECERA_SIS item)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var query = db.EXACTUS_CABECERA_SISs.Find(item.IDE_EXACTUS_CABECERA_SIS);
                    query.ESTADO_TRANSFERENCIA = "T";//TRANSFERIDO
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}
