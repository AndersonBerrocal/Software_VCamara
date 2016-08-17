var listOperacion = [];
const changeControl = function (value, selected) {
    var data = { tipo: "12" };
    if (value == "13") {
        request(data, "/WebPage/Mantenimiento/frmTabla.aspx/GetConceptoByTipo").success(function (res) {
            listOperacion = res.d.Records;
            createControl(selected);
            document.getElementById("ctl00_ContentPlaceHolder1_txt_tipo_t").setAttribute("type", "hidden");
        });
    } else {
        document.getElementById("select_operacion").style.display = "none";
        var ctl00_ContentPlaceHolder1_txt_tipo_t = document.getElementById("ctl00_ContentPlaceHolder1_txt_tipo_t");
        ctl00_ContentPlaceHolder1_txt_tipo_t.setAttribute("type", "text");
        ctl00_ContentPlaceHolder1_txt_tipo_t.value = "0";
    };
}
const createControl = function (selected) {
    var options = "<option value='0'>---seleccione---</option>";
    for (var i in listOperacion) {
        options += '<option value=' + listOperacion[i]._codigo + '>' + listOperacion[i]._descripcion + '</option>';
    }
    var control = document.getElementById("select_operacion");
    control.style.display = "block";
    control.innerHTML = options;
    control.value = selected;
}
const request = function (data, url) {
    return $.ajax({
        url:url,
        method: 'POST',
        contentType: "application/json;",
        dataType: 'JSON',
        data: JSON.stringify(data),
    });
}
$(document).ready(function () {
    if (document.getElementById("ctl00_ContentPlaceHolder1_ddl_tabla_t").value == "13")
        changeControl("13", "0");
    $("#ctl00_ContentPlaceHolder1_ddl_estado_t").val("A");
    //validacion de informacion antes de enviar
    $("section").delegate("#ctl00_ContentPlaceHolder1_btn_enviar_t", "click", function (ev) {
        var tipo_tabla = $("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val();
        var codigo = $("#ctl00_ContentPlaceHolder1_txt_codigo_t").val();
        var descripcion = $("#ctl00_ContentPlaceHolder1_txt_descripcion_t").val();
        var id_tabla = $("#ctl00_ContentPlaceHolder1_txt_idtabla").val();
        if (id_tabla == 0) {
            if (tipo_tabla == 0) {
                MessageBox("Selecione Tipo Tabla"); return false;
            } else if (codigo == "") {
                MessageBox("Ingrese Codigo"); return false;
            } else if (descripcion == "") {
                MessageBox("Escriba una Descripción"); return false;
            } else {
                return confirm("¿ Está Seguro de crear el Registro ?");
            }
        } else {
            return confirm("¿ Está Seguro de Actualizar el Registro ?");
        }
    });
    $('#ctl00_ContentPlaceHolder1_btn_nuevo_t').on('click', function () {
        $('#ctl00_hdf_control').val("210");
    });
    $('#ctl00_hdf_control').val("210");
    var clase = 0;
    $('#frmTabla').delegate('#ctl00_ContentPlaceHolder1_ddl_tabla_t', 'change', function (e) {
        clase = 1;
        jTableRequest($(this).val(), "NULL");
        changeControl($(this).val(),"0");
        if ($(this).val() == "01") {
            $("label[for='txt_clase_t']").text('Pais :');
        } else {
            $("label[for='txt_clase_t']").text('Clase :');
        }
    });
    $("#frmTabla").delegate("#ctl00_ContentPlaceHolder1_txt_descripcion_t", "keyup", function (ev) {
        if(ev.keyCode != 13){
            if ($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val() == 0) {
                clase = 0;
                jTableRequest("9999", $(this).val());
            } else {
                clase = 1;
                jTableRequest($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val(), $(this).val());
            }
        }else{
            return false;
        }
    });
    var idtable = $("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val();
    if (idtable == 0) {
        jTableRequest("9999","NULL");
    } else {
        clase = 1;
        jTableRequest(idtable,"NULL");
    }

    function jTableRequest(tipo_tabla,descripcion) {
        $('#tblTablaView').jtable({
            tableId: 'tableViewID',
            paging: true,
            sorting: true,
            pageSize: 12,
            defaultSorting: '_codigo ASC',
            selecting: true,
            actions: {
                listAction: '/WebPage/Mantenimiento/frmTabla.aspx/ListConcepto',
            },
            recordsLoaded: function (event, data) {
                GetTipoTabla($("#ctl00_ContentPlaceHolder1_ddl_tabla_t").val());
            },
            fields: {
                _id_Concepto: { key: true, list: false },
                _tipo_Tabla: { title: 'Tipo de Tabla ', width: '10%' },
                _codigo: { title: 'Código', width: '8%' },
                _descripcion: { title: 'Descripción', width: '30%' },
                _valor: { title: 'Valor', width: '10%' },
                _clase: { title: 'Clase', width: '15%' },
                _tipo: { title: 'Tipo', width: '10%' },
                _estado: { title: 'Estado', width: '10%', }
            },
            selectionChanged: function () {
                var $selectedRows = $('#tblTablaView').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $('#ctl00_ContentPlaceHolder1_txt_descripcion_t').val(record._descripcion);
                        $('#ctl00_ContentPlaceHolder1_txt_codigo_t').val(record._codigo);
                        $('#ctl00_ContentPlaceHolder1_txt_clase_t').val(record._clase);
                        $('#ctl00_ContentPlaceHolder1_ddl_estado_t').val(record._estado);
                        if (clase == 0) {
                            $('#ctl00_ContentPlaceHolder1_ddl_tabla_t').val(record._codigo);
                        } else {
                            if(record._tipo_Tabla == "13")
                                createControl(record._tipo);
                            $('#ctl00_ContentPlaceHolder1_ddl_tabla_t').val(record._tipo_Tabla);
                        }
                        $('#ctl00_ContentPlaceHolder1_txt_valor_t').val(record._valor);
                        $('#ctl00_ContentPlaceHolder1_txt_tipo_t').val(record._tipo);
                        $('#ctl00_ContentPlaceHolder1_txt_idtabla').val(record._id_Concepto);
                        $('#ctl00_ContentPlaceHolder1_txt_9999').val(record._tipo_Tabla);
                        $('#ctl00_hdf_control').val("999");
                        if(parseInt($("#ctl00_ContentPlaceHolder1_txt_idtabla").val())>0)
                            $("#ctl00_ContentPlaceHolder1_btn_enviar_t").attr("disabled", false);
                    });
                } else {
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });

        $('.jtable-main-container').css({ "width": "1170px" });
        $('#tblTablaView').jtable('load', { TipoTabla: tipo_tabla, descripcion: descripcion });
    }
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
    var GetTipoTabla = function (codigo) {
        var data = { codigo: codigo };
        request(data, "/WebPage/Mantenimiento/frmTabla.aspx/GetConceptoByCodigo").success(function (data) {
            ControlarObjetos(data.d.Records);
        }).error(function (e) {
            console.log(e);
        });
    }
    var ControlarObjetos = function (tipo, active) {
        //if (parseInt(tipo[0]) == 2) {
        //    $("#ctl00_ContentPlaceHolder1_txt_codigo_t").attr("readonly", true).css({ "background": "rgba(0,10,10,0.1)" });
        //} else {
        //    $("#ctl00_ContentPlaceHolder1_txt_codigo_t").attr("readonly", false).css({ "background": "white" });
        //}
        //if (parseInt(tipo[1]) == 0) {
        //    $("#ctl00_ContentPlaceHolder1_btn_enviar_t").attr("disabled", true);
        //} else {
        //    $("#ctl00_ContentPlaceHolder1_btn_enviar_t").attr("disabled", false);
        //}
    }
    $("body").delegate("#select_operacion", "change", function (ev) {
        document.getElementById("ctl00_ContentPlaceHolder1_txt_tipo_t").value = $(this).val();
    });
});
