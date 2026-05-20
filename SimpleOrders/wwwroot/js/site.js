// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var token = $("input[name='__RequestVerificationToken']").val();
// Write your JavaScript code.
$(document).ready(function () {

});


//function call back to rehide the update cart div
function HideMessage() {
    $("#divDisplayMessage").addClass("collapse");
}


function ShowFullImage(itemID, itemName) {

    //Make ajax call to load the image up, we are doing it this way to keep the site speed up.
    var token = $("input[name='__RequestVerificationToken']").val();
    $.ajax(
        {
            method: "Post",
            headers: { "RequestVerificationToken": token },
            //url: "TagWriter/Index?handler=SaveTag",
            url: "Items/Index?handler=ShowFullImage",
            data: {
                ItemID: itemID,
                ItemName: itemName
            },
            datatype: "text",
            success: function (data) {
                var newModal = $(data).find("#mdlFullImage");
                $("#mdlFullImage").replaceWith(newModal);
                $("#mdlFullImage").modal('show');
                //console.log(data);

            },
            error: function (x, r, y) {
                $("#mdlFullImage").modal('show');
            }
        });
    //alert(fullImage);
    //$("#imgFullImage").attr("src", fullImage);

}

