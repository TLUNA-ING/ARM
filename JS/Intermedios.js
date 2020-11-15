$(document).ready(function () {
    ActualizarCombobox();
    URL = "";
});

function ActualizarCombobox() {
    var SELECCIONADO = parseFloat($("#tabla option:selected").val());
    LimpiarComboBox();

    if (isNaN(SELECCIONADO)) {
        LimpiarTablas();
    }else if (SELECCIONADO == 0) { //LIGAR CLIENTES
        ConsultarClientes();
    } else if (SELECCIONADO == 1) {
        ConsultarDepartamentos();
    }
    $('#intermedios').val(-1);   
}//ActualizarCombobox

function LimpiarTablas() {
    $("#DatosNOLigados td").remove();
    $("#DatosLigados td").remove();
}// FIN DE LimpiarTablas
    
function ConsultarClientes() {
    $.ajax({
        url: "/Intermedio/ConsultarClientes",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => {
                plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
            });

            $("#intermedios").html(plantilla);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE CargarClientes

function ConsultarDepartamentos() {
    $.ajax({
        url: "/Intermedio/ConsultarDepartamentos",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            var plantilla = '<option>-- Seleccione una opción --</option>';
            result.forEach(valor => {
                plantilla += `<option value="${valor.Value}">${valor.Text}</option>`
            });

            $("#intermedios").html(plantilla);
        },
        error: function (errormessage) {
        }
    });
}//FIN DE ConsultarDepartamentos


var URL = "";
var TABLA_LIGAR = "";
var CODIGO = ""
var ACCION_LIGAR = "";
var ACCION_DESLIGAR = "";

function LimpiarComboBox() {
    var select = document.getElementById("intermedios");

    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }
}//FIN DE LimpiarComboBox


function ActualizarTablas() {
    TABLA_LIGAR = parseFloat($("#tabla option:selected").val());
    CODIGO = parseFloat($("#intermedios option:selected").val());
    URL = "/Intermedio/CargarDatosTablas";


    if (isNaN(CODIGO) == false) {
        ACCION_LIGAR = "";
        ACCION_DESLIGAR = "";

        if (TABLA_LIGAR == 0) {
            ACCION_LIGAR = "LigarDepartamento";
            ACCION_DESLIGAR = "DesligarDepartamento"
        } else if (TABLA_LIGAR == 1) {
            ACCION_LIGAR = "LigarEquipo";
            ACCION_DESLIGAR = "DesligarEquipo"
        }

        if (URL != "" && ACCION_LIGAR != "" && ACCION_DESLIGAR != "") {
            Cargar_NO_Ligados();
            Cargar_Ligados();
        }
    } else {
        LimpiarTablas();
    }
}//FIN DE ActualizarTablas

function Cargar_NO_Ligados() {
    var table = $('#DatosNOLigados').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: URL,
            data:
            {
                TABLA: TABLA_LIGAR,
                ID: CODIGO,
                IND_LIGADO: "N"
            },
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
        },
        columns: [
            { "data": "ID" },
            { "data": "Descripcion" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick =" + ACCION_LIGAR +"(" + row.ID + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            }

        ]
    });
}//FIN DE Cargar_NO_Ligados

function Cargar_Ligados() {
    var table = $('#DatosLigados').dataTable({
        destroy: true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: true,
        scrollCollapse: true,
        ajax: {
            url: URL,
            data:
            {
                TABLA: TABLA_LIGAR,
                ID: CODIGO,
                IND_LIGADO: "S"
            },
            method: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            autoWidth: false,
            dataSrc: ""
        },
        columns: [
            { "data": "ID" },
            { "data": "Descripcion" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return "<div style='text-align:center'><button type ='button' class='btn btn-default btn-circle waves-effect' onclick ="+ ACCION_DESLIGAR+"(" + row.ID + ") > " +
                        "<i class='material-icons'>create</i>" +
                        " </button ></div>"
                }
            }

        ]
    });
}//FIN DE Cargar_Ligados

function LigarDepartamento(ID) {

    var DepartamentoXClienteObj = {
        ID_Departamento: ID,
        ID_Cliente: parseFloat($("#intermedios option:selected").val()),
    };

    $.ajax({
        url: "/Intermedio/LigarDepartamento",
        data: JSON.stringify(DepartamentoXClienteObj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Ligado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡Se ligó correctamente el departamento al cliente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            Cargar_NO_Ligados();
                            Cargar_Ligados();
                        }
                    });
            } else if ("Existe") {
                Cargar_NO_Ligados();
                Cargar_Ligados();
            } else {
                swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}//FIN DE LigarDepartamento

function DesligarDepartamento(ID) {

    var DepartamentoXClienteObj = {
        ID_Departamento: ID,
        ID_Cliente: parseFloat($("#intermedios option:selected").val()),
    };

    $.ajax({
        url: "/Intermedio/DesligarDepartamento",
        data: JSON.stringify(DepartamentoXClienteObj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "Desligado") {
                swal({
                    title: "¡Acción realizada!",
                    text: "¡Se desligó correctamente el departamento del cliente!",
                    type: "success",
                    confirmButtonColor: "#10AF5D",
                    confirmButtonText: "Aceptar"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            Cargar_NO_Ligados();
                            Cargar_Ligados();
                        }
                    });
            } else if ("No Existe") {
                Cargar_NO_Ligados();
                Cargar_Ligados();
            } else {
                swal("¡Error!", "¡Ocurrió un error, intentelo más tarde!", "error");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}//FIN DE DesligarDepartamento

function LigarEquipo(ID) {

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

function DesligarEquipo(ID) {

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