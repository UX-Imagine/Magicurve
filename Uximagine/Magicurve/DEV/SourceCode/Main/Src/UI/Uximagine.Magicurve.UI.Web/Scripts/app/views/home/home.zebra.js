var domain = "";
//var canvasWidth = screen.width * 80 / 144;
//var canvasHeight = screen.height * .6;

var canvasWidth = 960;
var canvasHeight = 810;
var stage = new Stage();
stage.controlUrl = domain + "/api/images/result";
stage.codeUrl = domain + "/api/images/download";
var zCanvas;
var root;
zebra.ready(function () {
    zCanvas = new zebra.ui.zCanvas("designer", 970, 820);
    root = zCanvas.root; // save reference to root UI component
    root.setBackground("black");
    getControls();
});

function getControls() {
    stage.getControls(function() {
        draw();
    });
}

function onSelectItem(index) {
    var x = document.getElementById("left");
    x.value = stage.controls[index].X;

    var y = document.getElementById("top");
    y.value = stage.controls[index].Y;

    var width = document.getElementById("width");
    width.value = stage.controls[index].Width;

    var height = document.getElementById("height");
    height.value = stage.controls[index].Height;

}

function draw() {
 root.removeAll();

    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(5, 5, 960, 810); // shape panel
    p.setBackground("white");    // set yellow background
    root.add(p);                  // add panel to root

    var ajustedControls = ajustToCanvasSize(stage.controls, stage.imageHeight, stage.imageWidth);
    var controlItems = genarateDesign(ajustedControls);

    for (var k = 0 ; k < controlItems.length; k++) {
        root.add(controlItems[k].control);
    }
}

/*
 * set the sizes according to the canvas size.
 */
function ajustToCanvasSize(resObject, height, width) {
    var ajustedControls = [];

    for (var i = 0 ; i < resObject.length ; i++) {
        var control = {};
        control.X = Math.round((resObject[i].X * canvasWidth) / width);
        control.Y = Math.round((resObject[i].Y * canvasHeight) / height);
        control.Type = resObject[i].Type;
        control.Height = Math.round((resObject[i].Height * canvasHeight) / height);
        control.Width = Math.round((resObject[i].Width * canvasHeight) / width);
        ajustedControls.push(control);
    }

    return ajustedControls;
}

function ajustToCanvasSizeReverse(resObject, height, width) {
    var ajustedControls = [];

    for (var i = 0 ; i < resObject.length ; i++) {
        var control = {};
        control.X = Math.round((resObject[i].X * width) / canvasWidth);
        control.Y = Math.round((resObject[i].Y * height ) / canvasHeight);
        control.Type = resObject[i].Type;
        control.Height = Math.round((resObject[i].Height * height ) / canvasHeight);
        control.Width = Math.round((resObject[i].Width * width ) / canvasHeight);
        ajustedControls.push(control);
    }

    return ajustedControls;
}

function genarateDesign(sourceControls) {
    var controls = Array(ControlItem);

    var controlsValid = 0;

    for (var i = 0; i < sourceControls.length; i++) {
        if (sourceControls[i].Type === "Button") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawButton(sourceControls[i].X, sourceControls[i].Y, 60, 30, "Button"), i));
        }
        else if (sourceControls[i].Type === "CheckBox") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawCheckBox(sourceControls[i].X, sourceControls[i].Y, 120, 30, "CheckBox"), i));
        }
        else if (sourceControls[i].Type === "ComboBox") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawComboBox(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30), i));
        }
        else if (sourceControls[i].Type === "HLine") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawHorizontalLine(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width), i));
        }
        else if (sourceControls[i].Type === "HyperLink") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawHyperlink(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height, "HyperLink"), i));
        }
        else if (sourceControls[i].Type === "Image") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawImageContent(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }
        else if (sourceControls[i].Type === "Label") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawLable(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height, "Lable Caption"), i));
        }
        else if (sourceControls[i].Type === "MenuBar") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawMenuBar(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }
        else if (sourceControls[i].Type === "Paragraph") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawPara(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }
        else if (sourceControls[i].Type === "RadioButton") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawRadioButton(sourceControls[i].X, sourceControls[i].Y, 120, 30, "RadioButton"), i));
        }
        else if (sourceControls[i].Type === "TextArea") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawTextArea(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }
        else if (sourceControls[i].Type === "InputText") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawTextBox(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30), i));
        }
        else if (sourceControls[i].Type === "Iframe") {
            //controls[controlsValid++] = new ControlItem(
            //    i,
            //    editable(drawTextBox(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30), i));
        }
        else if (sourceControls[i].Type === "DatePicker") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawDatePic(sourceControls[i].X, sourceControls[i].Y), i));
        }
        else if (sourceControls[i].Type === "Range") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawRage(sourceControls[i].X, sourceControls[i].Y), i));
        }
        else if (sourceControls[i].Type === "InputPassword") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawTextArea(sourceControls[i].X, sourceControls[i].Y), i));
        }
        else if (sourceControls[i].Type === "Iframe") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawIframe(sourceControls[i].X, sourceControls[i].Y), i));
        }
    }

    return controls;
}

function downloadSource() {

    stage.controls = ajustToCanvasSizeReverse(stage.controls, stage.imageHeight, stage.imageWidth);

    stage.getCode(function() {
        window.open(stage.sourceUrl, '_blank');
    });
}


///////////////////////////////// drag and drop from tool bar////////////////////////////////////

