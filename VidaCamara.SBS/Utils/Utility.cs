using System.Collections.Generic;
using VidaCamara.SBS.Entity;
using VidaCamara.SBS.Negocio;

namespace VidaCamara.SBS.Utils
{
    public class Utility
    {
        public List<eContratoVC> getContrato(out int total)
        {
            try
            {
                var  o = new eContratoVC()
                {
                    _inicio = 0,
                    _fin = 10000,
                    _orderby = "IDE_CONTRATO ASC",
                    _nro_Contrato = "NO",
                    _estado = "A"
                };

                return new bContratoVC().GetSelecionarContrato(o, out total);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<eContratoSys> getContratoSys(out int total)
        {
            try
            {
                var  o = new eContratoSys()
                {
                    _inicio = 0,
                    _fin = 10000,
                    _orderby = "IDE_CONTRATO ASC",
                    _nro_Contrato = "NO",
                    _estado = "A"
                };

                return new bContratoSys().GetSelecionarContratoSys(o, out total);
            }
            catch (System.Exception)
            {                
                throw;
            }
        }
        public List<eTabla> getConceptoByTipo(string e,string f,out int total)
        {
            var o = new eTabla()
            {
                _id_Empresa = 0,
                _tipo_Tabla = e,
                _descripcion = "NULL",
                _valor = "N",
                _estado = "A",
                _tipo = f,
                _inicio = 0,
                _fin = 10000000,
                _order = "DESCRIPCION ASC"
        };
            return new bTablaVC().GetSelectConcepto(o, out total);
        }
    }
}
