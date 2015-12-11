magicurveApp.controller("homeController",
    function ($scope, $rootScope, toaster, homeService) {
        $scope.message = "Hello";
        $scope.spices = homeService.shape;

        $scope.control = { left: 0, top: 0, width: 0, height: 0 };

        $scope.changed = function() {
            console.log($scope.control);
            $rootScope.stage.controls[$rootScope.stage.activeControlIndex].X = $scope.control.left;
            draw();
        }
        
        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
            
        };
    });


