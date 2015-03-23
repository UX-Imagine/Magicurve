var root = "/Magicurve";

var clickCount = 1;

var types =
    [
        "/Home/Capture",
        "/Home/CaptureTemplate"
    ];

var ImageType = types[0];

$(document).ready(function () {
    $("#Camera").webcam({
        width: 320,
        height: 240,
        mode: "save",
        swffile: root + "/Content/swf/jscam.swf",
        onTick: function () { },
        onSave: function () {
            if (clickCount % 2 == 0 ) {
                loadImage();
            }
        },
        onCapture: function () {
            webcam.save(root + ImageType);
        },
        debug: function () { },
        onLoad: function () { }
    });

    $("#shoot").click(function () {
        webcam.capture();
        setImageType((clickCount++) % 2);
    });

    $("#sample_btn").click(function () {
        window.location = root + "/Home/Sample";
    });
});


function setImageType(type) {
    /// <summary>
    /// Sets the type of the image.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The capture type.</returns>
    ImageType = types[type];
}


function loadImage() {
    $.ajax(
        {
            url: root + "/api/values/2",
            type: "GET"
        }).done(function (data) {
            $("#img_blob").attr("src", root + data + '?' + new Date().getTime());
            $("#or_img_blob").attr("src", root + "/Content/Images/Capture/capture.jpg" + '?' + new Date().getTime());
            $("#or_img_blob").attr("src", root + "/Content/Images/Capture/template.jpg" + '?' + new Date().getTime());
            console.log(data);
        }).fail(function (error) {
            $("#image_place_holder").html(error);
            console.log(error);
        });
}