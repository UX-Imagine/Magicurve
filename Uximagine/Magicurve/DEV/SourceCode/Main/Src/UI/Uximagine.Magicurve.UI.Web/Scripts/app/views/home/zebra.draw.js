CanvasRenderingContext2D.prototype.clear =
  CanvasRenderingContext2D.prototype.clear || function (preserveTransform) {
      if (preserveTransform) {
          this.save();
          this.setTransform(1, 0, 0, 1, 0, 0);
      }

      this.clearRect(0, 0, this.canvas.width, this.canvas.height);

      if (preserveTransform) {
          this.restore();
      }
  };

function editable(control, index) {
    var shaperPan = new zebra.ui.designer.ShaperPan(control, [
       function mousePressed(e) {
           stage.activeControlIndex = index;
           if (typeof onSelectItem == "function") {
               onSelectItem(index);
           }
       },
       function mouseReleased(e) {
           stage.controls[index].X = shaperPan.x;
           stage.controls[index].Y = shaperPan.y;
           
       },
        function mouseDragEnded(e) {
            stage.controls[index].Width = shaperPan.width;
            stage.controls[index].Height = shaperPan.height;
        }
    ]);

    return shaperPan;
}


//////////////// method drawing non editable ///////////


//function for drawing i frame
function drawIframe(dX, dY) {
    var iframePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
    var imageIframe = new zebra.ui.ImagePan(iframePic);
    imageDatePic.setBounds(dX, dY, 100, 100);

    // var shperData = new zebra.ui.designer.ShaperPan(imageDatePic);

    return imageIframe;

}

//function for drawing date pic
function drawDatePic(dX, dY) {
    var datePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
    var imageDatePic = new zebra.ui.ImagePan(datePic);
    imageDatePic.setBounds(dX, dY, 140, 40);

   // var shperData = new zebra.ui.designer.ShaperPan(imageDatePic);
   
    return imageDatePic;

}

//function for drawing rang
function drawRage(dX, dY) {
    var rangePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/range.png");
    var imageRangePic = new zebra.ui.ImagePan(rangePic);
    imageRangePic.setBounds(dX, dY, 140, 30);

    // var shperData = new zebra.ui.designer.ShaperPan(imageRangePic);

    return imageRangePic;

}

//function for drawing para
function drawPara(dX, dY,paraWidth,paraHeight) {
    var paraImage = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/paragrap.png");
    var imageParaImage = new zebra.ui.ImagePan(paraImage);
    imageParaImage.setBounds(dX, dY, paraWidth, paraHeight);

    // var shperData = new zebra.ui.designer.ShaperPan(imageParaImage);

    return imageParaImage;

}

//function for drawing button
function drawButton(bX, bY, bwidth, bheight, ButttonCaption) {
    var button = new zebra.ui.Button(ButttonCaption);
    button.setBounds(bX, bY, bwidth, bheight);
    return button;

}

//function for drawing button
function drawPanel(px, py, pw, ph, panelColor) {
    var panel = new zebra.ui.Panel();
    panel.setBounds(px, py, pw, ph);
    panel.setBackground(panelColor);

    return panel;

}

//function for drawing lable
function drawLable(lx, ly, lw, lh, labaleCaption) {
    var lable = new zebra.ui.Label(labaleCaption);
    lable.setBounds(lx, ly, lw, lh);
    lable.setColor("black");

    return lable;

}

//function for drawing text box
function drawTextBox(tx, ty, tw, th) {
    var textBox = new zebra.ui.TextField("Type Here...");
    textBox.setBounds(tx, ty, tw, th);

    return textBox;
}

//function for drawing Horizontal Line
function drawHorizontalLine(hx, hy, hw) {

    var horizontal = new zebra.ui.Line();
    horizontal.setBounds(hx, hy, hw, 3);

    return horizontal;
}

//function for drawing checkBox
function drawCheckBox(checkX, checkY, checkW, checkH, checkValue) {

    var checkBox = new zebra.ui.Checkbox(checkValue);
    checkBox.setBounds(checkX, checkY, checkW, checkH);

    return checkBox;
}

//function for drawing radio button
function drawRadioButton(radioX, radioY, radioW, radioH, radioValue) {
    var radioButton = new zebra.ui.Radiobox(radioValue);
    radioButton.setBounds(radioX, radioY, radioW, radioH);

    return radioButton;
}

//function for drawing combo box
function drawComboBox(comboX, comboY, comboW, comboH) {
    var combo = new zebra.ui.Combo(new zebra.ui.List([
   "Item 1",
   "Item 2",
   "Item 3"
    ]));

    combo.setBounds(comboX, comboY, comboW, comboH);
    return combo;
}

//function for drawing TextArea
function drawTextArea(textAreaX, textAreaY, textAreaW, textAreaH) {
    var textArea = new zebra.ui.TextArea();
    textArea.setBounds(textAreaX, textAreaY, textAreaW, textAreaH);

    return textArea;
}

//function for drawing combo box
function drawHyperlink(hyX, hyY, hyW, hyH, hyperValue) {
    var hyperlink = new zebra.ui.Link(hyperValue);
    hyperlink.setBounds(hyX, hyY, hyW, hyH);
    hyperlink.setColor("#2E64FE");

    return hyperlink;
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

    return menu;
}

//function for drawing image
function drawImageContent(imageX, imageY, imageW, imageH) {
  
    var image = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/image.png");
    var imageImage = new zebra.ui.ImagePan(image);
    imageImage.setBounds(imageX, imageY, imageW, imageH);

    // var shperData = new zebra.ui.designer.ShaperPan(imageImage);

    return imageImage;
}

