//Change Participant Additional Data
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
    $("table").append(
        ' <tr> <td class="align-middle" style="width: 5%"><img src="~/images/unknownEmployee.png" id="participant-picture" class="proj-participant-img rounded-circle mx-left " alt="..."></td> <td class="align-middle" style="width: 28.32%"> <div class="input-group mb-2"> <div class="input-group"> <vc:employees-department-list employee-id=""></vc:employees-department-list> <span asp-validation-for="EmployeeId" class="text-danger"></span> </div> </div> </td> <td id="participant-department" class="align-middle text-center">-</td> <td class="align-middle"> <select class="form-control" asp-for="ProjectPositionId" asp-items="@ViewBag.ProjectPositions"> <option selected disabled>Please select one</option> </select> </td> <td class="align-middle"> <button type="button" class="btn btn-danger" onclick="removeRow()">Remove</button> </td> </tr>'
    );
    $('.selectpicker').selectpicker('refresh')
}