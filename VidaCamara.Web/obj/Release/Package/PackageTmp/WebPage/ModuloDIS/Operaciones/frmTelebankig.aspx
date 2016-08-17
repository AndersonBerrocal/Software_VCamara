<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmTelebankig.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloDIS.Operaciones.frmTelebankig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/ModuloDIS/operaciones/js/frmTelebanking.js"></script>
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

                <label class="label_to" for="ddl_contrato_c">Contrato </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <%--<label class="label_to" for="ddl_tipcom_c">Tipo de Archivo </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipo_archivo" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>--%>

                <label class="label_to" for="ddl_ramo_c">Fecha Aprobación</label>
                <asp:TextBox ID="txt_fecha" runat="server"  CssClass="input_to datetime" Height="25px" Width="25%"/>

                <label class="input_right_L" for="ddl_estado">Estado</label>
                <asp:DropDownList runat="server" ID="ddl_estado" CssClass="input_right" Height="25px" Width="25%">
                    <asp:ListItem Text="---Todos---" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Aprobados" Value="A" />
                    <asp:ListItem Text="Pagados"  Value="P"/>
                </asp:DropDownList>

                <div class="iframe">
                    <div id="tblTelebanking"></div>
                </div>  
            </asp:View>
        </asp:MultiView>
    <iframe id="fileDonwload" src="" style="display:none;visibility:hidden;width:1px;height:1px"></iframe>
    </div>
</asp:Content>
