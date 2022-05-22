$(document).ready(function () {
    $("#user").DataTable();
    $("#form").modal("hide");
})

function CreateOrEdit(Id) {
    if (Id == null) {
        $.ajax({
            type: "GET",
            url: "/CRUD/Create",
            dataType: "html",
            success: function (res) {
                $("#form").find(".modal-body").html(res);
                $("#form").find(".modal-title").text("Ceate a new user");
                $("#form").modal("show");
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
            type: "GET",
            url: "/CRUD/Edit",
            data: { "Id": Id },
            dataType: "html",
            success: function (res) {
                $("#form").find(".modal-body").html(res);
                $("#form").find(".modal-title").text("Update user details");
                $("#form").modal("show");
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
    $.ajax({
        type: "GET",
        url: "/CRUD/Delete",
        data: { "Id": Id },
        dataType: "html",
        success: function (res) {
            $("#form").find(".modal-body").html(res);
            $("#form").find(".modal-title").text("Should we delete following record?");
            $("#form").modal("show");
        },
        error: function (res) {
            console.log(res.responseText);
        },
        failure: function (res) {
            console.log(res.responseText);
        }
    })
}
