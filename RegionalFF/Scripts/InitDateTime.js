
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
$('#Desde, #Hasta').datetimepicker({
    icons: {
        time: "fa fa-clock-o",
        date: "fa fa-calendar",
        up: "fa fa-arrow-up",
        down: "fa fa-arrow-down"
    },
    locale: 'es',
    format: 'DD/MM/YYYY'
});
