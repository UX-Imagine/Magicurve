var root = "/Magicurve";

$(document).ready(function () {

    $('#btnUploadFile').on('click', function () {

        var data = new FormData();

        var files = $("#uploadImage").get(0).files;

        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
        }

        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: root + "/Home/UploadFile",
            contentType: false,
            processData: false,
            data: data
        });

        ajaxRequest.done(function (xhr, textStatus) {
            if (textStatus.message != 'error') {
                loadImage();
            }
        });
    });
});

function loadImage() {
    $.ajax(
        {
            url: root + "/api/images/result",
            type: "GET"
        }).done(function (data) {
            $("#img_blob").attr("src", root + data.url + "?" + new Date().getTime());
            $("#img").attr("src", root + "/Content/Images/Upload/upload.jpg" + "?" + new Date().getTime());
            window.controls = data.controls;
            console.log(data);
            //downloadCode();
            generateCode();
    }).fail(function (error) {
            $("#image_place_holder").html(error);
            console.log(error);
        });
}

function downloadCode() {
    $.ajax(
        {
            url: root + "/Home/Download?" + JSON.stringify(window.controls),
            type: "GET"
        }).done(function (data) {
          console.log(data);
        }).fail(function (error) {
            
            console.log(error);
        });
}

function generateCode() {
    $.ajax(
        {
            url: root + "/api/images/code",
            type: "POST",
            data: { controls: window.controls }
        }).done(function (data) {
            console.log(data);
            $('#code').html(JSON.stringify(data.code));
    }).fail(function (error) {

            console.log(error);
        });
}