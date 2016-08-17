using System;
using System.Collections.Generic;
using System.Linq;
using VidaCamara.DIS.Modelo;

namespace VidaCamara.DIS.data
{
    public class dReglaArchivo
    {
        public List<ReglaArchivo> getListReglaArchivo(ReglaArchivo regla, int jtStartIndex, int jtPageSize,string jtSorting, out int total)
        {
            var listReglaArchivo = new List<ReglaArchivo>();
            try
            {
                var sort = jtSorting.Split(' ');
                using (var db = new DISEntities())
                {
                    var query = db.ReglaArchivos.OrderBy(a=>a.CaracterInicial)
                                                .Where(a=>(a.Archivo.Equals(regla.Archivo) || regla.Archivo.Equals("0"))&&
                                                (a.TipoLinea.Equals(regla.TipoLinea) || regla.TipoLinea.Equals("0")) && a.vigente == regla.vigente && 
                                                a.NUM_CONT_LIC == regla.NUM_CONT_LIC && (a.IdReglaArchivo == regla.IdReglaArchivo ||regla.IdReglaArchivo == 0)).ToList();
                    switch (sort[0])
                    {
                        case "TipoLinea":
                            query = sort[1].Equals("ASC")? query.OrderBy(a=>a.TipoLinea).ToList() : query.OrderByDescending(a=>a.TipoLinea).ToList();
                            break;
                        case "Archivo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.Archivo).ToList() : query.OrderByDescending(a => a.Archivo).ToList();
                            break;
                        case "CaracterInicial":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.CaracterInicial).ToList() : query.OrderByDescending(a => a.CaracterInicial).ToList();
                            break;
                        case "LargoCampo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.LargoCampo).ToList() : query.OrderByDescending(a => a.LargoCampo).ToList();
                            break;
                        case "TipoCampo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.TipoCampo).ToList() : query.OrderByDescending(a => a.TipoCampo).ToList();
                            break;
                        case "InformacionCampo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.InformacionCampo).ToList() : query.OrderByDescending(a => a.InformacionCampo).ToList();
                            break;
                        case "TipoValidacion":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.TipoValidacion).ToList() : query.OrderByDescending(a => a.TipoValidacion).ToList();
                            break;
                        case "ReglaValidacion":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.ReglaValidacion).ToList() : query.OrderByDescending(a => a.ReglaValidacion).ToList();
                            break;
                        case "NombreCampo":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.NombreCampo).ToList() : query.OrderByDescending(a => a.NombreCampo).ToList();
                            break;
                        case "TituloColumna":
                            query = sort[1].Equals("ASC") ? query.OrderBy(a => a.TituloColumna).ToList() : query.OrderByDescending(a => a.TituloColumna).ToList();
                            break;
                        default:
                            query = query.OrderBy(a=>a.IdReglaArchivo).ToList();
                            break;
                    }
                    total = query.Count();
                    foreach (var item in query.Skip(jtStartIndex).Take(jtPageSize))
                    {
                        var reglaArchivo = new ReglaArchivo()
                        {
                            IdReglaArchivo = item.IdReglaArchivo,
                            Archivo = item.Archivo,
                            NombreCampo = item.NombreCampo,
                            InformacionCampo = item.InformacionCampo,
                            TipoLinea = item.TipoLinea,
                            CaracterInicial = item.CaracterInicial,
                            LargoCampo = item.LargoCampo,
                            TipoCampo = item.TipoCampo,
                            FormatoContenido = item.FormatoContenido,
                            TituloColumna = item.TituloColumna,
                            ReglaValidacion = item.ReglaValidacion,
                            TipoValidacion = item.TipoValidacion,
                            vigente = item.vigente
                        };
                        listReglaArchivo.Add(reglaArchivo);
                    }
                }
                return listReglaArchivo;
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        public void grabarReglaArchivo(ReglaArchivo regla)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    db.ReglaArchivos.Add(regla);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        public void actualizarReglaArchivo(ReglaArchivo regla)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var entity = db.ReglaArchivos.Find(regla.IdReglaArchivo);
                    entity.Archivo = regla.Archivo;
                    entity.TipoLinea = regla.TipoLinea;
                    entity.CaracterInicial = regla.CaracterInicial;
                    entity.LargoCampo = regla.LargoCampo;
                    entity.TipoCampo = regla.TipoCampo;
                    entity.InformacionCampo = regla.InformacionCampo;
                    entity.FormatoContenido = regla.FormatoContenido;
                    entity.TipoValidacion = regla.TipoValidacion;
                    entity.ReglaValidacion = regla.ReglaValidacion;
                    entity.vigente = regla.vigente;
                    entity.NombreCampo = regla.NombreCampo;
                    entity.TituloColumna = regla.TituloColumna;
                    entity.NUM_CONT_LIC = regla.NUM_CONT_LIC;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {   
                throw(new Exception(ex.Message));
            }
        }

        public int validarExisteReglaByContrato(CONTRATO_SYS contratoSisEF)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    var numeroContrato = Convert.ToInt32(contratoSisEF.NRO_CONTRATO);
                    return db.ReglaArchivos.Where(r => r.NUM_CONT_LIC == numeroContrato).Count();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        public string copiarUltimaReglaArchivo(CONTRATO_SYS contrato)
        {
            try
            {
                using (var db = new DISEntities())
                {
                    return db.pa_copia_reglaArchivo(Convert.ToInt32(contrato.NRO_CONTRATO)).FirstOrDefault().Value.ToString();
                }
            }
            catch (Exception ex)
            { 
                throw(new Exception(ex.Message));
            }
        }
    }
}
