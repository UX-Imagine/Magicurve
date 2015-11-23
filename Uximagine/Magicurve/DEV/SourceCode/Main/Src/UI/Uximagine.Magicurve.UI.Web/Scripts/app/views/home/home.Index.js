zebra.ready(function () {
    // build Zebra canvas component that adds new
    // Canvas DOM element into page with the given size
    var zCanvas = new zebra.ui.zCanvas("designer", 970, 820);
    //zCanvas.setBackground("red");
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
    labaleImage.setBounds(425,65,450,75)
    header.add(labaleImage);


    /*
    // insert image 
        var image = new zebra.ui.ImagePan(); // create panel
        image.setBounds(0, 0, 950, 150); // shape panel
        image.setBackground("red");    // set yellow background
        header.add(image);*/

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

    

    // fill root with UI components
    var label = new zebra.ui.Button("Label").properties({
        value: true,
        location: [90, 50]
    });
    //label.setBounds(10, 10, 100, 50);



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

    var comboBox = new zebra.ui.Combo();
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


    zebra.ui.MouseEvent ( zebra.ui.Button("Submit"),  zebra.ui.MouseEvent.DRAGGED,  100,  100 ,zebra.ui.MouseEvent.LEFT_BUTTON,  1 )



    var footer = new zebra.ui.Panel();
    footer.setBounds(5, 761, 950, 45); // shape panel
    footer.setBackground("#F2F2F2");    // set yellow background
    p.add(footer);

    var hyperlink = new zebra.ui.Link("This is a hyperlink");
    hyperlink.setBounds(350, 20, 150, 20);
    hyperlink.setColor("#2E64FE");
    footer.add(hyperlink);

});