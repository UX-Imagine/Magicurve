magicurveApp.controller("homeController",
    function ($scope, toaster, homeService) {
        $scope.message = "Hello";
        $scope.spices = homeService.shape;
         jsonObject = $scope.spices;
       //control = genarateDesign(jsonObject);
        // alert("test this");
         //drawButton(300,300,60,30,"test");
       // alert(control[0]);
      //  drawDesign();
       //alert();
        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
            
        };
    });


