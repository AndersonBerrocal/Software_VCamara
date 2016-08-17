<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmConsultaOperaciones.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Consultas.frmConsultaOperaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Consultas/js/tblOperacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Comienzo de los Tabs-->
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_Click" />
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_buscar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" />
        </div>
    <!--Cuerpo de los tabs-->
    <div class="tabBody" id="frmOperacion">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA OPERACIONES-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_o">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_o" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipcom_o">Tipo de Operación </label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipcom_o" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_ramo_o">Ramo </label>
                <asp:DropDownList CssClass="input_right" ID="ddl_ramo_o" runat="server" Height="25px" Width="14.8%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_producto_o" style="visibility:hidden;">Producto :</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_producto_o" runat="server" Height="25px" Width="14.6%" Visible="false"></asp:DropDownList>

                <label class="label_to" for="txt_operacion_o">N° de Operación </label>
                <asp:TextBox CssClass="input_to" ID="txt_operacion_o" runat="server" Height="25px" Width="14.6%"></asp:TextBox>

                <label class="input_right_L" for="txt_fec_ini_o">Fecha Inicial</label>
                <asp:TextBox CssClass="input_right" ID="txt_fec_ini_o" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_fec_hasta_o">Hasta  </label>
                <asp:TextBox CssClass="input_right" ID="txt_fec_hasta_o" runat="server" Height="25px" Width="14.7%"></asp:TextBox>


                <div class="iframe" id="tbl_operacion">
                    <div id="procesapago" style="display:none"></div>
                    <div id="procesaibnr" style="display:none"></div>
                    <div id="procesaprima" style="display:none"></div>
                    <div id="procesarsp" style="display:none"></div>
                    <div id="procesageneral" style="display:none"></div>
                </div>  

            </asp:View>
        </asp:MultiView>    
    </div>
</asp:Content>
