$(document).ready(function () {
    CargarGrid();
});

function CargarGrid() {
    var table = $('#DatoDepa').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
        scrollCollapse: true,
        ajax: {
            url: "/Departamento/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
        },
        columns: [
            { "data": "ID_Departamento" },
            { "data": "Descripcion" },
            { "data": "Estado" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarDepartamento(" + row.ID_Departamento + ") > " +
                            "<i class='material-icons'>create</i>"+
                                " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.ID_Departamento + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}//FIN DE CargarGrid

function cargarAgregar() {
    $("#Id_depa").val("")
    $("#Desc_Depa").val("")
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}//FIN DE CARGAR_AGREGAR

function VALIDAR() {
    var ENTRAR = false;
    if ($('#Desc_Depa').val().trim() == "") {
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

function AgregarDepartamento() {
    if (VALIDAR() == true) {
        var DepartamentoObj = {
            Descripcion: $('#Desc_Depa').val()
        };

        $.ajax({
            url: "/Departamento/AgregarDepartamento",
            data: JSON.stringify(DepartamentoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El departamento fue agregado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CargarGrid();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });
                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un departamento con la descripción: " + $('#Desc_Depa').val() + " !");
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

function ConsultarDepartamento(ID) {
    $.ajax({
        url: "/Departamento/ConsultarDepartamento/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#Id_depa').val(result.ID_Departamento);
            $('#Desc_Depa').val(result.Descripcion);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}//FIN DE ConsultarDepartamento

function ModificarDepartamento() {

    if (VALIDAR() == true) {
        var DepartamentoObj = {
            ID_Departamento: $('#Id_depa').val(),
            Descripcion: $('#Desc_Depa').val(),
        };

        $.ajax({
            url: "/Departamento/ModificarDepartamento",
            data: JSON.stringify(DepartamentoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El departamento fue modificado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CargarGrid();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });
                } else if ("Existe") {
                    MENSAJE_WARNING("¡Ya existe un departamento con la descripción: " + $('#Desc_Depa').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE ModificarDepartamento



function P_ModificarEstado(ID) {
    swal({
        title: "¡Validación!",
        text: "¿Está seguro que desea modificar el estado del departamento?",
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
        url: "/Departamento/ModificarEstado/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Modificado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡El estado del equipo cambió correctamente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            CargarGrid();
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
    $('#Id_depa').val("");
    $('#Desc_Depa').val("");
}//FIN DE clearTextBox
