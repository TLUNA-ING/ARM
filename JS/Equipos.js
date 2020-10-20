//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
    cargarArea();
});
//Load Data function
function loadTable() {
    var table = $('#DatoEquipo').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
        scrollCollapse: true,
        ajax: {
            url: "/Equipo/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""


        },
        columns: [
            { "data": "ID_Equipo" },
            { "data": "Descripcion" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_Equipo + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > " +
                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_Equipo + ")>" +
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

    var equipoObj = {
        Descripcion: $('#Descripcion').val(),

    };
    try {
        $.ajax({
            url: "/Equipo/Agregar",
            data: JSON.stringify(equipoObj),
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
    } catch (err) { alert(err.responseText); }
}
//Function for getting the Data Based upon Employee ID
function getbyID(ID) {
    //cargarAgregar();
    $.ajax({
        url: "/Equipo/consultar/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#ID_Equipo').val(result.ID_Equipo);
            $('#Descripcion').val(result.Descripcion);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#ID_Equipo").prop("disabled", true);
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
    var equipoObj = {
        ID_Equipo: $('#ID_Equipo').val(),
        Descripcion: $('#Descripcion').val(),
    };
    $.ajax({
        url: "/Equipo/Actualizar",
        data: JSON.stringify(equipoObj),
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
        url: "/Equipo/Eliminar/" + ID ,
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

//Function for clearing the textboxes
function clearTextBox() {
    $('#ID_Equipo').val("");
    $('#Descripcion').val("");   

}

function cargarAgregar() {
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function cargarArea() {
    $.ajax({
        url: "/Equipo/CargarDatos",
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

// Validar datos
function validate() {
    var isValid = true;
    if ($('#Descripcion').val().trim() == "") {
        $('#Descripcion').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Descripcion').css('border-color', 'lightgrey');
    }
}
