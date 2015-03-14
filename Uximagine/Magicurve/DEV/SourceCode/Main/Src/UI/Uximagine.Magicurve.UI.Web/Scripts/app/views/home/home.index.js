$(document).ready(function () {
    $("#Camera").webcam({
        width: 320,
        height: 240,
        mode: "save",
        swffile: "/Content/swf/jscam.swf",
        onTick: function () { },
        onSave: function () {
        },
        onCapture: function () {
            webcam.save("/Home/Capture");
        },
        debug: function () { },
        onLoad: function () { }
    });

    $("#shoot").click(function () {
        webcam.capture();
    });

    $("#sample_btn").click(function () {
        window.location = "/Home/Sample";
    });
});


function loadImage() {
    $.ajax(
        {
            url: "/api/values/1",
            type: "GET"
        }).done(function (data)
        {
            $("#img_blob").attr("src", data + '?' + new Date().getTime());
            console.log(data);
        }).fail(function (error)
        {
            $("#image_place_holder").html(error);
            console.log(error);
        });
}