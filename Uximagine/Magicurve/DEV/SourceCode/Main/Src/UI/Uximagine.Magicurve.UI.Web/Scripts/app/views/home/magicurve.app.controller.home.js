magicurveApp.controller("homeController",
    function ($scope, toaster, homeService) {
        $scope.message = "Hello";

        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
        };
    });

