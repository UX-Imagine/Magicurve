magicurveApp
    .constant("urlConfig", {
        "domain": "/Magicurve",
        "controlUrl": "/api/images/result",
        "codeUrl": "/api/images/download",
        "uploadUrl": "/Home/UploadFile"
    }
    )
    .run(function ($rootScope) {
        $rootScope.mymodel = 'search.name';
        $rootScope.activeIndex = -1;
        $rootScope.properties = {};
        $rootScope.properties.show = false;
        $rootScope.properties.itemLeft = 0;
        $rootScope.properties.itemTop = 0;
        $rootScope.properties.itemWidth = 0;
        $rootScope.properties.itemHeight = 0;
        $rootScope.properties.itemText = 0;
        $rootScope.properties.itemColor = "#000";
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
                            canvasService.clearAll();
                            messagingService.UploadSuccess();
                            messagingService.showMessage("Please click Draw or Edit on tool-bar to proceed...");
                        });

                        element.val(null);
                    });
                }
            };
        }
    ]
    )
    .directive('inputFilter', function ($rootScope) {
        return {
            restrict: 'E',
            replace: true,
            controller: function ($scope) {
                console.log($scope.mymodel);
                console.log($rootScope.mymodel);

            },
            template: '<input class="filter" type="text" ng-model="' + $rootScope.mymodel + '" placeholder="Nach filtern">'
        }

    })
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

            this.clearAll = function () {
                if (root) {
                    $rootScope.stage.controls = null;
                    root.removeAll();
                }
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
                    var text = "";
                    var color = "";

                    if (typeof source.Styles !== "undefined" ) {
                        if (source.Styles !== null) {
                        text = source.Styles[0].value;
                        color = source.Styles[1].value;
                        }
                       
                    }
                    
                    if (sourceControls[i].Type === "Button") {
                        if (text.length === 0) {
                            text = "Button";
                        }
                        control = this.drawButton(source.X, source.Y, source.Width, source.Height, text);
                    }
                    else if (sourceControls[i].Type === "CheckBox") {
                        if (text.length === 0) {
                            text = "CheckBox";
                        }
                        control = this.drawCheckBox(source.X, source.Y, 120, 30, text);
                    }
                    else if (sourceControls[i].Type === "ComboBox") {
                        control = this.drawComboBox(source.X, source.Y, 120, source.Height);
                    }
                    else if (sourceControls[i].Type === "HLine") {
                        control = this.drawHorizontalLine(source.X, source.Y, source.Width);
                    }
                    else if (sourceControls[i].Type === "HyperLink") {
                        if (text.length === 0) {
                            text = "HyperLink";
                        }
                        control = this.drawHyperlink(source.X, source.Y, source.Width, source.Height, text);
                    }
                    else if (sourceControls[i].Type === "Image") {
                        control = this.drawImageContent(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "Label") {
                        if (text.length === 0) {
                            text = "Label Caption";
                        }
                        control = this.drawLabel(source.X, source.Y, source.Width, source.Height, text);
                    }
                    else if (sourceControls[i].Type === "MenuBar") {
                        control = this.drawMenuBar(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "Paragraph") {
                        control = this.drawPara(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "RadioButton") {
                        if (text.length === 0) {
                            text = "RadioButton";
                        }

                        control = this.drawRadioButton(source.X, source.Y, 120, 30, text);
                    }
                    else if (sourceControls[i].Type === "TextArea") {
                        control = this.drawTextArea(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "InputText") {
                        if (text.length === 0) {
                            text = "Type Here...";
                        }
                       
                        control = this.drawTextBox(source.X, source.Y, source.Width, 30, text);
                    }
                    else if (sourceControls[i].Type === "Iframe") {
                        control = this.drawIframe(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "DatePicker") {
                        control = this.drawDatePic(source.X, source.Y);
                    }
                    else if (sourceControls[i].Type === "Range") {
                        control = this.drawRage(source.X, source.Y, source.Width, source.Height);
                    }
                    else if (sourceControls[i].Type === "InputPassword") {
                        control = this.drawTextBox(source.X, source.Y, source.Width, source.Height);
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
                       $rootScope.activeIndex = index;
                       self.onSelectItem(index);

                   },
                   function mouseReleased(e) {
                       $rootScope.stage.controls[index].X = shaperPan.x + offset;
                       $rootScope.stage.controls[index].Y = shaperPan.y + offset;
                       self.onSelectItem(index);
                   },
                    function mouseDragEnded(e) {
                        $rootScope.stage.controls[index].X = shaperPan.x + offset;
                        $rootScope.stage.controls[index].Y = shaperPan.y + offset;
                        $rootScope.stage.controls[index].Width = control.width;
                        $rootScope.stage.controls[index].Height = control.height;
                        self.onSelectItem(index);

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
                var source = $rootScope.stage.controls[index];
              
                $rootScope.properties.itemLeft = source.X;
                $rootScope.properties.itemTop = source.Y;
                $rootScope.properties.itemWidth = source.Width;
                $rootScope.properties.itemHeight = source.Height;

                var text = "";
                var color = "#000";
                if (typeof source.Styles !== "undefined") {
                    if (source.Styles !== null) {
                        text = source.Styles[0].value;
                        color = source.Styles[1].value;
                    }
                }

                $rootScope.properties.itemText = text;
                $rootScope.properties.itemColor = color;

                $rootScope.$apply();

            }

            //////////////// method drawing non editable ///////////

            this.drop = function (data) {

                var tx = $rootScope.stage.canvasWidth / 2;
                var ty = $rootScope.stage.canvasHeight / 2;

                if (data === "Button") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 150, 30, "Button"));
                }
                else if (data === "Checkbox") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "Checkbox"));
                }
                else if (data === "ComboBox") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 30, "ComboBox"));
                }
                else if (data === "DatePicker") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 50, 30, "DatePicker"));
                }
                else if (data === "Image") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 400, 200, "Image"));
                }
                else if (data === "RadioButton") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "RadioButton"));
                }
                else if (data === "InputText") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "InputText"));
                }
                else if (data === "Label") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "Label"));
                }
                else if (data === "MenuBar") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 600, 50, "MenuBar"));
                }
                else if (data === "Paragraph") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 150, 150, "Paragraph"));

                }
                else if (data === "Range") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 150, 20, "Range"));
                }
                else if (data === "HLine") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 80, 3, "HLine"));
                }
                else if (data === "HyperLink") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 100, 20, "HyperLink"));
                }
                else if (data === "Iframe") {

                    $rootScope.stage.controls.push(new Control(tx, ty, 200, 200, "Iframe"));
                }
                else if (data === "InputPassword") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 120, 30, "InputPassword"));
                }
                else if (data === "TextArea") {
                    $rootScope.stage.controls.push(new Control(tx, ty, 150, 75, "TextArea"));
                }

                canvasService.populateCanvas(true);

            }

            //function for drawing i frame
            this.drawIframe = function (dX, dY, ifraWidth, ifraHeight) {
                var iframePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/iframe.png");
                var imageIframe = new zebra.ui.ImagePan(iframePic);
                imageIframe.setBounds(dX, dY, ifraWidth, ifraHeight);
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
            this.drawRage = function (dX, dY, rangWidth, rangHeight) {
                var rangePic = zebra.ui.loadImage("/Magicurve/Content/Images/Icon/rang.png");
                var imageRangePic = new zebra.ui.ImagePan(rangePic);
                imageRangePic.setBounds(dX, dY, 150, 15);
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
            this.drawTextBox = function (tx, ty, tw, th, text) {
                var textBox = new zebra.ui.TextField(text);
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
    ]
    )
    .controller("mainController",
        function ($scope, $rootScope, messagingService, $http, httpService, canvasService) {

            $scope.message = "Hello Magicurve";

            var canvasWidth = (screen.width * 8 / 12) - 50;
            var canvasHeight = screen.height * .65;


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

            $scope.clear = function () {
                canvasService.clearAll();
            }

        }
    )
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
    }
    )
    .controller("propController", function ($rootScope, $scope, canvasService, messagingService) {
        $scope.testItem = "test";
        //// models for resizing using the text input.
  
        $scope.properties.showUploaded = false;
        $scope.properties.uploadedImageUrl = '';

        $rootScope.activeIndex = -1;

        var scope = $rootScope;

        scope.$watch('activeIndex', function (index) {
            if (typeof index !== "undefined") {
                $scope.properties.show = true;
            }
        });

        $scope.update = function () {
            if ($rootScope.stage.controls.length !== 0) {
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Height = $rootScope.properties.itemHeight;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Width = $rootScope.properties.itemWidth;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].X = $rootScope.properties.itemLeft;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Y = $rootScope.properties.itemTop;
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Styles = [];
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Styles.push(new Style("value", $rootScope.properties.itemText));
                $rootScope.stage.controls[$rootScope.stage.activeControlIndex].Styles.push(new Style("color", $rootScope.properties.itemColor));
                canvasService.populateCanvas(true);
            }

        }

        $scope.clear = function () {
            canvasService.clearAll();
            $rootScope.properties.itemLeft = 0;
            $rootScope.properties.itemTop = 0;
            $rootScope.properties.itemWidth = 0;
            $rootScope.properties.itemHeight = 0;
            $rootScope.properties.itemText = "";
            $rootScope.properties.itemColor = 0;
        }
    });