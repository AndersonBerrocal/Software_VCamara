<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmOperacionManual.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.frmOperacionManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Operaciones/js/ProcesoManual.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnGuardar_m" ToolTip="Guardar" runat="server" ImageUrl="~/Resources/Imagenes/u14_normal.png" OnClick="btnGuardar_m_Click"/>
            <asp:ImageButton CssClass="btn_crud_button" ID="btnNuevo_m" ToolTip="Nuevo" runat="server" ImageUrl="~/Resources/Imagenes/u13_normal.jpg"/>
    </div>
    <div class="tabBody" id="tblopemanual">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_m">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_reasegurador_m">Reasegurador (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_reasegurador_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_tipope_m">Tipo Operación (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_tipope_m" runat="server" Height="25px" Width="15%" AutoPostBack="True" OnSelectedIndexChanged="ddl_tipope_m_SelectedIndexChanged"></asp:DropDownList>

                <label class="label_to" for="ddl_comprobante">Tipo Comprobante (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_comprobante" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_tipreg_m">Tipo Registro (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_tipreg_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_codasegurado_m">Asegurado (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_codasegurado_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="label_to" for="ddl_codramo_m">Ramo (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_codramo_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_codmoneda_m">Moneda (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_codmoneda_m" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_T" for="txt_primaced_m">Prima Cedida (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_primaced_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="label_to" for="txt_impuesto_m">Impuesto (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_impuesto_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_L" for="txt_prima_x_pag_m">Pri_XPag_Rea_Ced (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_prima_x_pag_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_prima_x_cob_m">Pri_Xcob_Rea_Ace (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_prima_x_cob_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="label_to" for="txt_sin_directo_m">Sin_Directo (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_sin_directo_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_L" for="txt_sin_x_cob_m">Sin_Xcob_Rea_Ced (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_sin_x_cob_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_sin_x_pag_m">Sin_Xpag_Rea_Ace (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_sin_x_pag_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="label_to" for="txt_otr_x_cob_m">Otr_Cta_Xcob_Rea_Ced (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_otr_x_cob_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_L" for="txt_otr_x_pag_m">Otr_Cta_Xpag_Rea_Ace (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_otr_x_pag_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_dscto_comis_m">Dscto_Comis_Rea (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_dscto_comis_m" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="label_to" for="ddl_origen_operacion">Origen Operación (*)</label>
                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl_origen_operacion" CssClass="input_to" Height="25px" Width="14.7%">
                    <asp:ListItem Text="Saldo Inicial" Value="1" />
                    <asp:ListItem Text="Otros" Value="2" />
                </asp:DropDownList>
            </asp:View>
        </asp:MultiView>  
    </div>
</asp:Content>
