//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
});
//Load Data function
function loadTable() {
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
            dataSrc: ""


        },
        columns: [
            { "data": "Cedula" },
            { "data": "Nombre" },
            { "data": "Primer_Apellido" },
            { "data": "Segundo_Apellido" },
            { "data": "Correo" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-danger' onclick= delete(" + row.Cedula + ")>" +
                        "<i class='	glyphicon glyphicon-trash'> </i>" +
                        "</button > "
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.Cedula + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > "
                }
            }

        ]
    });
}

function getbyID(Cedula) {

    $.ajax({
        url: "/Empleado/Consultar/" + Cedula,
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

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//function for deleting employee's record
function Delete(ID) {

    $.ajax({
        url: "/Empleado/Eliminar/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            loadTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//Cargar agregar div
function cargarAgregar() {
    //CARGAR_TIPO_CEDULA();
}


function CARGAR_TIPO_CEDULA() {
    $.ajax({
        url: "/Empleado/CARGAR_TIPO_CEDULA",
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
}

function clearTextBox() {
    $('#Cedula').val("");
    $('#Nombre').val("");
    $('#Primer_Apellido').val("");
    $('#Segundo_Apellido').val("");
    $('#Telefono').val("");
    $('#Correo').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#Cedula").prop("disabled", false);
    $('#Cedula').css('border-color', 'lightgrey');
    $('#Nombre').css('border-color', 'lightgrey');
    $('#Primer_Apellido').css('border-color', 'lightgrey');
    $('#Segundo_Apellido').css('border-color', 'lightgrey');
    $('#Correo').css('border-color', 'lightgrey');
}

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
    } else if (VALIDAR_EMAIL($('#Correo').val().trim())==false) {
        MENSAJE_WARNING("¡El correo posee un formato inválido, por favor revise los datos brindados!");
    } else {
        var CEDULA = $('#Cedula').val().trim();
        var COMBO = document.getElementById("tipo");
        var CODIGO = COMBO.options[COMBO.selectedIndex].text;
        var STR = CODIGO.substring(0, 1).toUpperCase();

        if (STR == "F" && CEDULA.length < 9) {
            MENSAJE_WARNING("¡Cédula inválida, el tipo de cédula seleccionado debe contener al menos 9 dígitos, actualmente contiene (" + CEDULA.length  +")!");
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
            url: "/Empleado/Agregar",
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

                                loadTable();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });

                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un empleado con la cédula: " + $('#Cedula').val() +" !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error al tratar de agregar esta categoría, intentelo más tarde!", "error");
                }

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarEmpleado


function ModificarEmpleado() {

    if (VALIDAR() == true) {

    }


    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    //var empObj = {
    //    Cedula: $('#Cedula').val(),
    //    Nombre: $('#Nombre').val(),
    //    Primer_Apellido: $('#Primer_Apellido').val(),
    //    Segundo_Apellido: $('#Segundo_Apellido').val(),
    //    Telefono: $('#Telefono').val(),
    //    Correo: $('#Correo').val(),
    //    Direccion: {
    //        Direccion: $('#Direccion').val(),
    //        canton: {
    //            ID_Canton: parseFloat($("#canton option:selected").val())
    //        }
    //    }
    //};
    //$.ajax({
    //    url: "/Empleado/Actualizar",
    //    data: JSON.stringify(empObj),
    //    type: "POST",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {
    //        loadTable();
    //        $('#myModal').modal('hide');

    //        clearTextBox();
    //    },
    //    error: function (errormessage) {
    //        alert(errormessage.responseText);
    //    }
    //});
}//FIN DE ModificarEmpleado