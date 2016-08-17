$(document).ready(function () {
    var dataQuery;
    var visivleColumn;
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_o").val();
        var tipo_comprob = $("#ctl00_ContentPlaceHolder1_ddl_tipcom_o").val();
        var cod_ramo = $("#ctl00_ContentPlaceHolder1_ddl_ramo_o").val();
        var nro_comprob = $("#ctl00_ContentPlaceHolder1_txt_operacion_o").val();
        var fecha_ini = $("#ctl00_ContentPlaceHolder1_txt_fec_ini_o").val();
        var fecha_fin = $("#ctl00_ContentPlaceHolder1_txt_fec_hasta_o").val();        
        if (nro_contrato == 0) {
            MessageBox("Seleccione Contrato");
        } else if (tipo_comprob != 0) {
            if (tipo_comprob == "RI") {
                dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
                visivleColumn = new Array(false, false, false, false, false, false, false, false, true, true, true, true, false, false, false, false);
                jatbleRequestOperacion(dataQuery, visivleColumn, "2600px", "procesaibnr");
            } else if (tipo_comprob == "RP") {
                dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
                visivleColumn = new Array(false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true);
                jatbleRequestOperacion(dataQuery, visivleColumn, "2400px", "procesapago");
            } else if (tipo_comprob == "RM") {
                dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
                visivleColumn = new Array(true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false);
                jatbleRequestOperacion(dataQuery, visivleColumn, "2900px", "procesaprima");
            } else if (tipo_comprob == "RR") {
                dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
                visivleColumn = new Array(false, false, false, false, false, false, false, false, true, true, true, true, false, false, false, false);
                jatbleRequestOperacion(dataQuery, visivleColumn, "2700px", "procesarsp");
            } else {
                dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
                visivleColumn = new Array(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                jatbleRequestOperacion(dataQuery, visivleColumn, "4100px", "procesageneral");//solo si no se selecciono el tipo de operacion
            }
        } else {
            dataQuery = new Array(nro_contrato, tipo_comprob, cod_ramo, nro_comprob, fecha_ini, fecha_fin);
            visivleColumn = new Array(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
            jatbleRequestOperacion(dataQuery, visivleColumn, "4100px", "procesageneral");//solo si no se selecciono el tipo de operacion
        }
        if (tipo_comprob == 0) {
            displayTableWithId("GE");
        } else {
            displayTableWithId(tipo_comprob);
        }
    });

    //funcio da cargada de jtable
    function jatbleRequestOperacion(data, visibleColumn, sizetable, tablaid) {
        $('#' + tablaid).jtable({
            tableId: 'operacion',
            paging: true,
            sorting: true,
            selecting: false,
            pageSize: 13,
            defaultSorting: '_Fec_Operacion ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/Consultas/frmConsultaOperaciones.aspx/GetSelectOperacion'
            },
            fields: {
                _Nro_Operacion: { title: 'N° Operación', key: true },
                _Tip_Operacion: { title: 'Tipo Operación' },
                _Cod_Reasegurador: { title: 'Reasegurador' },
                _Fec_Operacion: { title: 'Fecha de Operación', type: 'date', displayFormat: 'dd/mm/yy' },
                _Ano_Operacion: { title: 'Año' },
                _Mes_Operacion: { title: 'Mes' },
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
                _Sin_Directo: { title: 'Siniestros_Directos', list: visibleColumn[8] },
                _Sin_Xpag_Rea_Ace: { title: 'Siniestros_Cedidos', list: visibleColumn[9] },
                _Dscto_Comis_Rea: { title: 'Comisiones_/_Descuentos', list: visibleColumn[10] },
                _Sin_Xcob_Rea_Ced: { title: 'Siniestros_por_Cobrar', list: visibleColumn[11] },
                _Otr_Cta_Xcob_Rea_Ced: { title: 'Otr_Cta_Xcob_Rea_Ced', list: visibleColumn[12] },
                _Otr_Cta_Xpag_Rea_Ace: { title: 'Otr_Cta_Xpag_Rea_Ace', list: visibleColumn[13] },
                _Saldo_Deudor: { title: 'Saldo_Deudor', list: visibleColumn[14] },
                _Saldo_Acreedor: { title: 'Saldo_Acreedor', list: visibleColumn[15] },
                _Saldo_Deudor_Comp: { title: 'Saldo_Deudor_Comp', sorting: false, list: visibleColumn[15] },
                _Saldo_Acreedor_Comp: { title: 'Saldo_Acreedor_Comp', sorting: false, list: visibleColumn[15] },
                _Estado: { title: 'Estado' },
                _Fec_Reg: { title: 'Fecha_Registro', list: true, type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Reg: { title: 'Usu_Reg', list: true },
                _Fec_Mod: { title: 'Fec_Mod', list: false, type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Mod: { title: 'Usu_Mod', list: false },
            }
        });
        $('#' + tablaid + ' .jtable-main-container').css({ "width": sizetable });
        $('#' + tablaid).jtable('load', { 'dataFrm': data });
    }
    function displayTableWithId(tipo_info) {
        switch (tipo_info) {
            case "RI":
                $("#procesaibnr").css({ "display": "block" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "none" });
                $("#procesageneral").css({ "display": "none" });
                break;
            case "RP":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "block" });
                $("#procesageneral").css({ "display": "none" });
                break;
            case "RR":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "none" });
                $("#procesarsp").css({ "display": "block" });
                $("#procesapago").css({ "display": "none" });
                $("#procesageneral").css({ "display": "none" });
                break;
            case "RM":
                $("#procesaibnr").css({ "display": "none" });
                $("#procesaprima").css({ "display": "block" });
                $("#procesarsp").css({ "display": "none" });
                $("#procesapago").css({ "display": "none" });
                $("#procesageneral").css({ "display": "none" });
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
    //calendario
    $("#ctl00_ContentPlaceHolder1_txt_fec_ini_o").datepicker();
    $("#ctl00_ContentPlaceHolder1_txt_fec_hasta_o").datepicker();

    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});
