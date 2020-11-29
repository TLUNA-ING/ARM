
$(document).ready(function () {
    $("#usuario").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});//FIN DE validar solo números

function VALIDAR() {
    var ENTRAR = false;

    if ($('#usuario').val().trim() == "") {
        MENSAJE_WARNING("¡El usuario no puede ser nulo, por favor revise los datos brindados!");
    } else if ($('#contrasena').val().trim() == "") {
        MENSAJE_WARNING("¡La contraseña no puede ser nula, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE VALIDAR

function MENSAJE_WARNING(MENSAJE) {
    swal({
        title: "¡No se pudo iniciar sesión!",
        text: MENSAJE,
        type: "info",
        showCancelButton: false,
        confirmButtonText: "¡ Entendido !",
        confirmButtonColor: '#24a0ed',
        closeOnConfirm: true

    });
}//FIN DE MENSAJE_WARNING

function MENSAJE_WARNING_(MENSAJE) {
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

$("#validar").click(function () {
    if (VALIDAR() == true) {
        document.getElementById("validar").disabled = true;
        var usrObj = {
            Empleado: { Cedula: $('#usuario').val().trim() },
            Password: $('#contrasena').val().trim()
        };

        $.ajax({
            url: "/Acceso/IniciarSesion",
            data: JSON.stringify(usrObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Encontrado") {
                    window.location.href = "/Acceso/Index";
                } else if (result == "No encontrado") {
                    swal({
                        title: "¡No se pudo iniciar sesión!",
                        text: "¡Los credenciales brindados son inválidos!",
                        type: "error",
                        showCancelButton: false,
                        confirmButtonText: "¡ Entendido !",
                        confirmButtonColor: '#24a0ed',
                        closeOnConfirm: true
                    });
                    document.getElementById("validar").disabled = false;
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                    document.getElementById("validar").disabled = false;
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
});//FIN DE IniciarSesion

$("#contrasena").keyup(function (event) {
    if (event.keyCode == 13) {
        $("#validar").click()
    }
});

function ConsultarUsuarioAcceso(ID) {
    $.ajax({
        url: "/Usuario/ConsultarUsuario/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#usuario").val(result.Empleado.Cedula);
            var NombreCompleto = result.Empleado.Nombre + ' ' + result.Empleado.Primer_Apellido + ' ' + result.Empleado.Segundo_Apellido
            $("#nombre").val(NombreCompleto);
            $("#correo").val(result.Empleado.Correo);
            $("#actual_contrasena").focus();


            $("#actual_contrasena").val("");
            $("#nueva_contrasena").val("");
            $("#confirmar_contrasena").val("");

            document.getElementById("btnCambiar").disabled = true;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}//FIN DE ConsultarUsuarioAcceso

function ValidarPasswords() {
    var nueva_contrasena = $('#nueva_contrasena').val();
    var confirmar_contrasena = $('#confirmar_contrasena').val();

    if (nueva_contrasena != confirmar_contrasena) {
        document.getElementById("mensaje").innerHTML = "Las contraseñas no coinciden."; 
        document.getElementById('mensaje').style.color = "red";
        document.getElementById("btnCambiar").disabled = true;
    } else {
        document.getElementById("mensaje").innerHTML = ""; 

        if (nueva_contrasena !="") {
            document.getElementById("btnCambiar").disabled = false;
        }
    }
}//FIN DE ValidarPasswords


function VerificarPasswords() {
    var ENTRAR = false;
    if ($('#actual_contrasena').val().trim() == "") {
        MENSAJE_WARNING_("¡La contraseña actual no puede ser nula, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE ValidarPasswords


function ModificarContrasena() {
    if (VerificarPasswords()==true) {
        var usrObj = {
            Empleado: { Cedula: $('#usuario').val().trim() },
            Password: $('#nueva_contrasena').val().trim(),
            PasswordActual: $('#actual_contrasena').val().trim()
        };

        $.ajax({
            url: "/Acceso/CambiarPassword",
            data: JSON.stringify(usrObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡Se modificó correctamente la contraseña!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $('#myModalPerfil').modal('hide');
                            }
                        });
                } else if (result == "Misma contraseña") {
                    MENSAJE_WARNING_("¡No se pudo modificar la contraseña, debe ingresar una distinta a la actual!");

                } else if (result == "Contraseña inválida") {
                    MENSAJE_WARNING_("¡La contraseña actual no coincide, por favor revise los datos brindados!");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                    $('#myModalPerfil').modal('hide');
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE ConsultarUsuarioAcceso


function RecuperarPassword() {
    var USUARIO = $('#usuario').val().trim();
    if (USUARIO != "") {
        $('#myModalRecuperar').modal('show');
        $('#uno').val("");
        $('#dos').val("");
        $('#tres').val("");
        $('#cuatro').val("");
    } else {
        MENSAJE_WARNING_("¡El usuario no puede ser nulo, por favor revise los datos brindados!");
    }
}//FIN DE ValidarPasswords

$(document).ready(function () {
    $("#uno").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        } else {
            $('#dos').focus();
        }
    });

    $("#dos").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        } else {
            $('#tres').focus();
        }
    });

    $("#tres").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        } else {
            $('#cuatro').focus();
        }
    });

    $("#cuatro").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});