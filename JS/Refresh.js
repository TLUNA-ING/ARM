$(document).ready(function () {
    Refrescar(65000);
});

function Refrescar(TIEMPO) {
    setInterval(Refrescar_Pagina, TIEMPO);
}

function Refrescar_Pagina() {
    $.ajax({
        url: "/Home/ValidarRefrescar",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result == "SI") {
                location.reload(true);
            }
        },
        error: function (errormessage) {
            location.reload(true);
        }
    });
}