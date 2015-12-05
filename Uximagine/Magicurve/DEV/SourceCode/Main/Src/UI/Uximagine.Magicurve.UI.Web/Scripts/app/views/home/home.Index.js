﻿
zebra.ready(function () {
    getControls();

});

function draw(data) {
    var zCanvas = new zebra.ui.zCanvas("designer", 970, 820);
    var root = zCanvas.root; // save reference to root UI component
    root.setBackground("black");

    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(5, 5, 960, 810); // shape panel
    p.setBackground("white");    // set yellow background
    root.add(p);                  // add panel to root
    var arr = genarateDesign();

    root.add(arr[0]);
    //alert(arr[0]);
}

function getControls() {
    $.ajax(
       {
           url: root + "/api/images/result",
           type: "GET"
       }).done(function (data) {
           window.controls = data.controls;
           window.imageWidth = data.imageWidth;
            draw(data);
    }).fail(function (error) {
          
           console.log(error);
       });
}

function genarateDesign() {
    //create json object array
    var jsonObj = [
     {
         "Type": 0,
         "X": 200,
         "Y": 200,
         "Width": 60,
         "Height": 30,
         "EdgePoints": null
     },
     { 
         "Type": 8,
         "X": 5,
         "Y": 5,
         "Width": 950,
         "Height": 150,
         "EdgePoints": null
     },
     {
         "Type": 6,
         "X": 425,
         "Y": 65,
         "Width": 450,
         "Height": 75,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     }];

    var controls = [];
    //controls.length = jsonObj.length;

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].Type == 0) {
            controls[controlsValid] = drawButton(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "Button");
            controlsValid++;

        }
        else if (jsonObj[i].controlsName == 1) {
            controls[controlsValid] = drawCheckBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "CheckBox");
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 2) {
            controls[controlsValid] = drawComboBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 3) {
            controls[controlsValid] = drawHorizontalLine(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width);
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 4) {
            controls[controlsValid] = drawHyperlink(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "HyperLink");
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 5) {
            controls[controlsValid] = drawImageContent(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 6) {
            controls[controlsValid] = drawLable(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "LableCaption");
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 7) {
            controls[controlsValid] = drawMenuBar(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 8) {
            controls[controlsValid] = drawPanel(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "#E6E6E6");
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 9) {
            controls[controlsValid] = drawRadioButton(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height, "RadioButton");
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 10) {
            controls[controlsValid] = drawTextArea(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }
        else if (jsonObj[i].controlsName == 11) {
            controls[controlsValid] = drawTextBox(jsonObj[i].X, jsonObj[i].Y, jsonObj[i].Width, jsonObj[i].Height);
            controlsValid++;
        }

    }

    return controls;
}


//////////////// method drawing non editable ///////////

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
    var textBox = new zebra.ui.TextField("type here...");
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
    var image = new zebra.ui.ImagePan();

    image.setBounds(imageX, imageY, imageW, imageH);

    return image;
}

