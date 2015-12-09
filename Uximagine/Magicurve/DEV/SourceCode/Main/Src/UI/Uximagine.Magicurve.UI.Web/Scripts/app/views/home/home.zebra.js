var domain = "/Magicurve";
var canvasWidth = 960;
var canvasHeight = 810;
var stage = new Stage();
stage.controlUrl = domain + "/api/images/result";
stage.codeUrl = domain + "/api/images/download";

zebra.ready(function () {
    stage.getControls(function() {
        draw();
    });
});


function draw() {
    var controls = stage.controls;
    var imageWidth = stage.imageWidth;
    var imageHeight = stage.imageHeight;

    var zCanvas = new zebra.ui.zCanvas("designer", 970, 820);
    var root = zCanvas.root; // save reference to root UI component
    root.setBackground("black");

    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(5, 5, canvasWidth, canvasHeight); // shape panel
    p.setBackground("white");    // set yellow background
    root.add(p);                  // add panel to root

    var ajustedControls = ajustToCanvasSize(controls, imageHeight, imageWidth);
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
                editable(drawCheckBox(sourceControls[i].X, sourceControls[i].Y, 120, 30, "CheckBox"),i));
        }
        else if (sourceControls[i].Type === "ComboBox") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawComboBox(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 40), i));
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
                editable(drawImageContent(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height),i));
        }
        else if (sourceControls[i].Type === "Label") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawLable(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height, "LableCaption"), i));
        }
        else if (sourceControls[i].Type === "MenuBar") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawMenuBar(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }
        else if (sourceControls[i].Type === "Paragraph") {
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawPanel(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height, "#E6E6E6"), i));
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
    }

    return controls;
}

function downloadSource() {

    stage.controls = ajustToCanvasSizeReverse(stage.controls, stage.imageHeight, stage.imageWidth);

    stage.getCode(function() {
        window.open(stage.sourceUrl, '_blank');
    });
}