$(document).ready(function () {
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_consultar", "click", function (ev) {
        var filterSelected = 0;
        var contrato = $("#ddl_contrato").val();

        if (parseInt(contrato) == 0) {
            mostrarMensajeAlert("Seleccione Contrato"); return false;
        }
        ////$("#frmOperacion select").each(function (index, element) {
        ////    if (element.value != "0")
        ////        filterSelected++;
        ////});
        ////$("#frmOperacion input[type='text']").each(function (index, element) {
        ////    if (element.value != "") {
        ////        filterSelected++;
        ////    }
        ////});
        ////if (filterSelected < 2) {
        ////    mostrarMensajeAlert("Seleccione como minimo 2 o más campos"); return false;
        ////}
    });
})