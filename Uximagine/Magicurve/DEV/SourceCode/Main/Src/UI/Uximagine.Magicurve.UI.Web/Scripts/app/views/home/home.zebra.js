zebra.ready(function () {
    // build Zebra canvas component that adds new
    // Canvas DOM element into page with the given size
    var zCanvas = new zebra.ui.zCanvas("designer", 1300, 1300);
    zCanvas.setBackground("black");
    var root = zCanvas.root; // save reference to root UI component

  //  var root = (new zCanvas("designer", 400, 300)).root;

    // build the following UI Components hierarchy:
    //  root
    //   +--zebra.ui.Panel
    //        +-- zebra.ui.Label
    //        +-- zebra.ui.Button
    //        +-- zebra.ui.Panel
    //              +--- zebra.ui.TextField  
    var p = new zebra.ui.Panel(); // create panel
    p.setBounds(10, 10, 380, 280); // shape panel
    p.setBackground("green");    // set yellow background
    root.add(p);                  // add panel to root

    var d = new zebra.ui.Panel(); // create panel
    d.setBounds(400, 10, 600, 280); // shape panel
    d.setBackground("red");    // set yellow background
    root.add(d);                  // add panel to root

    var b = new zebra.ui.Button("Test Button"); // create button
    b.setBounds(10, 60, 360, 50); // shape button
  //  d.add(b);                     // add the button as a kid to panel

    var l = new zebra.ui.Label("Test Label"); // create label
    l.setBounds(10, 10, 360, 50); // shape label
    p.add(l);                     // add the label as a kid to panel
    
    var b = new zebra.ui.Button("Test Button"); // create button
    b.setBounds(10, 60, 360, 50); // shape button
    p.add(b);                     // add the button as a kid to panel

    var pp = new zebra.ui.Panel();  // create one more panel
    pp.setBounds(10, 120, 360, 150);// shape panel
    pp.setBackground("orange");     // set orange background
    p.add(pp);    // add the panel as a kid of panel

    var tf = new zebra.ui.TextField("Test Text Field");
    tf.setBounds(30, 10, 200, 30);
   
    pp.add(tf);

    var b = new zebra.ui.Checkbox("check");
    b.setBounds(10, 60, 360, 50); // shape button
    d.add(b);

    var b = new zebra.ui.Radiobox("radio");
    b.setBounds(80, 60, 360, 50); // shape button
    d.add(b);
    var b = new zebra.ui.Button("button");
    
    b.setBounds(160, 70, 50, 25); // shape button
    d.add(b);

    var b = new zebra.ui.Combo("Combo");
    b.setBounds(220, 70, 50, 25); // shape button
    d.add(b);

    var b = new zebra.ui.HtmlTextArea("TextArea");
    
    b.setBounds(275, 70, 150, 25); // shape button
    d.add(b);
   
    var b = new zebra.ui.HtmlTextField("HtmlTextField");
    b.setBounds(10, 100, 150, 25); // shape button
    d.add(b);

    var b = new zebra.ui.Line("line");
    b.setLineColors("black");
    b.setBounds(170, 100, 150, 25); // shape button
    d.add(b);

    var b = new zebra.ui.MenuItem("menu");
   
    b.setBounds(170, 100, 100, 25); // shape button
    d.add(b);

    var b = new zebra.ui.Progress("PassTextField");
    b.setBounds(270, 100, 150, 25); // shape button
    d.add(b);
    var b = new zebra.ui.Tool("Progress");
    b.setBounds(10, 100, 150, 25); // shape button10
    d.add(b);
});

