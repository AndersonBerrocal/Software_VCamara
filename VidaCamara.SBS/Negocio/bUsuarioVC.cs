using System;
using System.Collections.Generic;
using VidaCamara.SBS.Dao;
using VidaCamara.SBS.Entity;

namespace VidaCamara.SBS.Negocio
{
    public class bUsuarioVC
    {
        public Int32 SetInsertarUsuario(eUsuarioVC o) {
            dSqlUsuarioVC usu = new dSqlUsuarioVC();
            return usu.SetInsertarUsuario(o);
        }
        public Int32 SetValidarUsuario(eUsuarioVC o) {
            dSqlUsuarioVC usu = new dSqlUsuarioVC();
            return usu.SetValidarUsuario(o);
        }
        public Int32 SetActualizarUsuario(eUsuarioVC o)
        {
            dSqlUsuarioVC usu = new dSqlUsuarioVC();
            return usu.SetActualizarUsuario(o);
        }
        public List<eUsuarioVC> GetSelectUsuario(Int32 start, Int32 size, String orderby, out Int32 total)
        {
            dSqlUsuarioVC usu = new dSqlUsuarioVC();
            return usu.GetSelecionarUsuario(start,size,orderby,out total);
        }
        public List<eAccesoPagina> GetListaPagina(Int32 ide_usuario)
        {
            eTabla o = new eTabla();
            bTablaVC tb = new bTablaVC();
            dSqlUsuarioVC dusuario = new dSqlUsuarioVC();
            o._id_Empresa = 0;
            o._tipo_Tabla = "03";
            o._descripcion = "NULL";
            o._valor = "N";
            o._estado = "S";
            o._inicio = 0;
            o._fin = 1000;
            o._order = "DESCRIPCION ASC ";

            int total;

            List<eUsuarioVC> listUsuario = dusuario.GetSelecionarAccesoUsuario(ide_usuario);
            var lista_pagina = listUsuario[0]._Aceso_Pagina.Split(',');

            var list = tb.GetSelectConcepto(o, out total);
            var listPagina = new List<eAccesoPagina>();
            for (int l = 0; l < list.Count; l++)
            {
                var existeAcceso = false;
                eAccesoPagina acceso = new eAccesoPagina();
                acceso.ide_Pagina = list[l]._codigo;
                acceso.Descripcion = list[l]._descripcion;
                
                foreach (var item in lista_pagina)
                {
                    if (item.Equals(list[l]._codigo))
                    {
                        existeAcceso = true;
                        break;
                    }
                }
                acceso.permiso = existeAcceso;
                listPagina.Add(acceso);
            }
            return listPagina;
        }
        public Int32 SetActualizarAccesoPagina(eUsuarioVC o)
        {
            dSqlUsuarioVC usuario = new dSqlUsuarioVC();
            return usuario.SetActualizarAccesoUsuario(o);
        }
        public List<eUsuarioVC> GetListAccessoSessionUsuario(String usuario,String contrasena)
        {
            dSqlUsuarioVC dusuario = new dSqlUsuarioVC();
            return dusuario.GetSelecionarAccesoSession(usuario,contrasena);
        }
    }
}