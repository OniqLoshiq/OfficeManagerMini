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

    var id3 = document.getElementById("myOngoingAssignments");
    if (id3) {
        var table4 = $('#myOngoingAssignments').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis'],
            "aaSorting": []
        });
        table4.buttons().container()
            .appendTo('#myOngoingAssignments_wrapper .col-md-6:eq(0)');

        $('#myOngoingAssignments tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var tableSearch4 = $('#myOngoingAssignments').DataTable();

        // Apply the search
        tableSearch4.columns().eq(0).each(function (colIdx) {
            $('input', tableSearch4.column(colIdx).footer()).on('keyup change', function () {
                tableSearch4.column(colIdx)
                    .search(this.value.replace(/;/g, "|"), true, false)
                    .draw();
            });
        });
    }

    var id4 = document.getElementById("myCompletedAssignments");
    if (id4) {
        var table5 = $('#myCompletedAssignments').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis'],
            "aaSorting": []
        });
        table5.buttons().container()
            .appendTo('#myCompletedAssignments_wrapper .col-md-6:eq(0)');

        $('#myCompletedAssignments tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var tableSearch5 = $('#myCompletedAssignments').DataTable();

        // Apply the search
        tableSearch5.columns().eq(0).each(function (colIdx) {
            $('input', tableSearch5.column(colIdx).footer()).on('keyup change', function () {
                tableSearch5.column(colIdx)
                    .search(this.value.replace(/;/g, "|"), true, false)
                    .draw();
            });
        });
    }

    var id5 = document.getElementById("myOngoingProjects");
    if (id5) {
        var table6 = $('#myOngoingProjects').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis'],
            "aaSorting": []
        });
        table6.buttons().container()
            .appendTo('#myOngoingProjects_wrapper .col-md-6:eq(0)');

        $('#myOngoingProjects tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var tableSearch6 = $('#myOngoingProjects').DataTable();

        // Apply the search
        tableSearch6.columns().eq(0).each(function (colIdx) {
            $('input', tableSearch6.column(colIdx).footer()).on('keyup change', function () {
                tableSearch6.column(colIdx)
                    .search(this.value.replace(/;/g, "|"), true, false)
                    .draw();
            });
        });
    }

    var id6 = document.getElementById("myCompletedProjects");
    if (id6) {
        var table7 = $('#myCompletedProjects').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis'],
            "aaSorting": []
        });
        table7.buttons().container()
            .appendTo('#myCompletedProjects_wrapper .col-md-6:eq(0)');

        $('#myCompletedProjects tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var tableSearch7 = $('#myCompletedProjects').DataTable();

        // Apply the search
        tableSearch7.columns().eq(0).each(function (colIdx) {
            $('input', tableSearch7.column(colIdx).footer()).on('keyup change', function () {
                tableSearch7.column(colIdx)
                    .search(this.value.replace(/;/g, "|"), true, false)
                    .draw();
            });
        });
    }

    var id7 = document.getElementById("activities-list");
    if (id7) {
        var table8 = $('#activities-list').DataTable({
            buttons: ['copy', 'excel', 'pdf',
                {
                    extend: 'print',
                    exportOptions: {
                        stripHtml: false,
                    }
                },

                'colvis'],
            "aaSorting": []
        });
        table8.buttons().container()
            .appendTo('#activities-list_wrapper .col-md-6:eq(0)');

        $('#activities-list tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="' + title + '" />');
        });

        // DataTable
        var tableSearch8 = $('#activities-list').DataTable();

        // Apply the search
        tableSearch8.columns().eq(0).each(function (colIdx) {
            $('input', tableSearch8.column(colIdx).footer()).on('keyup change', function () {
                tableSearch8.column(colIdx)
                    .search(this.value.replace(/;/g, "|"), true, false)
                    .draw();
            });
        });
    }
});