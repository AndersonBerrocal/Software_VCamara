<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="VidaCamara.Web.WebPage.Inicio.frmLogin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">

    <link type="text/css" rel="stylesheet" href="../../Resources/CSS/mpHead.css" />
    <link type="text/css" rel="stylesheet" href="../../Resources/CSS/Login.css" />
    <link type="text/css" rel="stylesheet" href="../../Resources/CSS/AjaxMenu.css" />
    <link type="text/css" rel="stylesheet" href="../../Resources/CSS/BarraHerramientas.css" />
    <link type="text/css" rel="stylesheet" href="../../Resources/CSS/Page.css" />
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/themes/redmond/jquery-ui.css" />
    
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="Resources/js/jqueryUI/jquery-ui.min.js"></script>
    <script src="Resources/js/j-valida-login.js"></script>

    <title>Vida Cámara - Login </title>

    <style type="text/css">
        #imgSecurity
        {height: 92px;width: 89px;}
        .style1{width: 80px;height: 35px;}
        .style2{ height: 35px;}
        .style3{height: 36px;}
        .auto-style1{height: 38px;}
        .TextoLogin0 {}
        .TextoLogin00 {}
        .buttonLogin {}
        #login_error{color:red;text-align:center;font-size:1.1em;font-family:'Segoe UI Semilight','Open Sans',Verdana,Arial,Helvetica,sans-serif;}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="border-style: none">
        <div class="Ubicacion">
            <asp:Panel ID="pnlLogin" runat="server" Width="398px" Height="258px">
                    <table style="width: 394px; font-family: calibri; font-size: 13px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" align="center" style="padding-left:20px">
                            <asp:Image ID="vida_camara" runat="server" 
                                ImageUrl="~/Resources/Imagenes/u21.jpg" Height="85px" 
                                Width="325px" />
                        </td>
                    </tr>

                        <tr>
                            <td colspan="4" style="height:15px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td rowspan="6" style="width:15px">&nbsp;</td>
                            <td rowspan="6" valign="top"> 
                                <img runat="server" id="imgSecurity" src="~/Resources/Imagenes/Login.jpg" class="imgSecurity" />   
                            </td>
                            <td class="style1">
                                Usuario: 
                            </td>
                            <td class="style2">
                                <asp:TextBox runat="server" ID="txtUsuario" CssClass="TextoLogin0" 
                                    MaxLength="25" TabIndex="1" Font-Names="Calibri" Font-Size="8pt" Height="21px" Width="192px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                Contraseña: 
                            </td>
                            <td class="style3">
                                <asp:TextBox runat="server" ID="txtContrasena" CssClass="TextoLogin00" 
                                    TextMode="Password" MaxLength="25" TabIndex="2" Font-Names="Calibri" Font-Size="8pt" Height="21px" Width="192px"  />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td align="left">
  
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right" class="auto-style1">
                                <asp:Button ID="btnLogin" runat="server" CssClass="buttonLogin" onclick="btnLogin_Click" Text="Aceptar" Width="111px" Height="25px" Font-Names="Calibri"  />
                                <button id="btnCancelar" Class="buttonLogin" style="height:25px;width:111px;font-family:Calibri"> Cancelar</button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                                <div id="login_error"><asp:Label ID="lbl_error_login" runat="server" Text="..."></asp:Label></div>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
        </div>
    </form>
</body>
</html>
