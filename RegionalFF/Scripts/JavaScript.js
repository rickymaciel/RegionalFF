
    $(document).on("click", "[type='checkbox']", function (e) {
        if (this.checked) {
            $(this).attr("value", "true");
        } else {
            $(this).attr("value", "false");
        }
    });
    if (history.forward(1)) {
        location.replace(history.forward(1))
    }

    $("#Imagen").fileinput({
        theme: "fa",
        allowedFileExtensions: ["jpg", "png", "gif"],
        showCaption: false,
        showUpload: false,
        maxFileCount: 1
    });

    $('#datetimepicker6').datetimepicker({
        useCurrent: true,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        },
        locale: 'es',
        format: 'DD/MM/YYYY'
    });
    $('#datetimepicker7').datetimepicker({
        useCurrent: false,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        },
        locale: 'es',
        format: 'DD/MM/YYYY'
    });
    $("#datetimepicker6").on("dp.change", function (e) {
        $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
    });
    $("#datetimepicker7").on("dp.change", function (e) {
        $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
    });

    $('#Fecha').val('@ViewBag.Fecha');
    $('#UserId').val('@User.Identity.GetUserId()');

    $('#Fecha').datetimepicker({
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-arrow-up",
            down: "fa fa-arrow-down"
        },
        locale: 'es',
        format: 'DD/MM/YYYY HH:mm:ss'
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
    $('#mensaje').delay(2000).fadeOut(1000);