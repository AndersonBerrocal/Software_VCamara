using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.Negocio
{
    public class CheckListLogica
    {
        public IList<DocumentoSolicitado> ObtenerDocumentosSolicitados(string tipoSiniestro)
        {
            using (var repositorio = new DISEntities())
            {
                var tipoSiniestroL = repositorio.TipoMovimientos.FirstOrDefault(ts => ts.Abreviatura == tipoSiniestro);
                return (IList<DocumentoSolicitado>) tipoSiniestroL.DocumentoSolicitados;
            }
        }

        public TipoMovimiento ObtenerTipoDocumento(string unTipoSiniestro)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.TipoMovimientos.Include("DocumentoSolicitado").Include("DocumentoSolicitado.Documento").FirstOrDefault(ts => ts.Abreviatura == unTipoSiniestro);
            }
        }

        public Siniestro ObtenerSiniestro(string CUSPP)
        {
            using (var repositorio = new DISEntities())
            {
                return repositorio.Siniestros.FirstOrDefault(s => s.CUSPP == CUSPP);
            }
        }

        public void GuardarCheckList(CheckList checklist)
        {
            using (var repositorio = new DISEntities())
            {
                repositorio.CheckLists.Add(checklist);
                repositorio.SaveChanges();
            }
        }

        public int ObtenerTipoSolicitud(int idsolicitud)
        {
            using (var repositorio = new DISEntities())
            {
                var clasifica =
                    repositorio.TipoSolicitudes.FirstOrDefault(tipo => tipo.TipoSolicitudId == idsolicitud)
                        .Clasificacion;
                return
                    repositorio.TipoSolicitudes.FirstOrDefault(tipo => tipo.Nombre == clasifica).TipoSolicitudId;
            }
        }

        public CheckList ObtenerCheckList(string cuspp, string tipoSiniestroNombre, string idusuario, int idsolicitud)
        {
            TipoMovimiento tipoSiniestro;
            var checkList = new CheckList();
            
            //Dim lista
            using (var repositorio = new DISEntities()) {
                tipoSiniestro = ObtenerTipoDocumento(tipoSiniestroNombre);
                //Dim archivo = repositorio.Archivo.Include("Pagos").Include("Pagos.Siniestro").Where(Function(s) s.Pago.FirstOrDefault.Siniestro.CUSPP = cuspp)
                var _archivo = (from _archiv in repositorio.Archivos
                    join _pago in repositorio.Pagos on _archiv.ArchivoId equals _pago.ArchivoId
                    join _siniestro in repositorio.Siniestros on _pago.SiniestroId equals _siniestro.SiniestroId
                    where _siniestro.CUSPP == cuspp
                    select _archiv.ArchivoId).FirstOrDefault();

                var solicitudid = ObtenerTipoSolicitud(idsolicitud);
                if (!repositorio.CheckLists.Any(r => r.CUSPP == cuspp & r.TipoMovimientoId == tipoSiniestro.TipoMovimientoId & r.TipoSolicitudId == solicitudid)) {
                    foreach (var documentosolicitado_loopVariable in tipoSiniestro.DocumentoSolicitados) {
                        var documentosolicitado = documentosolicitado_loopVariable;
                        if (documentosolicitado.TipoSolicitudId == solicitudid) {
                            dynamic checkeado = new Checkeado();
                            //checkeado.Documento = documentosolicitado.Documento
                            checkeado.DocumentoId = documentosolicitado.Documento.DocumentoId;
                            checkeado.FechaModificacion = System.DateTime.Now;
                            checkeado.Vigente = true;
                            checkeado.UsuarioModificacion = idusuario;

                            // si el documento ya fue checkeado            
                            var visto = (from c in repositorio.Checkeados
                                join ch in repositorio.CheckLists on c.CheckListId equals ch.CheckListId
                                where
                                    c.Visto && c.DocumentoId == documentosolicitado.Documento.DocumentoId &&
                                    ch.CUSPP == cuspp
                                select c);
                            ;

                            if (visto.Any()) {
                                checkeado.Visto = visto.FirstOrDefault().Visto;
                            }

                            //checkList.Checkeados.Add(checkeado);
                            InsertaBitacora(_archivo, Convert.ToInt32(idusuario), "Ingresa los documentos a la tabla checkeados(documento): " + documentosolicitado.Documento.DocumentoId, "Insert");
                        }
                    }
                    checkList.CUSPP = cuspp;
                    checkList.FechaModificacion = System.DateTime.Now;
                    checkList.UsuarioModificacion = idusuario;
                    checkList.Vigente = true;
                    checkList.TipoMovimientoId = tipoSiniestro.TipoMovimientoId;
                    checkList.TipoSolicitudId = solicitudid;
                    checkList.Estado = 1;
                    checkList.Observaciones = "";
                    repositorio.CheckLists.Add(checkList);
                    repositorio.SaveChanges();

                    InsertaBitacora(_archivo, Convert.ToInt32(idusuario), "Ingresa un registro al Checklist por: " + cuspp + "," + tipoSiniestroNombre + "," + idusuario + "," + idsolicitud, "Insert");
                }

                checkList = repositorio.CheckLists.Include("Checkeado").Include("Checkeado.Documento").FirstOrDefault(cl => cl.CUSPP == cuspp & cl.TipoMovimientoId == tipoSiniestro.TipoMovimientoId & cl.TipoSolicitudId == solicitudid);

            }
            return checkList;
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

        public void ActualizarCheckeados(List<string> listaGrillaChekeadoId, List<bool> listaGrillaVisto, string cuspp)
        {
            var i = 0;
            using (var repositorio = new DISEntities())
            {
                var archivo = (from p in repositorio.Pagos
                    join s in repositorio.Siniestros on p.SiniestroId equals s.SiniestroId
                    where s.CUSPP == cuspp
                    select p.ArchivoId).FirstOrDefault();

                dynamic variable = repositorio.CheckLists.Where(cl => cl.CUSPP == cuspp);
                //Dim cuenta = variable.Count
                foreach (var tiposiniestroLoopVariable in variable) {
                    var tiposiniestro = tiposiniestroLoopVariable;
                    foreach (var checkeado_loopVariable in tiposiniestro.Checkeado) {
                        var checkeado = checkeado_loopVariable;
                        //se modifica para que grabe cada uno en forma inmediata
                        if (checkeado.CheckeadoId == listaGrillaChekeadoId[i]) {
                            checkeado.Visto = listaGrillaVisto[i];
                            InsertaBitacora(archivo, tiposiniestro.UsuarioModificacion, "Cambia el estado de los Checkeados: " + checkeado.CheckeadoId.ToString() + ", estado: " + checkeado.Visto.ToString(), "Update");
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    i = 0;
                }
                repositorio.SaveChanges();
            }
        }

        public List<int> ObtenerArchivoId(string cuspp, string tipoSiniestroNombre)
        {
            var tipoSiniestro = default(TipoMovimiento);

            using (var repositorio = new DISEntities())
            {
                var archivoid = (from siniestro in repositorio.Siniestros
                    join tipo in repositorio.TipoMovimientos on siniestro.TipoMovimientoId equals tipo.TipoMovimientoId
                    join pago in repositorio.Pagos on siniestro.SiniestroId equals pago.SiniestroId
                    join archivo in repositorio.Archivos on pago.ArchivoId equals archivo.ArchivoId
                    where
                        pago.Estado == 1 && siniestro.CUSPP == cuspp && tipo.Abreviatura == tipoSiniestroNombre &&
                        archivo.EstadoArchivoId == 2
                    select pago.ArchivoId).ToList();

                return archivoid;
            }
        }
    }
}