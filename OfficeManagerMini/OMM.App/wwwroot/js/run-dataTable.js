$(document).ready(function () {
    var id0 = document.getElementById("myDataTable");
    if (id0) {
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
    }

    var id1 = document.getElementById("myDataTableInactiveEmpl");
    if (id1) {
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

    var id2 = document.getElementById("myDataTableReleasedItems");
    if (id2) {
        var table3 = $('#myDataTableReleasedItems').DataTable({
            buttons: ['pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis']
        });

        table3.buttons().container()
            .appendTo('#myDataTableReleasedItems_wrapper .col-md-6:eq(0)');
    }


});