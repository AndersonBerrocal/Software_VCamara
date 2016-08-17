<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmConsultaComprobante.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Consultas.frmConsultaComprobante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Consultas/js/tblComprobante.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content_header">
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_Click" />
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_buscar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" />
        </div>
    </div>
      
    <!--Cuerpo de los tabs-->
    <div class="tabBody" id="frmComprobante">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA OPERACIONES-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_c">Contrato </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_c" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipcom_c">Tipo de Comprobante </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipcom_c" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_ramo_c">Ramo </label>
                <asp:DropDownList CssClass="input_right" ID="ddl_ramo_c" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_producto_c" style="visibility:hidden;">Producto </label>
                <asp:DropDownList CssClass="input_right" ID="ddl_producto_c" runat="server" Height="25px" Width="14.6%" Visible="false"></asp:DropDownList>

                <label class="label_to" for="txt_operacion_c">N° de Comprobante </label>
                <asp:TextBox CssClass="input_to" ID="txt_operacion_c" runat="server" Height="25px" Width="14.6%"></asp:TextBox>

                <label class="input_right_L" for="txt_fec_ini">Fecha Inicial</label>
                <asp:TextBox CssClass="input_right" ID="txt_fec_ini" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_fec_hasta">Hasta  </label>
                <asp:TextBox CssClass="input_right" ID="txt_fec_hasta" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <div class="iframe" id="tbl_comprobante">
                    <div id="tblcomprobante"></div>
                </div>  

            </asp:View>
        </asp:MultiView>
        <asp:HiddenField ID="hdf_ide_contrato_c" runat="server" Value="0"/>
        <asp:HiddenField ID="hdf_tip_comp_c" runat="server" Value="0"/>
        <asp:HiddenField ID="hdf_cod_ramo_c" runat="server" Value="0"/>
        <asp:HiddenField ID="hdf_nro_comp_c" runat="server" Value=""/>
        <asp:HiddenField ID="hdf_fecha_ini_c" runat="server" Value=""/>
        <asp:HiddenField ID="hdf_fecha_fin_c" runat="server" Value=""/>
    </div>
</asp:Content>
