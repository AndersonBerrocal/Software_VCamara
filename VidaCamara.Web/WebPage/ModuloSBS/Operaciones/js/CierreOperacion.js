$(document).ready(function () {
    //validacion de botones antes de grabar la informacion
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_exporta_excel", "click", function (ev) {
        var tabla_cierre = $("#tbl_cierre").length;
        if (tabla_cierre == 0)
            return false;
        else
            return true;
    });
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_contrato_detC", "change", function (ev) {
        $('#tbl_cierre').attr('style', 'display', 'block');
        var data = new Array($(this).val(),"0");
            jatbleRequest(data, 13);
    });
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tipinfo_detC", "change", function (ev) {
        $('#tbl_cierre').attr('style', 'display', 'block');
        var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_detC").val();
        var data = new Array(nro_contrato,$(this).val());
        if (nro_contrato == 0) {
            MessageBox("Selecione el Contrato");
            return false;
        } else {
            jatbleRequest(data, 13);
        }
    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_procesa_cierre", "click", function (ev) {
        var tableExist = $("#tbl_cierre").length;
        if (tableExist == 0) {
            var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_c").val();
            var tipo_registro = $("#ctl00_ContentPlaceHolder1_ddl_tipo_cierre").val();
            var tipo_info = $("#ctl00_ContentPlaceHolder1_ddl_tipinfo_c").val();
            if (nro_contrato == 0) { MessageBox("Selecione el Contrato"); return false; }
            else if (tipo_registro == 0) { MessageBox("Selecione el Tipo de Registro"); return false; }
            else if (tipo_info == 0) { MessageBox("Selecione de Información"); return false; }
            else { return confirm("Está Seguro de Realizar la Operación?"); }
        } else {
            return false;
        }
    });
    function jatbleRequest(data, pg) {
        $('#tblcierreproceso').jtable({
            tableId: 'cierre_proceso',
            paging: true,
            sorting: true,
            selecting: true,
            pageSize: pg,
            defaultSorting: '_Nro_Contrato ASC',
            selectingCheckboxes: true,
            actions: {
                listAction: '/WebPage/ModuloSBS/Operaciones/frmCierreProceso.aspx/GetSelectCierreProceso',
                deleteAction: '/WebPage/ModuloSBS/Operaciones/frmCierreProceso.aspx/SetEliminarCierreProceso'
            },
            recordsLoaded: function (event, data) {
                var totalFilas = data.serverResponse.TotalRecordCount;
            },
            fields: {
                _Id_Cierre: { title: 'N°_Cierre', key: true },
                _Nro_Contrato: { title: 'N°_Contrato' },
                _Tipo_Info: { title: 'Tipo_Información' },
                _Fec_Cie: { title: 'Fecha_Cierre', type: 'date', displayFormat: 'dd/mm/yy' },
                _Anio_Cierre: { title: 'Año_Cierre' },
                _Mes_Cierre:{ title:'Mes_Cierre'},
                _Nro_Registro: { title: 'N°_Registros' },
                _Tipo_Cierre: { title: 'Tipo_de_Cierre' },
                _Total_Monto: { title: 'Monto_Total' },
                _Estado: { title: 'Estado' },
                _Fec_Reg: { title: 'Fecha_Registro', type: 'date', displayFormat: 'dd/mm/yy' },
                _Usu_Reg: { title: 'Usuario_Registro' },
            }
        });
        $('#tbl_cierre .jtable-main-container').css({ "width": "2000px" });
        var table = $('#tblcierreproceso').jtable('load', { 'data': data });
    }
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});