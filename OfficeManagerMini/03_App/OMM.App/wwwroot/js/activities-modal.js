function OpenCreateActivityModal(reportId) {
    $.ajax({
        type: "Get",
        url: '/Activities/Create',
        data: { reportId: reportId },
        success: function (data) {
            $('#PreviewActivityForm').html(data);

            loadFlatpickr();

            $('#createActivity').modal('show');
        }
    });
}

function createActivity() {
    $.ajax({
        type: "Post",
        url: '/Activities/Create',
        data: $("#formContent").serialize(),
        success: function (result) {
            if (result.success) {
                window.location.href = result.url;
            } else {
                $('#PreviewActivityForm').html(result);
                loadFlatpickr();
            }
        }
    });
    return false;
};


function loadFlatpickr() {
    flatpickr.l10ns.default.firstDayOfWeek = 1;

    $(".flatpickrcontainer").flatpickr({
        wrap: true,
        weekNumbers: true,
        dateFormat: "d-m-Y",
        maxDate: "today"
    });

    $(".flatpickr-hours").flatpickr({
        wrap: true,
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        time_24hr: true,
    });
}
