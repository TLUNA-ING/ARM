//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
});
//Load Data function
function loadTable() {
    var table = $('#DatoUsuario').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/Usuario/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: "",
            stripeClasses: []
        },
        columns: [
            { "data": "Empleado.Cedula" },
            { "data": "Password" },
            { "data": "Rol.Rol" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarEmpleado(" + row.Empleado.Cedula + ") > " +
                            "<i class='material-icons'>create</i>"+
                                " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.Empleado.Cedula + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}


//Function for getting the Data Based upon Employee ID
function getbyID(IDUsuario) {
    $.ajax({
        url: "/Usuario/consultar/" + IDUsuario,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result.Rol.ID_Rol);
            $('#Usuario').val(result.Empleado.Cedula);
            $('#Contraseña').val(result.Password);
            $("#Rol option[value='" + result.Rol.ID_Rol + "']").attr("selected", true);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#Usuario").prop("disabled", true);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var usrObj = {
        Cedula: $('#Usuario').val(),
        Password: $('#Contraseña').val(),
        Rol: {
            ID_Rol: parseFloat($("#Rol option:selected").val())
        }
    };
    $.ajax({
        url: "/Usuario/Actualizar/",
        data: JSON.stringify(usrObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadTable();
            $('#myModal').modal('hide');

            clearTextBox();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//function for deleting employee's record
function Delete(ID) {
    console.log(ID);
    $.ajax({
        url: "/Usuario/Eliminar/" + ID + "",
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

function Add() {
    var CEDULA = parseFloat($("#fabricante option:selected").val());
    var CONTASENA = $('#Contraseña').val();
    var ROL = parseFloat($("#Rol option:selected").val());

    if (isNaN(CEDULA)) {
        swal("¡Cuidado!", "¡Debe seleccionar un empleado!", "warning");
    } else if (CONTASENA == "") {
        swal("¡Cuidado!", "¡Contraseña inválida!", "warning");
    } else if (isNaN(ROL)) {
        swal("¡Cuidado!", "¡Debe seleccionar un rol!", "warning");
    } else {
        var usrObj = {
            Empleado: {
                Cedula: CEDULA
            },
            Password: CONTASENA,
            Rol: {
                ID_Rol : ROL
            }
        };

        try {
            $.ajax({
                url: "/Usuario/Agregar",
                data: JSON.stringify(usrObj),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                    swal({
                        title: "¡Agregado!",
                        text: "¡El usuario fue ingresado correctamente!",
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





                },
                error: function (errormessage) {
                    alert("Verifique que la cédula ingresada como usuario si exista");
                }
            });
        } catch (err) { alert(err.responseText); }
    }
}

    function cargarAgregar() {
        CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO();
        CARGAR_COMBO_ROL();
        clearTextBox();
    }

    function CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO() {
        $.ajax({
            url: "/Usuario/CARGAR_EMPLEADOS",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                var plantilla = '';
                result.forEach(valor => {

                    plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
                });

                $("#empleados").html(plantilla);

            },
            error: function (errormessage) {
            }
        });
    }


    function CARGAR_COMBO_ROL() {
        $.ajax({
            url: "/Usuario/CARGAR_ROLES",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                var roles = '';

                result.forEach(valor => {roles += `<option value="${valor.Value}">${valor.Text}</option>`});
                $("#Rol").html(roles);

            },
            error: function (errormessage) {
            }
        });
    }

    function clearTextBox() {
        $('#Usuario').val("");
        $('#Contraseña').val("");
        $('#btnUpdate').hide();
        $('#btnAdd').show();
        $("#Usuario").prop("disabled", false);
        $('#Usuario').css('border-color', 'lightgrey');
        $('#Contraseña').css('border-color', 'lightgrey');
        $('#Rol').css('border-color', 'lightgrey');
    }


    // Validar datos
    function validate() {
        var isValid = true;
        if ($('#Usuario').val().trim() == "") {
            $('#Usuario').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Usuario').css('border-color', 'lightgrey');
        }
        if ($('#Contraseña').val().trim() == "") {
            $('#Contraseña').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Contraseña').css('border-color', 'lightgrey');
        }
        if ($('#Rol').val().trim() == "") {
            $('#Rol').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Rol').css('border-color', 'lightgrey');
        }
        return isValid;
    }
