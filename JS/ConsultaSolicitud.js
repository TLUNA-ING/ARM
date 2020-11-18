$(document).ready(function () {
    CARGAR_GRID();
    CargarProvincia();
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
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarSolicitud(" + row.solicitudID + ") > " +
                        "<i class='material-icons'>create</i>" +
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

            $('#IDSolicitud').val(result.solicitudID);
            $('#Provincia').val(result);
            $('#Cliente').val(result.clienteId);
            $('#Empleado').val(result.empleadoCedula);
            $('#tipoTrabajo').val(result.tipoTrabajoId);
            $('#Departamento').val(result.departamentoId);
            $('#Equipo').val(result.equipoId);
            $('#fechaReporte').val(result.fechaReporte);
            $('#horaEntrada').val(result.horaEntrada);
            $('#horaSalida').val(result.horaSalida);
            $('#tipoHora').val(result.tipoHora);
            $('#cantidadHoras').val(result.cantidadHoras);
            $('#motivoVisita').val(result.solicitudMotivo);
            $('#motivoDetalle').val(result.motivoDetalle);
            $('#solicitudRepuestos').val(result.solicitudRepuestos);
            $('#equipoDetenido').val(result.equipoDetenido);
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
    var res = validate();
    if (res == false) {
        return false;
    }

        var solObj = {
          
            solicitudID: $('#IDSolicitud').val(),
            clienteId: $('#Cliente option:selected').val(),
            empleadoCedula: $('#Empleado option:selected').val(),
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