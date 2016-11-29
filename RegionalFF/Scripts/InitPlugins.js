
$(function () {
    $(document).on("click", "[type='checkbox']", function (e) {
        if (this.checked) {
            $(this).attr("value", "true");
        } else {
            $(this).attr("value", "false");
        }
    });

    $("#Imagen").fileinput({
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        overwriteInitial: true,
        maxFileSize: 1000,
        maxFilesNum: 2,
        theme: "gly",
        showCaption: false,
        showUpload: false,
        maxFileCount: 1,
        browseClass: "btn btn-primary",
        fileType: "any",
        maxImageWidth: 600,
        maxImageHeight: 600,
        resizePreference: 'height',
        resizeImage: true,
    });

    $('#selectize, #Roles, #CiudadId, #PaisId, #OficinaId, #TransporteId, #MarcaId, #ConductorId, #UserId').selectize({
        allowEmptyOption: true,
        create: false,
        persist: false,
        createOnBlur: false
    });

    $('input').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'icheckbox_minimal-blue',
        increaseArea: '20%' // optional
    });

    $('#mensaje').delay(1000).fadeOut(500);
});