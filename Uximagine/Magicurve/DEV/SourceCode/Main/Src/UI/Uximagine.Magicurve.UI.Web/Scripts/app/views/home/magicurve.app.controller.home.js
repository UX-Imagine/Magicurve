magicurveApp.controller("homeController",
    function ($scope, toaster, homeService) {
        $scope.message = "Hello";
        $scope.spices = homeService.shape;



          //{ "name": "pasilla", "spiciness": "mild" };
       
                   

        $scope.pop = function() {
            toaster.pop("success", "Hello", homeService.shape.type);
            
            homeService.getShape().then(function (data) {
               // zebra.createButton(data.x,);
            });
        };
    });


function onclickd() {

    //alert();
   var edit = document.getElementById("demo").innerHTML = " &lt!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd \"&gt <br/> &lthtml xmlns=\"http://www.w3.org/1999/xhtml\"&gt <br/>"+

    "&lthead&gt <br/> &ltmeta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" /&gt <br/>" +
    "&lttitle&gtUntitled 1&lt/title&gt <br/>" +
    "&lt/head&gt <br/>" +

    "&ltbody&gt <br/>" +

    "&lt/body&gt <br/>" +

    "&lt/html&gt <br/>";
    
   //document.getElementById("test").innerHTML = edit;
}