
function loadImage() {
    $.ajax(
        {
            url     : "/api/values/1",
            type    : "GET"
        }).done(function (data) {
            $("#img_blob").attr("src", data);
            console.log(data);
        }).fail(function (error) {
            $("#image_place_holder").html(error);
            console.log(error);
        });
}