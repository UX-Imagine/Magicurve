var domain = "/Magicurve";
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
    stage.getControls(function () {
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
function ajustToCanvasSize(resObject, or_height, or_width) {
    var ajustedControls = [];

    for (var i = 0 ; i < resObject.length ; i++) {
        var control = {};
        stage.controls[i].X = control.X = Math.round((resObject[i].X * canvasWidth) / or_width);
        stage.controls[i].Y = control.Y = Math.round((resObject[i].Y * canvasHeight) / or_height);
        control.Type = resObject[i].Type;
        stage.controls[i].Height = control.Height = Math.round((resObject[i].Height * canvasHeight) / or_height);
        stage.controls[i].Width = control.Width = Math.round((resObject[i].Width * canvasWidth) / or_width);
        ajustedControls.push(control);
    }

    return ajustedControls;
}

function ajustToCanvasSizeReverse(resObject, or_height, or_width) {
    var ajustedControls = [];

    for (var i = 0 ; i < resObject.length ; i++) {
        var control = {};
        control.X = Math.round((resObject[i].X * or_width) / canvasWidth);
        control.Y = Math.round((resObject[i].Y * or_height) / canvasHeight);
        control.Type = resObject[i].Type;
        control.Height = Math.round((resObject[i].Height * or_height) / canvasHeight);
        control.Width = Math.round((resObject[i].Width * or_width) / canvasWidth);
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
            controls[controlsValid++] = new ControlItem(
                i,
                editable(drawIframe(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30), i));
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
                editable(drawTextArea(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height), i));
        }

    }

    return controls;
}

function downloadSource() {

    stage.controls = ajustToCanvasSizeReverse(stage.controls, stage.imageHeight, stage.imageWidth);

    stage.getCode(function () {
        window.open(stage.sourceUrl, '_blank');
    });
}


///////////////////////////////// drag and drop from tool bar////////////////////////////////////

function drop(ev) {
    
    var offset = ev.dataTransfer.getData("text/plain").split(',');
    var data = ev.dataTransfer.getData("Text");
    if (data === "") {
        return;
    }

    ev.preventDefault();
    // document.getElementById("desi").getContext("2d").drawImage(document.getElementById(data), ev.pageX - dx, ev.pageY - dy);

    if (data == "button") {
        var but = document.getElementById("button");
        var dx = pos[0] - but.offsetLeft;
        var dy = pos[1] - but.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawButton(X, Y, 60, 30, "Button"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 60, 30, "Button"));
    }
    else if (data == "check") {

        var but = document.getElementById("check");
        var dx = pos[0] - but.offsetLeft;
        var dy = pos[1] - but.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawCheckBox(X, Y, 100, 30, "Checkbox"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 100, 30, "Checkbox"));
    }
    else if (data == "combo") {

        var combo =  document.getElementById("combo");
        var dx = pos[0] - combo.offsetLeft;
        var dy = pos[1] - combo.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawComboBox(X, Y, 100, 30, "ComboBox"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 100, 30, "ComboBox"));
    }
    else if (data == "datepic") {
        var datepic =  document.getElementById("datepic");
        var dx = pos[0] - datepic.offsetLeft;
        var dy = pos[1] - datepic.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawDatePic(X, Y, 100, 30, "DatePicker"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 100, 30, "DatePicker"));
    }
    else if (data == "image") {

        var img = document.getElementById("image");
        var dx = pos[0] - img.offsetLeft;
        var dy = pos[1] - img.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawImageContent(X, Y, 200, 100), stage.controls.length));
        stage.controls.push(new Control(X, Y, 200, 100, "DatePicker"));
    }
    else if (data == "radio") {
        var radi = canvas = document.getElementById("radio");
        var dx = pos[0] - radi.offsetLeft;
        var dy = pos[1] - radi.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawRadioButton(X, Y, 120, 30), stage.controls.length));
        stage.controls.push(new Control(X, Y, 120, 30, "RadioButton"));
    }
    else if (data == "inputtext") {
        var text =  document.getElementById("inputtext");
        var dx = pos[0] - text.offsetLeft;
        var dy = pos[1] - text.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawTextBox(X, Y, 120, 30), stage.controls.length));
        stage.controls.push(new Control(X, Y, 120, 30, "InputText"));
    }
    else if (data == "lable") {
        var lab = document.getElementById("lable");
        var dx = pos[0] - lab.offsetLeft;
        var dy = pos[1] - lab.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawLable(X, Y, 120, 30, "Label"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 120, 30, "Label"));
    }
    else if (data == "menu") {
        var men = document.getElementById("menu");
        var dx = pos[0] - men.offsetLeft;
        var dy = pos[1] - men.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawMenuBar(X, Y, 600, 50), stage.controls.length));
        stage.controls.push(new Control(X, Y, 600, 50, "MenuBar"))
    }
    else if (data == "para") {
        var parag = document.getElementById("para");
        var dx = pos[0] - parag.offsetLeft;
        var dy = pos[1] - parag.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawMenuBar(X, Y, 600, 50), stage.controls.length));
        stage.controls.push(new Control(X, Y, 600, 50, "MenuBar"));

    }
    else if (data == "rang") {

        var ran = document.getElementById("rang");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawRage(X, Y), stage.controls.length));
        stage.controls.push(new Control(X, Y, 140, 30, "Range"));
    }
    else if (data == "hori") {

        var ran =  document.getElementById("hori");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawHorizontalLine(X, Y, 40), stage.controls.length));
        stage.controls.push(new Control(X, Y, 40, 3, "HLine"));
    }
    else if (data == "link") {

        var ran = document.getElementById("link");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawHyperlink(X, Y, 60, 10, "Link"), stage.controls.length));
        stage.controls.push(new Control(X, Y, 60, 10, "HyperLink"));
    }
    else if (data == "iframe") {

        var ran = document.getElementById("iframe");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawIframe(X, Y), stage.controls.length));
        stage.controls.push(new Control(X, Y, 150, 150, "Iframe"));
    }
    else if (data == "password") {

        var ran = document.getElementById("password");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawTextBox(X, Y, 120, 30), stage.controls.length));
        stage.controls.push(new Control(X, Y, 120, 30));
    }
    else if (data == "textArea") {

        var ran = document.getElementById("textArea");
        var dx = pos[0] - ran.offsetLeft;
        var dy = pos[1] - ran.offsetTop;
        var X = ev.pageX - dx;
        var Y = ev.pageY - dy;
        root.add(editable(drawTextArea(X, Y, 100, 40), stage.controls.length));
        stage.controls.push(new Control(X, Y, 100, 40));
    }

}