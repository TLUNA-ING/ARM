function mostrarMenu() {
    
    $(principal).removeClass("col-sm-12");
    $(principal).removeClass("col-md-12");
    $(principal).addClass("col-sm-10");
    $(principal).addClass("col-md-10");
    $(menu).show();
    $(btnMenu).attr('onclick', 'eliminarMenu()');
}

function eliminarMenu() {
    $(menu).hide();
    $(principal).removeClass("col-sm-10");
    $(principal).removeClass("col-md-10");
    $(principal).addClass("col-sm-12");
    $(principal).addClass("col-md-12");
    $(btnMenu).attr('onclick', 'mostrarMenu()');
}