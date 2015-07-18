var root = "/Magicurve";

$(document).ready(function () {
    $("#Camera").webcam({
        width: 320,
        height: 240,
        mode: "save",
        swffile: root + "/Content/swf/jscam.swf",
        onTick: function () { },
        onSave: function () {
            loadImage();
        },
        onCapture: function () {
            webcam.save( root + "/Home/Capture");
        },
        debug: function () { },
        onLoad: function () { }
    });

    $("#shoot").click(function () {
        webcam.capture();
    });

    $("#sample_btn").click(function () {
        window.location = root + "/Home/Sample";
    });
});


function loadImage() {
    $.ajax(
        {
            url: root + "/api/values/1",
            type: "GET"
        }).done(function (data)
        {
            $("#img_blob").attr("src", root + data + "?" + new Date().getTime());
            $("#img").attr("src", root + "/Content/Images/Capture/capture.jpg" + "?" + new Date().getTime());
            console.log(data);
        }).fail(function (error)
        {
            $("#image_place_holder").html(error);
            console.log(error);
        });
}