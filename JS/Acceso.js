
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
                        text: "¡Se modificó correctamente la contraseña, debe ingresar al sistema nuevamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                window.location.href = "/Home/Logout";
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
}//FIN DE ModificarContrasena

$(document).ready(function () {
    $("#uno").keypress(function (e) {
        $('#dos').focus();
    });

    $("#dos").keypress(function (e) {
       $('#tres').focus();
    });

    $("#tres").keypress(function (e) {
         $('#cuatro').focus();
    });

    $("#dos").keyup(function (e) {
        if (e.keyCode == 8) {
            $('#uno').focus();
        }
    });
    $("#tres").keyup(function (e) {
        if (e.keyCode == 8) {
            $('#dos').focus();
        }
    });

    $("#cuatro").keyup(function (e) {
        if (e.keyCode == 8) {
            $('#tres').focus();
        }
    });
    $("#myModalRecuperar").keyup(function (e) {
        if (e.keyCode == 13) {
            $("#btnValidarCodigo").click()
        }
    }); 
    $("#btnSalirCambiarPassword").click(function (e) {
        $('#myModalCambiarPassword').modal('hide');
    }); 
});


function RecuperarPassword() {
    var USUARIO = $('#usuario').val().trim();
    if (USUARIO != "") {
        document.getElementById("olvide").disabled = true;
        document.getElementById("validar").disabled = true;

        $.ajax({
            url: "/Acceso/UsuarioID/" + USUARIO,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result == "Encontrado") {
                    swal({
                        title: "¡Validación!",
                        text: "¿Desea enviar un código de recuperación al email configurado?",
                        type: "info",
                        showCancelButton: true,
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "¡Si!",
                        cancelButtonText: "¡No!"
                    },
                        function (isConfirm) {
                            if (isConfirm) {

                                $.ajax({
                                    url: "/Acceso/EnviarCorreoRecuperacion/" + USUARIO,
                                    type: "POST",
                                    contentType: "application/json;charset=UTF-8",
                                    dataType: "json",
                                    success: function (result) {

                                        document.getElementById("olvide").disabled = false;
                                        document.getElementById("validar").disabled = false;

                                        if (result == "Enviado") {
                                            swal({
                                                title: "¡Acción realizada!",
                                                text: "¡Se envió correctamente el código de recuperación al email configurado!",
                                                type: "success",
                                                confirmButtonColor: "#10AF5D",
                                                confirmButtonText: "Aceptar"
                                            },
                                                function (isConfirm) {
                                                    if (isConfirm) {
                                                        $('#myModalRecuperar').modal('show');
                                                        $('#uno').val("");
                                                        $('#dos').val("");
                                                        $('#tres').val("");
                                                        $('#cuatro').val("");
                                                    }
                                                });
                                        } else if (result == "No enviado") {
                                            MENSAJE_WARNING_("¡No se pudo enviar el código de recuperación, por favor intentelo más tarde o pongase en contacto con la empresa!");

                                        } else {
                                            swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                                        }
                                    },
                                    error: function (errormessage) {
                                        alert(errormessage.responseText);
                                    }
                                });

                            }
                        });
                } else if (result == "No encontrado") {
                    document.getElementById("olvide").disabled = false;
                    document.getElementById("validar").disabled = false;
                    MENSAJE_WARNING_("¡No fue posible encontrar el usuario, por favor revise los datos brindados!");
                } else {
                    document.getElementById("olvide").disabled = false;
                    document.getElementById("validar").disabled = false;
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                document.getElementById("olvide").disabled = false;
                document.getElementById("validar").disabled = false;
                alert(errormessage.responseText);
            }
        });

    } else {
        MENSAJE_WARNING_("¡El usuario no puede ser nulo, por favor revise los datos brindados!");
    }
}//FIN DE RecuperarPassword

function VerificarCodigoRecuperacion() {
    var uno    = $('#uno').val();
    var dos    = $('#dos').val();
    var tres   = $('#tres').val();
    var cuatro = $('#cuatro').val();

    if (uno == "") {
        $('#uno').focus();
        MENSAJE_WARNING_("¡Hacen falta datos, por favor verifique los datos brindados!")
    } else if (dos == "") {
        $('#dos').focus();
        MENSAJE_WARNING_("¡Hacen falta datos, por favor verifique los datos brindados!")
    } else if (tres == "") {
        $('#tres').focus();
        MENSAJE_WARNING_("¡Hacen falta datos, por favor verifique los datos brindados!")
    } else if (cuatro == "") {
        $('#cuatro').focus();
        MENSAJE_WARNING_("¡Hacen falta datos, por favor verifique los datos brindados!")
    } else {
        var CODIGO = uno + dos + tres + cuatro;
        var CEDULA = $('#usuario').val().trim();

        $.ajax({
            type: "POST",
            url: "/Acceso/VerificarCodigoRecuperacion ",
            data:{
                CODIGO: CODIGO,
                CEDULA: CEDULA
            },
            cache: false,
            success: function (result) {

                if (result == "Coincide") {
                    $('#nuevaContrasena').val("");
                    $('#ConfirmarnuevaContrasena').val();
                    $('#myModalCambiarPassword').modal('show');
                    $('#myModalRecuperar').modal('hide');
                    
                } else if (result == "No coincide") {
                    MENSAJE_WARNING_("¡El código no coincide con el código de recuperación enviado, verifique los datos brindados!");

                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE VerificarCodigo

function VerificarPasswordsRecuperacion() {
    var ENTRAR = false;
    if ($('#nuevaContrasena').val().trim() == "") {
        MENSAJE_WARNING_("¡La contraseña no puede ser nula, por favor revise los datos brindados!");
        $('#nuevaContrasena').focus();
    } else if ($('#ConfirmarnuevaContrasena').val().trim() == "") {
        MENSAJE_WARNING_("¡La confirmación de la contraseña no puede ser nula, por favor revise los datos brindados!");
        $('#ConfirmarnuevaContrasena').focus();
    } else if ($('#nuevaContrasena').val().trim() != $('#ConfirmarnuevaContrasena').val().trim()) {
        MENSAJE_WARNING_("¡Las contraseñas no coindicen, por favor revise los datos brindados!");
        $('#ConfirmarnuevaContrasena').focus();
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE VerificarPasswordsRecuperacion

function CambiarPassword() {
    if (VerificarPasswordsRecuperacion() == true) {

        $.ajax({
            type: "POST",
            url: "/Acceso/ModificarContrasenaRecuperacion",
            data: {
                CEDULA:   $('#usuario').val().trim(),
                PASSWORD: $('#nuevaContrasena').val().trim()
            },
            cache: false,
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
                                $('#myModalCambiarPassword').modal('hide');
                                window.location.href = "/Acceso/Index";
                            }
                        });
                } else {

                    swal({
                        title: "¡Error!",
                        text: "¡Ocurrió un error, intentelo más tarde!",
                        type: "error",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $('#myModalCambiarPassword').modal('hide');
                                window.location.href = "/Acceso/Index";
                            }
                        });
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE CambiarPassword