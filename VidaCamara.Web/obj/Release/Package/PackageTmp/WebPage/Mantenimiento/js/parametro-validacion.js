$(document).ready(function () {
    //REALIZAR VALIADACIONES DE FORMULARIO
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnGuardar", "click", function (ev) {
        const tablaidContratoSbs = $("#tblContratoView").length;
        const tablaidContratoSbsDet = $("#tblReasegurador").length;
        const tablaidContratoSis = $("#tblContratoViewSyS").length;
        const tableidContratoSisDet = $("#tblContratoViewSySDetalle").length;

        console.log(tablaidContratoSbs, tablaidContratoSbsDet, tablaidContratoSis, tableidContratoSisDet);
        //CONTRATO_SIS_DETALLE
        if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 0 && tableidContratoSisDet == 1) {
            var ide_contrato_det = $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis_det").val();
            var ide_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_sis").val();
            var cod_csv = $("#ctl00_ContentPlaceHolder1_ddl_compania_seg_vida").val();
            var por_particpacion = $("#ctl00_ContentPlaceHolder1_txt_participacion_sis").val();
            var orden = $("#ctl00_ContentPlaceHolder1_txt_orden_empresa_sis").val();

            if (parseInt(ide_contrato_det) == 0) {
                if (parseInt(ide_contrato) == 0) {
                    MessageBox("Seleccione el contrato"); return false;
                } else if (parseInt(cod_csv) == 0) {
                    MessageBox("Seleccione el Cia. Seguros Vida"); return false;
                } else if (por_particpacion == "") {
                    MessageBox("Ingrese el valor para % participación"); return false;
                } else if (orden == "") {
                    MessageBox("Ingrese valor para Orden"); return false;
                } else {
                    return confirm("Esta seguro de crear el registro?");
                }
            } else {
                return confirm("Esta seguro de actualizar el registro?");
            };
            //CONTRATO SIS
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 1 && tableidContratoSisDet == 0) {
            var ide_contrato_sis = $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis").val();
            if (parseInt(ide_contrato_sis) == 0) {
                var fecha_inicio = $("#ctl00_ContentPlaceHolder1_txtFechaInicio_sys").val();
                var fecha_fin = $("#ctl00_ContentPlaceHolder1_txtFechaFin_sys").val();
                var codContrato = $("#ctl00_ContentPlaceHolder1_txt_nrocont_sys").val();
                var numEmpresas = $("#ctl00_ContentPlaceHolder1_txt_numero_empresa").val();
                if (codContrato == "") {
                    mostrarMensajeAlert("El campo Codigo de Contrato esta vacio");
                    return false;
                }else if(numEmpresas == "") {
                    mostrarMensajeAlert("El campo Numero de Empresas esta vacio");
                    return false;
                }else if(fecha_inicio == "") {
                    mostrarMensajeAlert("El campo Fecha de Inicio esta vacio");
                    return false;
                }else if(fecha_fin == "") {
                    mostrarMensajeAlert("El campo Fecha de Fin esta vacio");
                    return false;
                }else 
                    return confirm("Está seguro de crear el registro?");
                
                //return false;
            } else {
                return confirm("Está seguro de actualizar el registro?");
            }
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 0 && tableidContratoSisDet == 0) {
            var idgeneral = $("ctl00_ContentPlaceHolder1_txt_idempresa").val();
            if (idgeneral == 0) {
                return confirm("¿Esta seguro de Grabar?");
            } else {
                return confirm("¿ Está Seguro de Actualizar el Registro ?");
            }
        } else if (tablaidContratoSbs == 1 && tablaidContratoSbsDet == 0 && tablaidContratoSis == 0 && tableidContratoSisDet == 0) {
            var idcontrato = $("#ctl00_ContentPlaceHolder1_txt_idContrato_c").val();
            if (idcontrato == 0) {
                var clacont = $("#ctl00_ContentPlaceHolder1_ddl_clasecontrato_c").val();
                var nrocont = $("#ctl00_ContentPlaceHolder1_txt_nrocont_c").val();
                var tipcont = $("#ctl00_ContentPlaceHolder1_ddl_tipcon_c").val();
                var ramo = $("#ctl00_ContentPlaceHolder1_ddl_ramo_c").val();
                var ramsin = $("#ctl00_ContentPlaceHolder1_ddl_seniestro_c").val();
                var tipcontdet = $("#ctl00_ContentPlaceHolder1_ddl_tipcont_det_c").val();
                var fecini = $("#ctl00_ContentPlaceHolder1_txt_fecini_c").val();
                var fecfin = $("#ctl00_ContentPlaceHolder1_txt_fecfin_c").val();
                var asegur = $("#ctl00_ContentPlaceHolder1_ddl_asegurado_c").val();
                var moneda = $("#ctl00_ContentPlaceHolder1_ddl_moneda_c").val();
                var porret = $("#ctl00_ContentPlaceHolder1_txt_retencion_c").val();
                var porcec = $("#ctl00_ContentPlaceHolder1_txt_cesion_c").val();
                var porcia = $("#ctl00_ContentPlaceHolder1_txt_cia_c").val();
                //por evaluar
                var modalidadContrato = $("#ctl00_ContentPlaceHolder1_ddl_modalidad_c").val();
                if (clacont == 0 || nrocont == "" || tipcont == 0 || ramo == 0 || ramsin == 0 || tipcontdet == 0 || fecini == "" || fecfin == "" || asegur == 0 || moneda == 0 || modalidadContrato == "0") {
                    MessageBox("Ingrese y Selecione los Campos Requiridos"); return false;
                } else if (porret == 0 || porcec == 0 || porcia == 0) {
                    return confirm("Hay Algunos Campos con valor cero. ¿Esta seguro de Grabar?");
                } else {
                    return confirm("¿Esta seguro de Grabar?");
                }
            } else {
                return confirm("¿ Está Seguro de Actualizar el Registro ?");
            }
        } else if (tablaidContratoSbs == 0 && tablaidContratoSbsDet == 1 && tablaidContratoSis == 0 && tableidContratoSisDet == 0) {
            var idcontdet = $("#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c").val();
            if (idcontdet == 0) {
                var calcula = 0;
                $(".tabBody input[type='text']").each(function (index, element) {
                    valor = $(this).val();
                    if (valor == 0) {
                        calcula++;
                    }
                });
                var contdet = $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val();
                var reasegu = $("#ctl00_ContentPlaceHolder1_ddl_reasegurador_r").val();
                var califica = $("#ctl00_ContentPlaceHolder1_ddl_calificadora_r").val();
                var crediti = $("#ctl00_ContentPlaceHolder1_ddl_crediticia_r").val();
                if (contdet == 0 || reasegu == 0 || califica == 0 || crediti == 0) {
                    MessageBox("Ingrese y Selecione los Campos Requiridos"); return false;
                } else if (calcula > 0) {
                    return confirm("Hay Algunos Campos con valor cero. ¿Esta seguro de Grabar?");
                } else {
                    return confirm("¿Esta seguro de Grabar?");
                }
            } else {
                return confirm("¿ Está Seguro de Actualizar el Registro ?");
            }
        };
    });
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});

$('<div style="font-size:14px;text-align:center;">Ocurrio el siguiente error The DELETE statement conflicted with the REFERENCE constraint "FK_CONTRATO_SIS_DET_CONTRATO_SYS". The conflict occurred in database "vcamara", table "dbo.CONTRATO_SIS_DET", column IDE_CONTRATO.The statement has been terminated.</div>').dialog({title:'Error',modal:true,width:400,height:160,buttons: [{id: 'aceptar',text: 'Aceptar',icons: { primary: 'ui-icon-circle-check' },click: function () {$(this).dialog('close');}}]});