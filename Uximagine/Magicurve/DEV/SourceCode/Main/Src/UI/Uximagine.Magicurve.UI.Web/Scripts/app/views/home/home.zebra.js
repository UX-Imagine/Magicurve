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



    var pos;
    function allowDrop(ev) {
        ev.preventDefault();

    }

    function get_pos(ev) {
        pos = [ev.pageX, ev.pageY];
    }


    function drag(ev) {
        ev.dataTransfer.setData("Text", ev.target.id);
    }

    function drop(ev) {
        ev.preventDefault();
        var offset = ev.dataTransfer.getData("text/plain").split(',');
        var data = ev.dataTransfer.getData("Text");

        // document.getElementById("desi").getContext("2d").drawImage(document.getElementById(data), ev.pageX - dx, ev.pageY - dy);

             if (data == "button") {
            var but = canvas = document.getElementById("button");
            var dx = pos[0] - but.offsetLeft;
            var dy = pos[1] - but.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragButton(X, Y);
        }
        else if (data == "check"){

            var but = canvas = document.getElementById("check");
            var dx = pos[0] - but.offsetLeft;
            var dy = pos[1] - but.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragCheckBox(X,Y);
        }
        else if (data == "combo") {

            var combo = canvas = document.getElementById("combo");
            var dx = pos[0] - combo.offsetLeft;
            var dy = pos[1] - combo.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragCombo(X,Y);
        }
        else if (data == "datepic"){
            var datepic = canvas = document.getElementById("datepic");
            var dx = pos[0] - datepic.offsetLeft;
            var dy = pos[1] - datepic.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragDatePicker(X,Y);
        }
        else if (data == "image"){

            var img = canvas = document.getElementById("image");
            var dx = pos[0] - img.offsetLeft;
            var dy = pos[1] - img.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragImageContent(X,Y);
        }
        else if (data == "radio"){
            var radi = canvas = document.getElementById("radio");
            var dx = pos[0] - radi.offsetLeft;
            var dy = pos[1] - radi.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragRadioButton(X,Y);
        }
        else if (data == "inputtext"){
            var text = canvas = document.getElementById("inputtext");
            var dx = pos[0] - text.offsetLeft;
            var dy = pos[1] - text.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragInputText(X,Y);
        }
        else if (data == "lable"){
            var lab = canvas = document.getElementById("lable");
            var dx = pos[0] - lab.offsetLeft;
            var dy = pos[1] - lab.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragLable(X,Y);
        }
        else if (data == "menu"){
            var men = canvas = document.getElementById("menu");
            var dx = pos[0] - men.offsetLeft;
            var dy = pos[1] - men.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragMenuBar(X,Y);
        }
        else if (data == "para"){
            var parag = canvas = document.getElementById("para");
            var dx = pos[0] - parag.offsetLeft;
            var dy = pos[1] - parag.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragPara(X,Y);

        }
        else if (data == "rang") {

            var ran = canvas = document.getElementById("rang");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragRange(X, Y);
        }
        else if (data == "hori") {

            var ran = canvas = document.getElementById("hori");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragHorizo(X, Y);
        }
        else if (data == "link") {

            var ran = canvas = document.getElementById("link");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragLink(X, Y);
        }
        else if (data == "iframe") {

            var ran = canvas = document.getElementById("iframe");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragIframe(X, Y);
        }
        else if (data == "password") {

            var ran = canvas = document.getElementById("password");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragInputText(X, Y);
        }
        else if (data == "textArea") {

            var ran = canvas = document.getElementById("textArea");
            var dx = pos[0] - ran.offsetLeft;
            var dy = pos[1] - ran.offsetTop;
            var X = ev.pageX - dx;
            var Y = ev.pageY - dy;
            dragTextArea(X, Y);
        }

    }

    function dragIframe(ix, iy) {
        zebra.ready(function () {
            var iframePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/iframe.png");
            var imageIfreme = new zebra.ui.ImagePan(iframePic);
            imageIfreme.setBounds(ix, iy, 150, 150);

            var shperIframe = new zebra.ui.designer.ShaperPan(imageIfreme);
            window.root.add(shperIframe);
        });
    }
    function dragTextArea(textAreaX, textAreaY) {
        var textArea = new zebra.ui.TextArea();
        textArea.setBounds(textAreaX, textAreaY, 100, 50);
        var shpertextArea = new zebra.ui.designer.ShaperPan(textArea);
        window.root.add(shpertextArea);
    }
    function dragLink(lx, ly) {
        zebra.ready(function () {

            var hyperlink = new zebra.ui.Link("Hyper Link");
            hyperlink.setBounds(lx, ly, 60, 10);
            hyperlink.setColor("#2E64FE");
            var linkShaperPan = new zebra.ui.designer.ShaperPan(hyperlink);
            window.root.add(linkShaperPan);

        });
    }
    function dragHorizo(lx, ly) {
        zebra.ready(function () {

            var horizontal = new zebra.ui.Line();
            horizontal.setBounds(lx, ly, 150, 3);

         
            var horiShaperPan = new zebra.ui.designer.ShaperPan(horizontal);
            window.root.add(horiShaperPan);

        });
    }
    function dragButton(bx,by) {


        zebra.ready(function () {

            var button = new zebra.ui.Button("Button");
            button.setBounds(bx, by, 80, 30);
           // button.setBackground("red");
            var buttonShaperPan = new zebra.ui.designer.ShaperPan(button);
            window.root.add(buttonShaperPan);

        });
    }
    function dragCheckBox(cx, cy) {
        zebra.ready(function () {


            var checkBox = new zebra.ui.Checkbox("Check box");
            checkBox.setBounds(cx, cy, 100, 30);
            var checkBoxShaperPan = new zebra.ui.designer.ShaperPan(checkBox);
            window.root.add(checkBoxShaperPan);

        });
    }
    function dragCombo(bx, by) {
        zebra.ready(function () {
            var combo = new zebra.ui.Combo(new zebra.ui.List([
              "Item 1",
              "Item 2",
              "Item 3"
            ]));

            combo.setBounds(bx, by, 120, 30);
            var shpercombo = new zebra.ui.designer.ShaperPan(combo);

            window.root.add(shpercombo);

        });
    }
    function dragDatePicker(bx, by) {
        zebra.ready(function () {
            var datePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
            var imageDatePic = new zebra.ui.ImagePan(datePic);
            imageDatePic.setBounds(bx, by, 140, 40);

            var shperDatePic = new zebra.ui.designer.ShaperPan(imageDatePic);
            window.root.add(shperDatePic);
        });
    }
    function dragImageContent(bx, by) {
        zebra.ready(function () {

            var image = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/image.png");
            var imageImage = new zebra.ui.ImagePan(image);
            imageImage.setBounds(bx, by, 200, 100);

            var shperimage = new zebra.ui.designer.ShaperPan(imageImage);
            window.root.add(shperimage);

        });
    }
    function dragRadioButton(bx, by) {
        zebra.ready(function () {

            var radioButton = new zebra.ui.Radiobox("Radio Button");
            radioButton.setBounds(bx, by, 120, 30);

            var shperRadioBox = new zebra.ui.designer.ShaperPan(radioButton);
            window.root.add(shperRadioBox);

        });
    }
    function dragInputText(bx, by) {
        zebra.ready(function () {


            var textBox = new zebra.ui.TextField("Type Here");
            textBox.setBounds(bx, by, 120, 30);
            var textBoxShaperPan = new zebra.ui.designer.ShaperPan(textBox);
            window.root.add(textBoxShaperPan);

        });
    }
    function dragLable(bx, by) {
        zebra.ready(function () {


            var lable = new zebra.ui.Label("Lable");
            lable.setBounds(bx, by, 120, 30);
            var lableShaperPan = new zebra.ui.designer.ShaperPan(lable);
            window.root.add(lableShaperPan);

        });
    }
    function dragMenuBar(bx, by) {
        zebra.ready(function () {
            var menu = new zebra.ui.Menubar({
                "HOME": {
                    "Subitem 1.1": null,
                    "Subitem 1.2": null,
                    "Subitem 1.3": null
                },
                "CONTENT": {
                    "Subitem 2.1": null,
                    "Subitem 2.2": null,
                    "Subitem 2.3": null
                },
                "CONTCT": null
            });

            menu.setBounds(bx, by, 600, 50);
            var shpermenu = new zebra.ui.designer.ShaperPan(menu);
            window.root.add(shpermenu);

        });
    }
    function dragRange(bx, by) {
        zebra.ready(function () {

            var rangePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/range.png");
            var imageRangePic = new zebra.ui.ImagePan(rangePic);
            imageRangePic.setBounds(bx, by, 140, 30);
            var shperRange = new zebra.ui.designer.ShaperPan(imageRangePic);
            window.root.add(shperRange);
        });
    }
    function dragPara(bx, by) {
        zebra.ready(function () {

            var paraPic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/paragrap.png");
            var imageParaPic = new zebra.ui.ImagePan(paraPic);
            imageParaPic.setBounds(bx, by, 100, 50);
            var shperPara = new zebra.ui.designer.ShaperPan(imageParaPic);
            window.root.add(shperPara);
        });
    }
