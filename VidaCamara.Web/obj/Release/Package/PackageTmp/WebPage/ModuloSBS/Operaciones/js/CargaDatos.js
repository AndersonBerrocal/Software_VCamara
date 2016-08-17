$(document).ready(function () {
    //validacion detablas para mostrar data
    var tipo_pago = $('#ctl00_ContentPlaceHolder1_ddl_tippago_p').val();
    var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_p").val();
    var tablapagoval = $("#cargaDatos").length;
    if (tablapagoval == 1) {
        jatbleRequest(nro_contrato, tipo_pago, 16);
    }
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tippago_p", 'change', function (e) {
        var nro_contrato_s = $("#ctl00_ContentPlaceHolder1_ddl_contrato_p").val();
        jatbleRequest(nro_contrato_s, $(this).val(), 16, "NO", 1);
    });

    //validacion de boton borrar despues de cargar data
    $('section').delegate('#ctl00_ContentPlaceHolder1_btn_borrar_c', 'click', function (e) {
        var rspid = $("#CargaRSP").length;
        var pagoid = $("#cargaDatos").length;
        if (rspid == 0 && pagoid == 0) {
            return false;
        } else if (rspid == 1 && pagoid == 0) {
            var contrato_rsp = $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val();
            if (contrato_rsp == 0) { MessageBox("Selecione el Contrato"); return false }
            else{return confirm("Esta seguro de Eliminar los Registros"); }
        } else if (rspid == 0 && pagoid == 1) {
            var informacion = $("#ctl00_ContentPlaceHolder1_ddl_tippago_p").val();
            var contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_p").val();
            if (contrato == 0) { MessageBox("Selecione el Contrato"); return false }
            else if (informacion == 0) { MessageBox("Selecione Tipo de Pago"); return false }
            else { return confirm("Esta seguro de Eliminar los Registros"); }
        }
    });
    //validacion de campos para grabar
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnGuardar", 'click', function () {
        var tablarsp = $("#CargaRSP").length;
        var tablapago = $("#cargaDatos").length;
        if (tablarsp == 0 && tablapago == 0) {
            var contrato = $("#ctl00_ContentPlaceHolder1_dbl_contrato_d").val();
            var informacion = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo_d").val();
            var informacion_text = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo_d option:selected").text();
            var archivo = $("#ctl00_ContentPlaceHolder1_txt_archivo_d").val();

            if (contrato == 0) { MessageBox("Selecione el Contrato"); return false }
            else if (informacion == 0) { MessageBox("Selecione Tipo de Información"); return false }
            else if (archivo == "") { MessageBox("Selecione El Archivo"); return false }
            else { return confirm("¿ Esta seguro de Guardar los Registros de "+informacion_text+" ?"); }
        } else {
            return false;
        }
    });
    //exportar excel errados
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnExportError", "click", function () {
        var excelerror = $("#ctl00_ContentPlaceHolder1_hdf_excel_error").val();
        if (excelerror == 0) {
            $("#ctl00_ContentPlaceHolder1_hdf_excel_error").val("499");
            return true;
        }
    });
    function jatbleRequest(nro_contrato,tipo,pg) {
        $('#tableCargaDatos').jtable({
            paging: true,
            sorting: true,
            pageSize: pg, 
            defaultSorting: '_Mes_Vigente ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/Operaciones/frmCargaDatos.aspx/GetSelectSepelio',
            },
            recordsLoaded: function (event, data) {
                var totalFilas = data.serverResponse.TotalRecordCount;
                if (totalFilas > 0) {
                    var estadoData = data.serverResponse.Records[0]._Estado;
                    $("#ctl00_ContentPlaceHolder1_lbl_estado_p").text("Leido y Guardado");
                    $("#ctl00_ContentPlaceHolder1_hdf_estado_borrar").val(estadoData);
                    $("#ctl00_ContentPlaceHolder1_hdf_tabla_id").val("PAGOS");
                } 
                else if (totalFilas == 0) { $("#ctl00_ContentPlaceHolder1_lbl_estado_p").text("*"); }
                $("#ctl00_ContentPlaceHolder1_lbl_conteo_tot").text(totalFilas);
            },
            fields: {
                _Ide_Empresa: { title: 'Empresa', list: false },
                _Nro_Contrato:{title: 'N° Contrato'},
                _Anio_Vigente:{title: 'Año'},
                _Mes_Vigente:{title: 'Mes Vigente.'},
                _Cod_Ramo: { title: 'Cod Ramo', list: false },
                _Cod_Producto: { title: 'Cod Prod.', list: false },
                _Cod_Asegurado:{title: 'Cod Asegur.'},
                _Cod_Moneda:{title: 'Cod Moneda'},
                _Ap_Mtno_Pens:{title: 'Ap_Mto_Pens'},
                _Ap_Mtno_Solic: { title: 'Ap_Mtno_Solic'},
                _Ap_Ptno_Pens: { title: 'Ap_Ptno_Pens'},
                _Ap_Ptno_Solic: { title: 'Ap_Ptno_Solic'},
                _Aport_Comp: { title: 'Aport_Comp'},
                _Aport_Oblig: { title: 'Aport_Oblig'},
                _Cap_Req_Pens: { title: 'Cap_Req_Pens'},
                _Cap_Req_Sepe: { title: 'Cap_Req_Sepe'},
                _Cic_Mon_Nsol: { title: 'Cic_Mon_Nsol'},
                _Cic_Mon_Orig: { title: 'Cic_Mon_Orig'},
                _Cru_Familiar: { title: 'Cru_Familiar'},
                _Cuspp: { title: 'Cuspp'},
                _Fe_Sin: { title: '&nbsp;&nbsp;Fe_Sin&nbsp;&nbsp;&nbsp;'},
                _Id_Afp: { title: 'Id_Afp'},
                _Id_Cod_Csv1: { title: 'Id_Cod_Csv1'},
                _Id_Cod_Csv2: { title: 'Id_Cod_Csv2'},
                _Id_Cod_Csv3: { title: 'Id_Cod_Csv3'},
                _Id_Cod_Csv4: { title: 'Id_Cod_Csv4'},
                _Id_Cod_Transf: { title: 'Id_Cod_Transf'},
                _Id_Tip_Soli: { title: 'Id_Tip_Soli'},
                _Mto_Pago_Csv1: { title: 'Mto_Pago_Csv1'},
                _Mto_Pago_Csv2: { title: 'Mto_Pago_Csv2'},
                _Mto_Pago_Csv3: { title: 'Mto_Pago_Csv3'},
                _Mto_Pago_Csv4: { title: 'Mto_Pago_Csv4'},
                _Ti_Mov: { title: 'Ti_Mov'},
                _Fe_Dev: { title: 'Fe_Dev'},
                _Fe_Falle: { title: 'Fe_Falle'},
                _Fe_Ini_Inv: { title: 'Fe_Ini_Inv'},
                _Fe_Pag_Aa: { title: 'Fe_Pag_Aa'},
                _Id_Tip_Doc: { title: 'Id_Tip_Doc'},
                _Ind_Pens_Pre: { title: 'Ind_Pens_Pre'},
                _Mto_Aa_Mon_Nsol: { title: 'Mto_Aa_Mon_Nsol'},
                _Mto_Aa_Mon_Orig: { title: 'Mto_Aa_Mon_Orig'},
                _Mto_Exc_Mon_Nsol: { title: 'Mto_Exc_Mon_Nsol'},
                _Mto_Exc_Mon_Orig: { title: 'Mto_Exc_Mon_Orig'},
                _Mto_Pens_Pre_Mon_Nsol: { title: 'Mto_Pens_Pre_Mon_Nsol'},
                _Mto_Pens_Pre_Mon_Orig: { title: 'Mto_Pens_Pre_Mon_Orig'},
                _Nro_Benef: { title: 'Nro_Benef'},
                _Nro_Iden_Solic: { title: 'Nro_Iden_Solic'},
                _Num_Csv: { title: 'Num_Csv'},
                _Pri_Nomb_Solic: { title: 'Pri_Nomb_Solic'},
                _Ram_Prom_Nsol: { title: 'Ram_Prom_Nsol'},
                _Segu_Nomb_Solic: { title: 'Segu_Nomb_Solic'},
                _Tasa_Int: { title: 'Tasa_Int', width: '18%' },
                _Ti_Camb_Compra: { title: 'Ti_Camb_Compra'},
                _Ti_Camb_Vta: { title: 'Ti_Camb_Vta'},
                _Ti_Reg: { title: 'Ti_Reg'},
                _Tip_Mon: { title: 'Tip_Mon'},
                _Tip_Pen_Equiv: { title: 'Tip_Pen_Equiv'},
                _Tot_Cap_Req_Mon_Nsol: { title: 'Tot_Cap_Req_Mon_Nsol'},
                _Tot_Cap_Req_Mon_Orig: { title: 'Tot_Cap_Req_Mon_Orig'},
                _Desct_Pens: { title: 'Desct_Pens'},
                _Desct_Pens_Csv1: { title: 'Desct_Pens_Csv1'},
                _Desct_Pens_Csv2: { title: 'Desct_Pens_Csv2'},
                _Desct_Pens_Csv3: { title: 'Desct_Pens_Csv3'},
                _Desct_Pens_Csv4: { title: 'Desct_Pens_Csv4'},
                _Fe_Dev_Act: { title: 'Fe_Dev_Act'},
                _Fe_Dev_Ini: { title: 'Fe_Dev_Ini'},
                _Fe_Fin_Subsi: { title: 'Fe_Fin_Subsi'},
                _Fe_Naci_Pens: { title: 'Fe_Naci_Pens'},
                _Fe_Seci: { title: 'Fe_Seci'},
                _Fec_Pago: { title: 'Fec_Pago'},
                _Frac_Mes_Dev: { title: 'Frac_Mes_Dev'},
                _Id_Cod_Ben: { title: 'Id_Cod_Ben'},
                _Id_Parent: { title: 'Id_Parent'},
                _Id_Tip_Docu_Pens: { title: 'Id_Tip_Docu_Pens'},
                _Mes_Dev: { title: 'Mes_Dev'},
                _Mon_Equi: { title: 'Mon_Equi'},
                _Mto_Aport_Comp_Csv1: { title: 'Mto_Aport_Comp_Csv1'},
                _Mto_Aport_Comp_Csv2: { title: 'Mto_Aport_Comp_Csv2'},
                _Mto_Aport_Comp_Csv3: { title: 'Mto_Aport_Comp_Csv3'},
                _Mto_Aport_Comp_Csv4: { title: 'Mto_Aport_Comp_Csv4'},
                _Mto_Aport_Oblig_Csv1: { title: 'Mto_Aport_Oblig_Csv1'},
                _Mto_Aport_Oblig_Csv2: { title: 'Mto_Aport_Oblig_Csv2'},
                _Mto_Aport_Oblig_Csv3: { title: 'Mto_Aport_Oblig_Csv3'},
                _Mto_Aport_Oblig_Csv4: { title: 'Mto_Aport_Oblig_Csv4'},
                _Mto_Pago: { title: 'Mto_Pago'},
                _Nro_Docu_Pens: { title: 'Nro_Docu_Pens'},
                _Nro_Eess: { title: 'Nro_Eess'},
                _Nro_Mes_Dev: { title: 'Nro_Mes_Dev'},
                _Nro_Sin: { title: 'Nro_Sin'},
                _Nro_Soli: { title: 'Nro_Soli'},
                _Pens_Base: { title: 'Pens_Base'},
                _Pens_Pagar_80: { title: 'Pens_Pagar_80'},
                _Pri_Nomb_Pens: { title: 'Pri_Nomb_Pens'},
                _Proc_Bene: { title: 'Proc_Bene'},
                _Prom_Act_6_Meses_Apocomp: { title: 'Prom_Act_6_Meses_Apocomp'},
                _Rempro_Act: { title: 'Rempro_Act'},
                _Segu_Nomb_Pens: { title: 'Segu_Nomb_Pens'},
                _Tip_Pen: { title: 'Tip_Pen'},
                _Estado: { title: 'Estado'},
                _Fec_Reg: {title: 'Fec_Reg', type: 'date',displayFormat: 'yy-mm-dd'},
                _Usu_Reg: { title: 'Usu_Reg'},
                _Fec_Mod: { title: 'Fec_Mod', list: false },
                _Usu_Mod: { title: 'Usu_Mod', list: false }
            }
        });
        $('#tableCargaDatos .jtable-main-container').css({ "width": "13000px" });
        $('#tableCargaDatos').jtable('load', { nro_contrato: nro_contrato, tipo: tipo });
    }

    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
    //asigncion de funciones jquery 
    //$("#ctl00_ContentPlaceHolder1_txt_mesvig_d").datepicker();
});
