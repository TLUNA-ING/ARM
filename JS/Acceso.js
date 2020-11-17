
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

function IniciarSesion() {
    if (VALIDAR() == true) {

        var usrObj = {
            Empleado: { Cedula: $('#usuario').val().trim()},
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
                        text:  "¡Los credenciales brindados son inválidos!",
                        type: "error",
                        showCancelButton: false,
                        confirmButtonText: "¡ Entendido !",
                        confirmButtonColor: '#24a0ed',
                        closeOnConfirm: true

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
}//FIN DE IniciarSesion