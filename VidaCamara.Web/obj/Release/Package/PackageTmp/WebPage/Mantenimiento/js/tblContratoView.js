var descripcion_contrato = "Contrato ";
var meses = new Array("-", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
var textboxs = new Array("ctl00_ContentPlaceHolder1_txt_participacion_cia_c", "ctl00_ContentPlaceHolder1_txt_tasariesgo_c", "ctl00_ContentPlaceHolder1_txt_tasareaseguro_c",
                        "ctl00_ContentPlaceHolder1_txt_impuesto_c", "ctl00_ContentPlaceHolder1_txt_retencion_c", "ctl00_ContentPlaceHolder1_txt_cesion_c",
                        "ctl00_ContentPlaceHolder1_txt_montomax_retenc_c", "ctl00_ContentPlaceHolder1_txt_montomax_cesion_c", "ctl00_ContentPlaceHolder1_txt_montopleno_c",
                         "ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c",
                        "ctl00_ContentPlaceHolder1_txt_prioridad_c1", "ctl00_ContentPlaceHolder1_txt_excesoprio_c1", "ctl00_ContentPlaceHolder1_txt_mto_max_lim_sup_c1",
                        "ctl00_ContentPlaceHolder1_txt_primaminima_deposit_c1", "ctl00_ContentPlaceHolder1_txt_prioridad_c2",
                        "ctl00_ContentPlaceHolder1_txt_excesoprio_c2", "ctl00_ContentPlaceHolder1_txt_mto_max_lim_sup_c2", "ctl00_ContentPlaceHolder1_txt_primaminima_deposit_c2");
var select = new Array("ctl00_ContentPlaceHolder1_ddl_seniestro_c", "ctl00_ContentPlaceHolder1_ddl_ramo_prima_c", "ctl00_ContentPlaceHolder1_ddl_clasecontrato_c",
                        "ctl00_ContentPlaceHolder1_ddl_tipcon_c", "ctl00_ContentPlaceHolder1_ddl_moneda_c", "ctl00_ContentPlaceHolder1_ddl_contratante_c", "ctl00_ContentPlaceHolder1_ddl_modalidad_c");

$(document).ready(function () {
    $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").val(0); $("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c1").val(0); $("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c2").val(0);
    var tablacontrato = $("#tblContratoView").length;
    var tablareasegurador = $("#tblReasegurador").length;
    //validar campos segun el contrato
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_modalidad_c", "change", function (ev) {
        if ($(this).val() == "01") {
            $("#ctl00_ContentPlaceHolder1_txt_montopleno_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_retencion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_retenc_c").attr("readonly", false);
        } else if ($(this).val() == "02") {
            $("#ctl00_ContentPlaceHolder1_txt_montopleno_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_cesion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_cesion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_cesion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_retencion_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_retenc_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c").attr("readonly", true);

        } else if ($(this).val() == "05") {
            $("#ctl00_ContentPlaceHolder1_txt_retencion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_cesion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_retenc_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_montopleno_c").attr("readonly", true);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_cesion_c").attr("readonly", true);
        } else {
            $("#ctl00_ContentPlaceHolder1_txt_retencion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_cesion_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_retenc_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montopleno_c").attr("readonly", false);
            $("#ctl00_ContentPlaceHolder1_txt_montomax_cesion_c").attr("readonly", false);
        }
    });
    //funcion de button nuevo
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnNuevo", "click", function (ev) {
        ev.preventDefault();
        $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").val(0);$("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c1").val(0);$("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c2").val(0);
        if (tablacontrato == 1 && tablareasegurador == 0) {
            $("#ctl00_ContentPlaceHolder1_txt_descrip_contrato").val("");
            $("#ctl00_ContentPlaceHolder1_txt_fecini_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_nrocont_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_fecfin_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_idContrato_c").val(0);
            for (s = 0; s < select.length; s++) {
                $("#" + select[s]).val(0);
            }
            for (t = 0; t < textboxs.length; t++) {
                $("#" + textboxs[t]).val("0.00");
            }
        } else {
            $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_reasegurador_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_codreasegurador_r").val("");
            $("#ctl00_ContentPlaceHolder1_ddl_tipcont_det_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_crediticia_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_calificadora_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_retencion_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_cesion_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_participacion_cesion").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_nombre_rea").val(" ");
            $("#ctl00_ContentPlaceHolder1_txt_nro_registro_rea").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c").val(0);
        }
    });
    //concatener descripcion de Descripcion 
    $('.tabBody').delegate("#ctl00_ContentPlaceHolder1_ddl_tipcon_c", "change", function (ev) {
        var fecha_inicio = new Array($("#ctl00_ContentPlaceHolder1_txt_fecini_c").val().split("/"));
        var fecha_fin = new Array($("#ctl00_ContentPlaceHolder1_txt_fecfin_c").val().split("/"));
        var tipo_contrato = $("#ctl00_ContentPlaceHolder1_ddl_clasecontrato_c option:selected").text();

        $("#ctl00_ContentPlaceHolder1_txt_descrip_contrato").val(descripcion_contrato +" "+ tipo_contrato +" Del "+ meses[parseInt(fecha_inicio[0][1])] + " "+fecha_inicio[0][2]+" Hasta " + meses[parseInt(fecha_fin[0][1])]+" "+fecha_fin[0][2]);
    });
    //jtable grid
    $('#ctl00_ContentPlaceHolder1_txt_cia_c').numeric();
    $('#ctl00_ContentPlaceHolder1_txt_retencion_c').numeric();
    $('#ctl00_ContentPlaceHolder1_txt_cesion_c').numeric();
    $("#ctl00_ContentPlaceHolder1_ddl_estado_c").val("R");
    /*var estado = $("#ctl00_ContentPlaceHolder1_ddl_estado_c").val();
    if (estado == "R") { $("#ctl00_ContentPlaceHolder1_ddl_estado_c").attr("disabled", true); }*/
        $('#tblContratoView').jtable({
            tableId: 'Contratos',
            paging: true,
            sorting: true,
            pageSize: 5,
            defaultSorting: '_estado ASC',
            selecting: true,
            saveUserPreferences: true,
            actions: {
                listAction: '/WebPage/Mantenimiento/frmGeneral.aspx/ContratoList',
            },
            fields: {
                _ide_Contrato: { key: true, list: false },
                _nro_Contrato: { title: 'N°_Contrato' },
                _cla_Contrato: { title: 'Clase_de_Contrato ' },
                _cod_Ramo_Sin: { title: 'Ramo_Siniestro. ' },
                _cod_Ramo_pri: { title: 'Ramo_Primas' },
                _fec_Ini_Vig: { title: 'Inicio_Vigencia', displayFormat: 'dd/mm/yy', type: 'date'},
                _fec_Fin_Vig: { title: 'Fin_Vigencia', displayFormat: 'dd/mm/yy', type: 'date' },
                _tip_Contrato: { title: 'Tipo_de_Contrato ' },
                _cod_Moneda: { title: 'Moneda' },
                _cod_Contratante: { title: 'Contratante' },
                _por_Tasa_Riesgo: {title:'Tasa_Riesgo_(%)'},
                _por_Tasa_Reaseguro: { title:'Tasa_Reaseguro_(%)'},
                _por_Impuesto :{ title:'Impuesto_(%)' },
                _por_Participa_Cia :{ title:'Participacion_(%)'},
                _des_Contrato: { title: 'Descripción_Contrato' },
                _mod_Contrato: { title: 'Modalidad_Contrato' }, 
                _por_Retencion: { title: 'Retención_(%)' },
                _por_Cesion: { title: 'Cesión_(%)' },
                _mto_Max_Retencion: { title: 'Mto_Max_Retención' },
                _mto_Max_Cesion: { title: 'Mto_Max_Cesión' },
                _mto_Pleno: { title: 'Mto_del_Pleno' },
                _nro_Linea_Mult: { title: 'N°_Linea_Multiplo' },
                _mto_Max_Cubertura: { title: 'Mto_Max_Cubertura' },
                _nro_Capa_Xl1: { title: 'Capa_N°_(XL)_(1)' },
                _Prioridad1: { title: 'Prioridad_(1)' },
                _Cesion_Exc_Prioridad1: { title: 'Cesión_Exc_Prioridad (1)' },
                _mto_Max_Cap_Lim_Sup1: { title: 'Mto_Max_Cap_Lim_Sup (1)' },
                _prima_Min_Deposito1: { title: 'Prima_Min_ó_Deposito (1)' },
                _nro_Capa_Xl2: { title: 'Capa_N°_(XL)_(2)' },
                _Prioridad2: { title: 'Prioridad_(2)' },
                _Cesion_Exc_Prioridad2: { title: 'Cesión_Exc_Prioridad (2)' },
                _mto_Max_Cap_Lim_Sup2: { title: 'Mto_Max_Cap_Lim_Sup (2)' },
                _prima_Min_Deposito2: { title: 'Prima_Min_ó_Deposito (2)' },
                _estado: { title: 'Estado' },
                _fec_reg: { title: 'Fecha_Registro', displayFormat: 'dd/mm/yy', type: 'date' },
                _usu_reg: { title: 'Usuario_Registro' }
            },
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#tblContratoView').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');

                        $('#ctl00_ContentPlaceHolder1_txt_idContrato_c').val(record._ide_Contrato);
                        $('#ctl00_ContentPlaceHolder1_txt_nrocont_c').val(record._nro_Contrato);
                        $("#ctl00_ContentPlaceHolder1_ddl_seniestro_c").val(record._cod_Ramo_Sin);
                        $("#ctl00_ContentPlaceHolder1_ddl_ramo_prima_c").val(record._cod_Ramo_pri);
                        $('#ctl00_ContentPlaceHolder1_ddl_clasecontrato_c').val(record._cla_Contrato);

                        $('#ctl00_ContentPlaceHolder1_txt_fecini_c').val(ConvertNumberToDate(record._fec_Ini_Vig));
                        $('#ctl00_ContentPlaceHolder1_txt_fecfin_c').val(ConvertNumberToDate(record._fec_Fin_Vig));

                        $("#ctl00_ContentPlaceHolder1_ddl_tipcon_c").val(record._tip_Contrato);
                        $('#ctl00_ContentPlaceHolder1_ddl_moneda_c').val(record._cod_Moneda);
                        $("#ctl00_ContentPlaceHolder1_ddl_contratante_c").val(record._cod_Contratante);
                        $("#ctl00_ContentPlaceHolder1_txt_participacion_cia_c").val(record._por_Participa_Cia);
                        $("#ctl00_ContentPlaceHolder1_txt_tasariesgo_c").val(record._por_Tasa_Riesgo);
                        $("#ctl00_ContentPlaceHolder1_txt_tasareaseguro_c").val(record._por_Tasa_Reaseguro);
                        $("#ctl00_ContentPlaceHolder1_txt_impuesto_c").val(record._por_Impuesto);
                        $("#ctl00_ContentPlaceHolder1_txt_descrip_contrato").val(record._des_Contrato);
                        $("#ctl00_ContentPlaceHolder1_ddl_estado_c").val(record._estado);

                        $("#ctl00_ContentPlaceHolder1_ddl_modalidad_c").val(record._mod_Contrato);
                        $("#ctl00_ContentPlaceHolder1_txt_retencion_c").val(record._por_Retencion);
                        $("#ctl00_ContentPlaceHolder1_txt_cesion_c").val(record._por_Cesion);
                        $("#ctl00_ContentPlaceHolder1_txt_montomax_retenc_c").val(record._mto_Max_Retencion);
                        $("#ctl00_ContentPlaceHolder1_txt_montomax_cesion_c").val(record._mto_Max_Cesion);
                        $("#ctl00_ContentPlaceHolder1_txt_montopleno_c").val(record._mto_Pleno);
                        $("#ctl00_ContentPlaceHolder1_txt_multiplo_c").val(record._nro_Linea_Mult);
                        $("#ctl00_ContentPlaceHolder1_txt_mto_max_cubert_c").val(record._mto_Max_Cubertura);
                        $("#ctl00_ContentPlaceHolder1_txt_cia_c").val(record._prc_Participa_Cia);

                        $("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c1").val(record._nro_Capa_Xl1);
                        $("#ctl00_ContentPlaceHolder1_txt_prioridad_c1").val(record._Prioridad1);
                        $("#ctl00_ContentPlaceHolder1_txt_excesoprio_c1").val(record._Cesion_Exc_Prioridad1);
                        $("#ctl00_ContentPlaceHolder1_txt_mto_max_lim_sup_c1").val(record._mto_Max_Cap_Lim_Sup1);
                        $("#ctl00_ContentPlaceHolder1_txt_primaminima_deposit_c1").val(record._prima_Min_Deposito1);

                        $("#ctl00_ContentPlaceHolder1_txt_nrocapaxl_c2").val(record._nro_Capa_Xl2);
                        $("#ctl00_ContentPlaceHolder1_txt_prioridad_c2").val(record._Prioridad2);
                        $("#ctl00_ContentPlaceHolder1_txt_excesoprio_c2").val(record._Cesion_Exc_Prioridad2);
                        $("#ctl00_ContentPlaceHolder1_txt_mto_max_lim_sup_c2").val(record._mto_Max_Cap_Lim_Sup2);
                        $("#ctl00_ContentPlaceHolder1_txt_primaminima_deposit_c2").val(record._prima_Min_Deposito2);

                        $("#ctl00_ContentPlaceHolder1_txt_centro_costo").val(record._Centro_Costo);

                        $('#ctl00_hdf_control').val(999);

                    });
                } else {
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });
        $('#tblContratoView .jtable-main-container').css({ "width": "4800px" });
        $('#tblContratoView').jtable('load', { WhereBy: "NO" });
    //asignar valor Inicial de (0) a los textbox
        console.log(tablacontrato, tablareasegurador);
        if (tablacontrato == 1 && tablareasegurador == 0) {
            $("#ctl00_ContentPlaceHolder1_txt_descrip_contrato").val("");
            $("#ctl00_ContentPlaceHolder1_txt_fecini_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_nrocont_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_fecfin_c").val("");
            $("#ctl00_ContentPlaceHolder1_txt_idContrato_c").val(0);
            for (t = 0; t < textboxs.length; t++) {
                $("#" + textboxs[t]).val("0.00");
            }
        } else {
            $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_reasegurador_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_codreasegurador_r").val("");
            $("#ctl00_ContentPlaceHolder1_ddl_tipcont_det_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_crediticia_r").val(0);
            $("#ctl00_ContentPlaceHolder1_ddl_calificadora_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_retencion_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_cesion_r").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_participacion_cesion").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_nombre_rea").val(" ");
            $("#ctl00_ContentPlaceHolder1_txt_nro_registro_rea").val(0);
            $("#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c").val(0);
        }
    //funcion mesagw box
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
