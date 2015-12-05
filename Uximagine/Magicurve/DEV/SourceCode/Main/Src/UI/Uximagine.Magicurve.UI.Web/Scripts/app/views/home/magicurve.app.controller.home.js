magicurveApp.controller("homeController",
    function ($scope, toaster, homeService) {
        $scope.message = "Hello";
        $scope.spices = homeService.shape;
         jsonObject = $scope.spices;
       //control = genarateDesign(jsonObject);
        // alert("test this");
         drawButton(300,300,60,30,"test");
       // alert(control[0]);
      //  drawDesign();
       //alert();
        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
            
        };
    });


//////////////// method drawing non editable ////////////////////

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


///////////////////////////editable controlles //////////////////////////////


//function for drawing button
function editDrawButton(bX,bY,bwidth, bheight, ButttonCaption) {
    var button = new zebra.ui.Button(ButttonCaption);
    button.setBounds(bX, bY, bwidth, bheight, ButttonCaption);

    var buttonShaperPan = new zebra.ui.designer.ShaperPan(button);

    return buttonShaperPan;

}

//function for drawing button
function editDrawPanel(px, py, pw, ph) {
    var panel = new zebra.ui.Panel();
    panel.setBounds(px, py, pw, ph);
    panel.setBackground("white");

    //var shperPanel = new zebra.ui.designer.ShaperPan(panel);

    return panel;

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