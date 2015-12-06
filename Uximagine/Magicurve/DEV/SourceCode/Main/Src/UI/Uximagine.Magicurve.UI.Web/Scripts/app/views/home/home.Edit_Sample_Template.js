zebra.ready(function () {
    eval(zebra.Import("ui", "layout"));

    var root = (new zCanvas("editDesigner", 970, 820)).root;
    root.setBackground("black");

    var pn1 = new editDrawPanel(5, 5, 960, 810);
    pn1.setBackground("white");
    root.add(pn1);

    var pn2 = new editDrawPanel(700, 155, 260, 655);
    pn2.setBackground("#CEF6F5");
    root.add(pn2);

    var image1 = new editDrawImageContent(5, 5, 950, 150);
    root.add(image1);

    var me = new editDrawMenuBar(10, 110, 950, 40);
    root.add(me);

    var lable1 = new editDrawLable(720, 183, 80, 30, "User Name");
    root.add(lable1);

    var text = new editDrawTextBox(820, 183, 120, 30);
    root.add(text);

    var lable2 = new editDrawLable(720, 235, 80, 30, "Password");
    root.add(lable2);

    var text1 = new editDrawTextBox(820, 235, 120, 30);
    root.add(text1);


    var hor = new editDrawHorizontalLine(700, 330, 250);
    root.add(hor);

    var radio1 = new editDrawRadioButton(720, 370, 80, 30, "Male");
    root.add(radio1);
    var radio2 = new editDrawRadioButton(720, 400, 80, 30, "Female");
    root.add(radio2);

    var hor1 = new editDrawHorizontalLine(700, 450, 250);
    root.add(hor1);

    var check1 = new editDrawCheckBox(720, 470, 80, 30, "Java");
    root.add(check1);

    var check2 = new editDrawCheckBox(720, 500, 80, 30, "C#");
    root.add(check2);

    var hor2 = new editDrawHorizontalLine(700, 570, 250);
    root.add(hor2);


    var com = new editDrawComboBox(720, 600, 100, 30);
    root.add(com);


    var texA = new editDrawTextArea(720, 650, 180, 100);
    root.add(texA);

    var butt = new editDrawButton(720, 760, 60, 30, "submit");
    root.add(butt);

    var lin = new editDrawHyperlink(350, 780, 150, 20, "this is a hyperlink");
    root.add(lin);

});


///////////////////////////editable controlles ///////////////////


//function for drawing button
function editDrawButton(bX, bY, bwidth, bheight, ButttonCaption) {
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

