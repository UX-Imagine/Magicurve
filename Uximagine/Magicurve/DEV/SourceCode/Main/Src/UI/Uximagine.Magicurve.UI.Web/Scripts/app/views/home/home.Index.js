var path = "/Magicurve";
var zCanvas;
var root;
var canvasWidth = (screen.width * 8 / 12) - 50;
var canvasHeight = screen.height * .65;

var stage;

zebra.ready(function () {
    stage = new Stage();
    zCanvas = new zebra.ui.zCanvas("designer", canvasWidth, canvasHeight);
    root = zCanvas.root; // save reference to root UI component
});

function getControls() {
    $.ajax(
       {
           url: path + "/api/images/result",
           type: "POST",
           data: { FileUrl: uploadedImageUrl }
       }).done(function (data) {
           window.controls = data.controls;
           window.imageWidth = data.imageWidth;
           draw(data);
       }).fail(function (error) {

           console.log(error);
       });
}

function draw(data) {
    var json = data.controls;
    var imageWidth = data.imageWidth;
    var imageHeight = data.imageHeight;

    root.removeAll();

    root.setBackground("black");

    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(5, 5, 960, 810); // shape panel
    p.setBackground("white");    // set yellow background
    root.add(p);                  // add panel to root

    var resalutonObject = ajustToCanvasSize(json, imageHeight, imageWidth);
    var arrayObject = genarateDesign(resalutonObject);

    for (var k = 0 ; k < arrayObject.length; k++) {
        root.add(arrayObject[k]);
    }
}

function ajustToCanvasSize(resObject, height, width) {
    var setResaObje = [];

    for (var i = 0 ; i < resObject.length ; i++) {
        var control = {};
        control.X = Math.round((resObject[i].X * 960) / width);
        control.Y = Math.round((resObject[i].Y * 810) / height);
        control.Type = resObject[i].Type;
        control.Height = Math.round((resObject[i].Height * 810) / height);
        control.Width = Math.round((resObject[i].Width * 960) / width);
        setResaObje.push(control);
    }
    return setResaObje;
}

function genarateDesign(jsonObj) {


    var controls = [];

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].Type === "Button") {
            controls[controlsValid] = drawButton(jsonObj[i].X, jsonObj[i].Y, 60, 30, "Button");
            controlsValid++;
        }
        else if (jsonObj[i].Type === "CheckBox") {
            controls[controlsValid] = drawCheckBox(jsonObj[i].X, jsonObj[i].Y, 120, 30, "CheckBox");
            controlsValid++;
        }
        else if (jsonObj[i].Type === "ComboBox") {
            controls[controlsValid] = drawComboBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, 40);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "HLine") {
            controls[controlsValid] = drawHorizontalLine(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "HyperLink") {
            controls[controlsValid] = drawHyperlink(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "HyperLink");
            controlsValid++;
        }
        else if (jsonObj[i].Type === "Image") {
            controls[controlsValid] = drawImageContent(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "Label") {
            controls[controlsValid] = drawLable(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "Lable Caption");
            controlsValid++;
        }
        else if (jsonObj[i].Type === "MenuBar") {
            controls[controlsValid] = drawMenuBar(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "Paragraph") {
            controls[controlsValid] = drawPara(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "RadioButton") {
            controls[controlsValid] = drawRadioButton(jsonObj[i].X, jsonObj[i].Y, 120, 30, "RadioButton");
            controlsValid++;
        }
        else if (jsonObj[i].Type === "TextArea") {
            controls[controlsValid] = drawTextArea(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "InputText") {
            controls[controlsValid] = drawTextBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, 30);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "InputPassword") {
            controls[controlsValid] = drawTextBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, 30);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "DatePicker") {
            controls[controlsValid] = drawDatePic(jsonObj[i].X, jsonObj[i].Y);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "Range") {
            controls[controlsValid] = drawRage(jsonObj[i].X, jsonObj[i].Y);
            controlsValid++;
        }
        else if (jsonObj[i].Type === "Iframe") {
            controls[controlsValid] = drawIframe(jsonObj[i].X, jsonObj[i].Y);
            controlsValid++;
        }
    }

    return controls;
}

