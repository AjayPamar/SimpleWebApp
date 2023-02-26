
$(document).ready(function () {
    $("#UsrTbl").DataTable();
})

function CreateOrEdit(Id) {
    if (Id == null) {
        $.ajax({
            type: "POST",
            url: "/Usercrud/CreateUser",
            dataType: "html",
            success: function (res) {
                $("#Usrform").find(".modal-body").html(res);
                $("#Usrform").find(".modal-title").text("Ceate a new user");
                $("#Usrform").modal("show");
            },
            error: function (res) {
                console.log(res.responseText);
            },
            failure: function (res) {
                console.log(res.responseText);
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/UserCRUD/EditUser",
            data: { "Id": Id },
            dataType: "html",
            success: function (res) {
                $("#Usrform").find(".modal-body").html(res);
                $("#Usrform").find(".modal-title").text("Update user details");
                $("#Usrform").modal("show");
            },
            error: function (res) {
                console.log(res.responseText);
            },
            failure: function (res) {
                console.log(res.responseText);
            }
        })
    }
}

function DeleteRecord(Id) {
    var confirm = window.confirm("Do you want to delete selected record?")
    if (confirm) {
        $.ajax({
            type: "POST",
            url: "/UserCRUD/DeleteUser",
            data: { "Id": Id },
            dataType: "html",
            success: function (res) {
                alert("Record deleted succcessfully");
                location.reload();
            },
            error: function (res) {
                alert("Error occured while deleting record");
            },
            failure: function (res) {
                alert("Delete user recrod operation failed");
            }
        })
    }
}
