zebra.ready(function () {
    eval(zebra.Import("ui", "layout"));

    //create json object array
    var jsonObj = [{ "controlsName": "Button", "width": 50, "height": 60 }, { "controlsName": "CheckBox", "width": 100, "height": 60 }, { "controlsName": "panel", "width": 100, "height": 60 }];

    var controls = [];
    //controls.length = jsonObj.length;

    //function for drawing button
    function drawButton(bwidth, bheight) {
        var button = new zebra.ui.Button("Button");
        button.properties({
            width: bwidth,
            height: bheight

        });
        var buttonShaperPan = new zebra.ui.designer.ShaperPan(button);
        buttonShaperPan.properties({
            value: true,
            location: [90, 50]

        });

       

        return buttonShaperPan;

    }

    //function for drawing button
    function drawPanel(px,py,pw,ph) {
        var panel = new zebra.ui.Panel();
        panel.setBounds(px, py, pw, ph);
        panel.setBackground("red");

        var shperPanel = new zebra.ui.designer.ShaperPan(panel);

        return shperPanel;

    }

    //function for drawing lable
    function drawLable(lx,ly,lw,lh) {
        var lable = new zebra.ui.Label("lable");
        lable.setBounds(lx, ly, lw, lh);
        lable.setColor("black");

          var shperlable = new zebra.ui.designer.ShaperPan(lable);

          return shperlable;

    }

    //function for drawing text box
    function drawTextBox(tx,ty,tw,th) {
        var textBox = new zebra.ui.TextField("type here...");
        textBox.setBounds(tx, ty, tw, th);

        var shpertextBox = new zebra.ui.designer.ShaperPan(textBox);

        return shpertextBox;
    }

    //function for drawing Horizontal Line
    function drawHorizontalLine(hx,hy,hw) {

        var horizontal = new zebra.ui.Line();
        horizontal.setBounds(hx, hy, hw, 3);
        var shperhorizontal = new zebra.ui.designer.ShaperPan(horizontal);

        return shperhorizontal;
    }

    //function for drawing checkBox
    function drawCheckBox(checkX,checkY,checkW,checkH) {

        var checkBox = new zebra.ui.Checkbox("Java");
        checkBox.setBounds(checkX, checkY, checkW, checkH);
        var shpercheckBox = new zebra.ui.designer.ShaperPan(checkBox);

        return shpercheckBox;
    }

    //function for drawing radio button
    function drawRadioButton(radioX,radioY,radioW,radioH) {
        var radioButton = new zebra.ui.Radiobox("radio");
        radioButton.setBounds(radioX, radioY, radioW, radioH);
        var shpercheckBox = new zebra.ui.designer.ShaperPan(radioButton);

        return shpercheckBox;
    }

    //function for drawing combo box
    function drawComboBox(comboX,comboY,comboW,comboH) {
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
    function drawTextArea(textAreaX, textAreaY, textAreaW, textAreaH) {
        var textArea = new zebra.ui.TextArea();
        textArea.setBounds(textAreaX, textAreaY, textAreaW, textAreaH);
        var shpertextArea = new zebra.ui.designer.ShaperPan(textArea);

        return shpertextArea;
    }

    //function for drawing combo box
    function drawHyperlink(hyX,hyY,hyW,hyH) {
        var hyperlink = new zebra.ui.Link("This is a hyperlink");
        hyperlink.setBounds(hyX, hyY, hyW, hyH);
        hyperlink.setColor("#2E64FE");
        var shperhyperlink = new zebra.ui.designer.ShaperPan(hyperlink);

        return shperhyperlink;
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
        var shpermenu = new zebra.ui.designer.ShaperPan(menu).properties({value : true});

        return shpermenu;
    }

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].controlsName == "Button") {
            controls[controlsValid] = drawButton(jsonObj[i].width, jsonObj[i].height);
            controlsValid++;
        } else if (jsonObj[i].controlsName == "CheckBox") {
            controls[controlsValid] = drawCheckBox(jsonObj[i].width, jsonObj[i].height);
            controlsValid++;
        }
    }

    var root = (new zCanvas("designer", 1000, 400)).root;
  
    var pn = new drawPanel(600,10,50,50);
    root.add(pn);
    var butt = new drawButton(50, 50);
    root.add(butt);
    
    var lable1 = new drawLable(0,0,100,50);
    root.add(lable1);

    var text = new drawTextBox(500,80,80,30);
    root.add(text);

    var hor = new drawHorizontalLine(0, 330, 250);
    root.add(hor);

    var radio = new drawRadioButton(20, 200, 80, 30);
    root.add(radio);

    var check = new drawCheckBox(20, 290, 80, 30);
    root.add(check);

    var com = new drawComboBox(130, 350, 100, 30);
    root.add(com);

    var texA = new drawTextArea(20, 300, 180, 100);
    root.add(texA);

    var lin = new drawHyperlink(350, 20, 150, 20);
    root.add(lin);

    var me = new drawMenuBar(0, 110, 950, 40);
    root.add(me);

  /*  root.properties({
        layout: new BorderLayout(4, 4),
        border: new Border(),
        padding: 8,
        kids: {
            CENTER: new BorderPan("UX panel", new Panel({
                //padding: 50,
                kids: controls
            })),
         
            BOTTOM: new Button("Align", [
                function fire() {
                    this.$super();
                    var y = 10, c = root.findAll("//zebra.ui.designer.ShaperPan");
                    for (var i = 0; i < c.length; i++) {
                        c[i].toPreferredSize();
                        c[i].setLocation(10, y);
                        y += c[i].height + 5;
                    }
                }
            ])
        }
    });*/







});


