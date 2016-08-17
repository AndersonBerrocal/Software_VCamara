<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmGeneral.aspx.cs" Inherits="VidaCamara.Web.WebPage.Mantenimiento.General" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/WebPage/Mantenimiento/js/parametro-validacion.js"></script>
    <script src="/WebPage/Mantenimiento/js/tblGeneral.js"></script>
    <script src="/WebPage/Mantenimiento/js/tblContratoView.js"></script>
    <script src="/WebPage/Mantenimiento/Js/tblReasegurador.js"></script>
    <script src="/WebPage/Mantenimiento/Js/tblContratoViewSyS.js"></script>
    <script src="/WebPage/Mantenimiento/js/tblContratoSisDetalle.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Comienzo de los Tabs-->
   <script runat="server">
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            var accesso = new VidaCamara.SBS.Negocio.bValidarAcceso();
            var index = Int32.Parse(menuTabs.SelectedValue);
            if (!accesso.GetValidarAcceso((index+100).ToString()))
            {
                Response.Redirect("Error");
            }
            multiTabs.ActiveViewIndex = index;
        }
    </script>
    <div class="btn_crud">
            <asp:HyperLink CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btn_borrar" ToolTip="Eliminar" runat="server" ImageUrl="~/Resources/Imagenes/u10_normal.png" OnClick="btn_borrar_Click"/>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnGuardar" ToolTip="Guardar" runat="server" ImageUrl="~/Resources/Imagenes/u14_normal.png"  OnClick="btnGuardar_Click"/>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnNuevo" ToolTip="Nuevo" runat="server" ImageUrl="~/Resources/Imagenes/u13_normal.jpg"/>
    </div>
    <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
            <Items>
                <asp:MenuItem Text="Generales" Value="0" Selected="true"/>
                <asp:MenuItem Text="Contratos Reaseguros" Value="1"/>
                <asp:MenuItem Text="Reasegurador" Value="2" />                
                <asp:MenuItem Text="Contratos SIS" Value="3" />
                <asp:MenuItem Text="CSV - SIS" Value="4" />
            </Items>
            <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>

            <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666" BorderStyle="None"></StaticSelectedStyle>
    </asp:Menu>  
    <div class="tabBody">
        <asp:HiddenField ID="hdf_form" Value="0" runat="server" />
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--lista general-->
            <asp:View ID="view1" runat="server">
                
                <label class="label_to" for="txt_empresa">Empresa (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_empresa" runat="server" Height="25px" Width="44%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_ruc">N° de RUC (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_ruc" runat="server" Height="25px" Width="20%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_vigente">Año Vigente (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_vigente" runat="server" Height="25px" Width="14%"  ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_mes_vig">Mes Vigente (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_mes_vig" runat="server" Height="25px" Width="14%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_ruta_archivo">Ruta Archivo Excel (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_ruta_archivo" runat="server" Height="25px" Width="14%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_cantidad_decimal">N° De Decimales  (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_cantidad_decimal" runat="server" Height="25px" Width="14%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to" for="txt_tcamesCont">T.Cambio Mes Contable (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_tcamesCont" runat="server" Height="25px" Width="14%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <label class="label_to"for="txt_tcaCierre">T.Cambio Mes de Cierre (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_tcaCierre" runat="server" Height="25px" Width="14%"  ToolTip="Ingrese este Campo"></asp:TextBox>

                <asp:HiddenField ID="txt_idempresa" Value="0" runat="server" />


            </asp:View>
            <!--vista de CONTRATO-->
            <asp:View ID="view2" runat="server">
                <link rel="stylesheet" href="Resources/CSS/bootstrap.css"/>
                    <div class="panel-group" id="accordion">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"> Contrato</span>
                                </a>
                            </h4>
                        </div>
                         <!--cabecera de contrato-->
                        <div id="collapseOne" class="panel-collapse collapse in">
                            <div class="panel-body">

                                <label class="label_to" for="txt_nrocont_c">Número de Contrato (*)</label>
                                <asp:TextBox CssClass="input_to" ID="txt_nrocont_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="ddl_seniestro_c">Ramos Siniestros (*)</label>
                                <asp:DropDownList CssClass="input_right" ID="ddl_seniestro_c" runat="server" Height="25px" Width="14.1%"></asp:DropDownList>

                                <label class="input_right_L" for="ddl_ramo_prima_c">Ramo Primas (*)</label>
                                <asp:DropDownList CssClass="input_right" ID="ddl_ramo_prima_c" runat="server" Height="25px" Width="14.1%"></asp:DropDownList>

                                <label class="label_to" for="ddl_clasecontrato_c">Clase de Contrato (*)</label>
                                <asp:DropDownList CssClass="input_to" ID="ddl_clasecontrato_c" runat="server" Height="25px" Width="14.3%" ToolTip="Selecione este Campo">
                                </asp:DropDownList>

                                <label class="input_right_L" for="txt_fecini_c">Fecha Inicio de Vigencia (*)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_fecini_c" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                                <label class="input_right_L" for="txt_fecfin_c">Fecha Fin de Vigencia (*)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_fecfin_c" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                                <label class="label_to" for="ddl_tipcon_c">Tipo de Contrato (*)</label>
                                <asp:DropDownList CssClass="input_to" ID="ddl_tipcon_c" runat="server" Height="25px" Width="14.3%"></asp:DropDownList>

                                <label class="input_right_L" for="ddl_moneda_c">Moneda (*)</label>
                                <asp:DropDownList CssClass="input_right" ID="ddl_moneda_c" runat="server" Height="25px" Width="14.1%"></asp:DropDownList>

                                <label class="input_right_L" for="ddl_contratante_c">Contratante (*)</label>
                                <asp:DropDownList CssClass="input_right" ID="ddl_contratante_c" runat="server" Height="25px" Width="14.1%"></asp:DropDownList>

                                <label class="label_to" for="txt_participacion_cia_c">% Participacion VC (% DIS) (*)</label>
                                <asp:TextBox CssClass="input_to" ID="txt_participacion_cia_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_tasariesgo_c">Tasa de Riesgo (%)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_tasariesgo_c" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                                <label class="input_right_L" for="txt_tasareaseguro_c">Tasa Reaseguro (%)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_tasareaseguro_c" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                                <label class="label_to" for="txt_impuesto_c">Tasa de Impuesto (%)</label>
                                <asp:TextBox CssClass="input_to" ID="txt_impuesto_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_centro_costo">Centro Costo</label>
                                <asp:TextBox CssClass="input_right" ID="txt_centro_costo" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="label_to" for="txt_descrip_contrato">Descripción</label>
                                <asp:TextBox CssClass="input_to" ID="txt_descrip_contrato" runat="server" Height="25px" Width="46.2%"></asp:TextBox>

                                <label class="input_right_L" for="ddl_estado_c" runat="server">Estado :</label>
                                <asp:DropDownList CssClass="input_right" ID="ddl_estado_c" runat="server" Height="25px" Width="14.1%"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                       <!-- tipo de contrato detalle proporcional-->
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"> Detalle Tipo Contrato - Proporcional</span>
                                </a>
                            </h4>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse">
                            <div class="panel-body">

                                <label class="label_to" for="ddl_modalidad_c">Modalidad Contrato (*)</label>
                                <asp:DropDownList CssClass="input_to" ID="ddl_modalidad_c" runat="server" Height="25px" Width="14%"></asp:DropDownList>

                                <label class="input_right_L" for="txt_retencion_c">% de Retención :</label>
                                <asp:TextBox CssClass="input_right" ID="txt_retencion_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_cesion_c">% De Cesión :</label>
                                <asp:TextBox CssClass="input_right" ID="txt_cesion_c" runat="server" Height="25px" Width="14.4%"></asp:TextBox>

                                <label class="label_to" for="txt_montomax_retenc_c">Monto Maximo Retención :</label>
                                <asp:TextBox CssClass="input_to" ID="txt_montomax_retenc_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_montomax_cesion_c">Monto Maximo Cesión :</label>
                                <asp:TextBox CssClass="input_right" ID="txt_montomax_cesion_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_montopleno_c">Monto del Pleno (a) :</label>
                                <asp:TextBox CssClass="input_right" ID="txt_montopleno_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="label_to" for="txt_multiplo_c">N° Lineas Multiplo :</label>
                                <asp:TextBox CssClass="input_to" ID="txt_multiplo_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_mto_max_cubert_c">Monto Máximo Cobertura:</label>
                                <asp:TextBox CssClass="input_right" ID="txt_mto_max_cubert_c" runat="server" Height="25px" Width="14%"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <!-- tipo de contrato della no proporcional-->
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"> Detalle Tipo de Contrato - No Proporcional</span>
                                </a>
                            </h4>
                        </div>
                        <div id="collapseThree" class="panel-collapse collapse">
                            <div class="panel-body">

                                 <label class="label_to" for="txt_nrocapaxl_c1">Capa N° (XL) (*)</label>
                                <asp:TextBox CssClass="input_to" ID="txt_nrocapaxl_c1" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_prioridad_c1">Prioridad (*)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_prioridad_c1" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_excesoprio_c1">Cesión Exceso d/l Prioridad </label>
                                <asp:TextBox CssClass="input_right" ID="txt_excesoprio_c1" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="label_to" id="vacio1" style="visibility:hidden;"></label>
                                <input type="text" class="input_to" style="width:14%;height:25px;visibility:hidden;"/>

                                <label class="input_right_L" for="txt_mto_max_lim_sup_c1">Mto. Max. d/l Capa Lim. Súp. </label>
                                <asp:TextBox CssClass="input_right" ID="txt_mto_max_lim_sup_c1" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_primaminima_deposit_c1">Primas Minima ó  Depósito.</label>
                                <asp:TextBox CssClass="input_right" ID="txt_primaminima_deposit_c1" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <!--para segunda capa xl-->
                                <label class="label_to" for="txt_nrocapaxl_c2">Capa N° (XL) (*)</label>
                                <asp:TextBox CssClass="input_to" ID="txt_nrocapaxl_c2" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_prioridad_2">Prioridad (*)</label>
                                <asp:TextBox CssClass="input_right" ID="txt_prioridad_c2" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_excesoprio_c2">Cesión Exceso d/l Prioridad </label>
                                <asp:TextBox CssClass="input_right" ID="txt_excesoprio_c2" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="label_to" id="vacio2" style="visibility:hidden;"></label>
                                <input type="text" class="input_to" style="width:14%;height:25px;visibility:hidden;"/>

                                <label class="input_right_L" for="txt_mto_max_lim_sup_c2">Mto. Max. d/l Capa Lim. Súp. </label>
                                <asp:TextBox CssClass="input_right" ID="txt_mto_max_lim_sup_c2" runat="server" Height="25px" Width="14%"></asp:TextBox>

                                <label class="input_right_L" for="txt_primaminima_deposit_c2">Primas Minima ó  Depósito.</label>
                                <asp:TextBox CssClass="input_right" ID="txt_primaminima_deposit_c2" runat="server" Height="25px" Width="14%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="txt_idContrato_c" runat="server" Value="0"/>

                <div class="iframe">
                  <div id="tblContratoView"></div>
                </div>
            </asp:View>

            <!--seccion de reasegurador-->
            <asp:View ID="View4" runat="server">
                <label class="label_to" for="ddl_contrato_r" >Contrato (*)</label>
                <asp:DropDownList ID="ddl_contrato_r" CssClass="input_to" runat="server" Height="25px" Width="78.2%"></asp:DropDownList>

                <label class="label_to" for="ddl_reasegurador_r" >Resegurador (*)</label>
                <asp:DropDownList ID="ddl_reasegurador_r" CssClass="input_to" runat="server" Height="25px" Width="46%"></asp:DropDownList>

                <label class="input_right_L" for="txt_codreasegurador_r">Código de Reasegurador (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_codreasegurador_r" runat="server" Height="25px" Width="13.8%" ReadOnly="true"></asp:TextBox>

                <label class="label_to" for="ddl_tipcont_det_r">Modalidad Contrato Rea (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipcont_det_r" runat="server" Height="25px" Width="14%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_crediticia_r" >Calificación Crediticia (*)</label>
                <asp:DropDownList ID="ddl_crediticia_r" CssClass="input_right" runat="server" Height="25px" Width="14%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_calificadora_r" >Empresa Calificadora (*)</label>
                <asp:DropDownList ID="ddl_calificadora_r" CssClass="input_right" runat="server" Height="25px" Width="14%"></asp:DropDownList>

                <label class="label_to" for="txt_retencion_r">% de Retención :</label>
                <asp:TextBox CssClass="input_to" ID="txt_retencion_r" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                <label class="input_right_L" for="txt_cesion_r">% De Cesión :</label>
                <asp:TextBox CssClass="input_right numeric" ID="txt_cesion_r" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                <label class="input_right_L" for="txt_participacion_cesion" >Part. del Rea. Sobre la Cesion (%)</label>
                <asp:TextBox ID="txt_participacion_cesion" CssClass="input_right" runat="server" Height="25px" Width="14%"></asp:TextBox>

                <label class="label_to" style="width:94%;height:20px!important;text-align:left!important;">Empresa Corredora de Reaseguro</label>

                <label class="label_to" for="txt_nombre_rea">Nombre </label>
                <asp:TextBox CssClass="input_to" ID="txt_nombre_rea" runat="server" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_L" for="txt_nro_registro_rea">N° Registro </label>
                <asp:TextBox CssClass="input_right" ID="txt_nro_registro_rea" runat="server" Height="25px" Width="15%"></asp:TextBox>

                <asp:HiddenField ID="txt_idContratoDetalle_c" runat="server" Value="0"/>
                
                <div class="iframe">
                  <div id="tblReasegurador"></div>
                </div>
            </asp:View>

             <!--seccion de Contrato SYS-->
            <asp:View ID="View3" runat="server">
                <asp:HiddenField runat="server" ID="txt_ide_contrato_sis" Value="0"/>

                <label class="label_to" for="txt_nrocont_sys">Codigo de Contrato (*)</label>
                <asp:TextBox CssClass="input_to numeric" ID="txt_nrocont_sys" runat="server" Height="25px" Width="14%"></asp:TextBox>

                <label class="input_right_L" for="txt_ruc">Nº  Empresas (*)</label>
                <asp:TextBox CssClass="input_right numeric" ID="txt_numero_empresa" runat="server" Height="25px" Width="14.5%" ToolTip="Ingrese este Campo"></asp:TextBox>

               <%-- <label class="label_to" for="ddl_clase_contrato_sys">Nº de Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_clase_contrato_sys" runat="server" Height="25px" Width="14.3%" ToolTip="Selecione este Campo">                          
                </asp:DropDownList>--%>
                <label class="label_to" for="txtFechaInicio_sys">Fecha de Inicio (*)</label>
                <asp:TextBox CssClass="input_to datetime" ID="txtFechaInicio_sys" runat="server" Height="25px" Width="13.8%"></asp:TextBox>

                <label class="input_right_L" for="txtFechaFin_sys">Fecha de Fin (*)</label>
                <asp:TextBox CssClass="input_right datetime" ID="txtFechaFin_sys" runat="server" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_L" for="txtFechaFin_sys">Centro Costo(*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_centro_costo_sys" runat="server" Height="25px" Width="12.1%"></asp:TextBox>

                <label class="label_to" for="txtdescripcion_sys">Descripción (*)</label>
                <asp:TextBox CssClass="input_to" ID="txtdescripcion_sys" runat="server" Height="25px" Width="47.2%"></asp:TextBox>

                <label class="input_right_L" for="ddl_estado_sys" runat="server">Estado (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_estado_sys" runat="server" Height="25px" Width="12.1%"></asp:DropDownList>

                <div class="iframe">
                  <div id="tblContratoViewSyS"></div>
                </div>
            </asp:View>

            <asp:View ID="view5" runat="server">

               <asp:HiddenField ID="txt_ide_contrato_sis_det" Value="0" runat="server"/>
               <label class="label_to" for="ddl_contrato_r" >Contrato (*)</label>
               <asp:DropDownList ID="ddl_contrato_sis" CssClass="input_to" runat="server" Height="25px" Width="78.2%"></asp:DropDownList>

               <label class="label_to" for="ddl_contrato_r" >Cia. Seguros Vida (*)</label>
               <asp:DropDownList ID="ddl_compania_seg_vida" CssClass="input_to" runat="server" Height="25px" Width="78.2%"></asp:DropDownList>
                
               <label class="label_to" for="txt_ruc" title="Ingrese el valor expresado en porcentajes">% Participación (*)</label>
               <asp:TextBox CssClass="input_to numeric" ID="txt_participacion_sis" runat="server" Height="25px" Width="20%" ToolTip="Ingrese este Campo"></asp:TextBox>

               <label class="input_right_L" for="txt_ruc">Orden (*)</label>
               <asp:TextBox CssClass="input_right numeric" ID="txt_orden_empresa_sis" runat="server" Height="25px" Width="20%" ToolTip="Ingrese este Campo"></asp:TextBox>

                <span id="totalPorcentaje" style="float:left;margin:5px 50px;font-size:11px;"></span>
                <div class="iframe">
                  <div id="tblContratoViewSySDetalle"></div>
                </div>
            </asp:View>
        </asp:MultiView>    
    </div>
    <script src="Resources/js/bootstrap.min.js"></script>
</asp:Content>
