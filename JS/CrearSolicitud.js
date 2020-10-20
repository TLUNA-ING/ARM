//Variables globales
var Provincia;
var Centro;
var Area;
var Empleado;
//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
    cargarArea();
    cargarProvincia();
    cargarCentro();

});
//Load Data function
function loadTable() {
    var table = $('#DatoSolicitud').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
        scrollCollapse: true,
        ajax: {
            url: "/Solicitud/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""


        },
        columns: [
            { "data": "ID_Solicitud" },
            { "data": "Centro.Descripcion" },
            { "data": "AreaTrabajo.Descripcion" },
            { "data": "Cliente" },
            { "data": "Cedula" },
            { "data": "Fecha" },
            { "data": "Reporte" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_Solicitud + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > " +
                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_Solicitud + ")>" +
                        "<i class='	glyphicon glyphicon-trash'> </i>" +
                        "</button > "
                }
            }

        ]
    });
}

//Add Data Function 
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var empObj = {
        IDSolicitud: $('#IDSolicitud').val(),
        Cedula: $('#Encargado').val(),
        Provincia: {
            ID_Provincia: parseFloat($("#provincia option:selected").val())
        },
        AreaTrabajo: {
            ID_AreaTrabajo: parseFloat($("#Area option:selected").val())
        },
        Centro: {
            ID_Centro: parseFloat($("#Centro option:selected").val())
        },
        Cliente: $('#Cliente').val(),
        Encargado: $('#Encargado').val(),
        Fecha_Reporte: $('#Fecha').val(),
        Reporte: $('#Reporte').val()
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
                $('#myModal').modal('hide');
                clearTextBox();
            },
            error: function (result) {
                alert('Revise los datos agregados, ya que no pueden haber campos vacios');
            }
        });
    } catch (err) { alert('Revise los datos agregados, ya que no pueden haber campos vacios'); }
}
//Function for getting the Data Based upon Employee ID
function getbyID(EmpID) {
    cargarAgregar();
    $.ajax({
        url: "/Solicitud/consultar/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result.Fecha)
            $('#IDSolicitud').val(result.ID_Solicitud);
            $("#provincia option[value='" + result.Centro.Provincia.ID_Provincia + "']").attr("selected", true);
            cargarCentroProvincia();
            $("#Centro option[value='" + result.Centro.ID_Centro + "']").attr("selected", true);
            $("#Area option[value='" + result.AreaTrabajo.ID_AreaTrabajo + "']").attr("selected", true);
            $('#Cliente').val(result.Cliente);
            $('#Fecha').val(result.Fecha);
            $('#Reporte').val(result.Reporte);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID_Solicitud: $('#IDSolicitud').val(),
        Cedula: $('#Encargado').val(),
        Provincia: {
            ID_Provincia: parseFloat($("#provincia option:selected").val())
        },
        AreaTrabajo: {
            ID_AreaTrabajo: parseFloat($("#Area option:selected").val())
        },
        Centro: {
            ID_Centro: parseFloat($("#Centro option:selected").val())
        },
        Cliente: $('#Cliente').val(),
        Encargado: $('#Encargado').val(),
        Fecha_Reporte: $('#Fecha').val(),
        Reporte: $('#Reporte').val()
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
}
//function for deleting employee's record
function Delete(ID) {

    $.ajax({
        url: "/Solicitud/Eliminar/" + ID + "",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            loadTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
//Cargar agregar div
function cargarAgregar() {
    $.each(Provincia, function (key, value) {
        $("#provincia").append('<option value=' + value.ID_Provincia + '>' + value.Provincia + '</option>');
        
    });
    cargarCentroProvincia();
    $.each(Area, function (key, value) {
        $("#Area").append('<option value=' + value.ID_AreaTrabajo + '>' + value.Descripcion + '</option>');
    });

}
$("#provincia").on('change', function () {
    cargarCentroProvincia();
});
//Cargar canton por provincia seleccionada
function cargarCentroProvincia() {
    var x = $("#provincia option:selected").val();
    $('#Centro').empty();
    $.each(Centro, function (key, value) {

        if (x == value.Provincia["ID_Provincia"]) {
           
            $("#Centro").append('<option value=' + value.ID_Centro + '>' + value.Descripcion + '</option>');
        }

    });
}
//Cargar Provincias
function cargarProvincia() {
    $.ajax({
        url: "/Provincia/CargarDatos",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            Provincia = result;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// Cargar Area de Trabajo
function cargarArea() {
    $.ajax({
        url: "/Area/CargarDatos",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            Area = result;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Cargar Cantones
function cargarCentro() {
    $.ajax({
        url: "/Centro/CargarDatos",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            Centro = result;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for clearing the textboxes
function clearTextBox() {
    $('#IDSolicitud').val("");
    $('#Cliente').val("");
    $('#Fecha').val("");
    $('#Reporte').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#IDSolicitud').css('border-color', 'lightgrey');
    $('#provincia').css('border-color', 'lightgrey');
    $('#Area').css('border-color', 'lightgrey');
    $('#Centro').css('border-color', 'lightgrey');
    $('#Cliente').css('border-color', 'lightgrey');
    $('#Encargado').css('border-color', 'lightgrey');
    $('#Fecha').css('border-color', 'lightgrey');
    $('#Reporte').css('border-color', 'lightgrey');
    $('#provincia option').remove();
    $('#Area option').remove();
    $('#Centro option').remove();
}


// Validar datos
function validate() {
    var isValid = true;
    if ($('#provincia').val().trim() == "") {
        $('#provincia').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#provincia').css('border-color', 'lightgrey');
    }
    if ($('#Area').val().trim() == "") {
        $('#Area').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Area').css('border-color', 'lightgrey');
    }
    if ($('#Centro').val().trim() == "") {
        $('#Centro').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Centro').css('border-color', 'lightgrey');
    }
    if ($('#Cliente').val().trim() == "") {
        $('#Cliente').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Cliente').css('border-color', 'lightgrey');
    }
    if ($('#Encargado').val().trim() == "") {
        $('#Encargado').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Encargado').css('border-color', 'lightgrey');
    }
    if ($('#Reporte').val().trim() == "") {
        $('#Reporte').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Reporte').css('border-color', 'lightgrey');
    }
    if ($('#Fecha').val().trim() == "") {
        $('#Fecha').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Fecha').css('border-color', 'lightgrey');
    }
    return isValid;
}
