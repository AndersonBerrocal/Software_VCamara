using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.Negocio
{
    public class SiniestroLogica
    {
        public void ImpactarNegocio(Siniestro siniestro, Pago pago)
        {
            using (var repositorio = new DISEntities())
            {
                if (Existe(siniestro))
                {
                    ImpactarPagos(siniestro, pago);
                    return;
                }
                else
                {
                    siniestro.TipoSolicitudId = 1;
                    repositorio.Siniestros.Add(siniestro);
                    repositorio.SaveChanges();
                    ImpactarPagos(siniestro, pago);
                    return;
                }
            }
        }

        private bool Existe(Siniestro siniestro)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.Siniestros.Any(s => s.CUSPP == siniestro.CUSPP);
            }
        }

        private bool ExistePago(int siniestroId, int archivo)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.Pagos.Any(s => s.SiniestroId == siniestroId & s.ArchivoId == archivo);
            }
        }

        public Siniestro Obtener(string cuspp)
        {
            var siniestro = default(Siniestro);
            using (var repositorio = new DISEntities())
            {
                siniestro = repositorio.Siniestros.Include("Pago").FirstOrDefault(s => s.CUSPP == cuspp);
            }
            return siniestro;
        }

        public List<string> ObtenerCusppAprobado(int estado)
        {
            using (var repositorio = new DISEntities())
            {
                var cuspp = (from siniestro in repositorio.Siniestros
                    join tipo in repositorio.TipoMovimientos on siniestro.TipoMovimientoId equals
                        tipo.TipoMovimientoId
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join archivo in repositorio.Archivos on pago.ArchivoId equals archivo.ArchivoId
                    where pago.Estado == estado && archivo.EstadoArchivoId == 2
                    select siniestro.CUSPP).ToList();

                return cuspp;
            }
        }

        public object ObtenerSolicitud(string cuspp)
        {
            using (var repositorio = new DISEntities())
            {
                var _solicitud = (from solicitud in repositorio.TipoSolicitudes
                    join siniestro in repositorio.Siniestros on solicitud.TipoSolicitudId equals
                        siniestro.TipoSolicitudId
                    join movimiento in repositorio.TipoMovimientos on siniestro.TipoMovimientoId equals
                        movimiento.TipoMovimientoId
                    where siniestro.CUSPP == cuspp
                    select
                        new
                        {
                            solicitud.TipoSolicitudId,
                            Clasificacion = solicitud.Clasificacion + " " + movimiento.Abreviatura
                        }).ToList();
                return _solicitud;
            }
        }

        public object ObtenerSiniestroMovimientos(string cuspp)
        {
            using (var repositorio = new DISEntities())
            {
                var _movimiento = (from movimiento in repositorio.TipoMovimientos
                    join siniestro in repositorio.Siniestros on movimiento.TipoMovimientoId equals
                        siniestro.TipoMovimientoId
                    where siniestro.CUSPP == cuspp
                    select new {movimiento.TipoMovimientoId, movimiento.Abreviatura}).ToList();
                return _movimiento;
            }
        }

        public object ObtenerTipoMovimiento(string cuspp, int solicitud)
        {
            using (var repositorio = new DISEntities())
            {
                var _movimiento = (from movimiento in repositorio.TipoMovimientos
                    join siniestro in repositorio.Siniestros on movimiento.TipoMovimientoId equals
                        siniestro.TipoMovimientoId
                    where siniestro.CUSPP == cuspp && siniestro.TipoSolicitudId == solicitud
                    select movimiento.Abreviatura).FirstOrDefault();

                return _movimiento;
            }
        }

        public object ObtenerindicadorSiniestro(string cuspp)
        {
            using (var repositorio = new DISEntities())
            {
                var _siniestro = (from siniestro in repositorio.Siniestros
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join tipo in repositorio.TipoMovimientos on siniestro.TipoMovimientoId equals tipo.TipoMovimientoId
                    where siniestro.CUSPP == cuspp
                    group new {siniestro, pago, tipo} by new
                    {
                        siniestro.SiniestroId,
                        tipo.TipoMovimientoId,
                        tipo.Abreviatura,
                        Fechaingreso = pago.FechaIngreso,
                        Cuspp = siniestro.CUSPP,
                        siniestro.TipoSolicitudId
                    }
                    into gr
                    select new
                    {
                        gr.Key.Abreviatura,
                        veces = gr.Count(x => string.IsNullOrWhiteSpace(x.siniestro.CUSPP)),
                        Fecha = gr.Max(x => x.pago.FechaPago),
                        gr.Key.SiniestroId,
                        TipoSolicitud = gr.Key.TipoSolicitudId
                    }
                    ).ToList();

                return _siniestro;
            }
        }

        public object ObtenerTipoSiniestro(string cuspp, string tipo)
        {
            using (var repositorio = new DISEntities())
            {
                var _pago = (from movimiento in repositorio.TipoMovimientos
                    join siniestro in repositorio.Siniestros on movimiento.TipoMovimientoId equals
                        siniestro.TipoMovimientoId
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join archivo in repositorio.Archivos on pago.ArchivoId equals archivo.ArchivoId
                    join moneda in repositorio.Monedas on pago.MonedaId equals moneda.MonedaId
                    where siniestro.CUSPP == cuspp && movimiento.Abreviatura == tipo && archivo.EstadoArchivoId == 2
                    group new {movimiento, siniestro, pago, archivo, moneda} by new
                    {
                        fechapago = pago.FechaPago,
                        moneda = moneda.MonedaId,
                        monto = pago.Monto,
                        siniestroid = siniestro.SiniestroId,
                        nombre = archivo.NombreArchivo,
                        idarchivo = archivo.ArchivoId
                    }
                    into gr
                    select new
                    {
                        FechaL = gr.Key.fechapago,
                        Monto = gr.Key.moneda == 1 ? gr.Key.monto : 0M,
                        MontoDolares = gr.Key.moneda == 2 ? gr.Key.monto : 0,
                        FechaT = DateTime.Now,
                        Nveces = 1,
                        SiniestroId = gr.Key.siniestroid,
                        NombreArchivo = gr.Key.nombre,
                        IdArchivo = gr.Key.idarchivo
                    }).ToList();
                return _pago;
            }
        }

        public object ObtenerNominasParaAprobar(int estado)
        {
            using (var repositorio = new DISEntities())
            {
                var _pago = (from siniestro in repositorio.Siniestros
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join afp in repositorio.AFPs on siniestro.AFPId equals afp.AFPId
                    join archivo in repositorio.Archivos on pago.NominaId equals archivo.ArchivoId
                    where pago.Estado > 0 && pago.Estado < estado && archivo.EstadoArchivoId == 2
                    group new {siniestro, pago, afp, archivo} by new
                    {
                        Nombre = archivo.NombreArchivo,
                        pago.ArchivoId
                    }
                    into gr
                    select new
                    {
                        gr.Key.Nombre,
                        gr.Key.ArchivoId
                    }).ToList();

                return _pago;
            }
        }

        public object ObtenerDetalleNominasParaAprobar(int idarchivo)
        {
            using (var repositorio = new DISEntities())
            {
                var _pago = (from siniestro in repositorio.Siniestros
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join afp in repositorio.AFPs on siniestro.AFPId equals afp.AFPId
                    where pago.ArchivoId == idarchivo
                    group new {siniestro, pago, afp} by new
                    {
                        FechaLiquidacion = pago.FechaPago,
                        Afp = afp.Descripcion,
                        pago.ArchivoId
                    }
                    into gr
                    select new
                    {
                        gr.Key.FechaLiquidacion,
                        gr.Key.Afp,
                        Monto_Liquidacion = gr.Sum(x => x.pago.Monto),
                        Monto_VC = gr.Sum(x => x.pago.PagoVC),
                        gr.Key.ArchivoId
                    }
                    ).ToList();
                return _pago;
            }
        }

        public void AprobarNomina(List<string> listaGrillaChekeadoId, string usuario)
        {
            using (var repositorio = new DISEntities())
            {
                for (var i = 0; i <= listaGrillaChekeadoId.Count - 1;)
                {
                    var nomina = listaGrillaChekeadoId[i];
                    var pagos = repositorio.Pagos.Where(p => p.ArchivoId == Convert.ToInt32(nomina)).ToList();

                    foreach (var item in pagos)
                    {
                        item.Estado = (int) Modelo.Enumerado.EstadoPago.Aprobado;
                        item.UsuarioModificacion = usuario;
                        InsertaBitacora(item.ArchivoId, Convert.ToInt32(usuario),
                            "nomina aprobada: " + item.ArchivoId, "Update");
                    }
                }
                repositorio.SaveChanges();
            }
        }

        public void InsertaBitacora(int idarchivo, int idusuario, string descripcion, string comando)
        {
            using (var repositorio = new DISEntities())
            {
                var bitacora = new Bitacora
                {
                    Comando = comando,
                    Descripcion = descripcion,
                    Fecha = System.DateTime.Now,
                    IdArchivo = idarchivo,
                    IdUsuario = idusuario
                };

                repositorio.Bitacoras.Add(bitacora);
                repositorio.SaveChanges();
            }
        }

        public void PagarNomina(List<string> listaGrillaChekeadoId, string usuario)
        {
            using (var repositorio = new DISEntities())
            {
                for (var i = 0; i <= listaGrillaChekeadoId.Count() - 1; i++)
                {
                    var nomina = listaGrillaChekeadoId[i];
                    var pagos = repositorio.Pagos.Where(p => p.ArchivoId == Convert.ToInt32(nomina)).ToList();

                    foreach (var item in pagos)
                    {
                        item.Estado = (int) Modelo.Enumerado.EstadoPago.Contabilizado;
                        item.UsuarioModificacion = usuario;
                        InsertaBitacora(item.ArchivoId, Convert.ToInt32(usuario), "nomina pagada: " + item.ArchivoId,
                            "Update");
                    }
                }
                ;
                repositorio.SaveChanges();
            }
        }

        public object ObtenerNominasAprobadas()
        {
            using (var repositorio = new DISEntities())
            {
                var nomina = (from siniestro in repositorio.Siniestros
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join afp in repositorio.AFPs on siniestro.AFPId equals afp.AFPId
                    join archivo in repositorio.Archivos on pago.NominaId equals archivo.ArchivoId
                    where pago.Estado == (int) Modelo.Enumerado.EstadoPago.Aprobado
                    group new {siniestro, pago, afp, archivo} by new
                    {
                        Nombre = archivo.NombreArchivo,
                        pago.ArchivoId
                    }
                    into gr
                    select new
                    {
                        gr.Key.Nombre,
                        gr.Key.ArchivoId
                    }).ToList();

                return nomina;
            }
        }

        public List<string> ConsultaCuspp()
        {
            using (var repositorio = new DISEntities())
            {
                var cuspp = (from siniestro in repositorio.Siniestros
                    select siniestro.CUSPP).ToList();
                return cuspp;
            }
        }

        public AFP GetAfp(string nombreAfp)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.AFPs.FirstOrDefault(x => x.Abreviatura == nombreAfp);
            }
        }

        public void ImpactarPagos(Siniestro siniestro, Pago pago)
        {
            using (var repositorio = new DISEntities())
            {
                var existente = repositorio.Siniestros.FirstOrDefault(s => s.CUSPP == siniestro.CUSPP);
                if (!ExistePago(existente.SiniestroId, pago.ArchivoId))
                {
                    pago.SiniestroId = existente.SiniestroId;
                    existente.Pagos.Add(pago);
                    repositorio.SaveChanges();
                }
                else
                {
                    var masPago =
                        repositorio.Pagos.Where(
                            x => x.ArchivoId == pago.ArchivoId && x.SiniestroId == existente.SiniestroId).ToList();
                    var sumaMintoTatal = masPago.Sum(x => x.Monto) + pago.Monto;
                    var sumaMintoVc = masPago.Sum(x => x.PagoVC) + pago.PagoVC;
                    existente.Pagos.FirstOrDefault().Monto = sumaMintoTatal;
                    existente.Pagos.FirstOrDefault().PagoVC = sumaMintoVc;
                    repositorio.SaveChanges();
                }
            }
        }

        public TipoMovimiento GetTipoMovimiento(string nombreTipo)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.TipoMovimientos.FirstOrDefault(x => x.Abreviatura == nombreTipo);
            }
        }

        public object ObtenerNombre(string cuspp)
        {
            using (var repositorio = new DISEntities())
            {
                var _cuspp = (from persona in repositorio.Personas
                    where persona.TipoIdentificacion == 99 && persona.NumeroIdentificador == cuspp
                    select new {persona.PrimerNombre, persona.ApellidoPaterno, persona.ApellidoMaterno});

                return _cuspp;
            }
        }

        public void PagoValidado(int siniestro)
        {
            using (var repositorio = new DISEntities())
            {
                dynamic pago = repositorio.Pagos.FirstOrDefault(p => p.SiniestroId == siniestro & p.Vigente == true);
                //estado = 3 --> nóminas aprobadas 
                if (pago.Estado < 3)
                {
                    pago.Estado = (int) Modelo.Enumerado.EstadoPago.Aprobado;
                    InsertaBitacora(pago.ArchivoId, pago.UsuarioModificacion,
                        "Cambia el estado del Pago: " + pago.PagoId.ToString() + ", estado: Validado", "Update");
                    repositorio.SaveChanges();
                }
            }
        }

        public void AprobarCheckList(string cuspp, string tipomovimiento, int idsolicitud, int estado,
            string observacion)
        {
            using (var repositorio = new DISEntities())
            {
                var solicitudid = ObtenerTipoSolicitud(idsolicitud);
                var tipo = repositorio.TipoMovimientos.FirstOrDefault(tm => tm.Abreviatura == tipomovimiento);
                var check =
                    repositorio.CheckLists.FirstOrDefault(
                        c =>
                            c.CUSPP == cuspp & c.TipoMovimientoId == tipo.TipoMovimientoId &
                            c.TipoSolicitudId == solicitudid);
                check.Estado = estado;
                check.Observaciones = observacion;
                repositorio.SaveChanges();

                var _estado = estado == 0 ? "Rechazado" : "Ingresado";

                var archivo = (from archiv in repositorio.Archivos
                    join pago in repositorio.Pagos on archiv.ArchivoId equals pago.ArchivoId
                    join siniestro in repositorio.Siniestros on pago.SiniestroId equals siniestro.SiniestroId
                    where siniestro.CUSPP == cuspp
                    select archiv.ArchivoId).FirstOrDefault();

                InsertaBitacora(archivo, Convert.ToInt32(check.UsuarioModificacion),
                    "Cambia el estado del CheckLists: " + check.CheckListId + ", estado: " + _estado,
                    "Update");
            }
        }

        public int ObtenerTipoSolicitud(int idsolicitud)
        {
            using (var repositorio = new DISEntities())
            {
                var clasifica = (from tipo in repositorio.TipoSolicitudes
                    where tipo.TipoSolicitudId == idsolicitud
                    select tipo.Clasificacion).FirstOrDefault();

                var solicitud = (from tipo in repositorio.TipoSolicitudes
                    where tipo.Nombre == clasifica
                    select tipo.TipoSolicitudId).FirstOrDefault();
                return solicitud;
            }
        }

        public object ObtenerTipoCambio()
        {
            using (var repositorio = new DISEntities())
            {
                var tipocambio = (from tipo in repositorio.TipoCambios
                    where tipo.Vigente
                    select tipo).ToList();
                return tipocambio;
            }
        }

        public void TipoCambio(string periodo, decimal monto)
        {
            using (var repositorio = new DISEntities())
            {
                var tipocambio = repositorio.TipoCambios.FirstOrDefault(tc => tc.Periodo == periodo);
                if (tipocambio == null)
                {
                    var tipo = new TipoCambio
                    {
                        Periodo = periodo,
                        Monto = monto,
                        Vigente = true
                    };
                    repositorio.TipoCambios.Add(tipo);
                    repositorio.SaveChanges();
                }
                else
                {
                    tipocambio.Monto = monto;
                    tipocambio.Vigente = true;
                    repositorio.SaveChanges();
                }
            }
        }

        public void EliminarTipoCambio(string periodo)
        {
            using (var repositorio = new DISEntities())
            {
                var tipocambio = repositorio.TipoCambios.FirstOrDefault(tc => tc.Periodo == periodo & tc.Vigente == true);
                tipocambio.Vigente = false;
                repositorio.SaveChanges();
            }
        }

        public object ObtenerCheckLists(string cuspp, string siniestro, int idsolicitud)
        {
            using (var repositorio = new DISEntities())
            {
                var solicitudid = ObtenerTipoSolicitud(idsolicitud);
                var tipo = repositorio.TipoMovimientos.FirstOrDefault(tm => tm.Abreviatura == siniestro);
                var check =
                    repositorio.CheckLists.FirstOrDefault(
                        c =>
                            c.CUSPP == cuspp & c.TipoMovimientoId == tipo.TipoMovimientoId &
                            c.TipoSolicitudId == solicitudid);
                return check;
            }
        }

        public object ObtenerTipoArchivo()
        {
            using (var repositorio = new DISEntities())
            {
                var tipo = repositorio.TipoArchivos.Where(ta => ta.Vigente == true).ToList();
                return tipo;
            }
        }

        public object ObtenerTipoArchivo(string tipoarchivo)
        {
            using (var repositorio = new DISEntities())
            {
                var tipo = (from archivo in repositorio.Archivos
                    where archivo.TipoArchivoId == Convert.ToInt32(tipoarchivo)
                    select new {archivo.FechaModificacion, archivo.NombreArchivo, archivo.ArchivoId}).ToList();
                return tipo;
            }
        }
    }
}
