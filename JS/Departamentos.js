//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
});

function cargarAgregar() {
    clearTextBox();
}
//Load Data function
function loadTable() {
    var table = $('#DatoDepa').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
        scrollCollapse: true,
        ajax: {
            url: "/Departamento/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""


        },
        columns: [
            { "data": "ID_Departamento" },
            { "data": "Descripcion" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_Departamento + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > " +
                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_Departamento + ")>" +
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

    var areaObj = {
        Descripcion: $('#Descripcion').val(),
    };
    try {
        $.ajax({
            url: "/Departamento/Agregar",
            data: JSON.stringify(areaObj),
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
    $.ajax({
        url: "/Departamento/Consultar/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#ID_Depa').val(result.ID_Departamento);
            $('#Descripcion').val(result.Descripcion);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#ID_Depa").prop("disabled", true);
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
    var objArea = {
        ID_Departamento: $('#ID_Depa').val(),
        Descripcion: $('#Descripcion').val()
    };
    $.ajax({
        url: "/Departamento/Actualizar/",
        data: JSON.stringify(objArea),
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
        url: "/Departamento/Eliminar/" + ID,
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
    $('#ID_Depa').val("");
    $('#Descripcion').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#ID_Depa").prop("disabled", false);
    $('#ID_Depa').css('border-color', 'lightgrey');
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
    return isValid;
}
