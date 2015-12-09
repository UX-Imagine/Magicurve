
zebra.ready(function () {

    var zCanvas = new zebra.ui.zCanvas("smpleDesigner", 970, 820);
    var root = zCanvas.root; // save reference to root UI component
    root.setBackground("black")

    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(5, 5, 960, 810); // shape panel
    p.setBackground("white");    // set yellow background
    root.add(p);                  // add panel to root

    var header = new zebra.ui.Panel(); // create panel
    header.setBounds(5, 5, 950, 150); // shape panel
    header.setBackground("#F2F2F2");    // set yellow background
    p.add(header);

    var labaleImage = new zebra.ui.Label("This is image");
    labaleImage.setBounds(425, 65, 450, 75)
    header.add(labaleImage);

    var m = new zebra.ui.Menubar({
        "Item 1": {
            "Subitem 1.1": null,
            "Subitem 1.2": null,
            "Subitem 1.3": null
        },
        "Item 2": {
            "Subitem 2.1": null,
            "Subitem 2.2": null,
            "Subitem 2.3": null
        },
        "Item 3": null
    });

    m.setBounds(0, 110, 950, 40);

    header.add(m);

    var body = new zebra.ui.Panel();
    body.setBounds(5, 159, 950, 600); // shape panel
    body.setBackground("#E6E6E6");    // set yellow background
    p.add(body);

    var sideBar = new zebra.ui.Panel();
    sideBar.setBounds(700, 0, 248, 600);
    sideBar.setBackground("#CEF6F5");
    body.add(sideBar);

    var lable1 = new zebra.ui.Label("User Name");
    lable1.setBounds(20, 33, 80, 30);
    lable1.setColor("black");
    sideBar.add(lable1);

    var textBox = new zebra.ui.TextField("type here...");
    textBox.setBounds(120, 25, 120, 30);
    sideBar.add(textBox);

    var lable2 = new zebra.ui.Label("Password");
    lable2.setBounds(20, 77, 80, 30);
    lable2.setColor("black");
    sideBar.add(lable2);

    var passWord = new zebra.ui.TextField();
    passWord.setBounds(120, 70, 120, 30);
    sideBar.add(passWord);

    var horizontal = new zebra.ui.Line();
    horizontal.setBounds(0, 150, 250, 3);
    sideBar.add(horizontal);

    var radioButton1 = new zebra.ui.Radiobox("Male");
    radioButton1.setBounds(20, 170, 80, 30);
    sideBar.add(radioButton1);

    var radioButton2 = new zebra.ui.Radiobox("Female");
    radioButton2.setBounds(20, 200, 80, 30);
    sideBar.add(radioButton2);

    var horizonta2 = new zebra.ui.Line();
    horizonta2.setBounds(0, 250, 250, 3);
    sideBar.add(horizonta2);

    var checkBox1 = new zebra.ui.Checkbox("Java");
    checkBox1.setBounds(20, 270, 80, 30);
    sideBar.add(checkBox1);

    var checkBox2 = new zebra.ui.Checkbox("C#");
    checkBox2.setBounds(20, 290, 80, 30);
    sideBar.add(checkBox2);

    var horizonta3 = new zebra.ui.Line();
    horizonta3.setBounds(0, 330, 250, 3);
    sideBar.add(horizonta3);

    var lable3 = new zebra.ui.Label("Select Country");
    lable3.setBounds(20, 360, 100, 30);
    lable3.setColor("black");
    sideBar.add(lable3);

    var comboBox = new zebra.ui.Combo([
        "Srilanka",
        "USA",
        "Australia",
    ]);
    comboBox.setBounds(130, 350, 100, 30);
    sideBar.add(comboBox);

    var horizonta4 = new zebra.ui.Line();
    horizonta4.setBounds(0, 400, 250, 3);
    sideBar.add(horizonta4);

    var textArea = new zebra.ui.TextArea();
    textArea.setBounds(20, 420, 180, 100);
    sideBar.add(textArea);


    var button = new zebra.ui.Button("Submit"); // create button
    button.setBounds(20, 550, 60, 30); // shape button
    sideBar.add(button);


    zebra.ui.MouseEvent(zebra.ui.Button("Submit"), zebra.ui.MouseEvent.DRAGGED, 100, 100, zebra.ui.MouseEvent.LEFT_BUTTON, 1)



    var footer = new zebra.ui.Panel();
    footer.setBounds(5, 761, 950, 45); // shape panel
    footer.setBackground("#F2F2F2");    // set yellow background
    p.add(footer);

    var hyperlink = new zebra.ui.Link("This is a hyperlink");
    hyperlink.setBounds(350, 20, 150, 20);
    hyperlink.setColor("#2E64FE");
    footer.add(hyperlink);

    genarateDesign();

});

function genarateDesign() {

    // ajustToCanvasSize();

    //create json object array
    var jsonObj = [
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
         "X": 10,
         "Y": 10,
         "Width": 60,
         "Height": 30,
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
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     }];

    //  alert(jsonObj);
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

//////////////// cavers resalution /////////////////////

function ajustToCanvasSize() {

    // alert();
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
