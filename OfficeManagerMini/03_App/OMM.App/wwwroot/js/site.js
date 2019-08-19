// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addParticipant() {
    $.ajax({
        type: "Post",
        url: '/Projects/AddParticipant',
        data: $("#fotmContent").serialize(),
        success: function (result) {
            if (result.success) {

            } else {
                $('#Preview').html(result);
                $('.selectpicker').selectpicker('refresh');
            }
        }
    });
    return false;
};


