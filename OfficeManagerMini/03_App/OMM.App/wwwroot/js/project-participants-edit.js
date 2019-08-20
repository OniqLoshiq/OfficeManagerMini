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
                window.location.href = result.url;
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
        data: $("#formContent").serialize(),
        success: function (result) {
            if (result.success) {
                $('#changeProjectPosition').on('hidden.bs.modal', function (e) {
                    $("tr:contains('" + result.participantName + "') td:eq(4)").text(result.position);
                });

                $('#changeProjectPosition').modal('hide');
            } else {
                $('#PreviewChangePosition').html(result);
            }
        }
    });
    return false;
};


function OpenRemoveParticipantModal(projectId, participantId, projectPositionId) {
    $.ajax({
        type: "Get",
        url: '/Projects/RemoveParticipant',
        data: { projectId: projectId, participantId: participantId, projectPositionId: projectPositionId },
        success: function (data) {
            $('#PreviewRemoveParticipant').html(data);
            $('#removeParticipant').modal('show');
        }
    });
}

function removeParticipant() {
    $.ajax({
        type: "Post",
        url: '/Projects/RemoveParticipant',
        data: $("#formContent").serialize(),
        success: function (result) {
            if (result.success) {
                $('#removeParticipant').on('hidden.bs.modal', function (e) {

                    $("#participants-table td").filter(function () {
                        return $(this).text() === result.participantName;
                    }).closest('tr').remove();

                    var participant = 0;

                    $('#participants-table tr').each(function () {
                        $(this).find("td").eq(0).text(participant++);
                    });
                });
                $('#removeParticipant').modal('hide');
            } else {
                $('#PreviewRemoveParticipant').html(result);
            }
        }
    });
    return false;
};
