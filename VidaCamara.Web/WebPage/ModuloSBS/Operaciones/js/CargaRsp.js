$(document).ready(function () {
    var nro_contrato = $("#ctl00_ContentPlaceHolder1_ddl_contrato_r").val();
    var tablarspval = $("#CargaRSP").length;
    if (tablarspval == 1) {
        jatbleRequest(nro_contrato, "01", 16);
    }
    $(".tabBody").delegate("#ctl00_ContentPlaceHolder1_ddl_contrato_r", 'change', function () {
        jatbleRequest($(this).val(), "01", 16, "NO", 1);
    });

    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_exportar_error_rsp", "click", function () {
        var excelerror = $("#ctl00_ContentPlaceHolder1_hdf_excel_error").val();
        console.log(excelerror);
        if (excelerror == 0){
            $("#ctl00_ContentPlaceHolder1_hdf_excel_error").val("499");
            return true;
        }   
    });

    //funcio da cargada de jtable
    function jatbleRequest(nro_contrato, tipo_info, pg) {
        $('#tblRsp').jtable({
            paging: true,
            sorting: true,
            pageSize: pg,
            defaultSorting: 'NRO_CONTRATO ASC',
            actions: {
                listAction: '/WebPage/ModuloSBS/Operaciones/frmCargaDatos.aspx/GetSelectRSP',
            },
            recordsLoaded: function (event, data) {
                var totalFilas = data.serverResponse.TotalRecordCount;
                if (totalFilas > 0) {
                    $("#ctl00_ContentPlaceHolder1_lbl_estado_rsp").text("Leido y Guardado");
                    var estadoData = data.serverResponse.Records[0].ESTADO;
                    $("#ctl00_ContentPlaceHolder1_hdf_estado_borrar").val(estadoData);
                    $("#ctl00_ContentPlaceHolder1_hdf_tabla_id").val("RSP");
                }
                else if (totalFilas == 0) { $("#ctl00_ContentPlaceHolder1_lbl_estado_rsp").text("*"); }
                $("#ctl00_ContentPlaceHolder1_lbl_total_rsp").text(totalFilas);
            },
            fields: {
                NRO_CONTRATO: { title: 'N° Contrato'},
                ANIO_VIGENTE: { title: 'Año'},
                MES_VIGENTE: { title: 'Mes Vigente'},
                COD_ASEGURADO: { title: 'Asegurado'},
                CUSPP: { title: 'Cuspp'},
                TI_MOV: { title: 'Ti_Mov'},
                TI_REG: { title: 'Ti_Reg'},
                TIP_MON: { title: 'Tip_Mon'},
                FE_DEV: { title: 'Fe_Dev'},
                FE_SIN: { title: '&nbsp;&nbsp;Fe_Sin&nbsp;&nbsp;&nbsp;'},
                MTO_PAGO_S: { title: 'Mto_Pago'},
                ID_AFP: { title: '&nbsp;Id_Afp&nbsp;'},
                FE_SECI: { title: '&nbsp;Fe_Seci&nbsp;&nbsp;&nbsp;&nbsp;' },
                ESTADO:{title:'Estado'},
                FEC_REG: { title: '&nbsp;Fec_Reg&nbsp;', width: '10%', type: 'date', displayFormat: 'yy-mm-dd' },
                USU_REG: { title: 'Usu_Reg'},
            }
        });
        $('#tblRsp .jtable-main-container').css({ "width": "1600px" });
        var table = $('#tblRsp').jtable('load', { nro_contrato: nro_contrato, tipo: tipo_info});
    }

});
