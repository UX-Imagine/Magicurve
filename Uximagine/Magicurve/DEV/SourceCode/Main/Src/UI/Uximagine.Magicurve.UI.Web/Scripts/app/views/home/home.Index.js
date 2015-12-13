var path = "/Magicurve";
zebra.ready(function () {
    getControls();

});

function getControls() {
    $.ajax(
       {
           url: path + "/api/images/result",
           type: "GET"
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

    var zCanvas = new zebra.ui.zCanvas("designer", 970, 820);
    var root = zCanvas.root; // save reference to root UI component
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
        else if (jsonObj[i].Type === "Iframe") {

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

//////////////// method drawing non editable ///////////

//function for drawing i frame
function drawIframe(dX, dY) {
    var iframePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
    var imageIframe = new zebra.ui.ImagePan(iframePic);
    imageDatePic.setBounds(dX, dY, 100, 100);

    // var shperData = new zebra.ui.designer.ShaperPan(imageDatePic);

    return imageIframe;

}

//function for drawing date pic
function drawDatePic(dX, dY) {
    var datePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
    var imageDatePic = new zebra.ui.ImagePan(datePic);
    imageDatePic.setBounds(dX, dY, 140, 40);

   // var shperData = new zebra.ui.designer.ShaperPan(imageDatePic);
   
    return imageDatePic;

}

//function for drawing rang
function drawRage(dX, dY) {
    var rangePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/range.png");
    var imageRangePic = new zebra.ui.ImagePan(rangePic);
    imageRangePic.setBounds(dX, dY, 140, 30);

    // var shperData = new zebra.ui.designer.ShaperPan(imageRangePic);

    return imageRangePic;

}

//function for drawing para
function drawPara(dX, dY,paraWidth,paraHeight) {
    var paraImage = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/paragrap.png");
    var imageParaImage = new zebra.ui.ImagePan(paraImage);
    imageParaImage.setBounds(dX, dY, paraWidth, paraHeight);

    // var shperData = new zebra.ui.designer.ShaperPan(imageParaImage);

    return imageParaImage;

}

//function for drawing button
function drawButton(bX, bY, bwidth, bheight, ButttonCaption) {
    var button = new zebra.ui.Button(ButttonCaption);
    button.setBounds(bX, bY, bwidth, bheight);
    return button;

}

//function for drawing button
function drawPanel(px, py, pw, ph, panelColor) {
    var panel = new zebra.ui.Panel();
    panel.setBounds(px, py, pw, ph);
    panel.setBackground(panelColor);

    return panel;

}

//function for drawing lable
function drawLable(lx, ly, lw, lh, labaleCaption) {
    var lable = new zebra.ui.Label(labaleCaption);
    lable.setBounds(lx, ly, lw, lh);
    lable.setColor("black");

    return lable;

}

//function for drawing text box
function drawTextBox(tx, ty, tw, th) {
    var textBox = new zebra.ui.TextField("Type Here...");
    textBox.setBounds(tx, ty, tw, th);

    return textBox;
}

//function for drawing Horizontal Line
function drawHorizontalLine(hx, hy, hw) {

    var horizontal = new zebra.ui.Line();
    horizontal.setBounds(hx, hy, hw, 3);

    return horizontal;
}

//function for drawing checkBox
function drawCheckBox(checkX, checkY, checkW, checkH, checkValue) {

    var checkBox = new zebra.ui.Checkbox(checkValue);
    checkBox.setBounds(checkX, checkY, checkW, checkH);

    return checkBox;
}

//function for drawing radio button
function drawRadioButton(radioX, radioY, radioW, radioH, radioValue) {
    var radioButton = new zebra.ui.Radiobox(radioValue);
    radioButton.setBounds(radioX, radioY, radioW, radioH);

    return radioButton;
}

//function for drawing combo box
function drawComboBox(comboX, comboY, comboW, comboH) {
    var combo = new zebra.ui.Combo(new zebra.ui.List([
   "Item 1",
   "Item 2",
   "Item 3"
    ]));

    combo.setBounds(comboX, comboY, comboW, comboH);
    return combo;
}

//function for drawing TextArea
function drawTextArea(textAreaX, textAreaY, textAreaW, textAreaH) {
    var textArea = new zebra.ui.TextArea();
    textArea.setBounds(textAreaX, textAreaY, textAreaW, textAreaH);

    return textArea;
}

//function for drawing combo box
function drawHyperlink(hyX, hyY, hyW, hyH, hyperValue) {
    var hyperlink = new zebra.ui.Link(hyperValue);
    hyperlink.setBounds(hyX, hyY, hyW, hyH);
    hyperlink.setColor("#2E64FE");

    return hyperlink;
}

//function for drawing Menubar
function drawMenuBar(menuX, menuY, menuW, menuH) {
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

    menu.setBounds(menuX, menuY, menuW, menuH);

    return menu;
}

//function for drawing image
function drawImageContent(imageX, imageY, imageW, imageH) {
  
    var image = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/image.png");
    var imageImage = new zebra.ui.ImagePan(image);
    imageImage.setBounds(imageX, imageY, imageW, imageH);

    // var shperData = new zebra.ui.designer.ShaperPan(imageImage);

    return imageImage;
}

