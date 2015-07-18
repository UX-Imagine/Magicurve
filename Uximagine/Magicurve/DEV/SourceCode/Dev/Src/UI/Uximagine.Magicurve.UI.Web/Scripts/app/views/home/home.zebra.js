zebra.ready(function () {
    eval(zebra.Import("ui", "layout"));

    //create json object array
    var jsonObj = [{ "controlsName": "Button", "width": 50, "height": 60 }, { "controlsName": "CheckBox", "width": 100, "height": 60 }];

    var controls = [];
    //controls.length = jsonObj.length;



    //function for drawing checkbox
    function drawCheckBox(cwidth,cheight) {
        var checkbox = new zebra.ui.Checkbox("CheckBox");
        checkbox.properties({
            width: cwidth,
            height: cheight,
        });
        var checkboxShaperPan = new zebra.ui.designer.ShaperPan(checkbox);
        checkboxShaperPan.properties({
            value: true,
            location: [10, 10]

        });
        checkboxShaperPan.extend([
        function mouseDragged(e) {
            
            var dy = (e.absY - this.py),
                    dx = (e.absX - this.px),
                    s = this.state,
                    nw = this.width - dx * s.left + dx * s.right,
                    nh = this.height - dy * s.top + dy * s.bottom;

            var controls_width = nw - 14;
            var controls_height = nh - 14;

            var width_differ = Math.abs(this.width-nw);
            var height_differ = Math.abs(this.height - nh);
            
            var newXlocation = (this.x + dx * s.left)+7;
            var newYlocation = (this.y + dy * s.top) + 7;

            if (dx != 0 || dy != 0) {
                console.log("shaper xlocation" + " " + (this.x + dx * s.left) + " " + "shaper ylocation" + " " + (this.y + dy * s.top));
                console.log("controller xlocation" + " " + newXlocation + " " + "controller ylocation" + " " + newYlocation);
            } else {
                console.log("Location Not Changed");
            }

            if (width_differ != 0 || height_differ != 0) {
                console.log("shaper width" + " " + nw + " " + "shaper height" + " " + nh);
                console.log("controller width" + " " + controls_width + " " + "controller height" + " " + controls_height);
            } else {
                 console.log("Size Not Changed");

            }
            //console.log("dy " + dy);
            //console.log("e.absY " + e.absY);
            //console.log("this.py " + this.py);
            //console.log("dx " + dx);
            //console.log("e.absX " + e.absX);
            //console.log("this.px " + this.px);
            //console.log("state " + s);
            //console.log("state left " + s.left);

            this.$super(e);;
        }

        ]);
        return checkboxShaperPan;

    }


    //function for drawing button
    function drawButton(bwidth,bheight) {
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

        buttonShaperPan.extend([
        function mouseDragged(e) {
            this.$super(e);

            var dy = (e.absY - this.py),
                    dx = (e.absX - this.px),
                    s = this.state,
                    nw = this.width - dx * s.left + dx * s.right,
                    nh = this.height - dy * s.top + dy * s.bottom;
            //console.log(nw + " " + nh);
            //var buttonWidth = e.kids.width;
            //console.log(buttonWidth);
            //console.log(e);
        }

        ]);

        return buttonShaperPan;

    }

    function drawComboBox() {
        var combo = new zebra.ui.Combo(new zebra.ui.List([
       "Item 1",
       "Item 2",
       "Item 3"
        ]));

    }

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].controlsName == "Button") {
            controls[controlsValid] = drawButton(jsonObj[i].width,jsonObj[i].height);
            controlsValid++;
        } else if (jsonObj[i].controlsName == "CheckBox") {
            controls[controlsValid] = drawCheckBox(jsonObj[i].width, jsonObj[i].height);
            controlsValid++;
        }
    }




    var root = (new zCanvas("designer", 1000, 400)).root;
    //var p = new zebra.ui.Button("Test");
    //var testShaperPan = new zebra.ui.designer.ShaperPan(p);
    //var drawCheckBox = drawCheckBox();
   // var drawButton = drawButton();

    root.properties({
        layout: new BorderLayout(4, 4),
        border: new Border(),
        padding: 8,
        kids: {
            CENTER: new BorderPan("Designer panel", new Panel({
                padding: 6,
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
    });



});