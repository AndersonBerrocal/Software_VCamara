$(document).ready(function () {
    const urlListTelebanking = "/WebPage/ModuloDIS/Operaciones/frmTelebankig.aspx/listTelebanking";
    const urlAprobarNomina = "/WebPage/ModuloDIS/Operaciones/frmTelebankig.aspx/aprobarTelebanking";
    const urlAprobarFinal = "/WebPage/ModuloDIS/Operaciones/frmTelebankig.aspx/aprobarFinalTelebanking";
    var nomina = function (ArchivoId) {
        this.ArchivoId = ArchivoId,
        this.IDE_CONTRATO = parseInt($("#ctl00_ContentPlaceHolder1_ddl_contrato").val()),
        this.Estado = $("#ctl00_ContentPlaceHolder1_ddl_estado").val()
    }

    $("body").delegate("#ctl00_ContentPlaceHolder1_btn_buscar", "click", function (ev) {
        ev.preventDefault();
        var fecha = $("#ctl00_ContentPlaceHolder1_txt_fecha").val();
        listApruebaCarga(new nomina(0), fecha);
    });
    $("body #tblTelebanking").delegate("#lnk_confirmar", "click", function () {
        var archivoId = {archivoId:parseInt($(this).attr('class'))};
        if (confirm("¿Está seguro de confirmar?")) {
            llamarAjax(archivoId, urlAprobarNomina).success(function (res) {
                if (res.d.Result == true) {
                    mostrarMensajeAlert("Se confirmó el pago provisión del archivo");
                    listApruebaCarga(new nomina(0), $("#ctl00_ContentPlaceHolder1_txt_fecha").val())
                } else
                    mostrarMensajeAlert(res.d.Result);
            });
        }

    });
    //estado pago
    $("body #tblTelebanking").delegate("#lnk_estadoPago", "click", function () {
        var archivoId = { archivoId: parseInt($(this).attr('class')) };
        if (confirm("¿Está seguro de confirmar ?")) {
            llamarAjax(archivoId, urlAprobarFinal).success(function (res) {
                if (res.d.Result == true) {
                    mostrarMensajeAlert("Se confirmó el pago banco correctamente");
                    listApruebaCarga(new nomina(0), $("#ctl00_ContentPlaceHolder1_txt_fecha").val())
                } else
                    mostrarMensajeAlert(res.d.Result);
            });
        }

    });
    var fields = {
        ArchivoId: {
            title: 'Detalle', sorting: false, display: function (data) {
                var $icon = $("<a href='#'>Detalle</a>");
                $icon.click(function () {
                    $("#tblTelebanking").jtable('openChildTable',
                        $icon.closest('tr'),
                        {
                            actions: { listAction: "/WebPage/ModuloDIS/Operaciones/frmTelebankig.aspx/listTelebankingByArchivoId" },
                            fields: {
                                RUC_BENE: { title: 'RUC_BENE' },
                                NOM_BENE: { title: 'NOM_BENE' },
                                TIP_CTA: { title: 'TIP_CTA' },
                                CTA_BENE: { title: 'CTA_BENE' },
                                Importe: { title: 'Importe' }
                            }
                        }, function (dataDetail) {
                            dataDetail.childTable.jtable('load', { ArchivoId: data.record.ArchivoId });
                        });
                });
                return $icon;
            }
        },
        NombreArchivo: { title: 'NombreArchivo' },
        FechaOperacion: {
            title: 'FechaOperación', display: function (data) {
                return ConvertNumberToDateTime(data.record.FechaOperacion);
            }
        },
        Moneda: { title: 'Moneda' },
        Importe: { title: 'Importe' },
        RutaNomina: {
            sorting: false,title: 'Descargar', display: function (data) {
                return "<a id='lnk_descarga' class='" + data.record.RutaNomina + "' href='" + data.record.RutaNomina + "'>Descargar</a>";
            }
        },
        Estado: {
            sorting: false,title: 'Provisión', display: function (data) {
                if(data.record.Estado == "A")
                    return "<a id='lnk_confirmar' class='" + data.record.ArchivoId + "' href='#'>Provisionar pago</a>";
                else
                    return "<a href='#'><span>Confirmado</span></a>";
            }
        },
        EstadoPago: {
            sorting: false,title: 'Pago_Banco', display: function (data) {
                if (data.record.EstadoPago == "C")
                    return "<a id='lnk_estadoPago' class='" + data.record.ArchivoId + "' href='#'>Pago a banco</a>";
                else
                    return "<a href='#'><span>Pagado</span></a>";
            }
        }
    }
    function listApruebaCarga(nomina,fecha) {
        $('#tblTelebanking').jtable({
            tableId: 'Telebanking',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: 'FechaOperacion ASC',
            selecting: false,
            openChildAsAccordion: true,
            actions: {
                listAction: urlListTelebanking,
            },
            recordsLoaded: function (event, data) {
                //GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: fields
        });

        $("#tblTelebanking").css({ "text-align": "center" });
        $('#tblTelebanking.jtable-main-container').css({ "width": "1500px" });
        $('#tblTelebanking').jtable('load', { nomina: nomina, fecha: fecha });
    }
})