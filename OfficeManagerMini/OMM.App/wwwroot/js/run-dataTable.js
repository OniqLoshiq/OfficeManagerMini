$(document).ready(function () {
    var table = $('#myDataTable').DataTable({
        buttons: ['copy', 'excel', 'pdf',
            {
                extend: 'print',
                exportOptions: {
                    stripHtml: false,
                }
            },

            'colvis']
    });

    table.buttons().container()
        .appendTo('#myDataTable_wrapper .col-md-6:eq(0)');

    var id = document.getElementById("myDataTableInactiveEmpl");
    if (id) {
        var table2 = $('#myDataTableInactiveEmpl').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis']
        });

        table2.buttons().container()
            .appendTo('#myDataTableInactiveEmpl_wrapper .col-md-6:eq(0)');
    }
});