// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Open(projectId) {
    $.ajax({
        type: "Get",
        url: '/Projects/AddParticipant',
        data: { id: projectId },
        success: function (data) {
            $('#Preview').html(data);
            $('.selectpicker').selectpicker('refresh');
            $('#addParticipant').modal('show');
        }
    });
}

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

function OpenChangePositionModal(projectId, participantId, projectPositionId) {
    $.ajax({
        type: "Get",
        url: '/Projects/ChangeProjectPosition',
        data: { projectId: projectId, participantId: participantId, projectPositionId: projectPositionId },
        success: function (data) {
            $('#PreviewChangePosition').html(data);
            $('#changeProjectPosition').modal('show');
        }
    });
}

function changeProjectPosition() {
    $.ajax({
        type: "Post",
        url: '/Projects/ChangeProjectPosition',
        data: $('#formContent').serialize(),
        success: function (result) {
            if (result.success) {

            } else {
                $('#PreviewChangePosition').html(result);
            }
        }
    });
    return false;
};


