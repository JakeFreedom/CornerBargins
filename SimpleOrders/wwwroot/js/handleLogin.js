
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

        $.ajax(
            {
                method: "GET",
                headers: { "RequestVerificationToken": token },
                //url: "TagWriter/Index?handler=SaveTag",
                url: "Index?handler=CheckLogin",
                data: {
                    strUserName:userName
                },
                datatype: "text",
                success: function (data) {
                    location.reload();
                },
                error: function (x, r, y) { }
            });

    });
}