﻿$(document).ready(function () {
    CARGAR_GRID();
    CargarEmpleado();
    CargarTipoTrabajo();
});

var ID_PROVINCIA = "";
var ID_CLIENTE = "";
var ID_DEPARATAMENTO = "";
var ID_EQUIPO = "";

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

            $('#Provincias').val(result.Provincia.ID_PROVINCIA);
            $('#Cliente').val(result.Cliente.ID_Cliente);
            $('#Departamento').val(result.Departamento.ID_Departamento);
            $('#Equipo').val(result.Equipo.ID_Equipo);

            $('#Empleado').val(result.Empleado.Cedula);
            $('#tipoTrabajo').val(result.TipoTrabajo.ID_TipoTrabajo);



            var D = new Date();
            D = result.Fecha_Reporte;
            var N = D.toString();

            var DD = N.substring(0, 2);
            var MM = N.substring(2, 4);
            var AAAA = N.substring(5, 9);
            var FECHA = '06' + '/' + '12' + '/' + '2020'
            $('#fechaReporte').val(FECHA);


            D = result.horaEntrada;
            N = D.toString();

            var H = N.substring(10, 12);
            var M = N.substring(13, 15);
            var HORA = H + ':' + M
            $('#horaEntrada').val(HORA);


            D = result.horaSalida;
            N = D.toString();

            H = N.substring(10, 12);
            M = N.substring(13, 15);
            HORA = H + ':' + M
            $('#horaSalida').val(HORA);

            CargarProvincia();

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

function getMinutesBetweenDates(startDate, endDate) {
    var diff = endDate.getTime() - startDate.getTime();
    return (diff / 60000);
}


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
}//FIN DE FUNCION IMPRIMIR


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

            var Empleados = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => { Empleados += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#Empleado").html(Empleados);

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

            if (ID_PROVINCIA !="") {
                $('#Provincias').val(ID_PROVINCIA);
                CargarCliente();
            }

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

                if (ID_PROVINCIA != "") {
                    ID_PROVINCIA=""
                }
                if (ID_CLIENTE != "") {
                    $('#Cliente').val(ID_CLIENTE);
                    CargarDepartamentos();
                }

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

                if (ID_CLIENTE != "") {
                    ID_CLIENTE = ""
                }
                if (ID_DEPARATAMENTO != "") {
                    $('#Departamento').val(ID_DEPARATAMENTO);
                    CargarEquipos();
                }

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
                if (ID_DEPARATAMENTO != "") {
                    $('#Equipo').val(ID_EQUIPO);
                    ID_DEPARATAMENTO = ""
                    ID_EQUIPO = ""
                }

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
