$(document).ready(function () {
    var queryinicio = new Array($("#ctl00_ContentPlaceHolder1_ddl_contrato_c").val(), "0", "0", "","","");
    jatbleRequest(queryinicio, 13);

    var dataQuery;
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var comp_detalle = $("#tbl_comprobnate_det").length;
        if (comp_detalle == 0) {
            var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_c").val();
            var tipo_comprob = $("#ctl00_ContentPlaceHolder1_ddl_tipcom_c").val();
            var cod_ramo = $("#ctl00_ContentPlaceHolder1_ddl_ramo_c").val();
            var nro_comprob = $("#ctl00_ContentPlaceHolder1_txt_operacion_c").val();
            var fecha_ini = $("#ctl00_ContentPlaceHolder1_txt_fec_ini").val();
            var fecha_fin = $("#ctl00_ContentPlaceHolder1_txt_fec_hasta").val();

            $("#ctl00_ContentPlaceHolder1_hdf_ide_contrato_c").val(nro_contrato);
            $("#ctl00_ContentPlaceHolder1_hdf_tip_comp_c").val(tipo_comprob);
            $("#ctl00_ContentPlaceHolder1_hdf_cod_ramo_c").val(cod_ramo);
            $("#ctl00_ContentPlaceHolder1_hdf_nro_comp_c").val(nro_comprob);
            $("#ctl00_ContentPlaceHolder1_hdf_fecha_ini_c").val(fecha_ini);
            $("#ctl00_ContentPlaceHolder1_hdf_fecha_fin_c").val(fecha_fin);

            dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
            jatbleRequest(dataQuery, 13);
        }
    });

    //funcio da cargada de jtable
    function jatbleRequest(data, pg) {
        $('#tblcomprobante').jtable({
            tableId: 'Comprobante',
            paging: true,
            sorting: true,
            pageSize: pg,
            defaultSorting: '_Nro_Comprobante ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/consultas/frmConsultaComprobante.aspx/GetSelectComprobante',
            },
            recordsLoaded: function (event, data) {
                var totalFilas = data.serverResponse.TotalRecordCount;
                $("#ctl00_ContentPlaceHolder1_lbl_total_rsp").text(totalFilas);
            },
            fields: {
                _Nro_Comprobante: { title: 'N° Comprobante' },
                _Tip_Comprobante: { title: 'Tip. Comprobante' },
                _Cod_Reasegurador: { title: 'Asegurador' },
                _Fec_Comprobante: { title: 'F. Creación', type: 'date', displayFormat: 'dd/mm/yy' },
                _Ano_Comprobante: { title: 'Año' },
                _Mes_Comprobante: { title: 'Mes' },
                _Cod_Asegurado: { title: 'Asegurado' },
                _Ide_Contrato: { title: 'N° Contrato' },
                _Cod_Ramo: { title: 'Ramo'},
                _Cod_Moneda: { title: 'Cod. Moneda' },
                _Des_Reg_Trimestre: { title: 'Descripción'},
                _Pri_Xpag_Rea_Ced: { title: 'Pri_Xpag_Rea_Ced' },
                _Pri_Xcob_Rea_Ace: { title: 'Pri_Xcob_Rea_Ace' },
                _Sin_Xcob_Rea_Ced: { title: 'Sin_Xcob_Rea_Ced' },
                _Sin_Xpag_Rea_Ace: { title: 'Sin_Xpag_Rea_Ace' },
                _Otr_Cta_Xcob_Rea_Ced: { title: 'Otr_Cta_Xcob_Rea_Ced' },
                _Otr_Cta_Xpag_Rea_Ace: { title: 'Otr_Cta_Xpag_Rea_Ace' },
                _Dscto_Comis_Rea: { title: 'Dscto_Comis_Rea' },
                _Saldo_Deudor: { title: 'Saldo_Deudor', sorting: false },
                _Saldo_Acreedor: { title: 'Saldo_Acreedor', sorting: false },
                _Saldo_Deudor_Comp: { title: 'Saldo_Deudor_Comp', sorting: false },
                _Saldo_Acreedor_Comp: { title: 'Saldo_Acreedor_Comp', sorting: false },
                _Estado: { title: 'Estado', sorting: false },
                _Fec_Reg: { title: 'Fec_Registro', list: true, type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Reg: { title: 'Usu_Reg', list: false, },
                _Fec_Mod: { title: 'Fec_Mod', list:false,type: 'date', displayFormat: 'yy-mm-dd' },
                _Usu_Mod: { title: 'Usu_Mod', list:false },
            }
        });
        $('#tblcomprobante .jtable-main-container').css({ "width": "3500px" });
        var table = $('#tblcomprobante').jtable('load', { 'dataFrm': data });
    }
    //tipo de calendarios
    $("#ctl00_ContentPlaceHolder1_txt_fec_ini").datepicker();
    $("#ctl00_ContentPlaceHolder1_txt_fec_hasta").datepicker();
});
