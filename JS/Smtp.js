function ConsultarSMTP() {

    $.ajax({
        url: "/Smtp/CargarSMTP/",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#correoSMTP').val(result.Correo);
            $('#contrasenaSMTP').val(result.Password);
            $('#servidorSMTP').val(result.ServidorSMTP);
            $('#puertoSMTP').val(result.Puerto);

            var SELECCIONADO = "";

            if (result.SSL == "S") {
                SELECCIONADO = "1";
            } else {
                SELECCIONADO = "2";
            }
            $('#sslSMTP').val(SELECCIONADO);
            document.getElementById("btnAgregarSMTP").disabled = true;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}//FIN DE ConsultarEmpleado

function AgregarSmtp() {
    if (VALIDAR() == true) {
        document.getElementById("btnAgregarSMTP").disabled = true;
        document.getElementById("btnProbarSMTP").disabled = true;
        var SmtpObj = {
            Correo: $("#correoSMTP").val(),
            Password: $('#contrasenaSMTP').val(),
            ServidorSMTP: $('#servidorSMTP').val(),
            Puerto: $('#puertoSMTP').val(),
            SSL: $("#sslSMTP option:selected").val(),
        };

        $.ajax({
            url: "/Smtp/AgregarSmtp",
            data: JSON.stringify(SmtpObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡La configuración de SMTP se agregó correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $('#myModalSmtp').modal('hide');
                            }
                        });
                } else if (result == "No Enviado") {
                    MENSAJE_WARNING("¡Las pruebas con esta configuración NO fueron exitosas, compruebe los datos brindados!");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }

                document.getElementById("btnAgregarSMTP").disabled = false;
                document.getElementById("btnProbarSMTP").disabled = false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarSmtp

function VALIDAR_SMTP() {
    var ENTRAR = false;

    if ($("#correoSMTP").val() =="") {
        MENSAJE_WARNING("¡El correo no puede ser nulo, por favor revise los datos brindados!");
    } else if ($("#contrasenaSMTP").val() == "") {   
        MENSAJE_WARNING("¡La contraseña no puede ser nula, por favor revise los datos brindados!");
    } else if ($("#servidorSMTP").val() == "") { 
        MENSAJE_WARNING("¡El servidor SMTP no puede ser nulo, por favor revise los datos brindados!");
    } else if ($('#puertoSMTP').val() == "") { 
        MENSAJE_WARNING("¡El puerto SMTP no puede ser nulo, por favor revise los datos brindados!");
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


function ProbarSmtp() {
    if (VALIDAR_SMTP() == true) {
        document.getElementById("btnProbarSMTP").disabled = true;
        var SmtpObj = {
            Correo: $("#correoSMTP").val(),
            Password: $('#contrasenaSMTP').val(),
            ServidorSMTP: $('#servidorSMTP').val(),
            Puerto: $('#puertoSMTP').val(),
            SSL: $("#sslSMTP option:selected").val(),
        };

        $.ajax({
            url: "/Smtp/ProbarSmtp",
            data: JSON.stringify(SmtpObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Enviado") {

                    swal({
                        title: "¡Acción realizada!",
                        text: "¡Se probó correctamente la configuración, puede proceder a guardar esta configuración!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                document.getElementById("btnAgregarSMTP").disabled = false;
                            }
                        });

                } else if (result == "No Enviado") {
                    MENSAJE_WARNING("¡Las pruebas con esta configuración NO fueron exitosas, compruebe los datos brindados!");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }

                document.getElementById("btnProbarSMTP").disabled = false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE ProbarSmtp