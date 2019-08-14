//Change Participant Additional Data


$(".employee-id").on('change', function () {
    var contentPanelId = $(this).attr("id");
    alert(contentPanelId);
    console.log(contentPanelId);
});


$("#employee-department-list").on('change', function () {
    var val = $(this).val();

    $.get({
        url: `/Management/Employees/LoadProjectParticipantAdditionalData?employeeId=${val}`,
        success: function success(result) {
            importParticipantAdditionalInfo(result);
        },
        error: function error() {
            alert('Please select an employee!');
            resetParticipantAddionalInfo();
        }
    });
})

function importParticipantAdditionalInfo(data) {
    $('#participant-picture').attr('src', data.profilePicture);
    $('#participant-department').html(data.departmentName);
}

function resetParticipantAddionalInfo() {
    $('#participant-picture').attr('src', '/images/unknownEmployee.png');
    $('#participant-department').html('-');
}








//Adding and deleting rows from adding participants
function removeRow() {
    $(event.target).closest("tr").remove();
}

function addRow() {
    var i = $(".participants").length;

    var projectPositionsList;
    var employeesDepartmentsList;

    $.get({
        url: `/Management/Projects/GetEmployeesDepartmentViewComponent`,
        async: false,
        success: function success(result) {
            employeesDepartmentsList = result;
        },
    });

    $.get({
        url: `/Management/Projects/GetProjectPositionsViewComponent`,
        async: false,
        success: function success(result) {
            projectPositionsList = result;
        },
    });

    var rowToAppend = ' <tr class="participants"> <td class="align-middle" style="width: 5%"><img src="/images/unknownEmployee.png" id="participant-picture" class="proj-participant-img rounded-circle mx-left " alt="..."></td> <td class="align-middle" style="width: 28.32%"> <div class="input-group mb-2"> <div class="input-group">' + employeesDepartmentsList + ' </div> </div> </td> <td id="participant-department" class="align-middle text-center">-</td> <td class="align-middle">' + projectPositionsList + '</td> <td class="align-middle"> <button type="button" class="btn btn-danger" onclick="removeRow()">Remove</button> </td> </tr>';
    rowToAppend = rowToAppend.replace('id="ProjectPositionId"', 'id="ProjectPositionId-' + i + '"');
    rowToAppend = rowToAppend.replace('name="ProjectPositionId"', 'name = "Participants[' + i + '].ProjectPositionId"');
    rowToAppend = rowToAppend.replace('id="EmployeeId"', 'id="EmployeeId-' + i + '"');
    rowToAppend = rowToAppend.replace('name="EmployeeId"', 'name = "Participants[' + i + '].EmployeeId"');
    rowToAppend = rowToAppend.replace('participant-picture', 'participant-picture-' + i);
    rowToAppend = rowToAppend.replace('participant-department', 'participant-department-' + i);

    $("table").append(rowToAppend);
    $('.selectpicker').selectpicker('refresh')
}