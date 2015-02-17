

function draw_rect(x, y, width, height) {
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    context.fillStyle = "green";
    context.fillRect(x, y, width, height); //Draws a filled rectangle

    /*context.fillStyle = "red";
      context.fillRect(25, 125, width, height);
	  context.clearRect(45, 145, width-40, height-40);//Clears the specified rectangular area, making it fully transparent
			
	  context.strokeRect(x+200, y, width, height); //Draws a rectangular outline
			*/

}

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


window.onclick = function () {
    draw_rect(10, 10, 100, 100);
    draw_circle(300, 200, 50, 0);
    //form();
    draw_blank_triangle(325, 50, 350, 75, 350, 25);
    draw_filled_trangle(375, 100, 400, 125, 400, 75);
    /*createButton(elemId, "b1", 10, 10, 100, 30, "Google Search", "#0000FF", 12, 
						  "12pt Ariel", 5, highestDepth, 1, 0, null, null, "#bee6fd", "#a7d9f5", 
						  "#eaf6fd", "#d9f0fc", "#3c7fb1", null, 1, "http://www.google.com");*/



}