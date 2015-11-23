magicurveApp.factory("homeService",
    function($http) {
        return {
            shape : {/*
                type    :   "button",
                top: 20,
                left: 30,
                height: 100,
                width: 100*/


                 
   "Controls": [
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     },
     {
         "Type": 0,
         "X": 0,
         "Y": 0,
         "Width": 0,
         "Height": 0,
         "EdgePoints": null
     }
   ],
            },
            
            getShapeInfo: function () {
                var promise = $http.get("Magicurve/api/images/controls");
                return promise;
            }
        }
    });

