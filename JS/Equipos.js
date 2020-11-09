//Load Data in Table when documents is ready
$(document).ready(function () {
    CARGAR_GRID();
});

function CARGAR_GRID() {
    var table = $('#DatoEquipo').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
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
            { "data": "Estado" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-danger' onclick= getbyID(" + row.ID_Equipo + ")>" +
                        "<i class='	glyphicon glyphicon-pencil'> </i>" +
                        "</button > "
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<button type='button' class='btn btn-primary' onclick= ModificarEstado(" + row.ID_Equipo + ")>" +
                        "<i class='	glyphicon glyphicon-trash'> </i>" +
                        "</button > "
                }
            }

        ]
    });
}

function VALIDAR() {
    var ENTRAR = false;
    if ($('#Descripcion_equipo').val().trim() == "") {
        MENSAJE_WARNING("¡Descripción inválida, por favor revise los datos brindados!");
    } else {
        ENTRAR = true;
    }   
    return ENTRAR;
}//FIN DE VALIDAR

function MENSAJE_WARNING(MENSAJE) {
    swal({
        title: "¡No se pudo procesar!",
        text: MENSAJE,
        type: "info",
        showCancelButton: false,
        confirmButtonText: "¡ Entendido !",
        confirmButtonColor: '#24a0ed',
        closeOnConfirm: true
    });
}//FIN DE MENSAJE_WARNING

function cargarAgregar() {
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $("#Descripcion_equipo").focus()
}//FIN DE CARGAR_AGREGAR


function AgregarEquipo() {
    if (VALIDAR() == true) {
         var equipoObj = {
            Descripcion: $('#Descripcion_equipo').val()
        };

        $.ajax({
            url: "/Equipo/AgregarEquipo",
            data: JSON.stringify(equipoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Agregado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El equipo fue agregado correctamente!",
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

                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un equipo con la descripción: " + $('#Descripcion_equipo').val() + " !");
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE AgregarEquipo

function ModificarEstado(ID) {

    $.ajax({
        url: "/Equipo/ModificarEstado/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Modificado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡El estado del equipo cambió correctamente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            CARGAR_GRID();
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

}//FIN DE ModificarEstado


//function getbyID(ID) {
//    //cargarAgregar();
//    $.ajax({
//        url: "/Equipo/consultar/" + ID,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {

//            $('#ID_Equipo').val(result.ID_Equipo);
//            $('#Descripcion').val(result.Descripcion);
//            $('#myModal').modal('show');
//            $('#btnUpdate').show();
//            $('#btnAdd').hide();
//            $("#ID_Equipo").prop("disabled", true);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//    return false;
//}

//function Update() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var equipoObj = {
//        ID_Equipo: $('#ID_Equipo').val(),
//        Descripcion: $('#Descripcion').val(),
//    };
//    $.ajax({
//        url: "/Equipo/Actualizar",
//        data: JSON.stringify(equipoObj),
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
//function for deleting employee's record


function clearTextBox() {
    $('#ID_Equipo').val("");
    $('#Descripcion').val("");   
 }


