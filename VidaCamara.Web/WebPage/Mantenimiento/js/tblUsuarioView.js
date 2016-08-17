var ide_usuario;
$(document).ready(function () {
    GetUsuarioAcceso();
    //validcion de buton enviar
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnGuardar", "click", function (ev) {
        var tblUsuarioView = $("#tblUsuarioView").length;
        if (tblUsuarioView == 1) {
            var idusuario = $("#ctl00_ContentPlaceHolder1_txt_id_usuario").val();
            if (idusuario != 0) {
                return confirm("¿Esta Seguro de Actualizar el Usuario?");
            } else {
                return false;
            }
        } else {
            var $selectedRows = $('#frmAccesoPagina').jtable('selectedRows');
            var id_paginas = "";
            var contador = 1;
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    if (contador < $selectedRows.length)
                        id_paginas = id_paginas + record.ide_Pagina + ",";
                    else
                        id_paginas = id_paginas + record.ide_Pagina;
                    contador++;
                });
                $("#ctl00_ContentPlaceHolder1_lista_pagina").val(id_paginas);
                $("#ctl00_ContentPlaceHolder1_ide_usuario").val(ide_usuario);
                return confirm("Esta Seguro de Actualizar los Permisos");
        }

    });
    $("section").delegate("#ctl00_ContentPlaceHolder1_btnNuevo", "click", function (ev) {
        ev.preventDefault();
        $("input[type='text']").val("");
        $("input[type='hidden']").val("0");
        $("#ctl00_ContentPlaceHolder1_txt_fec_reg").text("");
        $("#ctl00_ContentPlaceHolder1_txt_fec_mod").text("");
    });
    //Prepare jtable plugin
    $('#tblUsuarioView').jtable({
        paging: true,
        sorting: true,
        pageSize: 10, //Actually this is not needed since default value is 10.
        defaultSorting: '_nombres ASC', //Optional. Default sorting on first load.
        selecting: true, //Enable selecting
        actions: {
            listAction: '/WebPage/Mantenimiento/frmUsuario.aspx/ListUsuario'
            //deleteAction: '/frmTabla.aspx/DeleteConcepto'
        },
        fields: {
            _ide_Usuario: { key: true ,list:false},
            _Usuario: { title: 'Usuario' },
            _nombres: {title: 'Nombres'},
            _cargo: { title: 'Cargo' },
            _email: { title: 'E-Mail' },
            _tipo_Usuario: {title: 'Tipo'},
            _fec_Reg: { title: 'Fecha Creación', displayFormat: 'dd/mm/yy', type: 'date' },
            _fec_Mod: { title: 'Fecha Modificación', displayFormat: 'dd/mm/yy', type: 'date' },
            _estado: { title: 'Estado'},
         },
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $('#tblUsuarioView').jtable('selectedRows');

            $('#SelectedRowList').empty();
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $("#ctl00_ContentPlaceHolder1_txt_id_usuario").val(record._ide_Usuario);
                    $("#ctl00_ContentPlaceHolder1_txt_usuario").val(record._Usuario);
                    $("#ctl00_ContentPlaceHolder1_txt_nombres").val(record._nombres);
                    $("#ctl00_ContentPlaceHolder1_txt_cargo").val(record._cargo);
                    $("#ctl00_ContentPlaceHolder1_txt_email").val(record._email);
                    $("#ctl00_ContentPlaceHolder1_txt_fec_reg").text(ConvertNumberToDate(record._fec_Reg));
                    $("#ctl00_ContentPlaceHolder1_txt_fec_mod").text(record._fec_Mod);
                    $("#ctl00_ContentPlaceHolder1_ddl_estado").val(record._estado);
                });
            } else {
                //No rows selected
                $('#SelectedRowList').append('No row selected! Select rows to see here...');
            }
        }
    });

    $('#tblUsuarioView .jtable-main-container').css({ "width": "1500px" });
    var table = $('#tblUsuarioView').jtable('load');

    //listar usuario solo par accesos
    function GetUsuarioAcceso() {
        $('#frmUsuario').jtable({
            paging: true,
            sorting: true,
            pageSize: 5, //Actually this is not needed since default value is 10.
            defaultSorting: '_nombres ASC', //Optional. Default sorting on first load.
            selecting: true, //Enable selecting
            actions: {
                listAction: '/WebPage/Mantenimiento/frmUsuario.aspx/ListUsuario'
            },
            fields: {
                _ide_Usuario: { key: true, list: false },
                _Usuario: { title: 'Usuario' },
                _nombres: { title: 'Nombres',width:'60%' }
            },
            selectionChanged: function () {
                var $selectedRows = $('#frmUsuario').jtable('selectedRows');

                $('#SelectedRowList').empty();
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        $("#frmAccesoPagina").css({"display":"block"});
                        GetListarPaginas(record._ide_Usuario);
                        ide_usuario = record._ide_Usuario;
                    });
                } else {
                    //No rows selected
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });

        var table = $('#frmUsuario').jtable('load');
    }
    //listar jtable de Paginas
    function GetListarPaginas(ide_usuario) {
        $('#frmAccesoPagina').jtable({
        paging: false,
        sorting: false,
        defaultSorting: false, //Optional. Default sorting on first load.
        selecting: true, //Enable selecting
        multiselect: true,
        selectingCheckboxes: true,
        actions: {
            listAction: '/WebPage/Mantenimiento/frmUsuario.aspx/ListAccesopagina'
        },
        fields: {
            ide_Pagina: { key: true, list: false },
            Descripcion :{ title: 'Opciones del Sistema',width:'70%' },
            permiso: { title: 'Permitido', type:'checkbox',values: { 'false': 'No', 'true': 'Si' }, defaultValue: 'false' },
         }
    });
        var table = $('#frmAccesoPagina').jtable('load', { ide_usuario:ide_usuario});
    }
    //funcion mesagw box
    function MessageBox(texto) {
        $("<div style='font-size:14px;text-align:center;'>" + texto + "</div>").dialog({ title: 'Alerta', modal: true, width: 400, height: 160, buttons: [{ id: 'aceptar', text: 'Aceptar', icons: { primary: 'ui-icon-circle-check' }, click: function () { $(this).dialog('close'); } }] })
    }
});