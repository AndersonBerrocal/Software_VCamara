$(document).ready(function () {
    var contrato_sis = function () {
        this.IDE_CONTRATO = $("#ddl_contrato").val()
    };
    //eventos
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var filters = [$("#ddl_tipo_archivo").val(), $("#txt_fec_ini_o").val(), $("#txt_fec_hasta_o").val(), $("#txt_fecha_aprobacion_desde").val(), $("#txt_fecha_aprobacion_hasta").val()];
        listSegDescarga(new contrato_sis(), filters);
    });
    const action = "/WebPage/ModuloDIS/Consultas/frmSegDescarga.aspx/listSegDescarga";
    var fields = {
        NombreArchivo: { title: 'NombreArchivo' },
        FechaCarga: { title: 'FechaCarga', type: 'date', displayFormat: 'dd/mm/yy' },
        FechaAprobacion: { title: 'FechaAprobación', type: 'date', displayFormat: 'dd/mm/yy' },
        Usuario: {title:'Usuario'},
        NroLineas: {title:'NroLineas'},
        Estado: { title:'Estado' },
        Importe: { title:'Importe' }
    }
    function listSegDescarga(contrato, filters) {
        $('#tblApruebaCarga').jtable({
            tableId: 'ApruebaCarga',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: 'NombreArchivo ASC',
            selecting: false,
            actions: {
                listAction: action,
            },
            recordsLoaded: function (event, data) {
                //GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: fields
        });

        $("#ApruebaCarga").css({"text-align":"center"});
        $('#tblApruebaCarga.jtable-main-container').css({ "width": "1500px" });
        $('#tblApruebaCarga').jtable('load', { contrato: contrato, filters: filters });
    }
})