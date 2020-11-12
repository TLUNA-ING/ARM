////Variables globales
////var Provincia;
//var Cliente;
//var Departamento;
//var Equipo;
//var TipoTrabajo;
//var Empleado;
////Load Data in Table when documents is ready
$(document).ready(function () {
    //loadTable();
    //cargarDepartamento();
    ////cargarProvincia();
    //cargarCliente();
    //cargarEquipo();
    //cargarTipoTrabajo();
    //cargarEmpleado();
    CargarEquipo();
    CargarDepartamento();
    CargarCliente();
    CargarTipoTrabajo();
    CargarEmpleado();
});

////window.onload = cargarAgregar;

////Load Data function
//function loadTable() {
//    var table = $('#DatoSolicitud').dataTable({
//        destroy: true,
//        "language": {
//            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
//        },
//        responsive: true,
//        scrollY: "30em",
//        scrollCollapse: true,
//        ajax: {
//            url: "/Solicitud/CargarDatos",
//            method: "GET",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            autoWidth: false,
//            dataSrc: ""


//        },
//        columns: [
//            { "data": "ID_Solicitud" },
//            { "data": "Cliente.Descripcion" },
//            { "data": "Departamento.Descripcion" },
//            { "data": "Equipo.Descripcion" },
//            { "data": "Empleado.Cedula" },
//            { "data": "TipoTrabajo.Descripcion" },
//            { "data": "fechaReporte" },
//            { "data": "horaEntrada" },
//            { "data": "horaSalida" },
//            { "data": "tipoHora" },
//            { "data": "cantidadHoras" },
//            { "data": "solicitudMotivo" },
//            { "data": "motivoDetalle" },
//            { "data": "solicitudRepuestos" },
//            { "data": "equipoDetenido" },
//            {
//                "data": null,
//                "render": function (data, type, row) {
//                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_Solicitud + ")>" +
//                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
//                        "</button > " +
//                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_Solicitud + ")>" +
//                        "<i class='	glyphicon glyphicon-trash'> </i>" +
//                        "</button > "
//                }
//            }

//        ]
//    });
//}

////Add Data Function 
function Add() {
    var empObj = {
        IDSolicitud: $('#IDSolicitud').val(),
        Empleado: {
            Cedula: parseFloat($("#Empleado option:selected").val())
        },
        Nombre: $('#Empleado').val(),
        Departamento: {
            ID_Departamento: parseFloat($("#Departamento option:selected").val())
        },
        Descripcion: $('#Departamento').val(),
        Cliente: {
            ID_Cliente: parseFloat($("#Cliente option:selected").val())
        },
        Descripcion: $('#Cliente').val(),
        Equipo: {
            ID_Equipo: parseFloat($("#Equipo option:selected").val())
        },
        Descripcion: $('#Equipo').val(),
        TipoTrabajo: {
            ID_TipoTrabajo: parseFloat($("#TipoTrabajo option:selected").val())
        },
        Descripcion: $('#tipoTrabajo').val(),

        Fecha_Reporte: $('#FechaReporte').val(),
        horaEntrada: $('#horaEntrada').val(),
        horaSalida: $('#horaSalida').val(),
        tipoHora: $('#tipoHora').val(),
        cantidadHoras: $('#cantidadHoras'),
        solicitudMotivo: $('#solicitudMotivo').val(),
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
                loadTable();
                //$('#myModal').modal('hide');
                clearTextBox();
            },
            error: function (result) {
                alert('Revise los datos agregados, ya que no pueden haber campos vacios');
            }
        });
    } catch (err) { alert('Revise los datos agregados, ya que no pueden haber campos vacios'); }
 
}
////Function for getting the Data Based upon Employee ID
//function getbyID(EmpID) {
//    cargarAgregar();
//    $.ajax({
//        url: "/Solicitud/consultar/" + EmpID,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            console.log(result.Fecha)
//            $('#IDSolicitud').val(result.ID_Solicitud);
//            $("#Empleado option[value='" + result.Empleado.Nombre + "']").attr("selected", true);
//            $("#Cliente option[value='" + result.Cliente.Descripcion + "']").attr("selected", true);
//            $("#Equipo option[value='" + result.Equipo.Descripcion + "']").attr("selected", true);
//            $("#TipoTrabajo option[value='" + result.TipoTrabajo.Descripcion + "']").attr("selected", true);
//            $("#Departamento option[value='" + result.Departamento.Descripcion + "']").attr("selected", true);
//            $('#FechaReporte').val(result.Fecha_Reporte);
//            $('#horaEntrada').val(result.horaEntrada);
//            $('#horaSalida').val(result.horaSalida);
//            $('#tipoHora').val(result.tipoHora);
//            $('#cantidadHoras').val(result.cantidadHoras);
//            $('#solicitudMotivo').val(result.solicitudMotivo);
//            $('#motivoDetalle').val(result.motivoDetalle);
//            $('#solicitudRepuestos').val(result.solicitudRepuestos);
//            $('#equipoDetenido').val(result.equipoDetenido);
//            $('#myModal').modal('show');
//            $('#btnUpdate').show();
//            $('#btnAdd').hide();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//    return false;
//}
////function for updating employee's record
//function Update() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var empObj = {
//        IDSolicitud: $('#IDSolicitud').val(),
//        Empleado: {
//            Cedula: parseFloat($("#Empleado option:selected").val())
//        },
//        Nombre: $('#Nombre').val(),
//        Departamento: {
//            ID_Departamento: parseFloat($("#Departamento option:selected").val())
//        },
//        Descripcion: $('#Descripcion').val(),
//        Cliente: {
//            ID_Cliente: parseFloat($("#Cliente option:selected").val())
//        },
//        Descripcion: $('#Descripcion').val(),
//        Equipo: {
//            ID_Equipo: parseFloat($("#Equipo option:selected").val())
//        },
//        Descripcion: $('#Descripcion').val(),
//        TipoTrabajo: {
//            ID_TipoTrabajo: parseFloat($("#TipoTrabajo option:selected").val())
//        },
//        Descripcion: $('#Descripcion').val(),

//        Fecha_Reporte: $('#Fecha').val(),
//        //Reporte: $('#Reporte').val(),
//        //Fecha: $('#Fecha').val(),
//        horaEntrada: $('#horaEntrada').val(),
//        horaSalida: $('#horaSalida').val(),
//        tipoHora: $('#tipoHora').val(),
//        cantidadHoras: $('#cantidadHoras'),
//        solicitudMotivo: $('#solicitudMotivo').val(),
//        motivoDetalle: $('#motivoDetalle').val(),
//        solicitudRepuestos: $('#solicitudRepuestos').val(),
//        equipoDetenido: $('#equipoDetenido').val(),
//    };
//    $.ajax({
//        url: "/Solicitud/Actualizar",
//        data: JSON.stringify(empObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            loadTable();
//            $('#myModal').modal('hide');

//            clearTextBox();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
////function for deleting employee's record
//function Delete(ID) {

//    $.ajax({
//        url: "/Solicitud/Eliminar/" + ID + "",
//        type: "POST",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            loadTable();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });

//}
////Cargar agregar div
//function cargarAgregar() {
//    $.each(Empleado, function (key, value) {
//        $("#Empleado").append('<option value=' + value.Cedula + '>' + value.Nombre + '</option>');

//    }),
//        $.each(Departamento, function (key, value) {
//            $("#Departamento").append('<option value=' + value.ID_Departamento + '>' + value.Descripcion + '</option>');

//        }),
//        $.each(Cliente, function (key, value) {
//            $("#Cliente").append('<option value=' + value.ID_Cliente + '>' + value.Descripcion + '</option>');

//        }),
//        $.each(Equipo, function (key, value) {
//            $("#Equipo").append('<option value=' + value.ID_Equipo + '>' + value.Descripcion + '</option>');

//        }),
//        $.each(TipoTrabajo, function (key, value) {
//            $("#TipoTrabajo").append('<option value=' + value.ID_TipoTrabajo + '>' + value.Descripcion + '</option>');

//        });
//}

////Cargar canton por provincia seleccionada
////function cargarCentroProvincia() {
////    var x = $("#provincia option:selected").val();
////    $('#Centro').empty();
////    $.each(Centro, function (key, value) {

////        if (x == value.Provincia["ID_Provincia"]) {

////            $("#Centro").append('<option value=' + value.ID_Centro + '>' + value.Descripcion + '</option>');
////        }

////    });
////}
////Cargar Empleado
//function cargarEmpleado() {
//    $.ajax({
//        url: "/Empleado/CargarDatos",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            Empleado = result;
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
//// Cargar Departamento
//function cargarDepartamento() {
//    $.ajax({
//        url: "/Departamento/CargarDatos",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            Departamento = result;
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
////Cargar Cliente
//function cargarCliente() {
//    $.ajax({
//        url: "/Cliente/CargarDatos",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            Cliente = result;
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
//// Cargar Equipo
//function cargarEquipo() {
//    $.ajax({
//        url: "/Equipo/CargarDatos",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            Equipo = result;
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
////Cargar Tipo Trabajo
//function cargarTipoTrabajo() {
//    $.ajax({
//        url: "/TipoTrabajo/CargarDatos",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            TipoTrabajo = result;
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
////Function for clearing the textboxes
//function clearTextBox() {
//    $('#IDSolicitud').val("");
//    $('#Empleado').val("");
//    $('#Departamento').val("");
//    $('#Cliente').val("");
//    $('#Equipo').val("");
//    $('#TipoTrabajo').val("");
//    $('#FechaReporte').val("");
//    $('#horaEntrada').val("");
//    $('#horaSalida').val("");
//    $('#tipoHora').val("");
//    $('#cantidadHoras').val("");
//    $('#solicitudMotivo').val("");
//    $('#motivoDetalle').val("");
//    $('#solicitudRepuestos').val("");
//    $('#equipoDetenido').val("");
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//    $('#IDSolicitud').css('border-color', 'lightgrey');
//    $('#Empleado').css('border-color', 'lightgrey');
//    $('#Departamento').css('border-color', 'lightgrey');
//    $('#Cliente').css('border-color', 'lightgrey');
//    $('#Equipo').css('border-color', 'lightgrey');
//    $('#TipoTrabajo').css('border-color', 'lightgrey');
//    $('#FechaReporte').css('border-color', 'lightgrey');
//    $('#horaEntrada').css('border-color', 'lightgrey');
//    $('#horaSalida').css('border-color', 'lightgrey');
//    $('#tipoHora').css('border-color', 'lightgrey');
//    $('#cantidadHoras').css('border-color', 'lightgrey');
//    $('#solicitudMotivo').css('border-color', 'lightgrey');
//    $('#motivoDetalle').css('border-color', 'lightgrey');
//    $('#equipoDetenido').css('border-color', 'lightgrey');
//    $('#Empleado option').remove();
//    $('#Departamento option').remove();
//    $('#Cliente option').remove();
//    $('#Equipo option').remove();
//    $('#TipoTrabajo option').remove();
//}


//// Validar datos
//function validate() {
//    var isValid = true;
//    if ($('#Empleado').val() == "") {
//        $('#Empleado').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Empleado').css('border-color', 'lightgrey');
//    }
//    if ($('#Departamento').val() == "") {
//        $('#Departamento').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Departamento').css('border-color', 'lightgrey');
//    }
//    if ($('#Cliente').val() == "") {
//        $('#Cliente').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Cliente').css('border-color', 'lightgrey');
//    }
//    if ($('#Equipo').val() == "") {
//        $('#Equipo').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Equipo').css('border-color', 'lightgrey');
//    }
//    if ($('#TipoTrabajo').val() == "") {
//        $('#TipoTrabajo').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#TipoTrabajo').css('border-color', 'lightgrey');
//    }
//    if ($('#FechaReporte').val() == "") {
//        $('#FechaReporte').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#FechaReporte').css('border-color', 'lightgrey');
//    }
//    if ($('#horaEntrada').val() == "") {
//        $('#horaEntrada').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#horaEntrada').css('border-color', 'lightgrey');
//    }
//    if ($('#horaSalida').val() == "") {
//        $('#horaSalida').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#horaSalida').css('border-color', 'lightgrey');
//    }
//    if ($('#tipoHora').val() == "") {
//        $('#tipoHora').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#tipoHora').css('border-color', 'lightgrey');
//    }
//    if ($('#cantidadHoras').val() == "") {
//        $('#cantidadHoras').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#cantidadHoras').css('border-color', 'lightgrey');
//    }
//    if ($('#solicitudMotivo').val() == "") {
//        $('#solicitudMotivo').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#solicitudMotivo').css('border-color', 'lightgrey');
//    }
//    if ($('#motivoDetalle').val() == "") {
//        $('#motivoDetalle').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#motivoDetalle').css('border-color', 'lightgrey');
//    }
//    if ($('#equipoDetenido').val() == "") {
//        $('#equipoDetenido').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#equipoDetenido').css('border-color', 'lightgrey');
//    }
//    return isValid;
//}



function CargarCliente() {
    $.ajax({
        url: "/Solicitud/CargarCliente",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var Clientes = '';
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

            var Departamento = '';
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

            var Empleados = '';
            result.forEach(valor => { Empleados += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Empleado").html(Empleados);

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

            var Equipos = '';
            result.forEach(valor => { Equipos += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Equipo").html(Equipos);

        },
        error: function (errormessage) {
        }
    });//FIN DE CargarEmpleado
}