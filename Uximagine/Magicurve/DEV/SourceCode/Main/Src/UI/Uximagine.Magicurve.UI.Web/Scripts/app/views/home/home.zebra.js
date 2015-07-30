zebra.ready(function () {
    // build Zebra canvas component that adds new
    // Canvas DOM element into page with the given size
    var zCanvas = new zebra.ui.zCanvas("designer", 1300, 1300);
    zCanvas.setBackground("red");
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
    p.setBackground("yellow");    // set yellow background
    root.add(p);                  // add panel to root
   
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

   
});