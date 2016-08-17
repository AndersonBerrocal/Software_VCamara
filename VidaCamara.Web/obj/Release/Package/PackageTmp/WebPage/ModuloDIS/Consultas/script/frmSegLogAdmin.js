$(document).ready(function () {
    var logOperacion = function () {
        this.IDE_CONTRATO = $("#ctl00_ContentPlaceHolder1_ddl_contrato").val(),
        this.TipoOper = $("#ctl00_ContentPlaceHolder1_ddl_tipo_evento").val(),
        this.Evento = $("#ctl00_ContentPlaceHolder1_txt_evento_descripcion").val()
        //this.CodiEven = $("#ctl00_ContentPlaceHolder1_ddl_tipo_evento").val(),
    };
    //eventos
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var filters = [$("#ctl00_ContentPlaceHolder1_txt_fec_ini_o").val(), $("#ctl00_ContentPlaceHolder1_txt_fec_hasta_o").val()];
        listLogOperacion(new logOperacion(), filters);
    });
    var action = "/WebPage/ModuloDIS/Consultas/frmSegLogAdmin.aspx/listLogOperacion";
    var fields = {
        IDE_CONTRATO: {
            title: 'Contrato', display: function (data) {
                return data.record.CONTRATO_SYS.DES_CONTRATO;
            }
        },
        Tabla : {title:'Tabla - ID'},
        TipoEvento: { title: 'Tipo de Evento' },
        FechEven: {
            title: 'Fecha de creación', display: function (data) {
                return ConvertNumberToDateTime(data.record.FechEven);
            }
        },
        Evento: { title: 'Evento' },
        Columna: {title:'Columna - Dato'},
        CodiUsu: {title:'Usuario'}
    }
    //
    //ejecutar recarga de la grilla por tipo de archivo
    //$(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tipo_linea", "change", function () {
    //    if (existePestanaRegla == 1 && parseInt(tipoArchivo) != 0)
    //        listLogOperacion(new logOperacion());
    //});
    function listLogOperacion(log,filters) {
        $('#tblLogOperacion').jtable({
            tableId: 'IDE_CONTRATO',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: 'IDE_CONTRATO ASC',
            selecting: true,
            actions: {
                listAction: action,
            },
            recordsLoaded: function (event, data) {
                //GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: fields
        });

        $('.jtable-main-container').css({ "width": "1200px" });
        $('#tblLogOperacion').jtable('load', { log: log, filters: filters });
    }
})