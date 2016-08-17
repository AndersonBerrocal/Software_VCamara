var dataOperacion;
var visibleColumn;             //visibleColumn es para mostrar u ocultar columnas de la grilla trbaja con true/false
$(document).ready(function () {
    $("#operacion th").css({"text-align":"center"});
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tipinfo_det", "change", function (ev) {
        $('#tbl_precosa_ope').css({'display':'block'});
        var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_det").val();
        if (nro_contrato != 0) {
            var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo_det").val();
            dataOperacion = new Array(nro_contrato, tipo_info);
            if (tipo_info == "RI") {
                visibleColumn = new Array(false, false, false,false,false, false, false, false, true, true, true, true,false, false, false, false);
                jatbleRequestOperacionDetalle(dataOperacion, visibleColumn, "2600px", "procesaibnr");
            } else if (tipo_info == "RP") {
                visibleColumn = new Array(false, false, false,false,false, false, false, false, false, false, false, true,false, false, true,true);
                jatbleRequestOperacionDetalle(dataOperacion, visibleColumn, "2400px", "procesapago");
            } else if (tipo_info == "RM") {
                visibleColumn = new Array(true, true,true, true,true, true, true, false, false, false, false, false, false, false, false,false);
                jatbleRequestOperacionDetalle(dataOperacion, visibleColumn, "2900px", "procesaprima");
            } else if (tipo_info == "RR") {
                visibleColumn = new Array(false, false, false,false,false, false, false,false, true, true, true, true, false, false, false, false);
                jatbleRequestOperacionDetalle(dataOperacion, visibleColumn, "2700px", "procesarsp");
            }else {
                visibleColumn = new Array(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                jatbleRequestOperacionDetalle(dataOperacion, visibleColumn, "2700px", "procesageneral");
            }
            displayTableWithId(tipo_info);
        } else {
            MessageBox("Selecione el Contrato"); return false;
        }
    });
    //eliminado registros selecionados
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_borrar_po", "click", function (ev) {
        var tablaid = $("#tbl_precosa_ope").length;
        if (tablaid == 0) {
            return false;
        } else {
            ev.preventDefault();
            $("<div style='font-size:14px;text-align:center;' id='dialogSend'>Esta Seguro de Borrar?</div>").dialog({
                title: 'Alerta',
                modal: true,
                width: 400,
                height: 160,
                buttons: [{
                    id: 'aceptar',
                    text: 'Aceptar',
                    icons: { primary: 'ui-icon-circle-check' },
                    click: function () {
                        var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo_det").val();
                        if (tipo_info == "RI") {
                            visibleColumn = new Array(false, false, false,false,false, false, false, false, true, true, true, true,false, false, false, false);
                            var $selectedRows = $('#procesaibnr').jtable('selectedRows');
                            if ($selectedRows.length > 0) {
                                jatbleRequestOperacionDetalle("OK", visibleColumn, "2600px", "procesaibnr");
                            } else {
                                $("#dialogSend").dialog("destroy");
                                MessageBox("Selecione Registros para Borrar");
                            }
                        }
                        else if (tipo_info == "RP") {
                            visibleColumn = new Array(false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true);
                            var $selectedRows = $('#procesapago').jtable('selectedRows');
                            if ($selectedRows.length > 0) {
                                jatbleRequestOperacionDetalle("OK", visibleColumn, "2400px", "procesapago");
                            } else {
                                $("#dialogSend").dialog("destroy");
                                MessageBox("Selecione Registros para Borrar");
                            }
                        }
                        else if (tipo_info == "RM") {
                            visibleColumn = new Array(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false);
                            var $selectedRows = $('#procesaprima').jtable('selectedRows');
                            if ($selectedRows.length > 0) {
                                jatbleRequestOperacionDetalle("OK", visibleColumn, "2900px", "procesaprima");
                            } else {
                                $("#dialogSend").dialog("destroy");
                                MessageBox("Selecione Registros para Borrar");
                            }
                        } else if (tipo_info == "RR") {
                            visibleColumn = new Array(false, false, false, false, false, false, false, false, true, true, true, true, false, false, false, false);
                            var $selectedRows = $('#procesarsp').jtable('selectedRows');
                            if ($selectedRows.length > 0) {
                                jatbleRequestOperacionDetalle("OK", visibleColumn, "2700px", "procesarsp");
                            } else {
                                $("#dialogSend").dialog("destroy");
                                MessageBox("Selecione Registros para Borrar");
                            }
                        }
                        $("#dialogSend").dialog("destroy");
                    }
                },
                {
                    id: 'cancelar',
                    text: 'Cancelar',
                    icons: { primary: 'ui-icon-cancel' },
                    click: function () {
                        $(this).dialog("close");
                    }
                }]
            });
        }
    });

    //validacion para botn procesar antes de enviar el request
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_procesa_ope", "click", function (ev) {
        var tableExist = $("#tbl_precosa_ope").length;
        if (tableExist == 0) {
            var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_o").val();
            var tipo_registro = $("#ctl00_ContentPlaceHolder1_ddl_tipo_registro").val();
            var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo").val();
            if (nro_contrato == 0) { MessageBox("Selecione el Contrato"); return false; }
            else if (tipo_registro == 0) { MessageBox("Selecione el Tipo de Registro"); return false; }
            else if (tipo_info == 0) { MessageBox("Selecione de Información"); return false; }
            else { return confirm("Está Seguro de Procesar la Información?"); }
        } else {
            return false;
        }
    });

    function jatbleRequestOperacionDetalle(data, visibleColumn, sizetable, tablaid) {
        $('#' + tablaid).jtable({
            tableId: 'operacion',
            paging: true,
            sorting: true,
            selecting: true,
            multiselect: true,
            selectingCheckboxes: true,
            pageSize: 13,
            defaultSorting: '_Fec_Operacion ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/Operaciones/frmProcesaOperacion.aspx/GetSelectOperacionDetalle',
                deleteAction: '/WebPage/ModuloSBS/Operaciones/frmProcesaOperacion.aspx/SetEliminarOperacion',
            },
            fields: {
                _Nro_Operacion: { title: 'N° Operación',key:true },
                _Tip_Operacion: { title: 'Tipo Operación' },
                _Cod_Reasegurador: { title: 'Reasegurador' },
                _Fec_Operacion: { title: 'Fecha de Operación', type: 'date', displayFormat: 'dd/mm/yy' },
                _Ano_Operacion: { title: 'Año' },
                _Mes_Operacion: { title: 'Mes'},
                _Cod_Asegurado: { title: 'Asegurado' },
                _Ide_Contrato: { title: 'N° Contrato' },
                _Des_Reg_Mes: { title: 'Descripción ' },
                _Cod_Ramo: { title: 'Ramo', width: '200px' },
                _Cod_Moneda: { title: 'Moneda' },
                _Entorno_Prima_T_2: { title: 'Extorno_d_Prima_t-2', list: visibleColumn[0] },
                _Entorno_Prima_T_1: { title: 'Extorno_d_Prima_t-1', list: visibleColumn[1] },
                _Prima_Abonada: { title: 'Prima_Abonada', list: visibleColumn[2] },
                _Prima_Reg_Contab: { title: 'Prima_Reg_Contab.', list: visibleColumn[3] },
                _Pri_Ced_Mes: { title: 'Prima_Cedida', list: visibleColumn[4] },
                _Imp_Impuesto_Mes: { title: 'Impuestos', list: visibleColumn[5] },
                _Pri_Xpag_Rea_Ced: { title: 'Prima_por_Pagar', list: visibleColumn[6] },
                _Pri_Xcob_Rea_Ace: { title: 'Pri_Xcob_Rea_Ace', list: visibleColumn[7] },
                _Sin_Directo: { title: 'Siniestros_Directos', list: visibleColumn[8]},
                _Sin_Xpag_Rea_Ace: { title: 'Siniestros_Cedidos', list: visibleColumn[9] },
                _Dscto_Comis_Rea: { title: 'Comisiones_/_Descuentos', list: visibleColumn[10] },
                _Sin_Xcob_Rea_Ced: { title: 'Siniestros_por_Cobrar', list: visibleColumn[11] },
                _Otr_Cta_Xcob_Rea_Ced: { title: 'Otr_Cta_Xcob_Rea_Ced', list: visibleColumn[12] },
                _Otr_Cta_Xpag_Rea_Ace: { title: 'Otr_Cta_Xpag_Rea_Ace', list: visibleColumn[13] },
                _Saldo_Deudor: { title: 'Saldo_Deudor', list: visibleColumn[14] },
                _Saldo_Acreedor: { title: 'Saldo_Acreedor', list: visibleColumn[15] },
                _Estado: { title: 'Estado' },
                _Fec_Reg: { title: 'Fecha_Registro', list: true, type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Reg: { title: 'Usu_Reg', list: true },
                _Fec_Mod: { title: 'Fec_Mod', list: false, type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Mod: { title: 'Usu_Mod', list: false },
            }
        });
        $('#'+tablaid+' .jtable-main-container').css({ "width": sizetable });
        if (data == "OK") {
            var $selectedRows = $('#' + tablaid).jtable('selectedRows');
            $('#' + tablaid).jtable('deleteRows', $selectedRows);
        } else {
            $('#' + tablaid).jtable('load', { 'data': data });
        }
    }
    //mostrar informacion acuerdo  tipo de operacion
    function displayTableWithId(tipo_info) {
        console.log(tipo_info);
        switch(tipo_info){
            case "RI":
                $("#procesaibnr").css({ "display": "block" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "none" });
                break;
            case "RP":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "block" });
                break;
            case "RR":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "block" });
                $("#procesapago").css({ "display": "none" });
                break;
            case "RM":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "block"});
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "none" });
                break;
            default:
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "none" });
                $("#procesageneral").css({ "display": "block" });
                break;
          }
    }
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});