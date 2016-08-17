$(document).ready(function () {
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_anexo_i", "change", function () {
        var value = $(this).val();
        if (parseInt(value) == 8 || parseInt(value) == 9)
            $("#div_reasegurador").css({ "display": "block" });
        else
            $("#div_reasegurador").css({ "display": "none" });
    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnExcel", "click", function (ev) {
        var codReasegurador = $("#ctl00_ContentPlaceHolder1_ddl_reasegurador").val();
        var anexo = $("#ctl00_ContentPlaceHolder1_ddl_anexo_i").val();
        var fecha_crea = $("#ctl00_ContentPlaceHolder1_txt_fecha_creacion").val();
        var fecha_fin = $("#ctl00_ContentPlaceHolder1_txt_hasta").val();

        if (anexo == 8 || anexo == 9) {
            if (codReasegurador == "0") {
                MessageBox("Seleccione el reasegurador");
                return false;
            }
        }
        if (anexo == 0) {
            MessageBox("Selecione el Anexo"); return false;
        } else if (anexo == 3 || anexo == 4 || anexo == 5) {
            return true;
        } else if (anexo != 3 && anexo != 4 && anexo != 5 && fecha_crea != "" && fecha_fin != "") {
            return true;
        } else {
            MessageBox("Seleccione el Rango de Fechas"); return false;
        }
    });
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
