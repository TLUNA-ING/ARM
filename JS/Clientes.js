$(document).ready(function () {
    Cargar_grid();
    ConsultarProvincias();
});

function Cargar_grid() {
    var table = $('#DatoCliente').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/Cliente/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
        },
        columns: [
            { "data": "ID_Cliente" },
            { "data": "Nombre" },
            { "data": "Correo" },
            { "data": "Provincia.Descripcion" },
            { "data": "Estado" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarCliente(" + row.ID_Cliente + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.ID_Cliente + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}//FIN DE Cargar_grid

function VALIDAR() {
    var ENTRAR = false;

    if ($('#nombre_cliente').val().trim() == "") {
        MENSAJE_WARNING("¡Nombre inválido, por favor revise los datos brindados!");
    } else if ($('#correo_cliente').val().trim() == "") {
        MENSAJE_WARNING("¡Correo inválido, por favor revise los datos brindados!");
    } else if (VALIDAR_EMAIL($('#correo_cliente').val().trim()) == false) {
        MENSAJE_WARNING("¡El correo posee un formato inválido, por favor revise los datos brindados!");
    } else if (isNaN($("#id_provincia option:selected").val())) {
        MENSAJE_WARNING("¡Provincia inválida, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE VALIDAR

function VALIDAR_EMAIL(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}//FIN DE VALIDAR_EMAIL

function MENSAJE_WARNING(MENSAJE) {
    swal({
        title: "¡No se pudo procesar!",
        text: MENSAJE,
        type: "info",
        showCancelButton: false,
        confirmButtonText: "¡ Entendido !",
        confirmButtonColor: '#24a0ed',
        closeOnConfirm: true

    });
}//FIN DE MENSAJE_WARNING

function clearTextBox() {
    document.getElementById("id_provincia").selectedIndex = "0";
    $("#id_cliente").val("")
    $("#nombre_cliente").val("")
    $("#correo_cliente").val("")
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}//FIN DE clearTextBox

function cargarAgregar() {
    clearTextBox();
}//FIN DE CARGAR_AGREGAR

function ConsultarProvincias() {
    $.ajax({
        url: "/Cliente/ConsultarProvincias",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '';
            result.forEach(valor => {
                plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
            });

            $("#id_provincia").html(plantilla);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE ConsultarPronvincias

function AgregarCliente() {
    if (VALIDAR() == true) {
        var cliObj = {
            ID_Cliente: $('#id_cliente').val(),
            Nombre: $('#nombre_cliente').val(),
            Correo: $('#correo_cliente').val(),
            Provincia: {
            ID_Provincia: parseFloat($("#id_provincia option:selected").val())
            },
        };

        $.ajax({
            url: "/Cliente/AgregarCliente",
            data: JSON.stringify(cliObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {

                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El cliente fue agregado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                Cargar_grid();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });

                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un cliente con el nombre: " + $('#nombre_cliente').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarCliente

function ConsultarCliente(ID) {
    $.ajax({
        url: "/Cliente/ConsultarCliente/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#id_cliente').val(result.ID_Cliente);       
            $('#nombre_cliente').val(result.Nombre);
            $('#correo_cliente').val(result.Correo);
            $('#id_provincia').val(result.Provincia.ID_Provincia);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}//FIN DE ConsultarEmpleado

function ModificarCliente() {
    if (VALIDAR() == true) {
        var cliObj = {
            ID_Cliente: $('#id_cliente').val(),
            Nombre: $('#nombre_cliente').val(),
            Correo: $('#correo_cliente').val(),
            Provincia: {
                ID_Provincia: parseFloat($("#id_provincia option:selected").val())
            },
        };

        $.ajax({
            url: "/Cliente/ModificarCliente",
            data: JSON.stringify(cliObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El cliente fue modificado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                Cargar_grid();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });
                } else if ("Existe") {
                    MENSAJE_WARNING("¡Ya existe un cliente con el nombre: " + $('#nombre_cliente').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE ModificarEmpleado

function P_ModificarEstado(ID) {
    swal({
        title: "¡Validación!",
        text: "¿Está seguro que desea modificar el estado del cliente?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#10AF5D",
        confirmButtonText: "¡Si!",
        cancelButtonText: "¡No!"
    },
        function (isConfirm) {
            if (isConfirm) {
                ModificarEstado(ID);
            }
        });
}//FIN DE PreguntaModificarEstado

function ModificarEstado(ID) {
    $.ajax({
        url: "/Cliente/ModificarEstado/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Modificado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡El estado del cliente cambió correctamente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            Cargar_grid();
                        }
                    });
            } else {
                swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}//FIN DE ModificarEstado