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

function OpenEditActivityModal(activityId) {
    $.ajax({
        type: "Get",
        url: '/Activities/Edit',
        data: { id: activityId },
        success: function (data) {
            $('#PreviewEditActivityForm').html(data);
            loadFlatpickr();
            $('#editActivity').modal('show');
        }
    });
}

function editActivity() {
    $.ajax({
        type: "Post",
        url: '/Activities/Edit',
        data: $("#formContent").serialize(),
        success: function (result) {
            if (result.success) {
                window.location.href = result.url;
            } else {
                $('#PreviewEditActivityForm').html(result);
                loadFlatpickr();
            }
        }
    });
    return false;
};

function loadFlatpickr() {
    flatpickr.l10ns.default.firstDayOfWeek = 1;

    flatpickr(".flatpickrcontainer", {
        wrap: true,
        weekNumbers: true,
        dateFormat: "d/m/Y",
        maxDate: "today"
    });

    flatpickr(".flatpickr-hours", {
        dateFormat: "H:i",
        wrap: true,
        enableTime: true,
        noCalendar: true,
        time_24hr: true,
    });
}
