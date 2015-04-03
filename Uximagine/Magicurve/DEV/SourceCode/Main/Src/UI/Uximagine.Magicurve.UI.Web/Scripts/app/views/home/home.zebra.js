zebra.ready(function () {
    eval(zebra.Import("ui", "layout"));

    //create json object array
    var jsonObj = [{ "controlsName": "Button" }, { "controlsName": "CheckBox" }];

    var controls = [];
    //controls.length = jsonObj.length;

    

    //function for drawing checkbox
    function drawCheckBox() {
        var checkbox = new zebra.ui.Checkbox("CheckBox");
        var checkboxShaperPan = new zebra.ui.designer.ShaperPan(checkbox);
        checkboxShaperPan.properties({
            value: true,
            location: [10, 10]

        });
        return checkboxShaperPan;

    }


    //function for drawing button
    function drawButton() {
        var button = new zebra.ui.Button("Button");
        var buttonShaperPan = new zebra.ui.designer.ShaperPan(button);
        buttonShaperPan.properties({
            value: true,
            location: [90,50]

        });

        buttonShaperPan.extend([
        function mouseDragged(e) {
            this.$super(e);
            console.log(e);
        }

        ]);

        return buttonShaperPan;

    }

    var controlsValid = 0;

    for (var i = 0; i < jsonObj.length; i++) {
        if (jsonObj[i].controlsName == "Button") {
            controls[controlsValid] = drawButton();
            controlsValid++;
        } else if(jsonObj[i].controlsName == "CheckBox"){
            controls[controlsValid] = drawCheckBox();
            controlsValid++;
        }
    }

    //function assignKids(){

        

    //}
    


    var root = (new zCanvas("designer", 1000, 400)).root;
    //var p = new zebra.ui.Button("Test");
    //var testShaperPan = new zebra.ui.designer.ShaperPan(p);
    //var drawCheckBox = drawCheckBox();
    var drawButton = drawButton();
  
    root.properties({
        layout: new BorderLayout(4, 4),
        border: new Border(),
        padding: 8,
        kids: {
            CENTER: new BorderPan("Designer panel", new Panel({
                padding: 6,
                kids: controls//[

                    //drawCheckBox,

                    //testShaperPan.properties({
                    //    value: true,
                    //    location: [10, 50]

                    //}),

                    //drawButton,

                    //new zebra.ui.designer.ShaperPan(new Button("Button").properties({
                    //    value: true,
                    //    location: [90, 50]
                    //})

                        //).properties({
                        //    isResizeEnabled: true
                        //}),

                //    new zebra.ui.designer.ShaperPan(new Slider().properties({
                //        value: true,
                //        size: [120, 60],
                //        location: [30, 100]
                //    }))
                //]
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