using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Dao.Interface;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bTablaVC : ITablaVC
    {
        Int32 total;
        public Int32 SetInsertarConcepto(eTabla o) { 
            dSqlTablaVC dg = new dSqlTablaVC();
            return dg.SetInsertarConcepto(o);
        }
        //actualizar
        public Int32 SetActualizarConcepto(eTabla o) {
            dSqlTablaVC dg = new dSqlTablaVC();
            return dg.SetActualizarConcepto(o);
        }
        public Int32 SetEliminarConcepto(int indice) {
            dSqlTablaVC dg = new dSqlTablaVC();
            return dg.SetEliminarConcepto(indice);
        }
        public List<eTabla> GetSelectConcepto(eTabla o,out int total)
        {
            dSqlTablaVC dg = new dSqlTablaVC();
            return dg.GetSelectConcepto(o,out total);
        }
        public DropDownList SetEstablecerDataSourceConcepto(DropDownList control,String codigo_tabla,String descripcion = "NULL") {
                dSqlTablaVC dg = new dSqlTablaVC();
                control.DataSource = dg.GetSelectConcepto(entity(codigo_tabla, descripcion), out total);
                control.DataTextField = "_descripcion";
                control.DataValueField = "_codigo";
                control.DataBind();
                if(codigo_tabla != "21" && codigo_tabla != "09")
                    control.Items.Insert(0, new ListItem("Seleccione ----", "0"));
            return control;
        }
        public StringCollection GetConceptoByCodigo(String codigo) {
            return new dSqlTablaVC().GetConceptoByCodigo(codigo);
        }
        public List<eTabla> getConceptoByTipo(string tipo_tabla)
        {
            var o = entity(tipo_tabla,"NULL");
            return new dSqlTablaVC().GetSelectConcepto(o, out total);
        }
        private static eTabla entity(string tipo_tabla,string descripcion)
        {
            return new eTabla()
            {
                _id_Empresa = 0,
                _tipo_Tabla = tipo_tabla,
                _descripcion = descripcion,
                _valor = "N",
                _estado = "A",
                _inicio = 0,
                _fin = 1000000,
                _order = "DESCRIPCION ASC"
            };
        }
    }
}