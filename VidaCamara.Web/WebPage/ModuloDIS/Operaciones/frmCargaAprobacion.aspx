<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmCargaAprobacion.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloDIS.Operaciones.CargaAprobacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/ModuloDIS/operaciones/js/ApruebaCarga.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content_header">
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_buscar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" />
        </div>
    </div>
        <div class="tabBody" id="frmComprobante">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA OPERACIONES-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato">Contrato </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipcom_c">Tipo de Archivo </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipo_archivo" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_ramo_c">F Carga Desde</label>
                <asp:TextBox runat="server"  CssClass="input_right datetime" ID="txt_fecha_inicio" Height="25px" Width="14.8%"/>

                <label class="input_right_T" for="ddl_ramo_c">F. Carga Hasta </label>
                <asp:TextBox runat="server"  CssClass="input_right datetime" ID="txt_fecha_fin" ClientIDMode="Static" Height="25px" Width="14.8%"/>

                <div class="iframe">
                    <div id="tblApruebaCarga"></div>
                </div>  

            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
