function validarCorreoElectronico() {
    var pmtPeticion = new Object();
    pmtPeticion.correoElectronico = $("#txtCorreoElectronico").val();
    $.ajax
        ({
            type: 'POST',
            data: pmtPeticion,
            url: urlValidarCorreoElectronico,
            success: function (data) {
                if (data[0]) {
                    $("#txtCorreoElectronico").css({ 'border-color': '' });
                }
                else {
                    $("#txtCorreoElectronico").css("border-color", "red");
                }
            }
        });
}

$(document).ready(function () {
    $('#txtCorreoElectronico').on('input', function (e) {
        validarCorreoElectronico();
    });
});