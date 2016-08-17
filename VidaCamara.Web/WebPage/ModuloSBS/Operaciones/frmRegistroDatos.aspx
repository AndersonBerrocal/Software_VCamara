<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmRegistroDatos.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.RegistroDatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/ModuloSBS/Operaciones/js/RegistroDato.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Comienzo de los Tabs-->
   <script runat="server">
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }
    </script>
      <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_borrar_r" runat="server" ToolTip="Elimianr" ImageUrl="~/Resources/Imagenes/u10_normal.png" OnClick="btn_borrar_r_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_detalle" runat="server" ToolTip="Ver Detalle" ImageUrl="~/Resources/Imagenes/Buscar_Documento.jpg" OnClick="btn_detalle_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_nuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Resources/Imagenes/u13_normal.jpg" OnClick="btn_nuevo_Click"/>
        </div>
    <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
            <Items>
                <asp:MenuItem Text="Primas" Value="0" Selected="true" />
                <asp:MenuItem  Text="IBNR" Value="1"/>
            </Items>
            <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
            <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
    </asp:Menu>  
    <!--Cuerpo de los tabs-->
    <div class="tabBody">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA PRIMAS-->
            <asp:View ID="view1" runat="server">
                    <label class="label_to" for="ddl_contrato_p">Contrato (*)</label>
                    <asp:DropDownList CssClass="input_to" ID="ddl_contrato_p" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                    <label class="input_right_L" for="cld_mesContable_p">Mes Registro Contable (*)</label>
                    <asp:TextBox CssClass="input_right" ID="txt_mesContable_p" runat="server" Height="25px" Width="30%"></asp:TextBox>

                    <label class="label_to" for="ddl_ramo_p">Ramo (*)</label>
                    <asp:DropDownList CssClass="input_to" ID="ddl_ramo_p" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                    <label class="input_right_L" for="ddl_producto_p">Producto (*)</label>
                    <asp:DropDownList CssClass="input_right" ID="ddl_producto_p" runat="server" Height="25px" Width="30%"  OnSelectedIndexChanged="ddl_producto_p_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                    <label class="label_to" for="">Total Abono Mes S/.</label>
                    <asp:Label CssClass="input_right" ID="lbl_totAbono" runat="server" Text="0" Height="25px" Width="30%"></asp:Label>

                    <label class="input_right_L" for="">Total Devengue Mes S/.</label>
                    <asp:Label CssClass="input_right" ID="lbl_totDevengue" runat="server" Text="0" Height="25px" Width="30%"></asp:Label>

                    <asp:GridView ID="gvPrima" runat="server" AutoGenerateColumns="true" OnRowEditing="gvPrima_RowEditing" OnRowCancelingEdit="gvPrima_RowCancelingEdit" OnRowUpdating="gvPrima_RowUpdating" AutoGenerateEditButton="True">
                        <HeaderStyle BackColor="#009bb8" ForeColor="White" />
                    </asp:GridView>

            </asp:View>

            <!--seccion de IBNR-->
            <asp:View ID="view2" runat="server">

                <label class="label_to" for="ddl_contrato_ib">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_ib" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                <label class="input_right_L" for="txt_mesContable_ib">Mes Registro Contable (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_mesContable_ib" runat="server" Height="25px" Width="30%"></asp:TextBox>

                <label class="label_to" for="ddl_ramo_ib">Ramo (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_ramo_ib" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_producto_ib">Producto (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_producto_ib" runat="server" Height="25px" Width="30%" AutoPostBack="True" OnSelectedIndexChanged="ddl_producto_ib_SelectedIndexChanged"></asp:DropDownList>

                <label class="label_to" for="lbl_toto_Abono_ib">Total Abono mes S/.</label>
                <asp:Label CssClass="input_right" ID="lbl_toto_Abono_ib" runat="server" Text="0" Height="25px" Width="30%"></asp:Label>

                <label class="input_right_L" for="lbl_tot_devengue_ib">Total Devengue mes </label>
                <asp:Label CssClass="input_right" ID="lbl_tot_devengue_ib" runat="server" Text="0" Height="25px" Width="30%"></asp:Label>

                <asp:GridView ID="gvIbnr" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="True" OnRowCancelingEdit="gvIbnr_RowCancelingEdit" OnRowEditing="gvIbnr_RowEditing" OnRowUpdating="gvIbnr_RowUpdating">
                    <HeaderStyle BackColor="#009bb8" ForeColor="White" />
                </asp:GridView>

            </asp:View>
        </asp:MultiView>  
    </div>

    <div id="divPopPup" class="divOpacity">
        <a class="close" href="javascript:void(0)">X</a>
        <div class="gridViewPop" id="gridShow">
            <asp:GridView ID="gvDatoM" runat="server" BackColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3"  ShowFooter="True" Font-Size="11px">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#009bb8" ForeColor="White" />
                <RowStyle ForeColor="black" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div> 
    </div> 
</asp:Content>
