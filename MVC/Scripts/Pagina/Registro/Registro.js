var correoElectronicoDisponible = false;
var image = null;

function validarCorreoElectronicoDisponible() {
    var pmtPeticion = new Object();
    pmtPeticion.CorreoElectronico = $("#txtCorreoElectronico").val();
    $.ajax
        ({
            type: 'POST',
            data: pmtPeticion,
            url: urlValidarCorreoElectronicoDisponible,
            success: function (data) {
                if (data[0]) {
                    $("#txtCorreoElectronico").css({ 'border-color': '' });
                }
                else {
                    $("#txtCorreoElectronico").css("border-color", "red");
                }
                correoElectronicoDisponible = data[0];
            }
        });
}

function validarContraseña() {
    if ($("#txtConfirmarPassword").val() != $("#txtPassword").val()) {
        $("#txtConfirmarPassword").css("border-color", "red");
        return false;
    }
    else {
        $("#txtConfirmarPassword").css({ 'border-color': '' });
        return true;
    }
}

function llenarSelectTipoMascota() {
    var pmtPeticion = new Object();
    $.ajax
        ({
            type: 'POST',
            data: pmtPeticion,
            url: urlCatalogoTipoMascota,
            beforeSend: function () {
                MensajeCargando();
            },
            success: function (data) {
                bootbox.hideAll();
                $("#sctTipo").children('option:not(:first)').remove();
                for (i = 0; i < data.length; i++) {
                    $("#sctTipo").append($('<option>', {
                        value: data[i].ID,
                        text: data[i].Descripcion
                    }));
                }
            }
        });
}

function llenarSelectSexoMascota() {
    var pmtPeticion = new Object();
    $.ajax
        ({
            type: 'POST',
            data: pmtPeticion,
            url: urlCatalogoSexoMascota,
            beforeSend: function () {
                MensajeCargando();
            },
            success: function (data) {
                bootbox.hideAll();
                $("#sctSexo").children('option:not(:first)').remove();
                for (i = 0; i < data.length; i++) {
                    $("#sctSexo").append($('<option>', {
                        value: data[i].ID,
                        text: data[i].Descripcion
                    }));
                }
            }
        });
}

function limpiarFormulario() {
    $("#txtNombre").val('');
    $("#txtDireccion").val('');
    $("#txtNombreMascota").val('');
    $("#sctTipo").prop('selectedIndex', 0);
    $("#txtRaza").val('');
    $("#sctSexo").prop('selectedIndex', 0);
    $("#txtCorreoElectronico").val('');
    $("#txtPassword").val('');
    $("#txtConfirmarPassword").val('');
    $("#fileImagen").val('');
    $("#myImg").attr("src", "../../Recursos/ImagenesPerfil/noImage.jpg");  
}

function registrar() {
    var pmtPeticion = new FormData();
    if ($("#txtNombre").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#txtDireccion").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#txtNombreMascota").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#sctTipo option:selected").val() == "0") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#txtRaza").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#sctSexo option:selected").val() == "0") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#txtCorreoElectronico").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if (!correoElectronicoDisponible) {
        bootbox.alert
            ({
                message: '<center><label>El correo electr&oacute;nico no est&aacute; disponible.</label></center>'
            });
        return;
    }
    if ($("#txtPassword").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if ($("#txtConfirmarPassword").val() == "") {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    if (!validarContraseña()) {
        bootbox.alert
            ({
                message: '<center><label>Las contrase&ntilde;as no coinciden.</label></center>'
            });
        return;
    }
    if (image == null) {
        bootbox.alert
            ({
                message: '<center><label>Aseg&uacute;rate de llenar todos los campos correctamente.</label></center>'
            });
        return;
    }
    pmtPeticion.append("Nombre", $("#txtNombre").val());
    pmtPeticion.append("Direccion", $("#txtDireccion").val());
    pmtPeticion.append("NombreMascota", $("#txtNombreMascota").val());
    pmtPeticion.append("IDTipoMascota", $("#sctTipo option:selected").val());
    pmtPeticion.append("Raza", $("#txtRaza").val());
    pmtPeticion.append("IDSexo", $("#sctSexo option:selected").val());
    pmtPeticion.append("CorreoElectronico", $("#txtCorreoElectronico").val());
    pmtPeticion.append("Password", $("#txtConfirmarPassword").val());
    pmtPeticion.append("UploadImagen", image);
    $.ajax
        ({
            type: 'POST',
            data: pmtPeticion,
            url: urlRegistrar,
            contentType: false,
            processData: false,
            beforeSend: function () {
                MensajeCargando();
            },
            success: function (data) {
                bootbox.hideAll();
                if (data.Exitoso) {
                    bootbox.alert
                        ({
                            message: '<center><label>Tu cuenta se ha creado con &eacute;xito. Inicia sesi&oacute;n para continuar.</label></center>'
                        });
                    limpiarFormulario();
                }
            }
        });
}

$(document).ready(function () {
    llenarSelectTipoMascota();
    llenarSelectSexoMascota();
    $('#txtCorreoElectronico').on('input', function (e) {
        validarCorreoElectronicoDisponible();
    });
    $('#txtConfirmarPassword').on('input', function (e) {
        validarContraseña();
    });
    $("#btnRegistrar").click(function () {
        registrar();
    });
    $('input[type="file"]').change(function () {
        var imagePreview = document.getElementById('myImg');
        image = event.target.files[0];
        imagePreview.src = URL.createObjectURL(event.target.files[0]);
    }); 
});