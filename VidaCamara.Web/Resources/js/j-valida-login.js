$(document).ready(function () {
    $("body").delegate("#txtUsuario", "focus", function () {
        $("#lbl_error_login").text("...");
    });
    $("body").delegate("#btnCancelar", "click", function (ev) {
        return false;
    });
    $("#form1").on("submit", function (ev) {
        if ($("#txtUsuario").val() == "") {
            MessageBox("Ingrese su Usuario");
            return false;
        } else if ($("#txtContrasena").val() == "") {
            MessageBox("Ingrese su Contraseña");
            return false;
        } else {
            return true;
        }
    });
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Validación Login', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
