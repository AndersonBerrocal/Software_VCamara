/// <reference path="../frmTipoCambio.aspx" />
const urlInactivaTipo = "/WebPage/Mantenimiento/frmTipoCambio.aspx/setInactivaTipo";
$(document).ready(function () {
    limpiarFormulario();
    var cambio = function () {
        this.Periodo = $("#ctl00_ContentPlaceHolder1_txt_periodo").val(),
        this.Monto = $("#ctl00_ContentPlaceHolder1_txt_monto").val()
    }
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        listTipoCambio(new cambio());
    });
    //inactivar
    $("body #tblTipoCambioGrid").delegate("#chk_inactivo", "click", function () {
        var tipoCambio = { IdTipoCambio: parseInt($(this).val()), Vigente: $(this).is(":checked") };
        if (confirm("¿Está seguro de grabar?")) {
            llamarAjax(tipoCambio, urlInactivaTipo).success(function (res) {
                if (res.d.Result == true) {
                    mostrarMensajeAlert("Tipo de cambio actualizado");
                    listTipoCambio(new cambio());
                } else
                    mostrarMensajeAlert(res.d.Result);
            });
        } else
            $(this).prop("checked",true);

    });
    //grabar
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_guardar", "click", function (ev) {
        var periodo = $("#ctl00_ContentPlaceHolder1_txt_periodo").val();
        var monto = $("#ctl00_ContentPlaceHolder1_txt_monto").val();
        if (periodo == "") {
            mostrarMensajeAlert("Ingrese el periodo"); return false;
        } else if (parseFloat(monto) == 0){
            mostrarMensajeAlert("El monto ingresado no puede ser 0");return false;
        }else
            return confirm("¿Está seguro de grabar?");
    });
    //datagrid
    var action = "/WebPage/Mantenimiento/frmTipoCambio.aspx/listTipoCambio";
    var fields = {
        IdTipoCambio: { key: true, list: false },
        Periodo: { title: 'Periodo' },
        Monto: { title: 'Monto' },
        Vigente: {
            title: 'Vigente',sorting:false, display: function (data) {
                if(data.record.Vigente === true)
                    return "<input id='chk_inactivo' type='checkbox' value='" + data.record.IdTipoCambio + "' checked/> Activo";
                else
                    return "<input id='chk_inactivo' type='checkbox' value='" + data.record.IdTipoCambio + "'/> Inactivo";
            }
        }
    }

    //cuando carga la pagina
    listTipoCambio(new cambio());

    function listTipoCambio(cambio) {
        $('#tblTipoCambioGrid').jtable({
            tableId: 'tipoCambioId',
            paging: true,
            sorting: true,
            pageSize: 10,
            defaultSorting: 'Periodo ASC',
            selecting: true,
            actions: {
                listAction: action,
            },
            fields: fields
        });

        //$('#tblTipoCambioGrid.jtable-main-container').css({ "width": "1000px" });
        $('#tblTipoCambioGrid').jtable('load', { cambio: cambio });
    }
    function limpiarFormulario() {
        $("#ctl00_ContentPlaceHolder1_txt_periodo").val("");
        $("#ctl00_ContentPlaceHolder1_txt_monto").val("0.00");
    }
})