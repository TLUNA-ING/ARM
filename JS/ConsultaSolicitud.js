$(document).ready(function () {
    CARGAR_GRID();
    CargarProvincia();
    CargarCliente();
    CargarDepartamento();
    CargarEmpleado();
    CargarEquipo();
    CargarTipoTrabajo();
});


function CARGAR_GRID() {


    var table = $('#DatoSol').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/ConsultaSolicitud/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: "",
            stripeClasses: []
              


        },

        columns: [
            
            { "data": "ID_Solicitud" },
            { "data": "Provincia.Descripcion" },
            { "data": "Cliente.Nombre" },
            { "data": "Empleado.Nombre" },
            { "data": "TipoTrabajo.Descripcion" },
            { "data": "Departamento.Descripcion" },
            { "data": "Equipo.Descripcion" },
            { "data": "Fecha_Reporte" },
            { "data": "horaEntrada" },
            { "data": "horaSalida" },
            { "data": "tipoHora" },
            { "data": "cantidadHoras" },
            { "data": "solicitudMotivo" },
            { "data": "motivoDetalle" },
            { "data": "solicitudRepuestos" },
            { "data": "equipoDetenido" },
            { "data": "tiempoDetenido" },
            { "data": "correoMQC" },
            { "data": "nombreMQC" },
            { "data": "cedulaMQC" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarSolicitud(" + row.ID_Solicitud + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = Print(" + row.ID_Solicitud + ") > " +
                        "<i class='material-icons'>print</i>" +
                        " </button ></div>"
                }
            },

        ]
    });
}//FIN DE CARGAR_GRID

function cargarAgregar() {
    $("#IDSolicitud").val("")
    $("#Provincias").val("")
    $("#Cliente").val("")
    $("#Empleado").val("")
    $("#tipoTrabajo").val("")
    $("#Departamento").val("")
    $("#Equipo").val("")
    $("#fechaReporte").val("")
    $("#horaEntrada").val("")
    $("#horaSalida").val("")
    $("#tipoHora").val("")
    $("#cantidadHoras").val("")
    $("#motivoVisita").val("")
    $("#motivoDetalle").val("")
    $("#solicitudRepuestos").val("")
    $("#equipoDetenido").val("")
    $("#tiempoDetenido").val("")
    $("#correoMQC").val("")
    $("#nombreMQC").val("")
    $("#cedulaMQC").val("")
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}//FIN DE CARGAR_AGREGAR


function ConsultarSolicitud(ID) {

    $.ajax({
        url: "/ConsultaSolicitud/ConsultarSolicitud/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

           


            $('#IDSolicitud').val(result.ID_Solicitud);
            $('#Provincias').val(result.Provincia.ID_Provincia);
            $('#Cliente').val(result.Cliente.ID_Cliente);
            $('#Empleado').val(result.Empleado.Cedula);
            $('#tipoTrabajo').val(result.TipoTrabajo.ID_TipoTrabajo);
            $('#Departamento').val(result.Departamento.ID_Departamento);
            $('#Equipo').val(result.Equipo.ID_Equipo);
            $('#fechaReporte').val(result.Fecha_Reporte);
            $('#horaEntrada').val(result.horaEntrada);
            $('#horaSalida').val(result.horaSalida);
            $('#tipoHora').val(result.tipoHora);
            $('#cantidadHoras').val(result.cantidadHoras);
            $('#motivoVisita').val(result.solicitudMotivo);
            $('#motivoDetalle').val(result.motivoDetalle);
            $('#solicitudRepuestos').val(result.solicitudRepuestos);
            $('#equipoDetenido').val(result.equipoDetenido);
            $('#tiempoDetenido').val(result.tiempoDetenido);
            $('#correoMQC').val(result.correoMQC);
            $('#nombreMQC').val(result.nombreMQC);
            $('#cedulaMQC').val(result.cedulaMQC);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}//FIN DE ConsultarSolicitud






function ModificarSolicitud() {


                var solObj = {
                    ID_Solicitud: $('#IDSolicitud').val(),
                    Fecha_Reporte: $('#fechaReporte').val(),
                    horaEntrada: $('#horaEntrada').val(),
                    horaSalida: $('#horaSalida').val(),
                    tipoHora: $('#tipoHora').val(),
                    cantidadHoras: $('#cantidadHoras').val(),
                    solicitudMotivo: $('#motivoVisita').val(),
                    motivoDetalle: $('#motivoDetalle').val(),
                    solicitudRepuestos: $('#solicitudRepuestos').val(),
                    equipoDetenido: $('#equipoDetenido').val(),
                    tiempoDetenido: $('#tiempoDetenido').val(),
                    correoMQC: $('#correoMQC').val(),
                    cedulaMQC: $('#cedulaMQC').val(),
                    nombreMQC: $('#nombreMQC').val(),
                    Provincia: {
                        ID_Provincia: parseFloat($("#Provincias option:selected").val())
                    },
                    Cliente: {
                        ID_Cliente: parseFloat($("#Cliente option:selected").val())
                    },
                    Empleado: {
                        Cedula: parseFloat($("#Empleado option:selected").val())
                    },
                    TipoTrabajo: {
                        ID_TipoTrabajo: parseFloat($("#tipoTrabajo option:selected").val())
                    },
                    Departamento: {
                        ID_Departamento: parseFloat($("#Departamento option:selected").val())
                    },
                    Equipo: {
                        ID_Equipo: parseFloat($("#Equipo option:selected").val())
                    },
                   
                   
                };
      

        $.ajax({
            url: "/ConsultaSolicitud/ModificarSolicitud",
            data: JSON.stringify(solObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Modificado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡La solicitud fue modificada!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CARGAR_GRID();
                                $('#myModal').modal('hide');
                                clearTextBox();
                                
                            }
                        });
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    
}//FIN DE ModificarSolicitud

// Validar datos
function validate() {
    var isValid = true;
    if ($('#Cliente').val() == "") {
        $('#Cliente').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Cliente').css('border-color', 'lightgrey');
    }
    if ($('#Empleado').val() == "") {
        $('#Empleado').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Empleado').css('border-color', 'lightgrey');
    }
    if ($('#tipoTrabajo').val() == "") {
        $('#tipoTrabajo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#tipoTrabajo').css('border-color', 'lightgrey');
    }
    if ($('#Departamento').val() == "") {
        $('#Departamento').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Departamento').css('border-color', 'lightgrey');
    }
    if ($('#Equipo').val() == "") {
        $('#Equipo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Equipo').css('border-color', 'lightgrey');
    }
    if ($('#fechaReporte').val() == "") {
        $('#fechaReporte').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#fechaReporte').css('border-color', 'lightgrey');
    }
    if ($('#horaEntrada').val() == "") {
        $('#horaEntrada').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#horaEntrada').css('border-color', 'lightgrey');
    }
    if ($('#horaSalida').val() == "") {
        $('#horaSalida').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#horaSalida').css('border-color', 'lightgrey');
    }
    if ($('#tipoHora').val() == "") {
        $('#tipoHora').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#tipoHora').css('border-color', 'lightgrey');
    }
    if ($('#cantidadHoras').val() == "") {
        $('#cantidadHoras').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cantidadHoras').css('border-color', 'lightgrey');
    }
    if ($('#motivoVisita').val() == "") {
        $('#motivoVisita').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#motivoVisita').css('border-color', 'lightgrey');
    }
    if ($('#motivoDetalle').val() == "") {
        $('#motivoDetalle').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#motivoDetalle').css('border-color', 'lightgrey');
    }
    if ($('#solicitudRepuestos').val() == "") {
        $('#solicitudRepuestos').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#solicitudRepuestos').css('border-color', 'lightgrey');
    }
    if ($('#equipoDetenido').val() == "") {
        $('#equipoDetenido').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#equipoDetenido').css('border-color', 'lightgrey');
    }
    return isValid;
}

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



function Print(id) {


    $.getJSON("/ConsultaSolicitud/Report2/"+id, function (data) {
        //Muestra el iframe 

        $("#myReport").modal("show");
        $('#reporte').html(data);
    });
    //location.href = '@Url.Action("Report1","ConsultaSolicitud")';

    //$.ajax({
    //    url: "/ConsultaSolicitud/Report1/" + id,
    //    type: "POST",
    //    dataType: JSON,
    //    success: function (result) {
            
    //    },
    //    error: function (errormessage) {
    //        alert(errormessage.responseText);
    //    }
    //});
//return false;
}//FIN DE ConsultarSolicitud