$(document).ready(function () {
    Cargar_grid();
});

function Cargar_grid() {
    var table = $('#DatoTipoTrabajo').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/TipoTrabajo/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: "",
            stripeClasses: []
        },
        columns: [
            { "data": "ID_TipoTrabajo" },
            { "data": "Descripcion" },
            { "data": "Estado" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarTipoTrabajo(" + row.ID_TipoTrabajo + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.ID_TipoTrabajo + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}//FIN DE Cargar_grid

function VALIDAR() {
    var ENTRAR = false;
    if ($('#Desc_Tipo').val().trim() == "") {
        MENSAJE_WARNING("¡Descripción inválida, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE VALIDAR

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

function cargarAgregar() {
    $("#ID_Tipo").val("")
    $("#Desc_Tipo").val("")
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}//FIN DE CARGAR_AGREGAR

function AgregarTipoTrabajo() {
    if (VALIDAR() == true) {
        var TipoTrabajoObj = {
            Descripcion: $('#Desc_Tipo').val()
        };

        $.ajax({
            url: "/TipoTrabajo/AgregarTipoTrabajo",
            data: JSON.stringify(TipoTrabajoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El tipo de trabajo fue agregado correctamente!",
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
                    MENSAJE_WARNING("¡Ya existe un tipo de trabajo con la descripción: " + $('#Desc_Tipo').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarEquipo

function ConsultarTipoTrabajo(ID) {
    $.ajax({
        url: "/TipoTrabajo/ConsultarTipoTrabajo/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#ID_Tipo').val(result.ID_TipoTrabajo);
            $('#Desc_Tipo').val(result.Descripcion);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}//FIN DE ConsultarEquipo

function ModificarTipoTrabajo() {

    if (VALIDAR() == true) {
        var TipoTrabajoObj = {
            ID_TipoTrabajo: $('#ID_Tipo').val(),
            Descripcion: $('#Desc_Tipo').val(),
        };

        $.ajax({
            url: "/TipoTrabajo/ModificarTipoTrabajo",
            data: JSON.stringify(TipoTrabajoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El tipo de trabajo fue modificado correctamente!",
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
                    MENSAJE_WARNING("¡Ya existe un tipo de trabajo con la descripción: " + $('#Desc_Tipo').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}////FIN DE ModificarTipoTrabajo


function P_ModificarEstado(ID) {
    swal({
        title: "¡Validación!",
        text: "¿Está seguro que desea modificar el estado del tipo de trabajo?",
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
        url: "/TipoTrabajo/ModificarEstado/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Modificado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡El estado del tipo de trabajo cambió correctamente!",
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

function clearTextBox() {
    $('#ID_Tipo').val("");
    $('#Desc_Tipo').val("");
}//FIN DE clearTextBox
