<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmInformes.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Consultas.frmInformes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Consultas/js/frmInformes.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="btn_crud">
        <asp:HyperLink   CssClass="btn_crud_button" ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
        <asp:ImageButton CssClass="btn_crud_button" ID="btnExcel" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btnExcel_Click" />
    </div>

    <!--Cuerpo de los tabs-->
    <div class="tabBody" id="frmInformes">
        <asp:MultiView ID="multiTabs" ActiveViewIndex="0" runat="server">
            <!--VISTA INFORMES-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_i" style="display:none;">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_i" runat="server" Height="25px" Width="77%" Visible="false"></asp:DropDownList>

                <label class="label_to" for="ddl_anexo_i">Anexo o Reporte (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_anexo_i" runat="server" Height="25px" Width="77%">
                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                    <asp:ListItem Value="1">ANEXO ES-2A | ES-2B</asp:ListItem>
                    <asp:ListItem Value="2">ES-18A</asp:ListItem>
                    <asp:ListItem Value="3">ES-18B</asp:ListItem>
                    <asp:ListItem Value="4">ES-18C</asp:ListItem>
                    <asp:ListItem Value="5">ES-18D</asp:ListItem>
                    <asp:ListItem Value="6">ES-18E</asp:ListItem>
                    <asp:ListItem Value="7">ES-18F</asp:ListItem>
                    <asp:ListItem Value="8">MODELO 1</asp:ListItem>
                    <asp:ListItem Value="9">MODELO 2</asp:ListItem>
                </asp:DropDownList> 

                <div  id="div_reasegurador" class="reasegurador" style="display:none">
                    <label class="label_to" for="ddl_reasegurador">Reasegurador (*)</label>
                    <asp:DropDownList runat="server" ID="ddl_reasegurador" CssClass="input_to" Height="25px" Width="77%"></asp:DropDownList>
                </div>

                <label class="label_to" for="ddl_fecreg_i">Fecha de Creación :</label>
                <asp:TextBox CssClass="input_to" ID="txt_fecha_creacion" runat="server" Height="25px" Width="14.5%"></asp:TextBox>

                <label class="input_right_L" for="ddl_hasta_i">Hasta :</label>
                <asp:TextBox CssClass="input_right" ID="txt_hasta" runat="server" Height="25px" Width="14.5%"></asp:TextBox>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
