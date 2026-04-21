$(document).ready(function () {
    
});


function ShowAddItemModal() {
    $("#mdlAddItem").modal('show');
}

function UploadImage() {
    var token = $("input[name='__RequestVerificationToken']").val();
    
    var itemName = $("#txtItemName").val();
    var itemDescription = $("#txtItemDescription").val();
    var itemCost = $("#txtItemCost").val();
    var itemLabelColor = $("#txtItemLabelColor").val();
    var itemLink = $("#txtItemLink").val();

    

    var fileInput = document.getElementById('userFile');
    //alert(fileInput.length);
    var file = fileInput.files[0];
    //alert(file);
    var fd = new FormData();
    fd.append('_file', file);

    fd.append('itemName', itemName);
    fd.append('itemDescription', itemDescription);
    fd.append('itemLink', itemLink);
    fd.append('itemCost', itemCost);
    fd.append('itemLabelColor', itemLabelColor);
    //alert(fd);
    //alert($('#userFile')[0].files[0].name);
    

    $.ajax(
        {
            method: "POST",
            headers: { "RequestVerificationToken": token },
            url: "AddItems/Index?handler=UploadMedia",
            data: fd,
            datatype: "text",
            contentType: false,
            processData: false,
            success: function (data) {
                //alert("success");
                var image = $(data).find("#uploadedImage");
                console.log(data);
                $("#uploadedImage").replaceWith(image);
                $("#mdlAddItem").modal('hide');
            }
        });


    //$("#frmImageUpload").submit();
    
}