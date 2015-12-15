magicurveApp
    .constant("urlConfig", {
        "domain": "/Magicurve",
        "controlUrl": "/api/images/result",
        "codeUrl": "/api/images/download",
        "uploadUrl": "/Home/UploadFile"
    })
    .directive('fileModel', [
                '$parse', 'httpService', 'toaster', '$rootScope', 'messagingService', 'canvasService',
        function ($parse, httpService, toaster, $rootScope, messagingService, canvasService) {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    var model = $parse(attrs.fileModel);
                    var modelSetter = model.assign;

                    element.bind('change', function () {

                        scope.$apply(function () {
                            modelSetter(scope, element[0].files[0]);
                        });

                        var file = element[0].files[0];
                        httpService.uploadFileToUrl(file, function (data) {
                            $rootScope.stage.currentImage = data.path;
                            messagingService.UploadSuccess();
                            messagingService.showMessage("Please click Draw or Edit on tool-bar to proceed...");
                        });

                        element.val(null);
                    });
                }
            };
        }
    ])
    .service('messagingService',
         function (toaster) {
             this.UploadSuccess = function () {
                 toaster.pop("success", "upload success");
             }

             this.success = function (message) {
                 toaster.pop("success", message);
             }
             this.UploadFail = function () {
                 toaster.pop("error", "upload failed");
             }

             this.fail = function (message) {
                 toaster.pop("error", message);
             }
             this.showMessage = function (message) {
                 toaster.pop("info", message);
             }
         }
    )
    .service('canvasService',
        function (messagingService, $rootScope) {
            var zCanvas;
            var root;
            var orWidth;
            var orHeight;
            var canvasHeight;
            var canvasWidth;
            var canvasService = this;

            this.init = function () {

                canvasHeight = $rootScope.stage.canvasHeight;
                canvasWidth = $rootScope.stage.canvasWidth;

                messagingService.showMessage("Initializing canvas. Please upload an image to begin..");

                zebra.ready(function () {
                    zCanvas = new zebra.ui.zCanvas("designer", canvasWidth, canvasHeight);
                    root = zCanvas.root;
                    messagingService.showMessage("canvas initialized.");
                });

                messagingService.showMessage("canvas initialized.");
            }

            this.populateCanvas = function (isEditable) {

                if ($rootScope.stage.controls === undefined) {
                    messagingService.fail("The controls are not loaded.");
                    return;
                }

                root.removeAll();
                var p = new zebra.ui.Panel(); // create panel
                p.setBounds(5, 5, canvasWidth, canvasHeight); // shape panel
                p.setBackground("white");    // set yellow background
                root.add(p);                  // add panel to root

                var controls = $rootScope.stage.controls;

                var controlItems = this.genarateDesign(controls, isEditable);

                for (var k = 0 ; k < controlItems.length; k++) {
                    root.add(controlItems[k].control);
                }
            }

            this.ajustSize = function () {
                orWidth = $rootScope.stage.imageWidth;
                orHeight = $rootScope.stage.imageHeight;

                for (var i = 0 ; i < $rootScope.stage.controls.length ; i++) {
                    var control = $rootScope.stage.controls[i];
                    control.X = Math.round((control.X * canvasWidth) / orWidth);
                    control.Y = Math.round((control.Y * canvasHeight) / orHeight);
                    control.Height = Math.round((control.Height * canvasHeight) / orHeight);
                    control.Width = Math.round((control.Width * canvasWidth) / orWidth);
                    $rootScope.stage.controls[i] = control;
                }
            }

            this.reverseSize = function () {
                for (var i = 0 ; i < $rootScope.stage.controls.length ; i++) {
                    var control = $rootScope.stage.controls[i];
                    control.X = Math.round((control.X * orWidth) / canvasWidth);
                    control.Y = Math.round((control.Y * orHeight) / canvasHeight);
                    control.Height = Math.round((control.Height * orHeight) / canvasHeight);
                    control.Width = Math.round((control.Width * orWidth) / canvasWidth);
                    $rootScope.stage.controls[i] = control;
                }
            }

            this.genarateDesign = function (sourceControls, isEditable) {
                var controls = Array(ControlItem);
                var controlsValid = 0;

                for (var i = 0; i < sourceControls.length; i++) {
                    var control = null;
                    var source = sourceControls[i];

                    if (sourceControls[i].Type === "Button") {
                        control = this.drawButton(source.X, source.Y, 60, 30, "Button");
                    }
                    else if (sourceControls[i].Type === "CheckBox") {
                        control = this.drawCheckBox(source.X, source.Y, 120, 30, "CheckBox");
                    }
                    else if (sourceControls[i].Type === "ComboBox") {
                        control = this.drawComboBox(source.X, source.Y, source.Width, 30);
                    }
                    else if (sourceControls[i].Type === "HLine") {
                        control = this.drawHorizontalLine(source.X, source.Y, source.Width);
                    }
                    else if (sourceControls[i].Type === "HyperLink") {
                        control = this.drawHyperlink(source.X, source.Y, source.Width, source.Height, "HyperLink");
                    }
                    else if (sourceControls[i].Type === "Image") {
                        control = this.drawImageContent(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height);
                    }
                    else if (sourceControls[i].Type === "Label") {
                        control = this.drawLabel(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height, "Label Caption");
                    }
                    else if (sourceControls[i].Type === "MenuBar") {
                        control = this.drawMenuBar(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height);
                    }
                    else if (sourceControls[i].Type === "Paragraph") {
                        control = this.drawPara(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height);
                    }
                    else if (sourceControls[i].Type === "RadioButton") {
                        control = this.drawRadioButton(sourceControls[i].X, sourceControls[i].Y, 120, 30, "RadioButton");
                    }
                    else if (sourceControls[i].Type === "TextArea") {
                        control = this.drawTextArea(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height);
                    }
                    else if (sourceControls[i].Type === "InputText") {
                        control = this.drawTextBox(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30);
                    }
                    else if (sourceControls[i].Type === "Iframe") {
                        control = this.drawIframe(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, 30);
                    }
                    else if (sourceControls[i].Type === "DatePicker") {
                        control = this.drawDatePic(sourceControls[i].X, sourceControls[i].Y);
                    }
                    else if (sourceControls[i].Type === "Range") {
                        control = this.drawRage(sourceControls[i].X, sourceControls[i].Y);
                    }
                    else if (sourceControls[i].Type === "InputPassword") {
                        control = this.drawTextArea(sourceControls[i].X, sourceControls[i].Y, sourceControls[i].Width, sourceControls[i].Height);
                    }

                    if (control != null) {
                        if (isEditable) {
                            control = this.editable(control, i);
                        }
                        controls[controlsValid++] = new ControlItem(i, control);
                    }

                }

                return controls;
            }

            this.editable = function (control, index) {
                var offset = 7;
                var self = this;
                var shaperPan = new zebra.ui.designer.ShaperPan(control, [
                   function mousePressed(e) {

                       $rootScope.stage.activeControlIndex = index;
                       self.onSelectItem(index);

                   },
                   function mouseReleased(e) {
                       $rootScope.stage.controls[index].X = shaperPan.x + offset;
                       $rootScope.stage.controls[index].Y = shaperPan.y + offset;
                   },
                    function mouseDragEnded(e) {
                        $rootScope.stage.controls[index].X = shaperPan.x + offset;
                        $rootScope.stage.controls[index].Y = shaperPan.y + offset;
                        $rootScope.stage.controls[index].Width = control.width;
                        $rootScope.stage.controls[index].Height = control.height;

                    }, function keyPressed(e) {
                        if (e.code === 46) { // on delete pressed.
                            $rootScope.stage.controls.splice(index, 1);
                            self.populateCanvas(true);
                        }
                    }
                ]);

                return shaperPan;
            }

            this.onSelectItem = function (index) {
                var x = document.getElementById("left");
                x.value = $rootScope.stage.controls[index].X;

                var y = document.getElementById("top");
                y.value = $rootScope.stage.controls[index].Y;

                var width = document.getElementById("width");
                width.value = $rootScope.stage.controls[index].Width;

                var height = document.getElementById("height");
                height.value = $rootScope.stage.controls[index].Height;

            }

            //////////////// method drawing non editable ///////////

            this.drop = function (data) {

                var tx = $rootScope.stage.canvasWidth / 2;
                var ty = $rootScope.stage.canvasHeight / 2;

                if (data === "button") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 60, 30, "Button"));
                }
                else if (data === "check") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 30, "Checkbox"));
                }
                else if (data === "combo") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 30, "ComboBox"));
                }
                else if (data === "datepic") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 30, "DatePicker"));
                }
                else if (data === "image") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 200, 100, "DatePicker"));
                }
                else if (data === "radio") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "RadioButton"));
                }
                else if (data === "inputtext") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "InputText"));
                }
                else if (data === "label") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "Label"));
                }
                else if (data === "menu") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 600, 50, "MenuBar"));
                }
                else if (data === "para") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 600, 50, "MenuBar"));

                }
                else if (data === "rang") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 140, 30, "Range"));
                }
                else if (data === "hori") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 40, 3, "HLine"));
                }
                else if (data === "link") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 60, 10, "HyperLink"));
                }
                else if (data === "iframe") {
                    root.add(editable(drawIframe(tx, ty), stage.controls.length));
                    $rootScope.stage.controls.push(new Control(tx, ty, 150, 150, "Iframe"));
                }
                else if (data === "password") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30));
                }
                else if (data === "textArea") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 40));
                }

                canvasService.populateCanvas(true);

            }

            //function for drawing i frame
            this.drawIframe = function (dX, dY) {
                var iframePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/iframe.png");
                var imageIframe = new zebra.ui.ImagePan(iframePic);
                imageIframe.setBounds(dX, dY, 100, 100);
                return imageIframe;
            }

            //function for drawing date pic
            this.drawDatePic = function (dX, dY) {
                var datePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/picker.png");
                var imageDatePic = new zebra.ui.ImagePan(datePic);
                imageDatePic.setBounds(dX, dY, 140, 40);
                return imageDatePic;
            }

            //function for drawing rang
            this.drawRage = function (dX, dY) {
                var rangePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/rang.png");
                var imageRangePic = new zebra.ui.ImagePan(rangePic);
                imageRangePic.setBounds(dX, dY, 140, 30);
                return imageRangePic;
            }

            //function for drawing para
            this.drawPara = function (dX, dY, paraWidth, paraHeight) {
                var paraImage = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/paragrap.png");
                var imageParaImage = new zebra.ui.ImagePan(paraImage);
                imageParaImage.setBounds(dX, dY, paraWidth, paraHeight);
                return imageParaImage;
            }

            //function for drawing button
            this.drawButton = function (bX, bY, bwidth, bheight, butttonCaption) {
                var button = new zebra.ui.Button(butttonCaption);
                button.setBounds(bX, bY, bwidth, bheight);
                return button;
            }

            //function for drawing button
            this.drawPanel = function (px, py, pw, ph, panelColor) {
                var panel = new zebra.ui.Panel();
                panel.setBounds(px, py, pw, ph);
                panel.setBackground(panelColor);
                return panel;
            }

            //function for drawing label
            this.drawLabel = function (lx, ly, lw, lh, caption) {
                var label = new zebra.ui.Label(caption);
                label.setBounds(lx, ly, lw, lh);
                label.setColor("black");
                return label;
            }

            //function for drawing text box
            this.drawTextBox = function (tx, ty, tw, th) {
                var textBox = new zebra.ui.TextField("Type Here...");
                textBox.setBounds(tx, ty, tw, th);
                return textBox;
            }

            //function for drawing Horizontal Line
            this.drawHorizontalLine = function (hx, hy, hw) {
                var horizontal = new zebra.ui.Line();
                horizontal.setBounds(hx, hy, hw, 3);
                return horizontal;
            }

            //function for drawing checkBox
            this.drawCheckBox = function (checkX, checkY, checkW, checkH, checkValue) {
                var checkBox = new zebra.ui.Checkbox(checkValue);
                checkBox.setBounds(checkX, checkY, checkW, checkH);
                return checkBox;
            }

            //function for drawing radio button
            this.drawRadioButton = function (radioX, radioY, radioW, radioH, radioValue) {
                var radioButton = new zebra.ui.Radiobox(radioValue);
                radioButton.setBounds(radioX, radioY, radioW, radioH);
                return radioButton;
            }

            //function for drawing combo box
            this.drawComboBox = function (comboX, comboY, comboW, comboH) {
                var combo = new zebra.ui.Combo(new zebra.ui.List([
               "Item 1",
               "Item 2",
               "Item 3"
                ]));

                combo.setBounds(comboX, comboY, comboW, comboH);
                return combo;
            }

            //function for drawing TextArea
            this.drawTextArea = function (textAreaX, textAreaY, textAreaW, textAreaH) {
                var textArea = new zebra.ui.TextArea();
                textArea.setBounds(textAreaX, textAreaY, textAreaW, textAreaH);
                return textArea;
            }

            //function for drawing combo box
            this.drawHyperlink = function (hyX, hyY, hyW, hyH, hyperValue) {
                var hyperlink = new zebra.ui.Link(hyperValue);
                hyperlink.setBounds(hyX, hyY, hyW, hyH);
                hyperlink.setColor("#2E64FE");

                return hyperlink;
            }

            //function for drawing Menu bar
            this.drawMenuBar = function (menuX, menuY, menuW, menuH) {
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
            this.drawImageContent = function (imageX, imageY, imageW, imageH) {
                var image = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/image.png");
                var imageImage = new zebra.ui.ImagePan(image);
                imageImage.setBounds(imageX, imageY, imageW, imageH);
                return imageImage;
            }
        }// end of canvas service
    )
    .service('httpService', [
        '$http', '$rootScope', 'messagingService', 'urlConfig', function ($http, $rootScope, messagingService, urlConfig) {
            this.uploadFileToUrl = function (file, callBack) {

                var url = urlConfig.domain + urlConfig.uploadUrl;
                messagingService.showMessage("Uploading image..");
                messagingService.showMessage("This will take few minutes..");

                var fd = new FormData();
                fd.append('UploadedImage', file);
                $http.post(url, fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        messagingService.UploadSuccess();
                        if (typeof callBack === "function") {
                            callBack(data);
                        }
                    })
                    .error(function () {
                        messagingService.UploadFail();
                    });
            }

            this.getControls = function (imgUrl, callback) {
                var url = urlConfig.domain + urlConfig.controlUrl;

                messagingService.showMessage("Recognizing shapes..");
                messagingService.showMessage("This will take few minutes..");

                $http({
                    url: url,
                    method: "POST",
                    data: { 'FileUrl': imgUrl }
                }).success(function (data) {
                    if (typeof callback === "function") {
                        callback(data);
                    }
                }).error(function (data) {
                    messagingService.fail("Could Not receive data");
                });
            }

            this.downloadCode = function (callback) {
                var url = urlConfig.domain + urlConfig.codeUrl;

                messagingService.showMessage("Download code..");

                $http({
                    url: url,
                    method: "POST",
                    data: {
                        controls: $rootScope.stage.controls,
                        imageWidth: $rootScope.stage.imageWidth,
                        imageHeight: $rootScope.stage.imageHeight
                    }
                }).success(function (data) {
                    if (typeof callback === "function") {
                        callback(data);
                    }
                }).error(function (data) {
                    messagingService.fail("Could Not receive data");
                });
            }
        }
    ])

    .controller("mainController",
        function ($scope, $rootScope, messagingService, $http, httpService, canvasService) {

            $scope.message = "Hello Magicurve";

            var canvasWidth = (screen.width * 8 / 12) - 50;
            var canvasHeight = screen.height * .65;

            //// models for resizing using the text input.
            $scope.itemLeft = 0;
            $scope.itemTop = 0;
            $scope.itemWidth = 0;
            $scope.itemHeight = 0;

            $rootScope.stage = new Stage(canvasWidth, canvasHeight);

            canvasService.init();

            $scope.getControls = function () {
                if ($rootScope.stage.currentImage === undefined) {
                    messagingService.fail("Please upload the sketch.");
                    return;
                }

                httpService.getControls($rootScope.stage.currentImage, function (data) {
                    $rootScope.stage.controls = data.controls;
                    $rootScope.stage.imageHeight = data.imageHeight;
                    $rootScope.stage.imageWidth = data.imageWidth;
                    canvasService.ajustSize();
                    $scope.draw();
                });
            }

            $scope.draw = function () {
                if ($rootScope.stage.controls != undefined) {
                    canvasService.populateCanvas(false);
                } else {
                    messagingService.fail("controls are loaded yet.");
                    $scope.getControls();
                }
            }

            $scope.edit = function () {
                if ($rootScope.stage.controls != undefined) {
                    canvasService.populateCanvas(true);
                } else {
                    messagingService.fail("controls are loaded yet.");
                    $scope.getControls();
                }
            }

            $scope.download = function () {
                canvasService.reverseSize();
                httpService.downloadCode(function (data) {
                    window.open(data.url, '_blank');
                    canvasService.ajustSize();
                });
            }

            $scope.updateItem = function () {
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Height = $scope.itemHeight;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Width = $scope.itemWidth;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].X = $scope.itemLeft;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Y = $scope.itemTop;
                canvasService.editable();
            }

        })
    .controller("sideController", function ($rootScope, $scope, canvasService, messagingService) {
        $scope.testItem = "test";
        $scope.addItem = function (event) {
            if ($rootScope.stage.controls != null) {
                canvasService.drop(event.target.id);
                messagingService.success("item added successfully");
            } else {
                messagingService.fail("Please go to edit mode.");
            }
        }
    });