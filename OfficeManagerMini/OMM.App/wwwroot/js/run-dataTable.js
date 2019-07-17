$(document).ready(function () {
    var table = $('#myDataTable').DataTable({
        buttons: ['copy', 'excel', 'pdf', 'colvis']
    });

    table.buttons().container()
        .appendTo('#myDataTable_wrapper .col-md-6:eq(0)');
});