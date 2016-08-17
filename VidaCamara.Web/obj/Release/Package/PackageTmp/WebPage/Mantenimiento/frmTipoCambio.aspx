<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmTipoCambio.aspx.cs" Inherits="VidaCamara.Web.WebPage.Mantenimiento.frmTipoCambio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/Mantenimiento/js/frmTipoCambio.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btn_guardar" ToolTip="Guardar" runat="server" ImageUrl="~/Resources/Imagenes/u14_normal.png" OnClick="btn_guardar_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_buscar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" />
    </div>
    <div class="tabBody" id="tblopemanual">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <asp:View ID="view1" runat="server">
             
                <label class="label_to" for="txt_periodo">Periodo (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_periodo" runat="server" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_L" for="txt_monto">Monto (*)</label>
                <asp:TextBox CssClass="input_right numeric" ID="txt_monto" runat="server" Text="0.00" Height="25px" Width="15%"></asp:TextBox>              

                <div class="iframe" id="tblTipoCambio">
                    <div id="tblTipoCambioGrid"></div>
               </div>
            </asp:View>
        </asp:MultiView>  
    </div>
</asp:Content>
