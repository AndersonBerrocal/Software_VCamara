<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmSegConsulta.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloDIS.Consultas.frmSegConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../../../Resources/CSS/bootstrap.css" />
<script src="/WebPage/ModuloDIS/Consultas/script/frmSeqConsulta.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--Comienzo de los Tabs-->
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_consultar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" OnClick="btn_consultar_Click1" />
        </div>
    <!--Cuerpo de los tabs-->
    <div class="tabBody" id="frmOperacion">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA OPERACIONES-->
            <asp:View ID="view1" runat="server">
                <label class="label_to" for="ddl_contrato_o">Contrato SIS(*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato" ClientIDMode="Static" runat="server" Height="25px" Width="48.2%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_tipo_archivo">Tipo de Archivo </label>
                <asp:DropDownList CssClass="input_right" ID="ddl_tipo_archivo" ClientIDMode="Static" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="label_to">Moneda </label>
                <asp:DropDownList runat="server" ID="ddl_moneda" ClientIDMode="Static" Height="25px" Width="15.2%" CssClass="input_to"></asp:DropDownList>

                <label class="input_right_L" for="txt_fec_ini_o">F. Carga Desde</label>
                <asp:TextBox CssClass="input_right datetime" ClientIDMode="Static" ID="txt_fec_ini_o" runat="server" Height="25px" Width="14.8%" ></asp:TextBox>

                <label class="input_right_T" for="txt_fec_hasta_o">F. Carga Hasta </label>
                <asp:TextBox CssClass="input_right datetime" ClientIDMode="Static" ID="txt_fec_hasta_o" runat="server" Height="25px" Width="14.7%" ></asp:TextBox>

                <label class="label_to" for="ddl_ramo_o">AFP </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_afp" runat="server" ClientIDMode="Static" Height="25px" Width="15.2%"></asp:DropDownList>

                <label class="input_right_L" for="txt_fecha_aprobacion_inicio">F. Aprobación Desde</label>
                <asp:TextBox CssClass="input_right datetime" ID="txt_fecha_aprobacion_inicio" ClientIDMode="Static" runat="server" Height="25px" Width="14.8%" ></asp:TextBox>

                <label class="input_right_T" for="txt_fecha_aprobacion_hasta">F. Aprobación Hasta</label>
                <asp:TextBox CssClass="input_right datetime" ID="txt_fecha_aprobacion_hasta" ClientIDMode="Static" runat="server" Height="25px" Width="14.7%" ></asp:TextBox>

                <label class="label_to">CUSPP </label>
                <asp:TextBox runat="server" ID="txt_cod_cusp" ClientIDMode="Static" CssClass="input_to" Height="25px" Width="14.8%"/>

                <label class="input_right_L">Nombres</label>
                <asp:TextBox runat="server" ID="txt_nombre" ClientIDMode="Static" CssClass="input_right" Height="25px" Width="14.8%"/>

                <label class="input_right_T">Apellidos</label>
                <asp:TextBox runat="server" ID="txt_apellido" ClientIDMode="Static" CssClass="input_right" Height="25px" Width="14.8%"/>

                <label class="label_to">DNI</label>
                <asp:TextBox runat="server" ID="txt_dni" ClientIDMode="Static" CssClass="input_to" Height="25px" Width="14.8%"/>

                <label class="input_right_L">Nº Solicitud</label>
                <asp:TextBox runat="server" ID="txt_nro_solicitud" ClientIDMode="Static" CssClass="input_right" Height="25px" Width="14.8%"/>

                <label class="input_right_T">Estado</label>
                <asp:DropDownList runat="server" ID="ddl_estado" ClientIDMode="Static" CssClass="input_right"  Height="25px" Width="14.8%">
                    <asp:ListItem Text="Seleccione" Value="0" Selected="True"/>
                    <asp:ListItem Text="Aprobados" Value="A" />                    
                    <asp:ListItem Text="Creados" Value="C" />
                    <asp:ListItem Text="Errados" Value="E" />
                    <asp:ListItem Text="Inactivos" Value="I" />
                    <asp:ListItem Text="Pagados (Solo Nomina)" Value="P" />
               </asp:DropDownList>

                <div class="iframe" id="tblConsulta1">
                    <%--<asp:GridView ID="gv_archivo_cargado" runat="server" ClientIDMode="Static" CssClass="table table-hover" Font-Size="9px"></asp:GridView>--%>
                    <div id="frmSeqConsulta"></div>
                </div>  

            </asp:View>
        </asp:MultiView>    
    </div>
</asp:Content>
