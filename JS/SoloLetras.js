$(document).ready(function () {

    $("#Nombre").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });

    $("#Primer_Apellido").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });


    $("#Segundo_Apellido").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });


    $("#nombre_cliente").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });

    $("#Desc_Tipo").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });

    $("#Desc_Depa").keypress(function (e) {
        var keyCode = e.keyCode || e.which;
        var regex = /^[A-Za-z]+$/;
        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        return isValid;
    });
});