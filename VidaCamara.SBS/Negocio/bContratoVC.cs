using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bContratoVC : IContratoVC
    {
        static Int32 totalContrato;
        public Int32 SetInsertarContrato(eContratoVC o) { 
            dSqlContratoVC dc = new dSqlContratoVC();
            return dc.SetInsertarContrato(o);
        }
        //actualizar contrat
        public Int32 SetActualizarContrato(eContratoVC o) {
            dSqlContratoVC dc = new dSqlContratoVC();
            return dc.SetActualizarContrato(o);
        }
        public Int32 SetEliminarContrato(int indice) {
            dSqlContratoVC dc = new dSqlContratoVC();
            return dc.SetEliminarContrato(indice);
        }
        //listar contrato
        public List<eContratoVC> GetSelecionarContrato(eContratoVC o,out int total) {
            dSqlContratoVC c = new dSqlContratoVC();
            return c.GetSelecionarContrato(o,out total);
        }
        public DropDownList SetEstablecerDataSourceContrato(DropDownList control)
        {
            eContratoVC o = new eContratoVC();
            o._inicio = 0;
            o._fin = 10000;
            o._orderby = "IDE_CONTRATO ASC";
            o._nro_Contrato = "NO";
            o._estado = "A";

            bContratoVC tb = new bContratoVC();
            control.DataSource = tb.GetSelecionarContrato(o, out totalContrato);
            control.DataTextField = "_des_Contrato";
            control.DataValueField = "_nro_Contrato";
            control.DataBind();
            control.Items.Insert(0, new ListItem("Seleccione ----", "0"));
            return control;
        }
    }
}
