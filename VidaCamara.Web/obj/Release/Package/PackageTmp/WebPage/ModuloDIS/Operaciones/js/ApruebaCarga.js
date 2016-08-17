$(document).ready(function () {
    const urlAprobar = "/WebPage/ModuloDIS/Operaciones/frmCargaAprobacion.aspx/setAprobar";
    const urlEliminar = "/WebPage/ModuloDIS/Operaciones/frmCargaAprobacion.aspx/setEliminar";
    var entityAprobar = function (linCabId,ArchivoId) {
        this.linCabId = linCabId,
        this.IdeContrato = parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val()),
        this.ArchivoId = ArchivoId
    }
    //eventos
    $("body #tblApruebaCarga").delegate("#link_aprobar", "click", function () {
        if (confirm("Esta seguro de aprobar este registro")) {
            //programar llamada ajax
            const controlAttr = $(this).attr('class').split(",");
            llamarAjax(new entityAprobar(parseInt(controlAttr[0]), parseInt(controlAttr[1])), urlAprobar).success(function (res) {
                if (res.d.Result == true) {
                    mostrarMensajeAlert("La información de pagos fue confirmada.");
                    consultarRegistros();
                }else
                    mostrarMensajeAlert(res.d.Result);
            });
        }
    });

    $("body #tblApruebaCarga").delegate("#link_eliminar", "click", function () {
        if (confirm("Esta seguro de descartar la información")) {
            //programar llamada ajax
            const controlAttr = $(this).attr('class').split(",");
            llamarAjax(new entityAprobar(parseInt(controlAttr[0]), parseInt(controlAttr[1])), urlEliminar).success(function (res) {
                console.log(res);
                if (res.d.Result == true) {
                    mostrarMensajeAlert("La información de pagos ha sido descartada, debera ser cargada nuevamente.");
                    consultarRegistros();
                } else
                    mostrarMensajeAlert(res.d.Result);
            });
        }
    });

    var contrato_sis = function () {
        this.IDE_CONTRATO = $("#ctl00_ContentPlaceHolder1_ddl_contrato").val()
    };
    //eventos
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        if (parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val()) == 0) {
            mostrarMensajeAlert("Seleccione el contrato");
        } else {
            consultarRegistros();
        }
    });
    function consultarRegistros() {
        var filters = [$("#ctl00_ContentPlaceHolder1_ddl_tipo_archivo").val(), $("#ctl00_ContentPlaceHolder1_txt_fecha_inicio").val(), $("#txt_fecha_fin").val(), ""];
        listApruebaCarga(new contrato_sis(), filters);
    }
    var action = "/WebPage/ModuloDIS/Operaciones/frmCargaAprobacion.aspx/listApruebaCarga";
    var fields = {
        IdHistoriaLinCab:{title:'Detalle',sorting:false,display:function(data){
            var $icon = $("<a href='#'>Detalle</a>");
            $icon.click(function(){
                console.log($icon,data);
                $("#tblApruebaCarga").jtable('openChildTable',
                    $icon.closest('tr'),
                    {
                        actions:{listAction:"/WebPage/ModuloDIS/Operaciones/frmCargaAprobacion.aspx/listApruebaCargaDetalle"},
                        fields:{
                            NombreArchivoNomina: { title: 'Nomina' },
                            NombreAseguradora: { title: 'AFP' },
                            TotalImporteNomina: { title: 'Monto Total' },
                            //PagoVcNomina: { title: 'Monto Pago_Vc' },
                        }
                    },function(dataDetail){
                        dataDetail.childTable.jtable('load', { IdLinCab: data.record.IdLinCab });
                    });
            });
            return $icon;
            }
        },
        NombreArchivo: { title: 'NombreArchivo' },
        FechaCarga: { title: 'FechaCarga', type: 'date', displayFormat: 'dd/mm/yy' },
        moneda: { title: '  Moneda' },
        TotalRegistros: { title: 'TotalRegistros' },
        TotalImporte: { title: 'TotalImporte' },
        //PagoVc: { title: 'PagoVc' },
        FechaInfo: { title: 'FechaInfo', type: 'date', displayFormat: 'dd/mm/yy' },
        UsuReg: { title: 'Usuario' },
        Aprobar: {
            sorting: false,title: 'Aprobar', align: 'center', display: function (data) {
                return "<a id='link_aprobar' class='" + data.record.IdLinCab + "," + data.record.IdArchivo + "' href='#'>Aprobar</a>";
            }
        },
        Eliminar: {
            sorting: false,title: 'Descartar', display: function (data) {
                return "<a id='link_eliminar' class='" + data.record.IdLinCab + "," + data.record.IdArchivo + "' href='#'>Descartar</a>";
            }
        }
    }
    function listApruebaCarga(contrato, filters) {
        $('#tblApruebaCarga').jtable({
            tableId: 'ApruebaCarga',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: 'NombreArchivo ASC',
            selecting: false,
            openChildAsAccordion: true,
            actions: {
                listAction: action,
            },
            recordsLoaded: function (event, data) {
                //GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: fields
        });

        $("#ApruebaCarga").css({ "text-align": "center" });
        $('#tblApruebaCarga.jtable-main-container').css({ "width": "1500px" });
        $('#tblApruebaCarga').jtable('load', { contrato: contrato, filtersP: filters });
    }
})