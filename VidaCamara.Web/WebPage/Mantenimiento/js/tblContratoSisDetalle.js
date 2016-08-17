var descripcion_contrato = "Contrato SYS ";
var meses = new Array("-", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
var nroEmpresa = 0;
$(document).ready(function () {
    limpiarFormularioDetalle();
    //LIMPIAR DATOS DEL FORMULARIO
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnNuevo", "click", function (ev) {
        ev.preventDefault();
        $("#ctl00_ContentPlaceHolder1_ddl_contrato_sis").val("0");
        limpiarFormularioDetalle();
    });
    //CARGAR GRILLA POR CONTRATO SELECIONADO
    $("section").delegate("#ctl00_ContentPlaceHolder1_ddl_contrato_sis", "change", function (ev) {
        getlistContratoSisDetalle($(this).val());
        limpiarFormularioDetalle();
    });
    //VALIDAR QUE EL NUMERO DE ORDEN NO SE A MAYOR QUE 7
    $("section").delegate("#ctl00_ContentPlaceHolder1_txt_orden_empresa_sis", "keyup", function (ev) {
        if (parseInt($(this).val()) > parseInt(nroEmpresa) || parseInt($(this).val()) < 1) {
            mostrarMensajeAlert("El numero ingresado es 0 o supera el limite permitido");
            $(this).val("");
        }
    });
    //VALIDAR QUE EL CAMPO ESTADO NO GUARDE EN 0
    $("section").delegate("#ctl00_ContentPlaceHolder1_ddl_estado_sys", "change", function (ev) {
        if ($(this).val() == "0") {
            mostrarMensajeAlert("Seleccione el campo estado");
            $(this).val("A");
        }
    });
    //VALIDAR QUE EL PORCENTAJE DE PARTICIPACION NO SUPERE LOS 100%
    $("section").delegate("#ctl00_ContentPlaceHolder1_txt_participacion_sis", "keyup", function (ev) {
        if (parseFloat($(this).val()) > 100) {
            mostrarMensajeAlert("El porcentaje ingresado supera el limite permitido");
            $(this).val("");
        }

        //var n = (parseFloat($(this).val()) + "").split(".");
        //if (n.length > 1) {
        //    if (n[1].length > 6) {
        //        mostrarMensajeAlert("La cantidad maxima de decimales del porcentaje de participación es 6");
        //        $(this).val("");
        //    }
        //}

    });
    //VALIDAR QUE EL NUMERO DE EMPRESAS ESTE ENTRE 4 Y 7
    $("section").delegate("#ctl00_ContentPlaceHolder1_txt_numero_empresa", "keyup", function (ev) {
        if (parseInt($(this).val()) < 4 || parseInt($(this).val()) > 7) {
            mostrarMensajeAlert("El rango de N° de empresas por contrato puede ser entre 4 y 7");
            $(this).val("");
        }
    });
    //CARGAR GRILLA POR SI EL CONTRATO TIENE ITEM SELECCIONADO
    if (parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato_sis").val()) != 0) {
        getlistContratoSisDetalle($("#ctl00_ContentPlaceHolder1_ddl_contrato_sis").val());
    };
    //FUNCION QUE CONSTRUYE LA GRILLA JTABLE
    function getlistContratoSisDetalle(ide_contrato) {
        $('#tblContratoViewSySDetalle').jtable({
            tableId: 'Contratos_SYS',
            paging: true,
            sorting: true,
            pageSize: 7,
            defaultSorting: 'NRO_ORDEN DESC',
            selecting: true,
            saveUserPreferences: true,
            actions: {
                listAction: '/WebPage/Mantenimiento/frmGeneral.aspx/ContratoSisDetalleList',
            },
            recordsLoaded: function (event, data) {
                mostrarTotalPorcentaje(data.records);
            },
            fields: {
                IDE_CONTRATO_DET: { key: true, list: false },
                IDE_CONTRATO: {
                    title: 'N° Contrato', display: function (data) {
                        return data.record.CONTRATO_SYS.NRO_CONTRATO
                    }
                },
                COD_CSV: { title: 'Cia. Seguros Vida' },
                nombreCompania:{title:'Descripción'},
                PRC_PARTICIACION: { title: '% Participación' },
                NRO_ORDEN: { title: 'Orden (*)' },
                ESTADO: { title: 'Estado', list: false },
                USU_REG: { title: 'Usu. Registro' },
                FEC_REG: { title: 'Fec. Registro', displayFormat: 'dd/mm/yy', type: 'date' },
            },
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#tblContratoViewSySDetalle').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $("#ctl00_ContentPlaceHolder1_ddl_contrato_sis").val(record.IDE_CONTRATO);
                        $("#ctl00_ContentPlaceHolder1_ddl_compania_seg_vida").val(record.COD_CSV);
                        $("#ctl00_ContentPlaceHolder1_txt_participacion_sis").val(record.PRC_PARTICIACION);
                        $("#ctl00_ContentPlaceHolder1_txt_orden_empresa_sis").val(record.NRO_ORDEN);
                        $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis_det").val(record.IDE_CONTRATO_DET);
                        $('#ctl00_hdf_control').val(999);

                    });
                } else {
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });
        $('#tblContratoViewSySDetalle.jtable-main-container').css({ "width": "4800px" });
        $('#tblContratoViewSySDetalle').jtable('load', { WhereBy: ide_contrato });
    }
    //TOTAlLIZADOR DE SUMA DE PORCENTAJES
    function mostrarTotalPorcentaje(contratoSisDet) {
        //asginar valor a numero de empresas
        nroEmpresa = contratoSisDet.length>0?contratoSisDet[0].CONTRATO_SYS.NRO_EMPRESAS:7;
        var porcentajeTotal = 0;
        for (var i in contratoSisDet) {
            porcentajeTotal += parseFloat(contratoSisDet[i].PRC_PARTICIACION);
        };
        $("#totalPorcentaje").html("Total % participación: " + parseFloat(porcentajeTotal).toFixed(0).toString() + "% /  Pendiente: " + parseFloat(100 - porcentajeTotal).toFixed(0).toString() + "%");
    }
    //LIMPIAR FORMULARIO
    function limpiarFormularioDetalle() {
        $("#ctl00_ContentPlaceHolder1_ddl_compania_seg_vida").val("0");
        $("#ctl00_ContentPlaceHolder1_txt_participacion_sis").val("");
        $("#ctl00_ContentPlaceHolder1_txt_orden_empresa_sis").val("");
        $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis_det").val("0");
    }
});
