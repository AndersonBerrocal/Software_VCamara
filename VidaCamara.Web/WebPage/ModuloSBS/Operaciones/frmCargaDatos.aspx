<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmCargaDatos.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloSBS.Operaciones.CargaDatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/Resources/CSS/Progressbar.css" rel="stylesheet" />
<script src="/WebPage/ModuloSBS/Operaciones/js/CargaDatos.js"></script>
<script src="/WebPage/ModuloSBS/Operaciones/js/CargaRsp.js"></script>

<script type="text/javascript">

    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            loading.css({ top: '40%', left: '45%' });
        }, 200);
    }
    $('form').live("submit", function () {
        var excelerror = $("#ctl00_ContentPlaceHolder1_hdf_excel_error").val();
        if (excelerror == 0) {
            ShowProgress();
        }
    });
</script>
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
            <asp:HyperLink CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_borrar_c" runat="server" ToolTip="Borrar" ImageUrl="~/Resources/Imagenes/u10_normal.png" OnClick="btn_borrar_c_Click"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Resources/Imagenes/upload.png" OnClick="btnGuardar_Click" />
        </div>
        <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
                <Items >
                    <asp:MenuItem  Text="Carga de Datos" Value="0" Selected="true" />
                    <asp:MenuItem  Text="RSP" Value="1"/>
                    <asp:MenuItem  Text="Pagos" Value="2"/>
                </Items>
            <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
            <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
        </asp:Menu>
      
    <!--Cuerpo de los tabs-->
    <div class="tabBody">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA CARGA DE DATOS-->
            <asp:View ID="view1" runat="server">

                <label class="label_to" for="dbl_contrato_d">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="dbl_contrato_d" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="txt_mesvig_d">Mes Vigente (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_mesvig_d" runat="server" Height="25px" Width="15%" ToolTip="Establece su Valor de Parametro (Mes vigente)" ReadOnly="true"></asp:TextBox>

                <label class="input_right_L" for="ddl_tipinfo_d">Tipo de Información (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipinfo_d" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_T" for="ddl_periodo_d" style="visibility:hidden;">Periodo Vigente (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_periodo_d" runat="server" Height="25px" Width="15%" Visible="false"></asp:DropDownList>

                <label class="label_to" for="fileUpload">Archivo (*)</label>
                <asp:FileUpload CssClass="input_to" ID="txt_archivo_d" ToolTip="Selecione el archivo a subir" runat="server" Height="25px" Width="48.4%" />

            </asp:View>

            <!--seccion de RSP-->
            <asp:View ID="view2" runat="server">
                <label class="label_to" for="ddl_contrato_r">Contrato :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_r" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <div class="iframe" id="CargaRSP">
                    <div id="tblRsp"></div>
                </div>
                <!--DIV PARA BOTONES DE CONTROL-->
                <div class="buttons" id="rsp">
                        <label for="lbl_total_rsp">Total Registros:</label>
                        <asp:Label ID="lbl_total_rsp" runat="server" Text="."></asp:Label>

                        <label  for="lbl_estado_rsp">Estado :</label>
                        <asp:Label  ID="lbl_estado_rsp" runat="server" Text="*"></asp:Label>

                        <label for="lbl_regsitro_errado_r">Registros Observados :</label>
                        <asp:Label ID="lbl_regsitro_errado_r" runat="server" Text="."></asp:Label>
                        
                        <label>Ver Registros Observados :</label>
                        <asp:ImageButton  CssClass="btn_crud_button" ID="btn_exportar_error_rsp" runat="server" ToolTip="Exportar Erradas" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btn_exportar_error_rsp_Click"/>
                </div>
            </asp:View>

            <!--seccion de calculo-->
            <asp:View ID="view3" runat="server">
                <label class="label_to" for="ddl_contrato_p">Contrato :</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato_p" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_tippago_p">Tipo de Pago</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_tippago_p" runat="server" Height="25px" Width="30%"></asp:DropDownList>

                 <div class="iframe" id="cargaDatos">
                     <div id="tableCargaDatos"></div>
                </div>
                <!--DIV PARA BOTONES DE CONTROL-->
                <div class="buttons" id="pagos">

                        <label for="lbl_total_rsp">Total Registros:</label>
                        <asp:Label ID="lbl_conteo_tot" runat="server" Text="."></asp:Label>

                        <label for="lbl_estado_p">Estado :</label>
                        <asp:Label ID="lbl_estado_p" runat="server" Text="*"></asp:Label>
                        
                        <label for="lbl_regsitro_errado_p">Registros Observados :</label>
                        <asp:Label ID="lbl_regsitro_errado_p" runat="server" Text="."></asp:Label>
                        
                        <label>Ver Registros Observados :</label>
                        <asp:ImageButton  CssClass="btn_crud_button_p" ID="btnExportError" runat="server" ToolTip="Exportar Erradas" ImageUrl="~/Resources/Imagenes/u123_normal.png" OnClick="btnExportError_Click"/>
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:HiddenField ID="hdf_excel_error" runat="server" Value="0"/>
        <asp:HiddenField ID="hdf_estado_borrar" runat="server" Value="A"/>

        <div class="loading">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Imagenes/loading19.gif" />
        </div>
    </div>
</asp:Content>
