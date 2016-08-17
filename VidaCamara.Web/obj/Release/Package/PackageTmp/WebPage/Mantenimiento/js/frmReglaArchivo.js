$(document).ready(function () {
    var reglaAarchivo = function () {
        this.NUM_CONT_LIC = parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val()),
        this.Archivo = $("#ctl00_ContentPlaceHolder1_ddl_Archivo").val(),
        this.TipoLinea = $("#ctl00_ContentPlaceHolder1_ddl_tipo_linea").val(),
        this.vigente = parseInt($("#ctl00_ContentPlaceHolder1_ddl_vigente").val()),
        this.IdReglaArchivo = parseInt($("#ctl00_ContentPlaceHolder1_txt_idRegla").val())
    }
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var nroContrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato").val();
        var archivo = $("#ctl00_ContentPlaceHolder1_ddl_Archivo").val();
        if (nroContrato == "0")
            mostrarMensajeAlert("Seleccione el contrato");
        else if (archivo == "0")
            mostrarMensajeAlert("Seleccione el tipo archivo");
        else
            listReglaArchivo(new reglaAarchivo());
    });

    //grabar
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_guardar", "click", function (ev) {
        var calcula = 0;
        $(".tabBody input[type='text']").each(function (index, element) {
            valor = $(this).val();
            if (valor == "") {
                calcula++;
            }
        });
        $(".tabBody select").each(function (index, element) {
            valor = $(this).val();
            if (parseInt(valor) == 0) {
                calcula++;
            }
        });
        if (calcula > 0) {
            mostrarMensajeAlert("Todos los campos son requeridos");
            return false;
        } else
            return confirm("¿Estas seguro de crear la regla?");
    });

    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_nuevo", "click", function (ev) {
        ev.preventDefault();
        limpiarFormulario();
    });
    //datagrid
    var action = "/WebPage/Mantenimiento/frmReglaArchivo.aspx/listReglaArchivo";
    var fields = {
        Archivo: { title: 'Archivo' },
        TipoLinea: { title: 'TipoLinea' },
        CaracterInicial: { title: 'CaracterInicial' },
        LargoCampo: { title: 'LargoCampo' },
        TipoCampo: { title: 'TipoCampo' },
        InformacionCampo: { title: 'InformacionCampo' },
        FormatoContenido: { title: 'FormatoContenido', sorting: false },
        TipoValidacion: { title: 'TipoValidacion' },
        ReglaValidacion: { title: 'ReglaValidacion' },
        NombreCampo: { title: 'NombreCampo' },
        TituloColumna: { title: 'TituloColumna' },
        vigente: { title: 'vigente',sorting:false }
    }

    function listReglaArchivo(regla) {
        $('#tblReglaArchivoGrid').jtable({
            tableId: 'reglaArchivoID',
            paging: true,
            sorting: true,
            pageSize: 10,
            defaultSorting: 'CaracterInicial ASC',
            selecting: true,
            actions: {
                listAction: action,
            },
            recordsLoaded: function (event, data) {
            },
            fields: fields,
            selectionChanged: function () {
                var $selectedRows = $('#tblReglaArchivoGrid').jtable('selectedRows');
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        asignarValorCampos(record);
                    });
                }
            }
        });

        $('#tblReglaArchivoGrid.jtable-main-container').css({ "width": "1420px" });
        $('#tblReglaArchivoGrid').jtable('load', { regla: regla });
    }
    function asignarValorCampos(data) {
        console.log(data);
        $("#ctl00_ContentPlaceHolder1_ddl_tipo_linea").val(data.TipoLinea);
        $("#ctl00_ContentPlaceHolder1_txt_caracter_inicial").val(data.CaracterInicial);
        $("#ctl00_ContentPlaceHolder1_txt_largo_Campo").val(data.LargoCampo);
        $("#ctl00_ContentPlaceHolder1_txt_informacion").val(data.InformacionCampo);
        $("#ctl00_ContentPlaceHolder1_txt_formato").val(data.FormatoContenido);
        $("#ctl00_ContentPlaceHolder1_ddl_tipo_validacion").val(data.TipoValidacion);
        $("#ctl00_ContentPlaceHolder1_txt_regla_validacion").val(data.ReglaValidacion);
        $("#ctl00_ContentPlaceHolder1_ddl_vigente").val(data.vigente);
        $("#ctl00_ContentPlaceHolder1_txt_nombre_Campo").val(data.NombreCampo);
        $("#ctl00_ContentPlaceHolder1_txt_titulo").val(data.TituloColumna);
        $("#ctl00_ContentPlaceHolder1_txt_tipo_Campo").val(data.TipoCampo);
        $("#ctl00_ContentPlaceHolder1_txt_idRegla").val(data.IdReglaArchivo);
    }
    function limpiarFormulario() {
        $("#ctl00_ContentPlaceHolder1_ddl_contrato").val("0");
        $("#ctl00_ContentPlaceHolder1_ddl_Archivo").val("0");
        $("#ctl00_ContentPlaceHolder1_ddl_tipo_linea").val("0");
        $("#ctl00_ContentPlaceHolder1_txt_caracter_inicial").val("");
        $("#ctl00_ContentPlaceHolder1_txt_largo_Campo").val("");
        $("#ctl00_ContentPlaceHolder1_txt_informacion").val("");
        $("#ctl00_ContentPlaceHolder1_txt_formato").val("");
        $("#ctl00_ContentPlaceHolder1_ddl_tipo_validacion").val("0");
        $("#ctl00_ContentPlaceHolder1_txt_regla_validacion").val("");
        $("#ctl00_ContentPlaceHolder1_ddl_vigente").val("1");
        $("#ctl00_ContentPlaceHolder1_txt_nombre_Campo").val("");
        $("#ctl00_ContentPlaceHolder1_txt_titulo").val("");
        $("#ctl00_ContentPlaceHolder1_txt_tipo_Campo").val("");
        $("#ctl00_ContentPlaceHolder1_txt_idRegla").val("0");
    }
})