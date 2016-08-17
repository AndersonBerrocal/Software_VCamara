$(document).ready(function () {
    $('#aspnetForm').delegate('#ctl00_ContentPlaceHolder1_ddl_reasegurador_r', 'change', function (e) {
        $('#ctl00_ContentPlaceHolder1_txt_codreasegurador_r').val($(this).val());
    });
    $('.tabBody').delegate('#ctl00_ContentPlaceHolder1_ddl_contrato_r', 'change', function (e) {
        var idcontdet = $("#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c").val();
        if (idcontdet == 0) {
            jTableRequestDetalle($(this).val(), 5);
        } else {
            jTableRequestDetalle($(this).val(), 5);
        }
    });
    var contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val();
    if (contrato != 0) {
        jTableRequestDetalle(contrato, 5);
    }
    //funcion de jtable
    function jTableRequestDetalle(nro_contrato, pg) {
        $('#tblReasegurador').jtable({
            tableId: 'Reaseguradores',
            paging: true,
            sorting: true,
            pageSize: 5,
            defaultSorting: '_estado ASC',
            selecting: true,
            saveUserPreferences: true,
            actions: {
                listAction: '/WebPage/Mantenimiento/frmGeneral.aspx/ContratoDetalleList',
            },
            fields: {
                _ide_Contrato_Det: { key: true, list: false },
                _nro_Contrato: { title: 'N° Contrato' },
                _ide_Reasegurador: { title: 'Código Reasegurador' },
                _cod_Reasegurador: { list: false },
                _cal_Crediticia: { title: 'Calif. Crediticia' },
                _cod_Empresa_Califica: { title: 'Emp. Calificadora' },
                _mod_Contrato: { title: 'Modalidad Contrato' },
                _prc_Retencion: { title: '% Retención' },
                _prc_Cesion: { title: '% Cesión' },
                _prc_participacion_rea:{title :'Participación Rea. (%)'},
                _nombre_Rea: { title: 'Nombre' },
                _nro_Registro_Rea: { title: 'N° Registro' },
                _estado: { title: 'Estado' },
                _usu_reg: { title: 'Usu. Registro' },
                _fec_reg: { title: 'Fec. Registro', displayFormat: 'dd/mm/yy', type: 'date' },
            },
            selectionChanged: function () {
                var $selectedRows = $('#tblReasegurador').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#ctl00_ContentPlaceHolder1_txt_idContratoDetalle_c').val(record._ide_Contrato_Det);

                        $('#ctl00_ContentPlaceHolder1_txt_codreasegurador_r').val(record._ide_Reasegurador);
                        $("#ctl00_ContentPlaceHolder1_txt_retencion_r").val(record._prc_Retencion);
                        $("#ctl00_ContentPlaceHolder1_txt_cesion_r").val(record._prc_Cesion);
                        $('#ctl00_ContentPlaceHolder1_txt_tasariesgo_r').val(record._prc_Tasa_Riesgo);
                        $('#ctl00_ContentPlaceHolder1_txt_tasareaseguro_r').val(record._prc_Tasa_Reaseguro);
                        $("#ctl00_ContentPlaceHolder1_txt_participacion_cesion").val(record._prc_participacion_rea);
                        $("#ctl00_ContentPlaceHolder1_txt_nombre_rea").val(record._nombre_Rea);
                        $("#ctl00_ContentPlaceHolder1_txt_nro_registro_rea").val(record._nro_Registro_Rea);
                        $('#ctl00_ContentPlaceHolder1_ddl_contrato_r').val(record._nro_Contrato);
                        $('#ctl00_ContentPlaceHolder1_ddl_reasegurador_r').val(record._ide_Reasegurador);
                        $('#ctl00_ContentPlaceHolder1_ddl_calificadora_r').val(record._cod_Empresa_Califica);
                        $('#ctl00_ContentPlaceHolder1_ddl_crediticia_r').val(record._cal_Crediticia);
                        $("#ctl00_ContentPlaceHolder1_ddl_tipcont_det_r").val(record._mod_Contrato);
                        $('#ctl00_hdf_control').val("999");
                    });
                } else {
                    //No rows selected
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });

        $('#tblReasegurador.jtable-main-container').css({ "width": "2200px" });
        $('#tblReasegurador').jtable('load', { WhereBy: nro_contrato });
    }
});
