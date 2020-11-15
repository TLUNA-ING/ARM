$(document).ready(function () {
    CARGAR_GRID();
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
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarSolicitud (" + row.IDSolicitud + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            },

        ]
    });
}//FIN DE CARGAR_GRID

//function cargarAgregar() {
//    $("#IDSolicitud").val("")
//    $("#Cliente").val("")
//    $("#Empleado").val("")
//    $("#tipoTrabajo").val("")
//    $("#Departamento").val("")
//    $("#Equipo").val("")
//    $("#fechaReporte").val("")
//    $("#horaEntrada").val("")
//    $("#horaSalida").val("")
//    $("#tipoHora").val("")
//    $("#cantidadHoras").val("")
//    $("#motivoVisita").val("")
//    $("#motivoDetalle").val("")
//    $("#solicitudRepuestos").val("")
//    $("#equipoDetenido").val("")
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//}//FIN DE CARGAR_AGREGAR


function ConsultarSolicitud(ID) {
    $.ajax({
        url: "/ConsultaSolicitud/ConsultarSolicitud/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#IDSolicitud').val(result.IDSolicitud);
            $('#Cliente').val(result.Cliente);
            $('#Empleado').val(result.Empleado);
            $('#tipoTrabajo').val(result.tipoTrabajo);
            $('#Departamento').val(result.Departamento);
            $('#Equipo').val(result.Equipo);
            $('#fechaReporte').val(result.fechaReporte);
            $('#horaEntrada').val(result.horaEntrada);
            $('#horaSalida').val(result.horaSalida);
            $('#tipoHora').val(result.tipoHora);
            $('#cantidadHoras').val(result.cantidadHoras);
            $('#motivoVisita').val(result.motivoVisita);
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
}//FIN DE ConsultarDepartamento