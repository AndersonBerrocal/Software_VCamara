$(document).ready(function () {
    var action = "/WebPage/ModuloDIS/Operaciones/frmCargaDatos.aspx/listReglaArchivo";
    var fields = {
        IdReglaArchivo:{title:'Id Regla'},
        NombreCampo: { title: 'NombreCampo'},
        InformacionCampo: { title: 'InformacionCampo' },
        TipoLinea: { title: 'TipoLinea' },
        CaracterInicial: { title: 'CaracterInicial' },
        LargoCampo: { title: 'LargoCampo' },
        TipoCampo: { title: 'TipoCampo' },
        FormatoContenido: { title: 'FormatoContenido' },
        ReglaValidacion: { title: 'ReglaValidacion' }
    }
    var reglaArchivo = function () {
        this.TipoLinea = $("#ctl00_ContentPlaceHolder1_ddl_tipo_linea").val(),
        this.Archivo = $("#ctl00_ContentPlaceHolder1_hdf_tipo_archivo").val(),
        this.IdReglaArchivo = $("#ctl00_ContentPlaceHolder1_txt_idregla").val() == ""?0:parseInt($("#ctl00_ContentPlaceHolder1_txt_idregla").val()),
        this.Vigente = 1
    };
    //verificar si nos encontramos en la pestaña de información
    const existePestanaRegla = $("#tblReglaArchivo").length;
    const tipoArchivo = $("#ctl00_ContentPlaceHolder1_hdf_tipo_archivo").val();
    if (existePestanaRegla == 1 && parseInt(tipoArchivo) != 0)
        listReglaArchivo(new reglaArchivo());
    //ejecutar recarga de la grilla por tipo de archivo
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_tipo_linea", "change", function () {
        if (existePestanaRegla == 1 && parseInt(tipoArchivo) != 0)
            listReglaArchivo(new reglaArchivo());
    });
    $(".tabBody").delegate("#buscar_regla_id", "click", function (ev) {
        if (existePestanaRegla == 1 && parseInt(tipoArchivo) != 0)
            listReglaArchivo(new reglaArchivo());
    });
    function listReglaArchivo(regla) {
        $('#tblReglaArchivo').jtable({
            tableId: 'reglaArchivoID',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: 'CaracterInicial ASC',
            selecting: true,
            actions: {
                listAction: action,
            },
            recordsLoaded: function (event, data) {
                //GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: fields
        });

        $('.jtable-main-container').css({ "width": "1420px" });
        $('#tblReglaArchivo').jtable('load', {regla : regla});
    }
})