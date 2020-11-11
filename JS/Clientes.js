//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
});
//Load Data function
function loadTable() {
    var table = $('#DatoClientes').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/Cliente/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
        },
        columns: [
            { "data": "ID_Cliente" },
            { "data": "Descripcion" },
            { "data": "Provincia.Descripcion" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= getbyID(" + row.ID_Cliente + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > " +
                        "<button type='button' class='btn btn-danger'  onclick= Delete(" + row.ID_Cliente + ")>" +
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

    var centroObj = {
        Provincia: {
            ID_Provincia: parseFloat($("#provincia option:selected").val())
        },
        Descripcion: $('#Descripcion').val(),
    };
    try {
        $.ajax({
            url: "/Cliente/Agregar",
            data: JSON.stringify(centroObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                loadTable();
                $('#myModal').modal('hide');
                clearTextBox();
            },
            error: function (result) {
                alert('Revise los datos agregados, ya que no pueden haber campos vacios o cedulas repetidas');
            }
        });
    } catch (err) { alert('Revise los datos agregados, ya que no pueden haber campos vacios o cedulas repetidas'); }
}

//Function for getting the Data Based upon Employee ID
function getbyID(centroID) {
    cargarAgregar();
    $.ajax({
        url: "/Cliente/consultar/" + centroID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#ID_Centro').val(result.ID_Centro);
            $("#provincia option[value='" + result.Provincia.ID_Provincia + "']").attr("selected", true);
            $('#Descripcion').val(result.Descripcion);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $("#ID_Centro").prop("disabled", true);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var centroObj = {
        ID_Cliente: $('#ID_Cliente').val(),
        Provincia: {
            ID_Provincia: parseFloat($("#provincia option:selected").val())
        },
        Descripcion: $('#Descripcion').val(),
    };
    $.ajax({
        url: "/Cliente/Actualizar",
        data: JSON.stringify(centroObj),
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

//function for deleting 
function Delete(ID) {

    $.ajax({
        url: "/Cliente/Eliminar/" + ID + "",
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
    CargarProvincias();
    clearTextBox();
}

function CargarProvincias() {
    $.ajax({
        url: "/Provincia/CargarDatos",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var provincias = '';

            result.forEach(valor => { provincias += `<option value="${valor.Value}">${valor.Text}</option>` });
            $("#provincia").html(provincias);

        },
        error: function (errormessage) {
        }
    });
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#ID_Cliente').val("");
    $('#Descripcion').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#ID_Centro").prop("disabled", false);
    $('#Descripcion').css('border-color', 'lightgrey');
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
