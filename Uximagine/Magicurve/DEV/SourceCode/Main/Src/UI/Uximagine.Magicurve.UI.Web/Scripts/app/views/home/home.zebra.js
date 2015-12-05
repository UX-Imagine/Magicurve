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

    var resalutonObject = setResalution(json, imageHeight, imageWidth);
    var arrayObject = genarateDesign(resalutonObject);

    for (var k = 0 ; k < arrayObject.length; k++) {
        root.add(arrayObject[k]);

    }

}

function setResalution(resObject, height, width) {
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


    for (var k = 0 ; k < jsonObj.length; k++) {
        //root.add(arr[k]);
        //   alert(jsonObj[k].Height + "," + jsonObj[k].Width + "," + jsonObj[k].Type + "," + jsonObj[k].X + "," + jsonObj[k].Y);
    }

    var controls = [];

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].Type == "Button") {
            controls[controlsValid] = editDrawButton(jsonObj[i].X, jsonObj[i].Y, 60, 30, "Button");
            controlsValid++;

        }
        else if (jsonObj[i].Type == "CheckBox") {
            controls[controlsValid] = editDrawCheckBox(jsonObj[i].X, jsonObj[i].Y, 120, 30, "CheckBox");
            controlsValid++;
        }
        else if (jsonObj[i].Type == "ComboBox") {
            controls[controlsValid] = editDrawComboBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, 40);
            controlsValid++;
        }
        else if (jsonObj[i].Type == "HLine") {
            controls[controlsValid] = editDrawHorizontalLine(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width);
            controlsValid++;
        }
        else if (jsonObj[i].Type == "HyperLink") {
            controls[controlsValid] = editDrawHyperlink(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "HyperLink");
            controlsValid++;
        }
        else if (jsonObj[i].Type == "Image") {
            controls[controlsValid] = editDrawImageContent(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type == "Label") {
            controls[controlsValid] = editDrawLable(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "LableCaption");
            controlsValid++;
        }
        else if (jsonObj[i].Type == "MenuBar") {
            controls[controlsValid] = editDrawMenuBar(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type == "Paragraph") {
            controls[controlsValid] = editDrawPanel(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "#E6E6E6");
            controlsValid++;
        }
        else if (jsonObj[i].Type == "RadioButton") {
            controls[controlsValid] = editDrawRadioButton(jsonObj[i].X, jsonObj[i].Y, 120, 30, "RadioButton");
            controlsValid++;
        }
        else if (jsonObj[i].Type == "TextArea") {
            controls[controlsValid] = editDrawTextArea(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].Type == "InputText") {
            controls[controlsValid] = editDrawTextBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, 30);
            controlsValid++;
        }

    }

    return controls;
}


///////////////////////////editable controlles ///////////////////


//function for drawing button
function editDrawButton(bX, bY, bwidth, bheight, ButttonCaption) {
    var button = new zebra.ui.Button(ButttonCaption);
    button.setBounds(bX, bY, bwidth, bheight, ButttonCaption);

    var buttonShaperPan = new zebra.ui.designer.ShaperPan(button);

    return buttonShaperPan;

}

//function for drawing button
function editDrawPanel(px, py, pw, ph,panelColor) {
    var panel = new zebra.ui.Panel();
    panel.setBounds(px, py, pw, ph);
    panel.setBackground(panelColor);
    var shperPanel = new zebra.ui.designer.ShaperPan(panel);

    return shperPanel;

}

//function for drawing lable
function editDrawLable(lx, ly, lw, lh, labaleCaption) {
    var lable = new zebra.ui.Label(labaleCaption);
    lable.setBounds(lx, ly, lw, lh);
    lable.setColor("black");

    var shperlable = new zebra.ui.designer.ShaperPan(lable);

    return shperlable;

}

//function for drawing text box
function editDrawTextBox(tx, ty, tw, th) {
    var textBox = new zebra.ui.TextField("type here...");
    textBox.setBounds(tx, ty, tw, th);

    var shpertextBox = new zebra.ui.designer.ShaperPan(textBox);

    return shpertextBox;
}

//function for drawing Horizontal Line
function editDrawHorizontalLine(hx, hy, hw) {

    var horizontal = new zebra.ui.Line();
    horizontal.setBounds(hx, hy, hw, 3);
    var shperhorizontal = new zebra.ui.designer.ShaperPan(horizontal);

    return shperhorizontal;
}

//function for drawing checkBox
function editDrawCheckBox(checkX, checkY, checkW, checkH, checkValue) {

    var checkBox = new zebra.ui.Checkbox(checkValue);
    checkBox.setBounds(checkX, checkY, checkW, checkH);
    var shpercheckBox = new zebra.ui.designer.ShaperPan(checkBox);

    return shpercheckBox;
}

//function for drawing radio button
function editDrawRadioButton(radioX, radioY, radioW, radioH, radioValue) {
    var radioButton = new zebra.ui.Radiobox(radioValue);
    radioButton.setBounds(radioX, radioY, radioW, radioH);

    var shpercheckBox = new zebra.ui.designer.ShaperPan(radioButton);

    return shpercheckBox;
}

//function for drawing combo box
function editDrawComboBox(comboX, comboY, comboW, comboH) {
    var combo = new zebra.ui.Combo(new zebra.ui.List([
   "Item 1",
   "Item 2",
   "Item 3"
    ]));

    combo.setBounds(comboX, comboY, comboW, comboH);
    var shpercombo = new zebra.ui.designer.ShaperPan(combo);

    return shpercombo;
}

//function for drawing TextArea
function editDrawTextArea(textAreaX, textAreaY, textAreaW, textAreaH) {
    var textArea = new zebra.ui.TextArea();
    textArea.setBounds(textAreaX, textAreaY, textAreaW, textAreaH);
    var shpertextArea = new zebra.ui.designer.ShaperPan(textArea);

    return shpertextArea;
}

//function for drawing combo box
function editDrawHyperlink(hyX, hyY, hyW, hyH, hyperValue) {
    var hyperlink = new zebra.ui.Link(hyperValue);
    hyperlink.setBounds(hyX, hyY, hyW, hyH);
    hyperlink.setColor("#2E64FE");
    var shperhyperlink = new zebra.ui.designer.ShaperPan(hyperlink);

    return shperhyperlink;
}

//function for drawing Menubar
function editDrawMenuBar(menuX, menuY, menuW, menuH) {
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
    var shpermenu = new zebra.ui.designer.ShaperPan(menu);


    return shpermenu;
}

//function for drawing image
function editDrawImageContent(imageX, imageY, imageW, imageH) {
    var image = new zebra.ui.ImagePan();

    image.setBounds(imageX, imageY, imageW, imageH);
    var shperimage = new zebra.ui.designer.ShaperPan(image);


    return shperimage;
}

