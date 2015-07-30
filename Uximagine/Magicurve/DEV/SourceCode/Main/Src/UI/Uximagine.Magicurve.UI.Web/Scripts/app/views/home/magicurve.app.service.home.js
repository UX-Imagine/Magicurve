magicurveApp.factory("homeService",
    function($http) {
        return {
            shape : {
                type    :   "button",
                top: 20,
                left: 30,
                height: 100,
                width: 100
            },

            getShapeInfo: function () {
                var promise = $http.get("/values");
                return promise;
            }
        }
    });

