//Toggling projects in create assignment
function myProjects() {
    var checkBox = document.getElementById("IsProjectRelated");
    var projectsList = document.getElementById("toggle-checkbox");
    if (checkBox.checked == false) {
        $("#ProjectId").prop("disabled", true);
        $(".selectpicker[data-id='ProjectId']").addClass("disabled");
        $('select[name=ProjectId]').val(0);
        $('.selectpicker').selectpicker('refresh')
        projectsList.disabled = true;
    } else {
        projectsList.disabled = false;
        $("#ProjectId").prop("disabled", false);
        $(".selectpicker[data-id='ProjectId']").removeClass("disabled");
        $('.selectpicker').selectpicker('refresh')

    }
}