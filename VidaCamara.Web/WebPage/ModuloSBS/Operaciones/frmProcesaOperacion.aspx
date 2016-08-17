<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmProcesaOperacion.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.frmProcesaOperacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Operaciones/js/ProcesaOperacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script runat="server">
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }
    </script>
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_borrar_po" runat="server" ToolTip="Cancelar" ImageUrl="~/Resources/Imagenes/u10_normal.png"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_procesa_ope" runat="server" ToolTip="Procesar" ImageUrl="~/Resources/Imagenes/u6_normal.png" OnClick="btn_procesa_ope_Click" />
            <asp:ImageButton CssClass="btn_crud_button" ID="btnNuevo" ToolTip="Nuevo" runat="server" ImageUrl="~/Resources/Imagenes/u13_normal.jpg"/>
        </div>
        <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
                <Items>
                    <asp:MenuItem Text="Procesar" Value="0" Selected="true" />
                    <asp:MenuItem  Text="Detalle" Value="1"/>
                </Items>
                <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
                <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
        </asp:Menu>
    <!--Cuerpo de los tabs-->
     <div class="tabBody">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA procesar-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_o" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipo_registro" runat="server">Tipo Registro (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipo_registro" runat="server" Height="25px" Width="14.4%"></asp:DropDownList>

                <label class="input_right_L" for="lbl_mes">Mes :</label>
                <asp:Label CssClass="input_to" ID="lbl_mes" runat="server" Text="_" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_T" for="lbl_trimestre">Trimestre :</label>
                <asp:Label CssClass="input_right" ID="lbl_trimestre" runat="server" Text="_" Height="25px" Width="15%"></asp:Label>

                <label class="label_to" for="ddl_tipinfo">Tipo de Operación (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipinfo" runat="server" Height="25px" Width="45%"></asp:DropDownList>

                <label class="label_to" for="lbl_regprocesado">Registros Procesados :</label>
                <asp:Label CssClass="input_to" ID="lbl_regprocesado" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_L" for="lbl_opecreada">Operaciones Creadas :</label>
                <asp:Label CssClass="input_right" ID="lbl_opecreada" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_T" for="lbl_totimporte">Total Importe S/.</label>
                <asp:Label CssClass="input_right" ID="lbl_totimporte" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>
            </asp:View>

            <!--seccion de detalle-->
            <asp:View ID="view2" runat="server">

                <label class="label_to" for="ddl_contrato_det">Contrato :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_det" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipinfo_det">Tipo de Información :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipinfo_det" runat="server" Height="25px" Width="45%"></asp:DropDownList>

                <div class="iframe" id="tbl_precosa_ope" style="display:none;">
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
