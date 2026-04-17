// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var token = $("input[name='__RequestVerificationToken']").val();
// Write your JavaScript code.
$(document).ready(function () {
    GetCartContents();

    $("#btnPlaceOrder").click(function () {

        //Get PO, Comment, email address and comedge customer number
        //We will use the customer number to verify after we make the call to the server
        var poNum = $("#txtPONumber").val();
        var email = $("#txtEmailAddress").val();
        var comment = $("#txtComments").val();
        var shipToAddress = $("#txtShiptoAddress").val();
        var customerNumber = $("#txtCustomerNumber").val();
        $.ajax(
            {
                method: "Post",
                headers: { "RequestVerificationToken": token },
                url: "ShoppingCart/Index?handler=PlaceOrder",
                data: {
                    PONumber: poNum,
                    Comments: comment,
                    Email: email,
                    CustomerNumber: customerNumber,
                    ShiptoAddress: shipToAddress
                },
                datatype: "text",
                success: function (data) {
                    //Update Cart screen with thank you message, your order has been sent to order@alkota.com
                    $("#MainCartWindow").addClass("collapse");
                    $("#divThanksForTheOrder").removeClass("collapse");
                },
                error: function (x, r, y) {


                }
            });
    });
        
});

function GetCartContents() {
    //$("#lblCartQty").text("14")
    $.ajax(
        {
            method: "Get",
            headers: { "RequestVerificationToken": token },
            //url: "TagWriter/Index?handler=SaveTag",
            url: "Parts/Index?handler=GetCartContents",
            data: {
            },
            datatype: "text",
            success: function (data) {
                //location.reload();
                //$(clickedButton).val('remove from cart');
                //var newViewPort = $(data).find("#PartsViewPort");
                //$("#PartsViewPort").replaceWith(newViewPort);
                //alert("Added")
                //alert($("#hidCartContentsQty").val())
                $("#lblCartQty").text($("#hidCartContentsQty").val());
            },
            error: function (x, r, y) { }
        });

}

function RemoveFromCart(itemID, accountID, userID) {
    //alert("Remove " + itemID + " from cart.")
    var token = $("input[name='__RequestVerificationToken']").val();
    //Get User Name

    //var clickedButton = "#" + control

    $.ajax(
        {
            method: "Post",
            headers: { "RequestVerificationToken": token },
            //url: "TagWriter/Index?handler=SaveTag",
            url: "Parts/Index?handler=RemoveFromCart",
            data: {
                ItemID: itemID,
                AccountID: accountID,
                UserID: userID

            },
            datatype: "text",
            success: function (data) {
                //location.reload();
                //$(clickedButton).val('remove from cart');
                var newViewPort = $(data).find("#PartsViewPort");
                $("#PartsViewPort").replaceWith(newViewPort);
                //alert("Added")
                GetCartContents();
            },
            error: function (x, r, y) { }
        });
}

///Handle button click on item page to add the current item to the cart.
function AddToCart(itemID, accountID, userID, control) {
    
    var token = $("input[name='__RequestVerificationToken']").val();
    //Get User Name

    //var clickedButton = "#"+control

    $.ajax(
        {
            method: "Post",
            headers: { "RequestVerificationToken": token },
            //url: "TagWriter/Index?handler=SaveTag",
            url: "Parts/Index?handler=AddToCart",
            data: {
                ItemID: itemID,
                AccountID: accountID,
                UserID: userID

            },
            datatype: "text",
            success: function (data) {
                //location.reload();
                //$(clickedButton).val('remove from cart');
                var newViewPort = $(data).find("#PartsViewPort");
                $("#PartsViewPort").replaceWith(newViewPort);
                GetCartContents();
                //alert("Added")
            },
            error: function (x, r, y) { }
        });
}

function UpdateQuantity(ItemID, quantity, userID, accountID) {
    var token = $("input[name='__RequestVerificationToken']").val();
    $.ajax(
        {
            method: "Post",
            headers: { "RequestVerificationToken": token },
            //url: "TagWriter/Index?handler=SaveTag",
            url: "ShoppingCart/Index?handler=UpdateCart",
            data: {
                ItemID: ItemID,
                AccountID: accountID,
                UserID: userID,
                Quantity: quantity

            },
            datatype: "text",
            success: function (data) {
                //console.log(data)
                var newViewPort = $(data).find("#divCartFrame");
                //Update current
                $("#divCartFrame").replaceWith(newViewPort);

                //Find Alert div and visibly show the user that card was updated
                var cartUpdate = $(data).find("#divDisplayMessage");
                //cartUpdate.fadeOut(3000, linear, HideMessage)
                $("#divDisplayMessage").replaceWith(cartUpdate);
                $("#divDisplayMessage").removeClass("collapse");
                $("#divDisplayMessage").fadeOut(2000, 'linear', HideMessage);
            },
            error: function (x, r, y) { }
        });
}

//function call back to rehide the update cart div
function HideMessage() {
    $("#divDisplayMessage").addClass("collapse");
}

function CustomerNumber_OnTextChange() {
    if ($("#txtCustomerNumber").val().length >= 1) {
        $("#btnPlaceOrder").removeClass("disabled");
    }
    else {
        $("#btnPlaceOrder").addClass("disabled");
    }
}