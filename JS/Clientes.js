//Variables globales
var Provincia;
//Load Data in Table when documents is ready
$(document).ready(function () {
    loadTable();
    cargarProvincia();
});
//Load Data function
function loadTable() {
    var table = $('#DatoClientes').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollY: "30em",
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
    $.each(Provincia, function (key, value) {
        $("#provincia").append('<option value=' + value.ID_Provincia + '>' + value.Descripcion + '</option>');
    });
}
//$("#provincia").on('change', function () {
//    cargarCantonProvincia();
//});

//Cargar canton por provincia seleccionada
//function cargarCantonProvincia() {
//    var x = $("#provincia option:selected").val();
//    $('#canton').empty();
//    $.each(Canton, function (key, value) {

//        if (x == value.Provincia["ID_Provincia"]) {
//            $("#canton").append('<option value=' + value.ID_Canton + '>' + value.Canton + '</option>');
//        }

//    });
//}

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

//Function for clearing the textboxes
function clearTextBox() {
    $('#ID_Cliente').val("");
    $('#Descripcion').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#ID_Centro").prop("disabled", false);
    $('#Descripcion').css('border-color', 'lightgrey');
    $('#provincia option').remove();    
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
