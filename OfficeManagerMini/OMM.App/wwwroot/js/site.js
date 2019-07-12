// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function myProjects() {
    var checkBox = document.getElementById("isProjectRelated");
    var projectsList = document.getElementById("toggle-checkbox");
    if (checkBox.checked == false) {
        $("#project").prop("disabled", true);
        $(".selectpicker[data-id='project']").addClass("disabled");
        $('select[name=project]').val(0);
        $('.selectpicker').selectpicker('refresh')
        projectsList.disabled = true;
    } else {
        projectsList.disabled = false;
        $("#project").prop("disabled", false);
        $(".selectpicker[data-id='project']").removeClass("disabled");
        $('.selectpicker').selectpicker('refresh')

    }
}

