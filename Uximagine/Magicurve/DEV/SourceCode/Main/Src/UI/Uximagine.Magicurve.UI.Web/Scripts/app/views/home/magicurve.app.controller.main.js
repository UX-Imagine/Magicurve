magicurveApp
    .directive('fileModel', [
                '$parse', 'fileUpload', 'toaster', '$rootScope', 'messagingService',
        function ($parse, fileUpload, toaster, $rootScope, messagingService) {
            return {
                restrict: 'A',
                link: function(scope, element, attrs) {
                    var model = $parse(attrs.fileModel);
                    var modelSetter = model.assign;
                    

                    element.bind('change', function () {

                        scope.$apply(function() {
                            modelSetter(scope, element[0].files[0]);
                        });

                        var file = element[0].files[0];
                        var uploadUrl = $rootScope.rootUrl + "/Home/UploadFile";
                        fileUpload.uploadFileToUrl(file, uploadUrl);
                        messagingService.showMessage("upload started");
                        element.val(null);

                    });
                }
            };
        }
    ])
    .service('messagingService',
         function (toaster, canvasService) {
             this.UploadSuccess = function () {

                 toaster.pop("success", "upload success");
                 canvasService.populateCanvas();
                 
             }
             this.UploadFail = function () {
                 toaster.pop("error", "upload failed");
             }
             this.showMessage = function(message) {
                 toaster.pop("success", message);
             }
         }
    )
    .service('canvasService',
        function(toaster) {
            this.populateCanvas = function (data) {
                console.log(data);
                if (window.zebra === 'undefined') {
                    toaster.pop("error", "Zebra not loaded");
                } else {
                    toaster.pop("success", "Zebra not loaded");
                }

            }
        }
    )
    .service('fileUpload', [
        '$http', 'toaster', 'messagingService', function ($http, toaster, messagingService) {
            this.uploadFileToUrl = function(file, uploadUrl) {
                var fd = new FormData();
                fd.append('UploadedImage', file);
                $http.post(uploadUrl, fd, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    })
                    .success(function () {
                        messagingService.UploadSuccess();
                        $('#viewCodeButton').css('display','block');
                    })
                    .error(function () {
                     messagingService.UploadFail();
                });
            }
        }
    ])
    
    .controller("mainController",
        function ($scope, $rootScope, toaster, $http, fileUpload, canvasService) {


            $scope.message = "Hello Magicurve";

            $rootScope.rootUrl = "/Magicurve";

            $scope.initUpload = function($event) {
                var input = window.angular.element($event.target.children[0]);
            };

            $scope.content = {type:'button', x:20, y:30};

            $scope.download = function () {
                downloadSource();
            }

            $scope.uploadFile = function($event) {
                var file = $scope.image;
                console.log('file is ' + JSON.stringify(file));
                var uploadUrl = $rootScope.rootUrl + "/Home/UploadFile";
                fileUpload.uploadFileToUrl(file, uploadUrl);
                toaster.pop("success", "upload file");
            };
        });