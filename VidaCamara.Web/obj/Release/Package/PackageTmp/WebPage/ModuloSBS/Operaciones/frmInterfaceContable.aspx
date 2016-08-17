<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmInterfaceContable.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.frmInterfaceContable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/ModuloSBS/Operaciones/js/InterfaceContable.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btn_borrar" ToolTip="Eliminar" runat="server" ImageUrl="~/Resources/Imagenes/u10_normal.png" OnClick="btn_borrar_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_procesar" runat="server" ToolTip="Procesar" ImageUrl="~/Resources/Imagenes/u6_normal.png" OnClick="btn_procesar_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_Click" />
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_transfer" TollTip="Transferir Data" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/data_transfer.jpg" OnClick="btn_transfer_Click"/>
        </div>
    <!--Cuerpo de los tabs-->
     <div class="tabBody" id="frmInterface">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--seccion de interface contable-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_i">Contrato :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_i" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tip_operacion_i">Tipo de Información :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tip_operacion_i" runat="server" Height="25px" Width="41.5%"></asp:DropDownList>

                <label class="input_right_L" for="txt_fecha_vigente_i">Mes y Año Vigente</label>
                <asp:Label CssClass="input_right" ID="txt_fecha_vigente_i" runat="server" Height="25px" Width="15%" Text="."></asp:Label>

                <div class="iframe">
                    <div id="tbl_interface"></div>
                </div>
            </asp:View>

        </asp:MultiView>    
    </div>
</asp:Content>
