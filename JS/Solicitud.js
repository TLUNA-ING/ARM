////Load Data in Table when documents is ready
//$(document).ready(function () {
//    loadTable();
//});
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
//            url: "/Consulta/Consulta",
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
//            { "data": "Cedula" },
//            { "data": "Fecha" },
//            { "data": "Reporte" },
//            { "data": "horaEntrada" },
//            { "data": "horaSalida" },
//            { "data": "tipoHora" },
//            { "data": "cantidadHoras" },
//            { "data": "solicitudMotivo" },
//            { "data": "motivoDetalle" },
//            { "data": "solicitudRepuestos" },
//            { "data": "equipoDetenido" }





//        ]
//    });
//}
