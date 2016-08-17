<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmCierreProceso.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.frmCierreProceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/WebPage/ModuloSBS/Operaciones/js/CierreOperacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script runat="server">
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }
    </script>

   <div id="content_header">
         <!--Botones de CRUD-->
        <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_procesa_cierre" runat="server" ToolTip="Procesar" ImageUrl="~/Resources/Imagenes/u6_normal.png" OnClick="btn_procesa_cierre_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exporta_excel" runat="server" ToolTip="Exportar" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exporta_excel_Click" />

        </div>
        <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
                <Items>
                    <asp:MenuItem Text="Cierre" Value="0" Selected="true" />
                    <asp:MenuItem  Text="Detalle de Cierres" Value="1"/>
                </Items>
                <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
                <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
        </asp:Menu>
    </div>
    <!--Cuerpo de los tabs-->
     <div class="tabBody">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA procesar-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="ddl_contrato_c">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_c" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="lbl_mes_c">Mes :</label>
                <asp:Label CssClass="input_to" ID="lbl_mes_c" runat="server" Text="_" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_L" for="lbl_trimestre_c">Trimestre :</label>
                <asp:Label CssClass="input_right" ID="lbl_trimestre_c" runat="server" Text="_" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_T" for="ddl_tipo_cierre">Tipo Cierre (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_tipo_cierre" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipinfo_c">Tipo de Informacion (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipinfo_c" runat="server" Height="25px" Width="45%"></asp:DropDownList>

                <label class="label_to" for="lbl_regprocesado_c">Registros Procesados :</label>
                <asp:Label CssClass="input_to" ID="lbl_regprocesado_c" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_L" for="lbl_opecreada_c">Operaciones Creadas :</label>
                <asp:Label CssClass="input_right" ID="lbl_opecreada_c" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>

                <label class="input_right_T" for="lbl_totimporte_c">Total Importe S/.</label>
                <asp:Label CssClass="input_right" ID="lbl_totimporte_c" runat="server" Text="0" Height="25px" Width="15%"></asp:Label>
            </asp:View>

            <!--seccion de detalle-->
            <asp:View ID="view2" runat="server">

                <label class="label_to" for="ddl_contrato_detC">Contrato :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_detC" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipinfo_detC">Tipo de Información :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipinfo_detC" runat="server" Height="25px" Width="45%"></asp:DropDownList>

                <div class="iframe" id="tbl_cierre" style="display:none">
                    <div id="tblcierreproceso"></div>
                </div>
            </asp:View>

        </asp:MultiView>    
    </div>
</asp:Content>
