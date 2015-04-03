magicurveApp.controller("homeController",
    function($scope, toaster, homeService) {
        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
        };
    });

