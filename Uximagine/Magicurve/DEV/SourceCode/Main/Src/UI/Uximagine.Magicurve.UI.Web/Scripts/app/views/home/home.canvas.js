
// Rectangle with color
function draw_filled_rect(x, y, width, height,color) {
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    context.fillStyle = color;
    context.fillRect(x, y, width, height); //Draws a filled rectangle
}

// Rectangle without color - transparent
function draw_trans_rect(x, y, width, height) {
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    context.fillRect(x, y, width, height);//Draws a filled rectangle
    context.clearRect(x, y, width, height);//Clears the specified rectangular area, making it fully transparent
    context.strokeRect(x, y, width, height); //Draws a rectangular outline
}

// Circule
function draw_circle(left, top, size, curve) {
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    context.beginPath();
    context.arc(left, top, size, curve, 2 * Math.PI);//arc(left,top,size,curve,--)
    context.stroke();
}

/*function form()
{
	var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
	createButton(canvas, 'b1', 10, 10, 100, 30, 'Google Search', '#0000FF', 12, 
  '12pt Ariel', 5, highestDepth, 1, 0, null, null, '#bee6fd', '#a7d9f5', 
  '#eaf6fd', '#d9f0fc', '#3c7fb1', null, 1, 'http://www.google.com');
		
}*/

function draw_blank_triangle(moveX, moveY, lineX1, lineY1, lineX2, lineY2) {
    var canvas = document.getElementById('myCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.beginPath();
        ctx.moveTo(moveX, moveY);
        ctx.lineTo(lineX1, lineY1);
        ctx.lineTo(lineX2, lineY2);
        ctx.closePath();
        ctx.stroke();

    }
}

function draw_filled_trangle(moveX, moveY, lineX1, lineY1, lineX2, lineY2) {
    var canvas = document.getElementById('myCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.beginPath();
        ctx.moveTo(moveX, moveY);
        ctx.lineTo(lineX1, lineY1);
        ctx.lineTo(lineX2, lineY2);
        ctx.fill();
    }

}

zebra.ready(function () {
    // import all classes, functions, constants
    // from zebra.ui, zebra.layout packages
    eval(zebra.Import("ui", "layout"));

    // create canvas
    var root = (new zCanvas(400, 400)).root;
    root.properties({
        layout: new BorderLayout(8, 8),
        border: new Border(),
        padding: 8,
        kids: {
            CENTER: new TextField("Hi ...\n", true),
            BOTTOM: new Button("Clear").properties({
                canHaveFocus: false
            })
        }
    });

    root.find("//zebra.ui.Button").bind(function () {
        root.find("//zebra.ui.TextField").setValue("");
    });
});

window.onclick = function () {
    //draw_filled_rect(10, 10, 100, 100, "red");
    //draw_trans_rect(210, 10, 100, 100);
    //draw_circle(300, 200, 50, 0);
    ////form();
    //draw_blank_triangle(325, 50, 350, 75, 350, 25);
    //draw_filled_trangle(375, 100, 400, 125, 400, 75);
    /*createButton(elemId, "b1", 10, 10, 100, 30, "Google Search", "#0000FF", 12, 
						  "12pt Ariel", 5, highestDepth, 1, 0, null, null, "#bee6fd", "#a7d9f5", 
						  "#eaf6fd", "#d9f0fc", "#3c7fb1", null, 1, "http://www.google.com");*/


}