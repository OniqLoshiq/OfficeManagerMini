﻿//Adding and deleting rows from adding participants
function removeRow() {
    $(event.target).closest("tr").remove();
}

function addRow() {
    $("table").append(
        '<tr>' +
        '<td class="align-middle" style="width: 5%"><img src="~/images/avatar.jpg" class="proj-participant-img rounded-circle mx-left " alt="..."></td>' +
        '<td class="align-middle" style="width: 28.32%">' +
        '<div class="input-group mb-2">' +
        '<select class="form-control selectpicker show-tick" name="participant" data-style="btn-white" data-live-search="true" data-width="auto">' +
        '<option selected disabled></option>' +
        '<optgroup label="Management Board">' +
        '<option>Oniq</option>' +
        '<option>Loshiq</option>' +
        '<option>Toq</option>' +
        '</optgroup>' +
        '<optgroup label="HR">' +
        '<option>Oniq</option>' +
        '<option>Loshiq</option>' +
        '<option>Toq</option>' +
        '</optgroup>' +
        '<optgroup label="Accounting">' +
        '<option>Oniq</option>' +
        '<option>Loshiq</option>' +
        '<option>Toq</option>' +
        '</optgroup>' +
        '<optgroup label="Engineering">' +
        '<option>Oniq</option>' +
        '<option>Loshiq</option>' +
        '<option>Toq</option>' +
        '</optgroup>' +
        '<optgroup label="Administration">' +
        '<option>Oniq</option>' +
        '<option>Loshiq</option>' +
        '<option>Toq</option>' +
        '</optgroup>' +
        '</select>' +
        '</div>' +
        '</td>' +
        '<td class="align-middle">Department</td>' +
        '<td class="align-middle">' +
        '<select class="form-control" id="projectPosition">' +
        '<option disabled selected></option>' +
        '<option>Project Manager</option>' +
        '<option>Participant</option>' +
        '<option>Assistant</option>' +
        '</select>' +
        '</td>' +
        '<td class="align-middle">' +
        '<button type="button" class="btn btn-danger" onclick="removeRow()">Remove</button>' +
        '</td>' +
        '</tr>'
    );
    $('.selectpicker').selectpicker('refresh')
}