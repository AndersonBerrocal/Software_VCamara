$(document).ready(function (e) {
    //EVENTOS DE BORRADO
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_borrar", "click", function () {

        const tablaidContratoSbs = $("#tblContratoView").length;
        const tablaidContratoSbsDet = $("#tblReasegurador").length;
        const tablaidContratoSis = $("#tblContratoViewSyS").length;
        const tableidContratoSisDet = $("#tblContratoViewSySDetalle").length;

        if (tablaidContratoSbs == 1 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 0 && tableidContratoSisDet == 0) {
            var idcontrat = $("#ctl00_ContentPlaceHolder1_txt_idContrato_c").val();
            if (idcontrat == 0) {
                MessageBox("Selecione un Registro"); return false;
            } else
                return confirm("¿ Está Seguro de Eliminar el Registro ?");
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 1 && tablaidContratoSis == 0 && tableidContratoSisDet == 0) {
            var idcontdet = $("#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c").val();
            if (idcontdet == 0) {
                MessageBox("Selecione un Registro"); return false;
            } else
                return confirm("¿ Está Seguro de Eliminar el Registro ?");
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 1 && tableidContratoSisDet == 0) {
            var ide_contrato_sis = $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis").val();
            if (parseInt(ide_contrato_sis) == 0) {
                MessageBox("Selecione un Registro"); return false;
            }else
                return confirm("¿ Está Seguro de Eliminar el Registro ?");
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 0 && tableidContratoSisDet == 1) {
            var ide_contrato_sis_detalle = $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis_det").val();
            if (parseInt(ide_contrato_sis_detalle) == 0) {
                MessageBox("Selecione un Registro"); return false;
            }else
                return confirm("¿ Está Seguro de Eliminar el Registro ?");
        }
    });
    //funcion de mostrar alerta
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});