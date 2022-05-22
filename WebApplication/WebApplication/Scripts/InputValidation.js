$(".submit").click(function () {
    debugger;
    var name = $("#Name").val();
    var email = $("#Email").val();
    var phone = $("#Phone").val();
    var validPhone = true;
    var validName = true;
    var validEmail = true;

    if (name == "" || name == null) {
        $("#NameVal").html("Please enter name.");
        validName = false;
    }
    else {
        validName = true;
        $("#NameVal").html("");
    }

    if (email == "" || email == null) {
        $("#EmailVal").html("Please enter email Id.");
        validEmail = false;
    }
    else {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        validEmail = expr.test(email);
        if (validEmail)
            $("#EmailVal").html("");
        else
            $("#EmailVal").html("Please enter valid email");
    }
    if (phone == "" || phone == null) {
        $("#PhoneVal").html("Please enter phone number.");
        validPhone = false;
    }
    else {
        if (phone.length != 10) {
            $("#PhoneVal").html("Please enter valid 10 digit phone number.");
            validPhone = false;
        }
        else {
            $("#PhoneVal").html("");
            validPhone = true;
        }
    }

    if (validEmail && validName && validPhone)
        return true;
    else
        return false;
});

$(document).ready(function () {
    $("#Phone").keypress(function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            evt.preventDefault()
        }
    })
})

$(".cancle").click(function () {
    $("#form").modal("hide");
})