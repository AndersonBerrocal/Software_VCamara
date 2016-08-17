using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bContratoSys : IContratoSys
    {
        static Int32 totalContrato;
        public Int32 SetInsertarContratoSys(eContratoSys o) {
            dSqlContratoSys dc = new dSqlContratoSys();
            return dc.SetInsertarContratoSys(o);
        }
        //actualizar contrat
        public Int32 SetActualizarContratoSys(eContratoSys o)
        {
            dSqlContratoSys dc = new dSqlContratoSys();
            return dc.SetActualizarContratoSys(o);
        }
        public Int32 SetEliminarContratoSys(int indice) {
            dSqlContratoSys dc = new dSqlContratoSys();
            return dc.SetEliminarContratoSys(indice);
        }
        //listar contrato
        public List<eContratoSys> GetSelecionarContratoSys(eContratoSys o, out int total)
        {
            dSqlContratoSys c = new dSqlContratoSys();
            return c.GetSelecionarContratoSys(o,out total);
        }
        public DropDownList SetEstablecerDataSourceContratoSys(DropDownList control)
        {
            eContratoSys o = new eContratoSys();
            o._inicio = 0;
            o._fin = 10000;
            o._orderby = "IDE_CONTRATO ASC";
            o._nro_Contrato = "NO";
            o._estado = "C";

            bContratoSys tb = new bContratoSys();
            control.DataSource = tb.GetSelecionarContratoSys(o, out totalContrato);
            control.DataTextField = "_des_Contrato";
            control.DataValueField = "_ide_Contrato";
            control.DataBind();
            control.Items.Insert(0, new ListItem("Seleccione ----", "0"));
            return control;
        }
    }
}
