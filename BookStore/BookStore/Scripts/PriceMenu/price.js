$("#RegisterViewModel_UserTypeId").change(function () {
    var selectedValue = $('#RegisterViewModel_UserTypeId :selected').text();
    var teacherDiv = $("#teacher");
    var studentDiv = $("#student");
    if (selectedValue == "Teacher") {
        teacherDiv.show();
        studentDiv.hide();
    } else {
        teacherDiv.hide();
        studentDiv.show();
    }

});