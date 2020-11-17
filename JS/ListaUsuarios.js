$(document).ready(function () {
    CargarGrid();
});

function CargarGrid() {
    var table = $('#DatoUsuario').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: "/Usuario/CargarDatos",
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: "",
            stripeClasses: []
        },
        columns: [
            { "data": "Empleado.Cedula" },
            { "data": "Password" },
            { "data": "Rol.Rol" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick = ConsultarUsuario(" + row.Empleado.Cedula + ") > " +
                            "<i class='material-icons'>create</i>"+
                                " </button ></div>"
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type='button' class='btn btn-default btn-circle waves-effect' onclick= P_ModificarEstado(" + row.Empleado.Cedula + ")>" +
                        "<i class='material-icons'>visibility</i>" +
                        "</button ></div> "
                }
            }

        ]
    });
}//FIN DE CargarGrid


function VALIDAR() {
    var ENTRAR = false;

    if (isNaN($("#empleado option:selected").val())) {
        MENSAJE_WARNING("¡Debe seleccionar una cédula de empleado, por favor revise los datos brindados!");
    } else if (isNaN($("#rol option:selected").val())) {
        MENSAJE_WARNING("¡Debe seleccionar un rol de usuario, por favor revise los datos brindados!");
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

function ActivarUsuario() {
    if (VALIDAR() == true) {
        var usrObj = {
            Empleado: { Cedula: $("#empleado option:selected").val() },
            Rol: { ID_Rol: $("#rol option:selected").val()}
        };

        $.ajax({
            url: "/Usuario/ActivarUsuario",
            data: JSON.stringify(usrObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result == "Activado") {
                    swal({
                        title: "¡Acción realizada!",
                        text: "¡El usuario fue activado correctamente!",
                        type: "success",
                        confirmButtonColor: "#10AF5D",
                        confirmButtonText: "Aceptar"
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                CargarGrid();
                                $('#myModal').modal('hide');
                                clearTextBox();
                            }
                        });

                } else if (result == "Existe") {
                    MENSAJE_WARNING("¡Ya existe un usuario activado con la cédula " + $("#empleado option:selected").val() + " !");
                    LimpiarComboBox();
                    CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO();
                } else {
                    swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}//FIN DE ActivarUsuario

function cargarAgregar() {
    CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO();
    CARGAR_COMBO_ROL();
    clearTextBox();
}//FIN DE cargarAgregar

function CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO() {
$.ajax({
    url: "/Usuario/CARGAR_EMPLEADOS",
    type: "POST",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {

        var plantilla = '';
        result.forEach(valor => {

            plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
        });

        $("#empleado").html(plantilla);

    },
    error: function (errormessage) {
    }
});
}//FIN DE CARGAR_COMBO_EMPLEADOS_PENDIENTES_USUARIO

function LimpiarComboBox() {
    var select = document.getElementById("empleado");

    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }
}//FIN DE LimpiarComboBox

function CARGAR_COMBO_ROL() {
$.ajax({
    url: "/Usuario/CARGAR_ROLES",
    type: "POST",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
        var roles = '';
        result.forEach(valor => {roles += `<option value="${valor.Value}">${valor.Text}</option>`});
        $("#rol").html(roles);

    },
    error: function (errormessage) {
    }
});
}//FIN DE CARGAR_COMBO_ROL

function clearTextBox() {
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}//FIN DE cargarAgregar
