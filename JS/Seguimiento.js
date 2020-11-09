
//function ConsultarSolicitud() {
//    if ($('#IDSolicitud').val().trim() == "") {
//        $('#IDSolicitud').css('border-color', 'Red');
//    }
//    else {
//        $('#IDSolicitud').css('border-color', 'lightgrey');
//        $.ajax({
//            url: "/Solicitud/consultar/" + $('#IDSolicitud').val(),
//            typr: "GET",
//            contentType: "application/json;charset=UTF-8",
//            dataType: "json",
//            success: function (result) {
//                $('#divDatos').removeAttr('hidden');
//                $('#IDSolicitud').prop("disabled", true);
//                loadTable()
//            },
//            error: function (errormessage) {
//                $('#IDSolicitud').val("");
//                alert('El número de solicitud no existe');
//            }
//        });
//    }
    
//    return false;
//}
////Load Data in Table when documents is ready

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
//            url: "/Seguimiento/CargarDatos/" + $('#IDSolicitud').val(),
//            method: "GET",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            autoWidth: false,
//            dataSrc: ""


//        },
//        columns: [
//            { "data": "ID_SeguimientoSolicitud" },
//            { "data": "Fecha" },
//            { "data": "HorasNormales" },
//            { "data": "HorasExtras" },
//            { "data": "HorasDoble" },
//            { "data": "TrabajoRealizado" },
//            {
//                "data": null,
//                "render": function (data, type, row) {
//                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_SeguimientoSolicitud + ")>" +
//                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
//                        "</button > " +
//                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_SeguimientoSolicitud + ")>" +
//                        "<i class='	glyphicon glyphicon-trash'> </i>" +
//                        "</button > "
//                }
//            }

//        ]
//    });
//}

////Add Data Function 
//function Add() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }

//    var empObj = {
        
//        Solicitud: {
//            ID_Solicitud: parseInt($('#IDSolicitud').val())
//        },
//        Fecha_Laborada: $('#Fecha').val(),
//        HorasNormales: parseInt($('#HorasNormales').val()),
//        HorasExtras: parseInt($('#HorasExtras').val()),
//        HorasDoble: parseInt($('#HorasDobles').val()),
//        TrabajoRealizado: $('#Reporte').val()
//    };
//    console.log(empObj)
//    try {
//        $.ajax({
//            url: "/Seguimiento/Agregar",
//            data: JSON.stringify(empObj),
//            type: "POST",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                loadTable();
//                $('#myModal').modal('hide');
//                clearTextBox();
//            },
//            error: function (result) {
//                alert('Revise los datos agregados, ya que no pueden haber campos vacios'+result);
//            }
//        });
//    } catch (err) { alert('Revise los datos agregados, ya que no pueden haber campos vacios'); }
//}
////Function for getting the Data Based upon Employee ID
//function getbyID(EmpID) {
//    cargarAgregar();
//    $.ajax({
//        url: "/Seguimiento/consultar/" + EmpID,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            $('#IDSeguimiento').val(result.ID_SeguimientoSolicitud);
//            $('#Fecha').val(result.Fecha);
//            $('#HorasNormales').val(result.HorasNormales);
//            $('#HorasExtras').val(result.HorasExtras);
//            $('#HorasDobles').val(result.HorasDoble);
//            $('#Reporte').val(result.TrabajoRealizado);
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
//        ID_SeguimientoSolicitud: $('#IDSeguimiento').val(),
//        Solicitud: {
//            ID_Solicitud: parseInt($('#IDSolicitud').val())
//        },
//        Fecha_Laborada: $('#Fecha').val(),
//        HorasNormales: parseInt($('#HorasNormales').val()),
//        HorasExtras: parseInt($('#HorasExtras').val()),
//        HorasDoble: parseInt($('#HorasDobles').val()),
//        TrabajoRealizado: $('#Reporte').val()
//    };
//    $.ajax({
//        url: "/Seguimiento/Actualizar",
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
//        url: "/Seguimiento/Eliminar/" + ID + "",
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
//}

////Function for clearing the textboxes
//function Limpiar() {
//    $('#divDatos').attr('hidden', 'hidden');
//    $('#IDSolicitud').val("");
//    $('#IDSolicitud').prop("disabled",false);
//    clearTextBox();
//}
//function clearTextBox() {
//    $('#IDSeguimiento').val("");
//    $('#HorasNormales').val("");
//    $('#Fecha').val("");
//    $('#HorasExtras').val("");
//    $('#Reporte').val("");
//    $('#HorasDobles').val("");
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//    $('#IDSeguimiento').css('border-color', 'lightgrey');
//    $('#HorasNormales').css('border-color', 'lightgrey');
//    $('#Fecha').css('border-color', 'lightgrey');
//    $('#HorasExtras').css('border-color', 'lightgrey');
//    $('#HorasDobles').css('border-color', 'lightgrey');
//    $('#Reporte').css('border-color', 'lightgrey');
//}


//// Validar datos
//function validate() {
//    var isValid = true;
//    if ($('#HorasNormales').val().trim() == "") {
//        $('#HorasNormales').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#HorasNormales').css('border-color', 'lightgrey');
//    }
//    if ($('#HorasExtras').val().trim() == "") {
//        $('#HorasExtras').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#HorasExtras').css('border-color', 'lightgrey');
//    }
//    if ($('#HorasDobles').val().trim() == "") {
//        $('#HorasDobles').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#HorasDobles').css('border-color', 'lightgrey');
//    }  
    
    
//    if ($('#Reporte').val().trim() == "") {
//        $('#Reporte').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Reporte').css('border-color', 'lightgrey');
//    }
//    if ($('#Fecha').val().trim() == "") {
//        $('#Fecha').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Fecha').css('border-color', 'lightgrey');
//    }
//    return isValid;
//}
