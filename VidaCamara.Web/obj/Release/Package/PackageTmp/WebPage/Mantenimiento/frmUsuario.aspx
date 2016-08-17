<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="VidaCamara.Web.WebPage.Mantenimiento.frmUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/Mantenimiento/js/tblUsuarioView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script runat="server">
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }
    </script>
    <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnGuardar" ToolTip="Guardar" runat="server" ImageUrl="~/Resources/Imagenes/u14_normal.png"  OnClick="btnGuardar_Click"/>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnNuevo" ToolTip="Nuevo" runat="server" ImageUrl="~/Resources/Imagenes/u13_normal.jpg"/>
    </div>
    <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
            <Items>
                <asp:MenuItem Text="Usuarios" Value="0" Selected="true" />
                <asp:MenuItem  Text="Accesos" Value="1"/>
            </Items>

        <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
        <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
    </asp:Menu> 
    <!--Inicio de cuerpo del formulario--> 
   <div class="tabBody">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--vista usuario-->
            <asp:View ID="view1" runat="server">

                <asp:HiddenField  ID="txt_id_usuario" Value="0" runat="server"/>

                <label class="label_to" for="txt_usuario">Usuario :</label>
                <asp:TextBox CssClass="input_to" ID="txt_usuario" runat="server"  Height="25px" Width="15%"></asp:TextBox>

                <label  class="input_right_L" for="txt_nombres">Nombres :</label>
                <asp:TextBox CssClass="input_right" ID="txt_nombres" runat="server"  Height="25px" Width="44.5%"></asp:TextBox>

                <label class="label_to" for="txt_cargo">Cargo :</label>
                <asp:TextBox CssClass="input_to" ID="txt_cargo" runat="server"  Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_L" for="txt_email">E-mail :</label>
                <asp:TextBox CssClass="input_right" ID="txt_email" runat="server"  Height="25px" Width="44.5%"></asp:TextBox>

                <label class="label_to" for="ddl_tipusuario">Tipo Usuario :</label>
                <asp:TextBox CssClass="input_to" ID="txt_tipo_usuario" runat="server" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_L" for="txt_fecreg">Fecha Creación :</label>
                <asp:Label CssClass="input_right" ID="txt_fec_reg" runat="server" Height="25px" Width="15%" ></asp:Label>

                <label class="input_right_T" for="txt_fec_mod">Estado :</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_estado" runat="server" Height="25px" Width="15%">
                    <asp:ListItem Value="A">Activo</asp:ListItem>
                    <asp:ListItem Value="I">Inactivo</asp:ListItem>
                </asp:DropDownList>

                 <div class="iframe">
                    <div id="tblUsuarioView"></div>
                </div>
            </asp:View>
            <!--vista acceso-->
             <asp:View ID="view2" runat="server">
                 <asp:HiddenField runat="server" ID="lista_pagina" Value="0"/>
                 <asp:HiddenField runat="server" ID="ide_usuario" Value="0"/>
                 <div class="iframe">
                     <div id="frmUsuario" style="float:left;width:60%;"></div>
                     <div id="frmAccesoPagina" style="display:none;float:left;width:40%;"></div>
                 </div>
            </asp:View>
        </asp:MultiView>    
    </div>
</asp:Content>
