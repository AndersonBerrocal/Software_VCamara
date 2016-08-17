<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Inicio/mpFEPCMAC.Master" AutoEventWireup="true" CodeBehind="frmReglaArchivo.aspx.cs" Inherits="VidaCamara.Web.WebPage.Mantenimiento.frmReglaArchivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/WebPage/Mantenimiento/js/frmReglaArchivo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="btn_crud">
            <asp:HyperLink ID="HyperLink1" CssClass="btn_crud_button"  ToolTip="Inicio" runat="server" ImageUrl="~/Resources/Imagenes/u158_normal.png" NavigateUrl="~/Inicio"></asp:HyperLink>
            <asp:ImageButton CssClass="btn_crud_button" ID="btn_guardar" ToolTip="Guardar" runat="server" ImageUrl="~/Resources/Imagenes/u14_normal.png" OnClick="btn_guardar_Click"/>
            <asp:ImageButton CssClass="btn_crud_button" ID="btn_nuevo" ToolTip="Nuevo" runat="server" ImageUrl="~/Resources/Imagenes/u13_normal.jpg"/>
            <asp:ImageButton  CssClass="btn_crud_button" ID="btn_buscar" runat="server" ToolTip="Buscar" ImageUrl="~/Resources/Imagenes/u154_normal.png" />
    </div>
    <div class="tabBody" id="tblopemanual">
        <asp:MultiView id="multiTabs" ActiveViewIndex="0" Runat="server">
            <asp:View ID="view1" runat="server">
             
                <label class="label_to" for="ddl_contrato">Contrato (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_contrato" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_L" for="txt_idRegla">Id Regla (*)</label>
                <asp:TextBox CssClass="input_right numeric" ID="txt_idRegla" runat="server" Text="0" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_T" for="ddl_Archivo">Archivo (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_Archivo" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="label_to" for="ddl_tipo_linea">Tipo Linea (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipo_linea" runat="server" Height="25px" Width="15%"></asp:DropDownList>

                <label class="input_right_L" for="ddl_comprobante">Caracter Inicial (*)</label>
                <asp:TextBox runat="server" ID="txt_caracter_inicial" CssClass="input_right numeric" Height="25px" Width="15%"></asp:TextBox>

                <label class="input_right_T" for="txt_largo_Campo">Largo Campo (*)</label>
                <asp:TextBox runat="server" ID="txt_largo_Campo" CssClass="input_right numeric" Height="25px" Width="14.6%"></asp:TextBox>  

                <label class="label_to" for="ddl_codasegurado_m">Información Campo (*)</label>
                <asp:TextBox runat="server" ID="txt_informacion" CssClass="input_to" Height="25px" Width="77.6%"></asp:TextBox>  

                <label class="label_to" for="txt_formato">Formato Contenido (*)</label>
                <asp:TextBox runat="server" ID="txt_formato" CssClass="input_to" Height="25px" Width="77.6%"></asp:TextBox>

                <label class="label_to" for="ddl_tipo_validacion">Tipo de Validación (*)</label>
                <asp:DropDownList CssClass="input_to" ID="ddl_tipo_validacion" runat="server" Height="25px" Width="15%">
                    <asp:ListItem Text="---- Seleccione ---" Value="0" />
                    <asp:ListItem Text="EQUAL" Value="EQUAL" />
                    <asp:ListItem Text="BOOL_SP" Value="BOOL_SP" />
                    <asp:ListItem Text="IN" Value="IN" />
                    <asp:ListItem Text="VACIO" Value="" />
                    <asp:ListItem Text="IN_QUERY" Value="IN_QUERY" />
                    <asp:ListItem Text="BOOL_IF_SP" Value="BOOL_IF_SP" />
                    <asp:ListItem Text="FILLER" Value="FILLER" />
                </asp:DropDownList>

                <label class="input_right_L" for="txt_regla_validacion">Regla de Validación (*)</label>
                <asp:TextBox runat="server" ID="txt_regla_validacion" CssClass="input_to numeric" Height="25px" Width="44.6%"></asp:TextBox>

                <label class="label_to" for="txt_impuesto_m">Vigente (*)</label>
                <asp:DropDownList CssClass="input_right" ID="ddl_vigente" runat="server" Height="25px" Width="15%">
                    <asp:ListItem Text="SI" Value="1"/>
                    <asp:ListItem Text="NO" Value="0" />
                </asp:DropDownList>

                <label class="input_right_L" for="txt_nombre_Campo">Nombre Campo (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_nombre_Campo" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="input_right_T" for="txt_titulo">Titulo (*)</label>
                <asp:TextBox CssClass="input_right" ID="txt_titulo" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <label class="label_to" for="txt_titulo">Tipo Campo (*)</label>
                <asp:TextBox CssClass="input_to" ID="txt_tipo_Campo" runat="server" Height="25px" Width="14.7%"></asp:TextBox>

                <div class="iframe" id="tblReglaArchivo">
                    <div id="tblReglaArchivoGrid"></div>
               </div>
            </asp:View>
        </asp:MultiView>  
    </div>
</asp:Content>

