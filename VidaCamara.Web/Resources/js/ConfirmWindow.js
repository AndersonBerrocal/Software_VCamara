function SomeMethod() {
    var message = "";
    var textbox = document.getElementById('ctl00_hdf_control');
    if (textbox.value == "210") {
        message = confirm("¿ Está Seguro de crear el Registro ?");
    } else if (textbox.value == "999") {
        message = confirm("¿ Está Seguro de Actualizar el Registro ?");
    }
    return message;
}
function DeleteMethod() {
    var message = ""
    var textbox = document.getElementById('ctl00_hdf_control');
    if (textbox.value == "210") {
        alert("Selecione un Registro");
        message = false;
    } else {
        message = confirm("¿ Está Seguro de Eliminar el Registro ?");
    }
    return message;

}
function mostrarMensajeAlert(texto) {
    $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 180, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] });
}
window.mostrarMensajeAlert = mostrarMensajeAlert;
$(document).ready(function () {
    $('.tabBody').delegate('#ctl00_ContentPlaceHolder1_btn_nuevo', 'click', function () {
        $('#ctl00_hdf_control').val("210");
    });
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: '',
        showButtonPanel: true, changeMonth: true, changeYear: true
    };
    //funcion datepicker
    $.datepicker.setDefaults($.datepicker.regional['es']);
    $(".datetime").datepicker();
    $('#ctl00_ContentPlaceHolder1_txt_fecini_c').datepicker();
    $('#ctl00_ContentPlaceHolder1_txt_fecfin_c').datepicker();
    $('#ctl00_ContentPlaceHolder1_txt_fecha_creacion').datepicker();
    $('#ctl00_ContentPlaceHolder1_txt_hasta').datepicker();

    //validacion de numeros
    $('#ctl00_ContentPlaceHolder1_txt_impAbono_p').numeric();
    $('#ctl00_ContentPlaceHolder1_txt_primaEst_p').numeric();
    $('#ctl00_ContentPlaceHolder1_txt_impAbono_ib').numeric();
    $(".numeric").numeric();
    //funcion para convertir numeros a fechas
    window.ConvertNumberToDate = ConvertNumberToDate;
    window.ConvertNumberToDateTime = ConvertNumberToDateTime;
    window.llamarAjax = llamarAjax;

    function ConvertNumberToDate(numberdate) {
        var milli = numberdate.replace(/\/Date\((-?\d+)\)\//, '$1');
        var d = new Date(parseInt(milli));
        mes = d.getMonth() + 1;
        dia = d.getDate()
        if (mes < 10) { mes = "0" + mes }
        if (dia < 10) { dia = "0" + dia }
        return f = dia + "/" + mes + "/" + d.getFullYear();
    }
    function ConvertNumberToDateTime(numberdatetime) {
        if (numberdatetime == null)
            return "";
        var milli = numberdatetime.replace(/\/Date\((-?\d+)\)\//, '$1');
        var d = new Date(parseInt(milli));
        var mes = d.getMonth() < 10 ? "0" + parseInt(d.getMonth() + 1) : parseInt(d.getMonth() + 1);
        var dia = d.getDate() < 10?"0"+d.getDate():d.getDate();
        var hora = d.getHours() < 10?"0"+ d.getHours():d.getHours();
        var minuto = d.getMinutes() < 10?"0"+d.getMinutes():d.getMinutes();
        var segundo = d.getSeconds() < 10 ? "0" + d.getSeconds() : d.getSeconds();
        return dia + "/" + mes + "/" + d.getFullYear() + " " + hora + ":" + minuto + ":" + segundo;
    }
    //funcion ajax
    function llamarAjax(data, url) {
        return $.ajax({
            url:url,
            method: 'POST',
            contentType: "application/json;",
            dataType: 'JSON',
            data: JSON.stringify(data),
        });
    }
    function dar_formato(num) {

        var cadena = ""; var aux;

        var cont = 1, m, k;

        if (num < 0) aux = 1; else aux = 0;

        num = num.toString();



        for (m = num.length - 1; m >= 0; m--) {

            cadena = num.charAt(m) + cadena;

            if (cont % 3 == 0 && m > aux) cadena = "," + cadena; else cadena = cadena;

            if (cont == 3) cont = 1; else cont++;

        }

        cadena = cadena.replace(/.,/, ",");

        return cadena;

    }

});