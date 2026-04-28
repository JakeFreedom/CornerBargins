
//This will be the global token. No need to get it in every .js file when doing a ajax call back
var token = $("input[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    BuildLoginButton();

});

function BuildLoginButton() {

    $("#btnLogin").click(function () {
        var token = $("input[name='__RequestVerificationToken']").val();
        //Get User Name
        var userName = $("#txtUserName").val()
        var password = $("#txtPassword").val()

        $.ajax(
            {
                method: "POST",
                headers: { "RequestVerificationToken": token },
                url: "Login/Index?handler=CheckLogin",
                data: {
                    UserName: userName,
                    Password: password
                },
                datatype: "text",
                success: function (data) {
                    //I don't like this
                    window.location.href = "https://" + window.location.host + "/Tools/Manage"
                },
                error: function (x, r, y) { }
            });

    });
}