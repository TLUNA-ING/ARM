
$(document).ready(function () {
    CargarProvincia();
    CargarEmpleado();
    CargarTipoTrabajo();

    $("#template").change(function () {
        var e = document.getElementById("template");
        var id = e.options[e.selectedIndex].value;
    });
    IniciarlizarFechas();
});

function AgregarNuevaSolicitud() {
    if (VALIDAR() == true) {
        var base64 = $("#firma")[0].toDataURL();
        var empObj = {
            solicitudID: $('#IDSolicitud').val(),
            provinciaId: $('#Provincias option:selected').val(),
            clienteId: $('#Cliente option:selected').val(),
            empleadoCedula: $('#Empleados option:selected').val(),
            tipoTrabajoId: $('#tipoTrabajo option:selected').val(),
            departamentoId: $('#Departamento option:selected').val(),
            equipoId: $('#Equipo option:selected').val(),
            fechaReporte: $('#fechaReporte').val(),
            horaEntrada: $('#horaEntrada').val(),
            horaSalida: $('#horaSalida').val(),
            tipoHora: $('#tipoHora').val(),
            cantidadHoras: $('#cantidadHoras').val(),
            solicitudMotivo: $('#motivoVisita').val(),
            motivoDetalle: $('#motivoDetalle').val(),
            solicitudRepuestos: $('#solicitudRepuestos').val(),
            equipoDetenido: $("#equipoDetenido option:selected").val(),
            tiempoDetenido: $('#tiempoDetenido').val(),
            firmaCliente: base64,
            correoMQC: $('#correoMQC').val(),
            cedulaMQC: $('#cedulaMQC').val(),
            nombreMQC: $('#nombreMQC').val()
        };

        try {
            $.ajax({
                url: "/Solicitud/Agregar",
                data: JSON.stringify(empObj),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    MENSAJE_EXITO("¡Solicitud agregada con exito" + " !");
                    clearTextBox();
                    IniciarlizarFechas();
                },
                error: function (result) {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            });
        } catch (err) {
            swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
        }
    }
}//FIN FUNCION AGREGAR

function CargarTipoTrabajo() {
    $.ajax({
        url: "/Solicitud/CargarTipoTrabajo",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => { plantilla += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#tipoTrabajo").html(plantilla);

        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarTipoTrabajo

function CargarEmpleado() {
    $.ajax({
        url: "/Solicitud/CargarEmpleado",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => { plantilla += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Empleados").html(plantilla);
            CargarEmpleadoActual();
        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarEmpleado

function CargarProvincia() {
    $.ajax({
        url: "/Provincia/CargarDatos",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var plantilla = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => {
                plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
            });

            $("#Provincias").html(plantilla);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarProvincia

function CargarCliente() {
    LimpiarCombobox("Cliente");
    LimpiarCombobox("Departamento");
    LimpiarCombobox("Equipo");
    var ID_PROVINCIA = $('#Provincias option:selected').val();
    if (isNaN(ID_PROVINCIA) == false) {

        var ProvinciaObj = {
            ID_Provincia: ID_PROVINCIA
        };

        $.ajax({
            url: "/Solicitud/CargarCliente",
            data: JSON.stringify(ProvinciaObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                var plantilla = '<option>-- Seleccione una opción --</option>';
                result.forEach(valor => {
                    plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
                });

                $("#Cliente").html(plantilla);
            },
            error: function (errormessage) {
            }
        });
    }
}//FIN DE CargarCliente

function CargarDepartamentos() {
    LimpiarCombobox("Departamento");
    LimpiarCombobox("Equipo");
    var ID_CLIENTE = $('#Cliente option:selected').val();
    if (isNaN(ID_CLIENTE) == false) {

        var ClienteObj = {
            ID_Cliente: ID_CLIENTE
        };

        $.ajax({
            url: "/Solicitud/CargarDepartamento",
            data: JSON.stringify(ClienteObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                var plantilla = '<option>-- Seleccione una opción --</option>';
                result.forEach(valor => {
                    plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
                });

                $("#Departamento").html(plantilla);
            },
            error: function (errormessage) {
            }
        });
    } 
}//FIN DE CargarDepartamento

function CargarEquipos() {
    var ID_DEPARTAMENTO = $('#Departamento option:selected').val();
    if (isNaN(ID_DEPARTAMENTO) == false) {
        var DepartamentoObj = {
            ID_Departamento: ID_DEPARTAMENTO
        };

        $.ajax({
            url: "/Solicitud/CargarEquipo",
            data: JSON.stringify(DepartamentoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                var plantilla = '<option>-- Seleccione una opción --</option>';
                result.forEach(valor => {
                    plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
                });

                $("#Equipo").html(plantilla);
            },
            error: function (errormessage) {
            }
        });
    } else {
        LimpiarCombobox("Equipo");
    }
}//FIN DE CargarEquipos

function LimpiarCombobox(COMBO) {
    var select = document.getElementById(COMBO);
    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }
}//FIN DE LimpiarCombobox

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

function MENSAJE_EXITO(MENSAJE) {
    swal({
        
        title: "¡Acción realizada!",
        text: MENSAJE,
        type: "success",
        confirmButtonColor: "#10AF5D",
        confirmButtonText: "Aceptar"
                    
    });
}//FIN DE MENSAJE_WARNING

function VALIDAR() {
    var ENTRAR = false;
    var PROVINCIA = parseFloat($("#Provincias option:selected").val());
    var ID_CLIENTE = parseFloat($("#Cliente option:selected").val());
    var ID_DEPARTAMENTO = parseFloat($("#Departamento option:selected").val());
    var ID_EQUIPO = parseFloat($("#Equipo option:selected").val());
    var Fecha_Reporte = $('#fechaReporte').val();
    var horaEntrada = $('#horaEntrada').val();
    var horaSalida = $('#horaSalida').val();
    var ID_TIPO_H = $('#tipoHora').val();
    var ID_EMPLEADO = parseFloat($("#Empleados option:selected").val());
    var ID_TIPO_T = parseFloat($("#tipoTrabajo option:selected").val());
    var motivoVisita = $('#motivoVisita').val();
    var motivoDetalle = $('#motivoDetalle').val();

    var tiempoDetenido = $('#tiempoDetenido').val();

    if (tiempoDetenido=="") {
        document.getElementById("tiempoDetenido").value = "00:00";
    }

    if (isNaN(PROVINCIA) == true) {
        MENSAJE_WARNING("¡Provincia inválida, por favor revise los datos brindados!");
    } else if (isNaN(ID_CLIENTE) == true) {
        MENSAJE_WARNING("¡Cliente inválido, por favor revise los datos brindados!");
    } else if (isNaN(ID_DEPARTAMENTO) == true) {
        MENSAJE_WARNING("¡Departamento inválido, por favor revise los datos brindados!");
    } else if (isNaN(ID_EQUIPO) == true) {
        MENSAJE_WARNING("¡Equipo inválido, por favor revise los datos brindados!");
    } else if (Fecha_Reporte == "") {
        MENSAJE_WARNING("¡Fecha inválida, por favor revise los datos brindados!");
    } else if (horaEntrada == "") {
        MENSAJE_WARNING("¡Hora de entrada inválida, por favor revise los datos brindados!");
    } else if (horaSalida == "") {
        MENSAJE_WARNING("¡Hora de salida inválida, por favor revise los datos brindados!");
    } else if (horaEntrada > horaSalida) {
        MENSAJE_WARNING("¡La hora de entrada no puede ser mayor a la hora de salida, por favor revise los datos brindados!");
    } else if (ID_TIPO_H == "") {
        MENSAJE_WARNING("¡El tipo de hora es inválido, por favor revise los datos brindados!");
    } else if (isNaN(ID_EMPLEADO) == true) {
        MENSAJE_WARNING("¡Empleado inválido, por favor revise los datos brindados!");
    } else if (isNaN(ID_TIPO_T) == true) {
        MENSAJE_WARNING("¡Tipo trabajo inválido, por favor revise los datos brindados!");
    } else if (motivoVisita == "") {
        MENSAJE_WARNING("¡Descrpción de trabajo inválido, por favor revise los datos brindados!");
    } else if (motivoDetalle == "") {
        MENSAJE_WARNING("¡Motivo de detalle inválido, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }
    return ENTRAR;
}//FIN DE VALIDAR

function IniciarlizarFechas() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();
    var hora = today.getHours();
    var minuto = today.getMinutes();

    if (minuto.length==1) {
        minuto = "0" + minuto;
    }


    document.getElementById("fechaReporte").value = yyyy + "-" + mm + "-" + dd;
    document.getElementById("horaSalida").value = hora + ":" + minuto;
    document.getElementById("tipoHora").value = "Normal";
    document.getElementById("equipoDetenido").value = "0";
    document.getElementById("tiempoDetenido").value = "00:00";
    CargarEmpleadoActual();
}//FIN DE IniciarlizarFechas

function clearTextBox() {
    $('#horaEntrada').val("")
    document.getElementById("Provincias").selectedIndex = "0";
    LimpiarCombobox("Cliente");
    LimpiarCombobox("Departamento");
    LimpiarCombobox("Equipo");
    document.getElementById("tipoTrabajo").selectedIndex = "0";
    $('#motivoVisita').val("");
    $('#motivoDetalle').val("");
    $('#solicitudRepuestos').val("");
    $('#cedulaMQC').val("");
    $('#nombreMQC').val("");
    $('#correoMQC').val("");
    $('#firma').val("");
    IniciarlizarFechas();
}//FIN FUNCION DE LIMPAR CASILLAS


function CargarEmpleadoActual() {
    $.ajax({
        url: "/Solicitud/CargarEmpleadoActual",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Empleados').val(result);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarProvincia