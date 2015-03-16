﻿zebra.ready(function () {
    eval(zebra.Import("ui", "layout"));

    var root = (new zCanvas("designer", 300, 300)).root;
    root.properties({
        layout: new BorderLayout(4, 4),
        border: new Border(),
        padding: 8,
        kids: {
            CENTER: new BorderPan("Designer panel", new Panel({
                padding: 6,
                kids: [
                    new zebra.ui.designer.ShaperPan(new Checkbox("Check-box").properties({
                        value: true,
                        location: [10, 10]
                    })),

                    new zebra.ui.designer.ShaperPan(new Button("Button").properties({
                        value: true,
                        location: [90, 50]
                    })),

                    new zebra.ui.designer.ShaperPan(new Slider().properties({
                        value: true,
                        size: [120, 60],
                        location: [30, 100]
                    }))
                ]
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