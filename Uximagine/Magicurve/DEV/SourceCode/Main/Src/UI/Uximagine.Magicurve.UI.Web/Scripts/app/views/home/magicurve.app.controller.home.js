magicurveApp.controller("homeController",
    function($scope, toaster) {
        $scope.pop = function() {
            toaster.pop("success", "Hello", "Magi-curve is here.");
        };
    });

