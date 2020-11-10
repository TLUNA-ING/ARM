//Load Data in Table when documents is ready
$(document).ready(function () {
    CARGAR_GRID();
});
function CARGAR_GRID() {
    var table = $('#DatoEmpleado').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/Empleado/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: "",
            stripeClasses: []


        },
        columns: [
            { "data": "Cedula" },
            { "data": "Nombre" },
            { "data": "Primer_Apellido" },
            { "data": "Segundo_Apellido" },
            { "data": "Correo" },
            { "data": "Estado" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarEmpleado(" + row.Cedula + ") > " +
                            "<i class='material-icons'>create</i>"+
                                " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.Cedula + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}//FIN DE CARGAR_GRID


function AgregarEmpleado() {
    if (VALIDAR() == true) {

        var empObj = {
            TipoId: $("#tipo option:selected").val(),
            Cedula: $('#Cedula').val(),
            Nombre: $('#Nombre').val(),
            Primer_Apellido: $('#Primer_Apellido').val(),
            Segundo_Apellido: $('#Segundo_Apellido').val(),
            Correo: $('#Correo').val(),
        };

        $.ajax({
            url: "/Empleado/AgregarEmpleado",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {

                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El empleado fue agregado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CARGAR_GRID();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });

                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un empleado con la cédula: " + $('#Cedula').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarEmpleado

function ConsultarEmpleado(Cedula) {

    $.ajax({
        url: "/Empleado/ConsultarEmpleado/" + Cedula,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#tipo').val(result.TipoId);
            $('#Cedula').val(result.Cedula);
            $('#Nombre').val(result.Nombre);
            $('#Primer_Apellido').val(result.Primer_Apellido);
            $('#Segundo_Apellido').val(result.Segundo_Apellido);
            $('#Correo').val(result.Correo);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#Cedula").prop("disabled", true);
            $("#tipo").prop("disabled", true);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}//FIN DE ConsultarEmpleado

function ModificarEmpleado() {

    if (VALIDAR() == true) {

        var empObj = {
            TipoId: $("#tipo option:selected").val(),
            Cedula: $('#Cedula').val(),
            Nombre: $('#Nombre').val(),
            Primer_Apellido: $('#Primer_Apellido').val(),
            Segundo_Apellido: $('#Segundo_Apellido').val(),
            Correo: $('#Correo').val(),
        };

        $.ajax({
            url: "/Empleado/ModificarEmpleado",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El empleado fue modificado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CARGAR_GRID();
                                $('#myModal').modal('hide');
                                clearTextBox();
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
    }
}//FIN DE ModificarEmpleado

function P_ModificarEstado(ID) {

    swal({
        title: "¡Validación!",
        text: "¿Está seguro que desea modificar el estado del empleado?",
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
        url: "/Empleado/ModificarEstado/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Modificado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡El estado del empleado cambió correctamente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            CARGAR_GRID();
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

function cargarAgregar() {
    clearTextBox();
    //CargarTipoCedula();
}//FIN DE cargarAgregar

function CargarTipoCedula() {
    $.ajax({
        url: "/Empleado/CargarTipoCedula",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '';
            result.forEach(valor => {
                plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
            });

            $("#tipo").html(plantilla);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarTipoCedula

function clearTextBox() {
    document.getElementById("tipo").selectedIndex = "0";

    $('#Cedula').val("");
    $('#Nombre').val("");
    $('#Primer_Apellido').val("");
    $('#Segundo_Apellido').val("");
    $('#Telefono').val("");
    $('#Correo').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#Cedula").prop("disabled", false);
    $("#tipo").prop("disabled", false);
}//FIN DE clearTextBox

function VALIDAR() {
    var ENTRAR = false;
    var TIPO = parseFloat($("#tipo option:selected").val());

    if (TIPO == 0) {//No se ha seleccionado un tipo de cédula
        MENSAJE_WARNING("¡Debe seleccionar un tipo de cédula!");
    } else if ($('#Cedula').val().trim() == "") {
        MENSAJE_WARNING("¡Cédula inválida, por favor revise los datos brindados!");
    } else if ($('#Nombre').val().trim() == "") {
        MENSAJE_WARNING("¡Nombre inválido, por favor revise los datos brindados!");
    } else if ($('#Primer_Apellido').val().trim() == "") {
        MENSAJE_WARNING("¡Primer apellido inválido, por favor revise los datos brindados!");
    } else if ($('#Segundo_Apellido').val().trim() == "") {
        MENSAJE_WARNING("¡Segundo apellido inválido, por favor revise los datos brindados!");
    } else if ($('#Correo').val().trim() == "") {
        MENSAJE_WARNING("¡Correo inválido, por favor revise los datos brindados!");
    } else if (VALIDAR_EMAIL($('#Correo').val().trim()) == false) {
        MENSAJE_WARNING("¡El correo posee un formato inválido, por favor revise los datos brindados!");
    } else {
        var CEDULA = $('#Cedula').val().trim();
        var COMBO = document.getElementById("tipo");
        var CODIGO = COMBO.options[COMBO.selectedIndex].text;
        var STR = CODIGO.substring(0, 1).toUpperCase();

        if (STR == "F" && CEDULA.length < 9) {
            MENSAJE_WARNING("¡Cédula inválida, el tipo de cédula seleccionado debe contener al menos 9 dígitos, actualmente contiene (" + CEDULA.length + ")!");
        } else if (STR == "J" && CEDULA.length < 10) {
            MENSAJE_WARNING("¡Cédula inválida, el tipo de cédula seleccionado debe contener al menos 10 dígitos, actualmente contiene (" + CEDULA.length + ")!");
        } else if (STR == "N" && CEDULA.length < 10) {
            MENSAJE_WARNING("¡Cédula inválida, el tipo de cédula seleccionado debe contener al menos 10 dígitos, actualmente contiene (" + CEDULA.length + ")!");
        } else if (STR == "D" && CEDULA.length < 12) {
            MENSAJE_WARNING("¡Cédula inválida, el tipo de cédula seleccionado debe contener al menos 12 dígitos, actualmente contiene (" + CEDULA.length + ")!");
        } else {
            ENTRAR = true;
        }
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

function CAMBIO_CMB() {
    var TIPO = parseFloat($("#tipo option:selected").val());

    if (TIPO == 1) { //Física
        $("#Cedula").attr('maxlength', '9');
    } else if (TIPO == 2) {//Jurídica
        $("#Cedula").attr('maxlength', '10');
    } else if (TIPO == 3) {//Nite
        $("#Cedula").attr('maxlength', '10');
    } else if (TIPO == 4) {//Dimex
        $("#Cedula").attr('maxlength', '12');
    }
    $("#Cedula").val('')
    document.getElementById("Cedula").focus();
}//FIN DE CAMBIO_CMB

$("#Cedula").keyup(function (event) {
    if (event.keyCode == 13) {
        document.getElementById("Nombre").focus();
    }
});

$("#Nombre").keyup(function (event) {
    if (event.keyCode == 13) {
        document.getElementById("Primer_Apellido").focus();
    }
});

$("#Primer_Apellido").keyup(function (event) {
    if (event.keyCode == 13) {
        document.getElementById("Segundo_Apellido").focus();
    }
});

$("#Segundo_Apellido").keyup(function (event) {
    if (event.keyCode == 13) {
        document.getElementById("Correo").focus();
    }
});

$("#Correo").keyup(function (event) {
    if (event.keyCode == 13) {

        if ($("#btnAdd").is(":hidden")) {
            $("#btnUpdate").click()
        } else {
            $("#btnAdd").click()
        }
    }
});

$(document).ready(function () {
    $("#Cedula").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});