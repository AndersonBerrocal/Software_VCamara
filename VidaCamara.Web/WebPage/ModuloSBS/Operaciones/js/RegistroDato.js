$(document).ready(function () {
    $("#ctl00_ContentPlaceHolder1_gvPrima tr td:nth-child(2) input[type='text']").attr("readonly", true).css({ "border": "none", "text-align": "center" });
    $("#ctl00_ContentPlaceHolder1_gvIbnr tr td:nth-child(2) input[type='text'],#ctl00_ContentPlaceHolder1_gvIbnr tr td:nth-child(5) input[type='text']").attr("readonly", true).css({ "border": "none", "text-align": "center", "background": "rgba(0,0,0,.1)" });
    $("#ctl00_ContentPlaceHolder1_gvPrima tr th:nth-child(1)").text("ACCIONES");
    $("#ctl00_ContentPlaceHolder1_gvIbnr tr th:nth-child(1)").text("ACCIONES");
    
    $('section').delegate('.close', 'click', function () {
        $('#divPopPup').attr('style', 'visibility', 'hidden');
    });
    //validacion prima
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_detalle", "click", function () {
        var contratoP = $('#ctl00_ContentPlaceHolder1_ddl_contrato_p').val();
        var contratoI = $('#ctl00_ContentPlaceHolder1_ddl_contrato_ib').val();
        var codramoP = $("#ctl00_ContentPlaceHolder1_ddl_ramo_p").val();
        var codrampI = $("#ctl00_ContentPlaceHolder1_ddl_ramo_ib").val();
        if (contratoP == 0 || contratoI == 0) { MessageBox("Selecione Contrato"); return false; }
        else if (codramoP == 0 || codrampI == 0) { MessageBox("Selecione el Ramo"); return false; }
        else { return true }
    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_enviar", 'click', function () {

        var numero = $("#ctl00_ContentPlaceHolder1_txt_primaEst_p").length;
        if (numero == 1) {
            var idprima = $("#ctl00_ContentPlaceHolder1_hdf_id_prima").val();
            if (idprima == 0) {
                var contratoP = $('#ctl00_ContentPlaceHolder1_ddl_contrato_p').val();
                var mesD = $('#ctl00_ContentPlaceHolder1_ddl_mesContable_p').val();
                var mesc = $("#ctl00_ContentPlaceHolder1_ddl_mesDevengue_p").val();
                var productoP = $('#ctl00_ContentPlaceHolder1_ddl_producto_p').val();
                var ramoP = $("#ctl00_ContentPlaceHolder1_ddl_ramo_p").val();
                var importeP = $('#ctl00_ContentPlaceHolder1_txt_impAbono_p').val();
                var primaP = $("#ctl00_ContentPlaceHolder1_txt_primaEst_p").val();
                if (contratoP == 0 || mesD == 0 || mesc == 0 || productoP == 0 || ramoP == 0 || importeP == "" || primaP == "") {
                    MessageBox("Ingrese y Selecione los campos requeridos"); return false;
                } else {
                    return confirm("Está Seguro de Grabar?");
                }
            } else {
                return confirm("¿ Está Seguro de Actualizar el Registro ?");
            }
        } else {
            var idibnr = $("#ctl00_ContentPlaceHolder1_hdf_id_ibnr").val();
            if (idibnr == 0) {
                var contratoIB = $('#ctl00_ContentPlaceHolder1_ddl_contrato_ib').val();
                var mesIB = $('#ctl00_ContentPlaceHolder1_ddl_mesDevengue_ib').val();
                var mesIB = $("#ctl00_ContentPlaceHolder1_ddl_mesContable_ib").val();
                var productoIB = $('#ctl00_ContentPlaceHolder1_ddl_producto_ib').val();
                var ramoIB = $("#ctl00_ContentPlaceHolder1_ddl_ramo_ib").val();
                var importeIB = $('#ctl00_ContentPlaceHolder1_txt_impAbono_ib').val();

                if (contratoIB == 0 || mesIB == 0 || mesIB == 0 || productoIB == 0 || ramoIB == 0 || importeIB == "") {
                    MessageBox("Ingrese y Selecione los campos requeridos"); return false;
                } else {
                    return confirm("Está Seguro de Grabar?");
                }
            } else {
                return confirm("¿ Está Seguro de Actualizar el Registro ?");
            }
        }
    });
    //validacion de borrar datom
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_borrar_r", 'click', function () {
        var contratoP = $("#ctl00_ContentPlaceHolder1_ddl_contrato_p").val();
        var codramoP = $("#ctl00_ContentPlaceHolder1_ddl_ramo_p").val();
        var contratoI = $("#ctl00_ContentPlaceHolder1_ddl_contrato_ib").val();
        var codramoI = $("#ctl00_ContentPlaceHolder1_ddl_ramo_ib").val();
        var codRamo = "";

        if (contratoP == 0 || contratoI == 0) {
            MessageBox("Seleccione el Contrato"); return false;
        } else if (codramoP == 0 || codramoI == 0) {
            MessageBox("Seleccione el Ramo"); return false;
        } else {
            if (codramoI == undefined) {
                codRamo = $("#ctl00_ContentPlaceHolder1_ddl_ramo_p option:selected").text();
            } else {
                codRamo = $("#ctl00_ContentPlaceHolder1_ddl_ramo_ib option:selected").text();;
            }
            return confirm("¿Esta seguro de Borrar los Registros de " + codRamo+" ?");
        }
    });
    //funcion mensaje
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
