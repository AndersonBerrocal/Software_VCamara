var descripcion_contrato = "Contrato SYS ";
$(document).ready(function () {
    var tablacontrato = $("#tblContratoViewSyS").length;
    limpiarContrato();
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnNuevo", "click", function (ev) {
        ev.preventDefault();
        if (tablacontrato == 1) {
            limpiarContrato();
        } 
    });
    $('#tblContratoViewSyS').jtable({
            tableId: 'Contratos_SYS',
            paging: true,
            sorting: true,
            pageSize: 5,
            defaultSorting: '_estado ASC',
            selecting: true,
            saveUserPreferences: true,
            actions: {
                listAction: '/WebPage/Mantenimiento/frmGeneral.aspx/ContratoSysList',
            },
            fields: {
                _ide_Contrato: { key: true, list: false },
                _nro_Contrato: { title: 'N°_Contrato' },
                _cla_Contrato: { title: 'Clase_de_Contrato', list: false },
                _nro_empresa: { title: 'Nro_empresas ' },
                _fec_Ini_Vig: { title: 'Inicio_Vigencia', displayFormat: 'dd/mm/yy', type: 'date'},
                _fec_Fin_Vig: { title: 'Fin_Vigencia', displayFormat: 'dd/mm/yy', type: 'date' },
                _des_Contrato: { title: 'Descripción_Contrato' },
                _centro_costo:{ title:'Centro Costo'},
                _estado: { title: 'Estado' },
                _fec_reg: { title: 'Fecha_Registro', displayFormat: 'dd/mm/yy', type: 'date' },
                _usu_reg: { title: 'Usuario_Registro' }
            },
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#tblContratoViewSyS').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                             
                        $('#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis').val(record._ide_Contrato);
                        $('#ctl00_ContentPlaceHolder1_txt_nrocont_sys').val(record._nro_Contrato);
                        $('#ctl00_ContentPlaceHolder1_ddl_clase_contrato_sys').val(record._cla_Contrato);
                        $('#ctl00_ContentPlaceHolder1_txtFechaInicio_sys').val(ConvertNumberToDate(record._fec_Ini_Vig));
                        $('#ctl00_ContentPlaceHolder1_txtFechaFin_sys').val(ConvertNumberToDate(record._fec_Fin_Vig));
                        $("#ctl00_ContentPlaceHolder1_txtdescripcion_sys").val(record._des_Contrato);
                        $("#ctl00_ContentPlaceHolder1_ddl_estado_sys").val(record._estado);
                        $("#ctl00_ContentPlaceHolder1_txt_numero_empresa").val(record._nro_empresa);
                        $("#ctl00_ContentPlaceHolder1_txt_centro_costo_sys").val(record._centro_costo);
                        $('#ctl00_hdf_control').val(999);

                    });
                } else {
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });
    $('#tblContratoViewSyS.jtable-main-container').css({ "width": "4800px" });
    $('#tblContratoViewSyS').jtable('load', { WhereBy: "NO" });
    //asignar valor Inicial de (0) a los textbox
    //FUNCION PARA LIMPIAR EL FORMULARIO CONTRATO
    function limpiarContrato() {
        $("#ctl00_ContentPlaceHolder1_txtdescripcion_sys").val("");
        $("#ctl00_ContentPlaceHolder1_txtFechaInicio_sys").val("");
        $("#ctl00_ContentPlaceHolder1_txt_nrocont_sys").val("");
        $("#ctl00_ContentPlaceHolder1_txtFechaFin_sys").val("");
        $("#ctl00_ContentPlaceHolder1_txt_ide_contrato_sis").val("0");
        $("#ctl00_ContentPlaceHolder1_ddl_estado_sys").val("C");
        $("#ctl00_ContentPlaceHolder1_ddl_clase_contrato_sys").val("0");
        $("#ctl00_ContentPlaceHolder1_txt_numero_empresa").val("");
        $("#ctl00_ContentPlaceHolder1_txt_centro_costo_sys").val("");
    }
});
