$(document).ready(function () {
    //entidades y variables
    var cabecera = function (ide_contrato) {
        this.IDE_CONTRATO = ide_contrato,
        this.ESTADO_TRANSFERENCIA = $("#ctl00_ContentPlaceHolder1_ddl_estado").val(),
        this.IDE_MONEDA = parseInt($("#ctl00_ContentPlaceHolder1_ddl_moneda").val())
    }
    const actionGeneral = "/WebPage/ModuloDIS/Operaciones/frmInterfaceContableSIS.aspx/listInterfaceContable";
    const actionParcial = "/WebPage/ModuloDIS/Operaciones/frmInterfaceContableSIS.aspx/listInterfaceContableParcial";
    const fieldsGeneral = {
        PAQUETE: {
            title: 'PAQUETE', display: function (data) {
                return data.record.EXACTUS_CABECERA_SIS.PAQUETE;
            }
        },
        ASIENTO: {
            title: 'ASIENTO', display: function (data) {
                return data.record.EXACTUS_CABECERA_SIS.ASIENTO;
            }
        },
        FECHA: {
            title: 'FECHA_REGISTRO', display: function (data) {
                return ConvertNumberToDateTime(data.record.EXACTUS_CABECERA_SIS.FECHA);
            }
        },
        TIPO_ASIENTO: {
            title: 'TIPO_ASIENTO', display: function (data) {
                return data.record.EXACTUS_CABECERA_SIS.TIPO_ASIENTO;
            }
        },
        CONTABILIDAD: {
            title: 'CONTABILIDAD', display: function (data) {
                return data.record.EXACTUS_CABECERA_SIS.CONTABILIDAD;
            }
        },
        FUENTE: { title: 'FUENTE' },
        REFERENCIA: { title: 'REFERENCIA' },
        CONTRIBUYENTE: { title: 'CONTRIBUYENTE' },
        CENTRO_COSTO: { title: 'CENTRO_COSTO' },
        CUENTA_CONTABLE: { title: 'CUENTA_CONTABLE' },
        DebitoSoles: { title: 'DebitoSoles' },
        CreditoSoles: { title: 'CreditoSoles' },
        DebitoDolar: { title: 'DebitoDolar' },
        CreditoDolar: { title: 'CreditoDolar' },
        MONTO_UNIDADES: { title: 'MONTO_UNIDADES' },
        ESTADO_TRANSFERENCIA: {
            title: 'EstadoTransferencia', display: function (data) {
                return data.record.EXACTUS_CABECERA_SIS.ESTADO_TRANSFERENCIA == "C" ? "CREADO" : "TRANSFERIDO";
            }
        },
        EstadoTransferenciaDetalle: {
            title: 'EstadoTransferenciaDetalle', display: function (data) {
                return data.record.EstadoTransferenciaDetalle == "C" ? "CREADO" : "TRANSFERIDO";
            }
        }
    }
    const fieldsParcial = {
        CUENTA_BANCARIA: { title: 'CUENTA_BANCARIA' },
        NUMEROSTR: { title: 'NUMERO' },
        TIPO_DOCUMENTO: { title: 'TIPO_DOCUMENTO' },
        FECHA_DOCUMENTO: { title: 'FECHA_DOCUMENTO', type: 'date', displayFormat: 'dd/mm/yy' },
        CONCEPTO: { title: 'CONCEPTO' },
        BENEFICIARIO: { title: 'BENEFICIARIO' },
        CONTRIBUYENTE: { title: 'CONTRIBUYENTE' },
        MONTOSTR: { title: 'MONTO' },
        DETALLE: { title: 'DETALLE' },
        CENTRO_COSTO: { title: 'CENTRO_COSTO' },
        CUENTA_CONTABLE: { title: 'CUENTA_CONTABLE' },
        RUBRO_1: { title: 'RUBRO_1' },
        RUBRO_2: { title: 'RUBRO_2' },
        RUBRO_3: { title: 'RUBRO_3' },
        RUBRO_4: { title: 'RUBRO_4' },
        RUBRO_5: { title: 'RUBRO_5' },
        PAQUETE: { title: 'PAQUETE' }
    }
    //eventos
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var contrato = parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val());
        var tipoInterface = $("#ctl00_ContentPlaceHolder1_ddl_tipo_interface").val();
        displayNoneGrid(tipoInterface);
        if (contrato == 0) {
            mostrarMensajeAlert("Seleccione el contrato");
        } else {
            var filter = [$("#ctl00_ContentPlaceHolder1_txt_desde").val(), $("#ctl00_ContentPlaceHolder1_txt_hasta").val(),
                         $("#ctl00_ContentPlaceHolder1_ddl_tipo_archivo").val()];
            if (parseInt(tipoInterface) == 1)
                createGridInterface(new cabecera(contrato), filter, actionGeneral, fieldsGeneral);
            else
                createGridInterfaceParcial(new cabecera(contrato), filter, actionParcial, fieldsParcial);
        }
    });
    //transferir
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_transfer", "click", function (ev) {
        var contrato = parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val());
        var tipoInterface = $("#ctl00_ContentPlaceHolder1_ddl_tipo_interface").val();
        if (parseInt(tipoInterface) == 1) {
            if (contrato == 0) {
                mostrarMensajeAlert("Seleccione el contrato");
                return false;
            } else {
                return confirm("¿Está seguro de transferir la información?");
            }
        } else {
            mostrarMensajeAlert("Tipo de interfaz no transfiere datos");
            return false;
        }
    });
 
    function createGridInterface(cabecera, filter, actions, fields) {
        $('#tblInterfaceContableSIS').jtable({
            tableId: 'interfaceContableID',
            paging: true,
            sorting: false,
            pageSize: 10,
            //defaultSorting: 'PAQUETE ASC',
            selecting: true,
            actions: {
                listAction: actions,
            },
            fields: fields
        });

        $('#tblInterfaceContableSIS.jtable-main-container').css({ "width": "1800px" });
        $('#tblInterfaceContableSIS').jtable('load', { cabecera: cabecera, filter: filter });
    }
    function createGridInterfaceParcial(cabecera, filter, actions, fields) {
        $('#tblInterfaceContableSISParcial').jtable({
            tableId: 'interfaceContableID',
            paging: true,
            sorting: false,
            pageSize: 10,
            //defaultSorting: 'PAQUETE ASC',
            selecting: true,
            actions: {
                listAction: actions,
            },
            fields: fields
        });

        $('#tblInterfaceContableSISParcial.jtable-main-container').css({ "width": "1800px" });
        $('#tblInterfaceContableSISParcial').jtable('load', { cabecera: cabecera, filter: filter });
    }
    function displayNoneGrid(tipoInterface) {
        if (parseInt(tipoInterface) == 2) {
            $("#tblInterfaceContableSISParcial").css({ "display": "block" });
            $("#tblInterfaceContableSIS").css({ "display": "none" });
        } else {
            $("#tblInterfaceContableSISParcial").css({ "display": "none" });
            $("#tblInterfaceContableSIS").css({ "display": "block" });
        }
    }
})