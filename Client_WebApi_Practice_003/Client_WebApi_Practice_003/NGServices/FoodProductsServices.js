//var app = angular.module("FoodProducts",[]);
//app.service("GetFoodProducts", function ($http) {
//    return {
//        GetFromWebApi: function ($scope) {
//            $.ajax({
//                url: webApiUri + "/api/FoodProducts/Get",
//                type: "GET",
//                headers:
//                    {
//                        'Authorization': 'Bearer ' + sessionStorage["token"],
//                        'Content-Type': 'application/xml',
//                        'Accept': 'text/json' //Accept:text/json (or) Accept:text/xml (or) Accept:text/custom_food_products
//                    },
//                crossDomain: true,
//                crossOrigin: true,
//                success: function (response, status, s) {
//                    alert(response);
//                    $scope.FoodProducts = [];
//                    if (response.length > 0) {
//                        angular.forEach(response, function (value, key) {
//                            $scope.FoodProducts.push(value);
//                        });
//                    }
//                    else {
//                        $scope.FoodProducts.push(data);
//                    }
//                },
//                error: function (jqXHR, status, s) {
//                    alert(jqXHR.responseText);
//                }
//            });
//        }
//    };
//});