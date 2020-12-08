
$(document).ready(function () {
    CargarProvincia();
    CargarEmpleado();
    CargarTipoTrabajo();

    $("#template").change(function () {
        var e = document.getElementById("template");
        var id = e.options[e.selectedIndex].value;
    });
});

function Add() {
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
                //loadTable();
                //$('#myModal').modal('hide');
                MENSAJE_EXITO("¡Solicitud agregada con exito" + " !");
                clearTextBox();
            },
            error: function (result) {
                //alert('Revise los datos agregados, ya que no pueden haber campos vacios');
                //swal("¡Error!", "¡Ocurrió un error, Revise los datos agregados, ya que no pueden haber campos vacios", "Error");
                MENSAJE_WARNING("¡Revise los datos agregados, ya que no pueden haber campos vacios "  + " !");
            }
        });
    } catch (err) { alert('Error'); }
 
}//FIN FUNCION AGREGAR

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        solicitudID: $('#IDSolicitud').val(),
        clienteId: $('#Cliente').val(),
        empleadoCedula: $('#Empleados').val(),
        tipoTrabajoId: $('#tipoTrabajo').val(),
        departamentoId: $('#Departamento').val(),
        equipoId: $('#Equipo').val(),
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
        correoMQC: $('#correoMQC').val(),
        cedulaMQC: $('#cedulaMQC').val(),
        nombreMQC: $('#nombreMQC').val()
    };
    $.ajax({
        url: "/Solicitud/Actualizar",
        data: JSON.stringify(empObj),
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
}//FIN FUNCION ACTUALIZAR

function clearTextBox() {
    $('#IDSolicitud').val("");
    $('#Provincias').val("");
    $('#Cliente').val("");
    $('#Empleado').val("");
    $('#tipoTrabajo').val("");
    $('#Departamento').val("");
    $('#Equipo').val("");
    $('#fechaReporte').val("");
    $('#horaEntrada').val("");
    $('#horaSalida').val("");
    $('#tipoHora').val("");
    $('#cantidadHoras').val("");
    $('#motivoVisita').val("");
    $('#motivoDetalle').val("");
    $('#solicitudRepuestos').val("");
    $('#equipoDetenido').val("");
    $('#tiempoDetenido').val("");
    $('#correoMQC').val("");
    $('#cedulaMQC').val("");
    $('#nombreMQC').val("");
    $('#firma').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#IDSolicitud').css('border-color', 'lightgrey');
    $('#Provincias').css('border-color', 'lightgrey');
    $('#Cliente').css('border-color', 'lightgrey');
    $('#Departamento').css('border-color', 'lightgrey');
    $('#Cliente').css('border-color', 'lightgrey');
    $('#Empleado').css('border-color', 'lightgrey');
    $('#tipoTrabajo').css('border-color', 'lightgrey');
    $('#Departamento').css('border-color', 'lightgrey');
    $('#Equipo').css('border-color', 'lightgrey');
    $('#fechaReporte').css('border-color', 'lightgrey');
    $('#horaEntrada').css('border-color', 'lightgrey');
    $('#horaSalida').css('border-color', 'lightgrey');
    $('#tipoHora').css('border-color', 'lightgrey');
    $('#cantidadHoras').css('border-color', 'lightgrey');
    $('#motivoVisita').css('border-color', 'lightgrey');
    $('#motivoDetalle').css('border-color', 'lightgrey');
    $('#solicitudRepuestos').css('border-color', 'lightgrey');
    $('#equipoDetenido').css('border-color', 'lightgrey');
    $('#Empleado option').remove();
    $('#Departamento option').remove();
    $('#Cliente option').remove();
    $('#Equipo option').remove();
    $('#TipoTrabajo option').remove();
}//FIN FUNCION DE LIMPAR CASILLAS

function CargarTipoTrabajo() {
    $.ajax({
        url: "/Solicitud/CargarTipoTrabajo",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var TipoTrabajos = '';
            result.forEach(valor => { TipoTrabajos += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#tipoTrabajo").html(TipoTrabajos);

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

            var Empleados = `<option value="0" selected="true" disabled>--Seleccione--</option>`;
            result.forEach(valor => { Empleados += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Empleados").html(Empleados);

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
    } else {
        LimpiarCombobox("Cliente");
    } 
}//FIN DE CargarCliente

function CargarDepartamentos() {
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
    } else {
        LimpiarCombobox("Departamento");
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

function Enviar() {
    $.ajax({
        url: "/Solicitud/EnviaCorreo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

        },
        error: function (errormessage) {
        }
    });
} 

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
