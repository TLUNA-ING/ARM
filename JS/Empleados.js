﻿//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
});
//Load Data function
function loadTable() {
    var table = $('#DatoEmpleado').dataTable({
        destroy: true,
        "language": {
            "url":"//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
        scrollCollapse: true,
        ajax: {
            url: "/Empleado/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
            
            
        },
        columns:[
            { "data": "Cedula" },
            { "data": "Nombre" },
            { "data": "Primer_Apellido" },
            { "data": "Segundo_Apellido" },
            { "data": "Correo" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.Cedula + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > " +
                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.Cedula + ")>" +
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
        Cedula: $('#Cedula').val(),
        Nombre: $('#Nombre').val(),
        Primer_Apellido: $('#Primer_Apellido').val(),
        Segundo_Apellido: $('#Segundo_Apellido').val(),
        Correo: $('#Correo').val(),
    };
    try {
        $.ajax({
            url: "/Empleado/Agregar",
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
    } catch (err) {alert('Error 2');}
}
//Function for getting the Data Based upon Employee ID
function getbyID(Cedula) {
    //cargarAgregar();
    $.ajax({
        url: "/Empleado/Consultar/" + Cedula,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#Cedula').val(result.Cedula);
            $('#Nombre').val(result.Nombre);
            $('#Primer_Apellido').val(result.Primer_Apellido);
            $('#Segundo_Apellido').val(result.Segundo_Apellido);
            $('#Correo').val(result.Correo);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#Cedula").prop("disabled", true);
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
        Cedula: $('#Cedula').val(),
        Nombre: $('#Nombre').val(),
        Primer_Apellido: $('#Primer_Apellido').val(),
        Segundo_Apellido: $('#Segundo_Apellido').val(),
        Telefono: $('#Telefono').val(),
        Correo: $('#Correo').val(),
        Direccion: {
            Direccion: $('#Direccion').val(),
            canton: {
                ID_Canton: parseFloat($("#canton option:selected").val())
            }
        }
    };
    $.ajax({
        url: "/Empleado/Actualizar",
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
        url: "/Empleado/Eliminar/" + ID,
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
function clearTextBox() {
    $('#Cedula').val("");
    $('#Nombre').val("");
    $('#Primer_Apellido').val("");
    $('#Segundo_Apellido').val("");
    $('#Telefono').val("");
    $('#Correo').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#Cedula").prop("disabled", false);
    $('#Cedula').css('border-color', 'lightgrey');
    $('#Nombre').css('border-color', 'lightgrey');
    $('#Primer_Apellido').css('border-color', 'lightgrey');
    $('#Segundo_Apellido').css('border-color', 'lightgrey');
    $('#Correo').css('border-color', 'lightgrey');
}


// Validar datos
function validate() {
    var isValid = true;
    if ($('#Nombre').val().trim() == "") {
        $('#Nombre').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Nombre').css('border-color', 'lightgrey');
    }
    if ($('#Cedula').val().trim() == "") {
        $('#Cedula').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Cedula').css('border-color', 'lightgrey');
    }
    if ($('#Primer_Apellido').val().trim() == "") {
        $('#Primer_Apellido').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Primer_Apellido').css('border-color', 'lightgrey');
    }
    if ($('#Segundo_Apellido').val().trim() == "") {
        $('#Segundo_Apellido').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Segundo_Apellido').css('border-color', 'lightgrey');
    }
    if ($('#Correo').val().trim() == "") {
        $('#Correo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Correo').css('border-color', 'lightgrey');
    }
    return isValid;
}
