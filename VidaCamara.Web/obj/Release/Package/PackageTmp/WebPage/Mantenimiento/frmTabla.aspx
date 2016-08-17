<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmTabla.aspx.cs" Inherits="VidaCamara.Web.WebPage.Mantenimiento.frmTabla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/Mantenimiento/js/tblTableView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_borrar" runat="server" ToolTip="Eliminar" ImageUrl="~/Resources/Imagenes/u10_normal.png"  OnClientClick="return DeleteMethod();" OnClick="btn_borrar_Click" />
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_enviar_t" runat="server" ToolTip="Guardar" ImageUrl="~/Resources/Imagenes/u14_normal.png" OnClick="btn_enviar_t_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_nuevo_t" runat="server"  ToolTip="Nuevo" ImageUrl="~/Resources/Imagenes/u13_normal.jpg" OnClick="btn_nuevo_t_Click"  />
    </div>

   <div class="tabBody" id="frmTabla">
        <label class="label_to" for="ddl_tabla_t">Tipo de Tabla (*)</label>
        <asp:DropDownList CssClass="input_to" ID="ddl_tabla_t" runat="server" Height="25px" Width="78.2%"></asp:DropDownList>

        <label  class="label_to" for="txt_codigo_t">Codigo (*)</label>
        <asp:TextBox CssClass="input_right" ID="txt_codigo_t" runat="server"  Height="25px" Width="10.2%"></asp:TextBox>

        <label class="input_right_T" for="txt_descripcion_t">Descripción (*)</label>
        <asp:TextBox CssClass="input_right" ID="txt_descripcion_t" runat="server"  Height="25px" Width="53%"></asp:TextBox>

        <label class="label_to" for="txt_valor_t">Valor :</label>
        <asp:TextBox CssClass="input_to" ID="txt_valor_t" runat="server"  Height="25px" Width="10%"></asp:TextBox>

        <label class="input_right_T" for="txt_clase_t">Clase :</label>
        <asp:TextBox CssClass="input_right" ID="txt_clase_t" runat="server"  Height="25px" Width="17%"></asp:TextBox>

        <label class="input_right_F" for="txt_tipo_t">Tipo :</label>
        <select id="select_operacion" class="input_right" style="height:27px;width:10%;display:none;"></select>
        <asp:TextBox CssClass="input_right" ID="txt_tipo_t" runat="server"  Height="25px" Width="10%" Visible="true"></asp:TextBox>

        <label class="input_right_F" for="ddl_estado_t">Estado (*)</label>
        <asp:DropDownList CssClass="input_right" ID="ddl_estado_t" runat="server" Height="25px" Width="10%"></asp:DropDownList>

        <asp:HiddenField ID="txt_9999" runat="server" Value="0"/>
        <asp:HiddenField ID="txt_idtabla" runat="server" Value="0" />

       <div class="iframe" id="tblConcepto">
            <div id="tblTablaView"></div>
       </div>  
    </div>
</asp:Content>
