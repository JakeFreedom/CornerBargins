$(document).ready(function () {


    //var dropFilesArea = $("dropFilesArea");
    //dropFilesArea.on('dragover', function(e) {
    //    alert('area entered')
    //    e.preventDefault();
    //    drogFilesArea.addClass('drag-over');
    //});

    $("#btnAddItem").click(function () {
        $("#mdlAddItem").modal('show');
    });

    $("#userFile").on('dragover', (e) => { e.preventDefault(); });


    //Well use this to create a new type file entry so the user can drop 
    $("#userFile").on('drop', (e) => {
        var fileInput = document.getElementById('userFile');
        alert(fileInput.files);
        console.log(fileInput.files);
    });

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
    var file = fileInput.files[0];
    //Loop these files and add them all
    //How do we control what slot they are in
    //Do we always assume the first image will be the primary image? Ebay does.
    //alert(file);
    var fd = new FormData();
    fd.append('_file', file);

    fd.append('itemName', itemName);
    fd.append('itemDescription', itemDescription);
    fd.append('itemLink', itemLink);
    fd.append('itemCost', itemCost);
    fd.append('itemLabelColor', itemLabelColor);
    

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
                //var image = $(data).find("#uploadedImage");
                //console.log(data);
                //$("#uploadedImage").replaceWith(image);
                $("#mdlAddItem").modal('hide');
                window.location.reload();
            }
        });
}




function UpdateItem(itemID) {
    var visCheckBox = "#chkItemVisible-" + itemID;
    var showResButton = "#chkShowResButton-" + itemID;
      var tableRowToToggle = "#tableRowDiv" + itemID;
    //These are obviously going to be status's
    //1 Visible
    //2 Show Reserve Box
      //3 ETC...
    


    var token = $("input[name='__RequestVerificationToken']").val();

    $.ajax(
        {
            method: "POST",
            headers: { "RequestVerificationToken": token },
            url: "Manage/Index?handler=UpdateItem",
            data: {
                ItemID: itemID
            },
            datatype: "text",
            success: function (data) {
                $(tableRowToToggle).toggleClass('collapse');
                //window.location.reload();
            },
            error: function (x, r, y) { }
        });

}

function ShowDiv(target) {

    $(target).toggleClass('collapse');
        
}


//Drag Drop Files to upload
//Not 100% sure what this is doing
function FileDropAreaEntered(event) {
    event.preventDefault();
}



function AddDroppedFile(event, itemID) {
    event.preventDefault();
    event.stopPropagation();
    alert(itemID);
    var dt = event.dataTransfer;
    alert(dt.files.length);
    //dt.files.each(function (x) { alert(x.name); });
    $.each(dt.files, function (index, file) {
        //alert(file.name);
        var fd = new FormData();
        fd.append('files', file);
        fd.append('itemID', itemID)

        $.ajax({
            method: "POST",
            headers: { "RequestVerificationToken": token },
            url: "Manage/Index?handler=AddImage",
            data: fd,
            datatype: "text",
            contentType: false,
            processData: false,
            success: function (data)
            {
                //var itemImageString = "#itemImage-" + itemID;
                //var itemImage = $(data).find(itemImageString);
                //$(itemImageString).replaceWith(itemImage);
                //$("#uploadedImage").replaceWith(image);
                //$("#mdlAddItem").modal('hide');
            }
            });
    });
    

}
//This will need to be modified for multiple files
function UploadDroppedFiles(event, itemID, binImage, binImageID) {
    event.preventDefault();
    event.stopPropagation();
    var dt = event.dataTransfer;
    //alert(dt.files[0].name);

    var fd = new FormData();
    fd.append('files', dt.files[0]);
    fd.append('itemID', itemID);
    fd.append('binImage', binImage);
    fd.append('binImageID', binImageID);

    $.ajax(
        {
            method: "POST",
            headers: { "RequestVerificationToken": token },
            url: "Manage/Index?handler=UpdateItemImage",
            data: fd,
            datatype: "text",
            contentType: false,
            processData: false,
            success: function (data) {
                //alert("success");
                //window.location.reload();
                var itemImageString = "#itemImage-" + itemID;
                var itemImage = $(data).find(itemImageString);
                $(itemImageString).replaceWith(itemImage);
                //$("#uploadedImage").replaceWith(image);
                //$("#mdlAddItem").modal('hide');
            }
        });


}