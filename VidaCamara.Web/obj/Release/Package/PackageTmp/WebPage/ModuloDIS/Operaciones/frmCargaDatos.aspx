<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmCargaDatos.aspx.cs" Inherits="VidaCamara.Web.WebPage.ModuloDIS.Operaciones.CargaDatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../../../Resources/CSS/bootstrap.css" />
<link rel="stylesheet" href="../../../Resources/CSS/Progressbar.css" />
<style>.margin-left{margin-left:15px;}</style>
<script src="../../../Resources/js/bootstrap.min.js"></script>
<script src="/WebPage/ModuloDIS/Operaciones/js/CargaDatos.js"></script>
<script src="/WebPage/ModuloDIS/Operaciones/js/frmReglaArchivo.js"></script>

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
        ShowProgress();
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
            <asp:ImageButton  CssClass="btn_crud_button" ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Resources/Imagenes/upload.png" OnClick="btnGuardar_Click" />
        </div>
        <asp:Menu id="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" Runat="server">
                <Items >
                    <asp:MenuItem  Text="Carga" Value="0" Selected="true" />
                    <asp:MenuItem  Text="Detalle" Value="1"/>
                    <asp:MenuItem  Text="Regla de Validación" Value="2"/>
                </Items>
            <StaticMenuItemStyle CssClass="tab"></StaticMenuItemStyle>
            <StaticSelectedStyle CssClass="selectedTab" BackColor="#006666"></StaticSelectedStyle>
        </asp:Menu>
      
    <!--Cuerpo de los tabs-->
    <div class="tabBody">
        <asp:HiddenField ID="control_grid" value="0"  runat="server"/>               
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <!--VISTA CARGA DE DATOS-->
            <asp:View ID="view1" runat="server">      
                          
                <label class="label_to" for="dbl_contrato_d">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_conrato1" runat="server" Height="25px" Width="77%"></asp:DropDownList>

                <label class="label_to" for="fileUpload">Nombre del Archivo (*)</label>
                <asp:FileUpload CssClass="input_to" ID="fileUpload" ToolTip="Selecione el archivo a subir" runat="server" Height="25px" Width="48.4%" />

                <label class="label_to">Tipo del Archivo (*)</label>
                <asp:DropDownList runat="server" ID="ddl_tipo_archivo" CssClass="input_to" Height="25px" Width="40%"></asp:DropDownList>

                <asp:CheckBox ID="chk_permitir" runat="server" Text="Permitir reemplazar archivo existente" CssClass="input_right margin-left" Height="25px" Width="20%" Visible="false"/>
            </asp:View>
            <!--seccion de RSP-->
            <asp:View ID="view2" runat="server">
               
               <label class="label_to" for="ddl_nombre_archivo_det">Nombre del Archivo (*)</label>
               <asp:Label ID="txt_nombre_archivo_det" Text="." runat="server"  CssClass="input_to" Width="22%"/>

                <label class="input_right_L" for="fileUpload">Importe</label>
                <asp:Label ID="txt_total_importe" runat="server" CssClass="input_right" Text="0.00" Width="15%"></asp:Label>

                <label class="input_right_T" for="fileUpload">Moneda</label>
                <asp:Label ID="txt_moneda" runat="server" CssClass="input_right" Text="."></asp:Label>

                <label class="label_to" for="dbl_contrato_d">Tipo del Archivo (*)</label>
                <asp:Label  ID="txt_tipo_informacion_det" Text="." runat="server"  CssClass="input_to" Width="22%"/>

                <label class="input_right_L" for="fileUpload">Lineas Procesados</label>
                <asp:Label ID="txt_registro_procesado" runat="server" CssClass="input_right" Text="0" Width="15%"></asp:Label>

                <label class="input_right_T" for="fileUpload">Datos Observados</label>
                <asp:Label ID="txt_registro_observado" runat="server" CssClass="input_right" Text="0"></asp:Label>

                <div style="margin-top:7%;">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h6 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" data-target="#screen1" aria-expanded="true" class="collapsed" href="javascript:void(0)">
                                        <span class="glyphicon glyphicon-plus-sign"></span> Información Cargada
                                    </a>
                                </h6>
                            </div>
                            <div id="screen1" class="panel-collapse collapse fade in" aria-expanded="false">
                                <div class="panel-body">
                                    <div class="iframe" id="Cargada">
                                        <div id="frmCargaExito"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
           
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h6 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" data-target="#screen2" aria-expanded="false" class="collapsed" href="javascript:void(0)">
                                        <span class="glyphicon glyphicon-plus-sign"></span> Información Observada
                                    </a>
                                </h6>
                            </div>
                            <div id="screen2" class="panel-collapse collapse fade" aria-expanded="false">
                                <div class="panel-body">
                                    <div class="iframe" id="Observada">
                                        <div id="frmCargaObservado"></div>
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>           
                </div>
            </asp:View>
            <asp:View ID="view3" runat="server">

                <label class="label_to" for="txt_nombre_archivo_inf">Nombre del Archivo (*)</label>
                <asp:Label ID="txt_nombre_archivo_inf" Text="_" runat="server"  CssClass="input_to" Width="35%"/>

                <label class="input_right_L" for="ddl_tipinfo_d">Id Regla (*)</label>
                <asp:TextBox CssClass="input_right numeric" ID="txt_idregla" runat="server"  Height="25px" Width="17%"></asp:TextBox>
                <a href="#" id="buscar_regla_id"><img src="Resources/Imagenes/u154_normal.png" style="width:30px;height:25px;float:left;margin-left:3px;"/></a> 

                <label class="label_to" for="txt_tipo_archivo">Tipo del Archivo (*)</label>
                <asp:Label ID="txt_tipo_archivo_inf" Text="_" runat="server"  CssClass="input_to" Width="35%"/>

                <label class="input_right_L" for="ddl_tipinfo_d">Tipo de Línea (*)</label>
                <asp:DropDownList runat="server" ID="ddl_tipo_linea" CssClass="input_to" Height="25px" Width="20%"></asp:DropDownList>
                
                <asp:HiddenField ID="hdf_tipo_archivo" runat="server" Value="0"/>
                <div class="iframe" id="informacion">
                    <div id="tblReglaArchivo"></div>
                </div>
            </asp:View>
        </asp:MultiView>
        <div class="loading">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Imagenes/loading19.gif" />
        </div>
    </div>
</asp:Content>
