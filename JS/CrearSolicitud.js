
$(document).ready(function () {
    CargarEquipo();
    CargarDepartamento();
    CargarCliente();
    CargarTipoTrabajo();
    CargarEmpleado();
    CargarProvincia();
});


function Add() {

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
        equipoDetenido: $('#equipoDetenido').val(),

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
                clearTextBox();
            },
            error: function (result) {
                alert('Revise los datos agregados, ya que no pueden haber campos vacios');
            }
        });
    } catch (err) { alert('Revise los datos agregados, ya que no pueden haber campos vacios'); }
 
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
        equipoDetenido: $('#equipoDetenido').val(),
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


////Function for clearing the textboxes
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



function CargarCliente() {
    $.ajax({
        url: "/Solicitud/CargarCliente",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var Clientes = `<option value="0" selected="true" disabled>--Seleccione--</option>`;
            result.forEach(valor => { Clientes += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Cliente").html(Clientes);

        },
        error: function (errormessage) {
        }
    });//FIN DE CargarCliente
}


function CargarDepartamento() {
    $.ajax({
        url: "/Solicitud/CargarDepartamento",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var Departamento = `<option value="0" selected="true" disabled>--Seleccione--</option>`;
            result.forEach(valor => { Departamento += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Departamento").html(Departamento);

        },
        error: function (errormessage) {
        }
    });//FIN DE CargarDepartamento
}

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
    });//FIN DE CargarTipoTrabajo
}


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
    });//FIN DE CargarEmpleado
}


function CargarEquipo() {
    $.ajax({
        url: "/Solicitud/CargarEquipo",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var Equipos = `<option value="0" selected="true" disabled>--Seleccione--</option>`;
            result.forEach(valor => { Equipos += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Equipo").html(Equipos);

        },
        error: function (errormessage) {
        }
    });//FIN DE CargarEmpleado
}

//Inicio CargarProvincia
function CargarProvincia() {
    $.ajax({
        url: "/Provincia/CargarDatos",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var Provincias = `<option value="0" selected="true" disabled>--Seleccione--</option>`;
            result.forEach(valor => { Provincias += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Provincias").html(Provincias);

        },
        error: function (errormessage) {
        }
    });//FIN DE CargarProvincia
}