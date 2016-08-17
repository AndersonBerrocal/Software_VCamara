$(document).ready(function () {
    $('#ctl00_ContentPlaceHolder1_txt_primaced_m,#ctl00_ContentPlaceHolder1_txt_impuesto_m,#ctl00_ContentPlaceHolder1_txt_prima_x_pag_m,#ctl00_ContentPlaceHolder1_txt_prima_x_cob_m,#ctl00_ContentPlaceHolder1_txt_sin_directo_m,#ctl00_ContentPlaceHolder1_txt_sin_x_cob_m,#ctl00_ContentPlaceHolder1_txt_sin_x_pag_m,#ctl00_ContentPlaceHolder1_txt_otr_x_cob_m,#ctl00_ContentPlaceHolder1_txt_otr_x_pag_m,#ctl00_ContentPlaceHolder1_txt_dscto_comis_m').numeric(".");
    $("#tblopemanual input[type='text']").val("0.00");
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnGuardar_m", "click", function (ev) {
        var codReasegurador = $("#ctl00_ContentPlaceHolder1_ddl_reasegurador_m").val();
        var tipOperacion = $("#ctl00_ContentPlaceHolder1_ddl_tipope_m").val();
        var tipComprobante = $("#ctl00_ContentPlaceHolder1_ddl_comprobante").val();
        var tipoRegistro = $("#ctl00_ContentPlaceHolder1_ddl_tipreg_m").val();
        var codAsegurado = $("#ctl00_ContentPlaceHolder1_ddl_codasegurado_m").val();
        var codRamo = $("#ctl00_ContentPlaceHolder1_ddl_codramo_m").val();
        var codMoneda = $("#ctl00_ContentPlaceHolder1_ddl_codmoneda_m").val();
        var calcula = 0;
        $("#tblopemanual input[type='text'],#tblopemanual select").each(function (index, element) {
            valor = $(this).val();
            if (valor == 0) {
                calcula++;
            }
        });
        if ($("#ctl00_ContentPlaceHolder1_ddl_contrato_m").val() == "0") {
            MessageBox("Seleccione el Contrato"); return false;
        } else if (codReasegurador == "0") {
            MessageBox("Seleccione reasegurador"); return false;
        } else if (tipOperacion == "0") {
            MessageBox("Seleccione el tipo de operación"); return false;
        } else if (tipComprobante == "0") {
            MessageBox("Seleccione el tipo de comprobante"); return false;
        } else if (tipoRegistro == "0") {
            MessageBox("Seleccione el tipo de registro"); return false;
        } else if (codAsegurado == "0") {
            MessageBox("Seleccione el asegurador"); return false;
        } else if (codRamo == "0") {
            MessageBox("Seleccione el ramo"); return false;
        } else if (codMoneda == "0") {
            MessageBox("Seleccion moneda"); return false;
        } else if (calcula > 0) {
            return confirm("Hay Algunos Campos con Valor (0) ¿Esta Seguro de Grabar?");
        } else {
            return confirm("¿Esta Seguro de Grabar " + $("#ctl00_ContentPlaceHolder1_ddl_tipope_m option:selected").text() + "?");
        }
    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnNuevo_m", "click", function (ev) {
        ev.preventDefault();
        $("#tblopemanual input[type='text']").val("0.00");
        $("#tblopemanual select").val("0");
    });
    //funcion mensaje
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
