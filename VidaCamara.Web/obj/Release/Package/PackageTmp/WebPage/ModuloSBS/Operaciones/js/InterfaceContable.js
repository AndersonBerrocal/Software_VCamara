$(document).ready(function () {
   /* $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_contrato_i", "change", function (ev) {
        jatbleRequestContabilidad($(this).val(),"NO","3000px");
    });*/
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_borrar", "click", function (ev) {
        var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i").val();
        var tipo_info_text = $("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i option:selected").text();
        var contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val();

        if (contrato == 0) {
            MessageBox("Seleccione el contrato"); return false;
        } else if (tipo_info == 0) {
            MessageBox("Seleccione el Tipo de información"); return false
        } else {
            return confirm("Está seguro de eliminar los registro (s) contable (s) de " + tipo_info_text.toUpperCase());
        }
    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_procesar", "click", function (ev) {
        var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i").val();
        var tipo_info_text = $("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i option:selected").text();
        var contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val();

        if (contrato == 0) {
            MessageBox("Seleccione el contrato"); return false;
        } else if (tipo_info == 0) {
            MessageBox("Seleccione el Tipo de información"); return false
        } else {
            return confirm("Está seguro de registrar el Asiento contable para " + tipo_info_text.toUpperCase());
        }
    });
    if ($("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i").val() != "" && $("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val() != "")
        jatbleRequestContabilidad($("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val(), $("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i").val(), "2500px");
    
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tip_operacion_i", "change", function (ev) {
        contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val();
        jatbleRequestContabilidad(contrato, $(this).val(), "2550px");
    });
    //validacion de transferencia de datos
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_transfer", "click", function (ev) {
        contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_i").val();
        if (contrato == 0) {
            MessageBox("Seleccione el contrato"); return false;
        } else {
            return confirm("¿Está seguro de realizar la transferencia de  asiento (s) contable (s) ?");
        }

    })

    function jatbleRequestContabilidad(contrato,tipo_info, sizetable) {
        $('#tbl_interface').jtable({
            tableId: 'interface',
            paging: false,
            sorting: false,
            selecting: false,
            multiselect: false,
            selectingCheckboxes: false,
            //pageSize: 13,
            //defaultSorting: '_Fec_Operacion ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/Operaciones/frmInterfaceContable.aspx/GetSelectContabilidad',
                //deleteAction: '/WebPage/Operaciones/frmProcesaOperacion.aspx/SetEliminarOperacion',
            },
            recordsLoaded: function (event, data) {
                if (data.records.length > 0) {
                    $("#ctl00_ContentPlaceHolder1_btn_borrar").css({ "display": "block" });
                    $("#ctl00_ContentPlaceHolder1_btn_procesar").css({ "display": "none" });
                } else {
                    $("#ctl00_ContentPlaceHolder1_btn_borrar").css({ "display": "none" });
                    $("#ctl00_ContentPlaceHolder1_btn_procesar").css({ "display": "block" });
                }
            },
            fields: {
                Nro_Contrato:  {title: 'N° Contrato'},
                Anio_Vigente :  {title: 'Año'},
                Mes_Vigente :  {title: 'Mes'},
                Tipo_Info: { title: 'Tipo_Información', list: false },
                Estado_Transferencia: { title: "Estado_Transferencia", type: 'checkbox', values: { "C": "CREADO", "T": "TRANSFERIDO" } },
                Paquete: { title: 'Paquete' },
                Asiento: { title: 'Asiento' },
                Fecha: { title: 'Fecha', type: 'date', displayFormat: 'dd/mm/yy' },
                Tipo_Asiento :  {title: 'Tipo_Asiento'},
                Contabilidad: { title: 'Tipo_Contabilidad' },
                Fuente: { title: 'Fuente' },
                Referencia: { title: 'Referencia' },
                Centro_Costo: { title: 'Centro_Costo' },
                Cuenta_Contable: { title: 'Cuenta_Contable' },
                debito_Local: { title: 'Débito_Local' },
                credito_Local: { title: 'Crédito_Local' },
                debito_dolar: { title: 'Débito_Dolar' },
                credito_dolar: { title: 'Crédito_Dolar' },
                Monto_Unidades: { title: 'Monto_Unidades' },
                Usuario_Registro: { title: 'Usuario_Registro' },

                Notas :  {title: 'Notas',list:false},
                Estado :  {title: 'Estado',list:false},
                Permitir_Descuadrado :  {title: 'N° Contrato',list:false},
                Conservar_Numeracion: { title: 'N° Contrato', list: false },
                Actualizar_Consecutivo: { title: 'N° Contrato', list: false },
                Fecha_Auditoria: { title: 'Fecha_Auditoria', type: 'date', displayFormat: 'dd/mm/yy',list:false },
                Consecutivo :  {title: 'Consecutivo',list:false},
                Nit :  {title: 'N° Contrato',list:false},
                Dimension1: { title: 'N° Contrato', list: false },
                Dimension2: { title: 'N° Contrato', list: false },
                Dimension3: { title: 'N° Contrato', list: false },
                Dimension4: { title: 'N° Contrato', list: false },
                Dimension5: { title: 'N° Contrato', list: false }
            }
        });

        $('#tbl_interface .jtable-main-container').css({ "width": sizetable });
        $('#tbl_interface').jtable('load', { contrato: contrato, tipo_info: tipo_info });
    }
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});