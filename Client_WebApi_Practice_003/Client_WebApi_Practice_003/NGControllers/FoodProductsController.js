var app = angular.module("FoodProducts", []);
app.service("GetFoodProducts", function ($http) {
    return {
        GetFromWebApi: function ($scope) {
            clearTimeout(timeOut);
            $scope.showDetails = false;
            $scope.showEdit = false;
            $scope.showDetailsCustFormatter = false;
            $scope.showAdd = false;

            $.ajax({
                url: webApiUri + "/api/FoodProducts/Get",
                type: "GET",
                headers:
                    {
                        'Authorization': 'Bearer ' + sessionStorage["token"],
                        'Content-Type': 'application/xml',
                        'Accept': 'text/json' //Accept:text/json (or) Accept:text/xml (or) Accept:text/custom_food_products
                    },
                crossDomain: true,
                crossOrigin: true,
                success: function (response, status, s) {
                    
                    $scope.ProductId = "";
                    $scope.ProductName = "";
                    $scope.CategoryName = "";
                    $scope.Price = "";
                    $scope.Quantity = "";
                    $scope.ImageFileName = "";

                    $scope.FoodProducts = [];
                    if (response.length > 0) {
                        angular.forEach(response, function (value, key) {
                            $scope.FoodProducts.push(value);
                        });
                    }
                    else {
                        $scope.FoodProducts.push(data);
                    }
                },
                error: function (jqXHR, status, s) {
                    alert(jqXHR.responseText);
                }
            }).always(function () {
                $scope.$apply();
            });
        }
        , GetFoodProductFromWebApi: function ($scope, foodProd) {
            $scope.showDetails = true;
            $scope.showEdit = false;
            $scope.showAdd = false;
            $scope.showDetailsCustFormatter = false;
            $scope.showMessage = false;

            $.ajax({
                url: webApiUri + "/api/FoodProducts/Get/" + foodProd.Id,
                type: "GET",
                data: foodProd.Id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage["token"],
                    'Content-Type': 'application/xml',
                    'Accept': 'text/json'
                },
                crossDomain: true,
                crossOrigin: true,
                success: function (response, status, s) {
                   
                    $scope.ProductId = "";
                    $scope.ProductName = "";
                    $scope.CategoryName = "";
                    $scope.Price = "";
                    $scope.Quantity = "";
                    $scope.ImageFileName = "";

                    $scope.ProductId = response.Id;
                    $scope.ProductName = response.ProductName;
                    $scope.CategoryName = response.ProductCategory.CategoryName;
                    $scope.Price = response.Price;
                    $scope.Quantity = response.Quantity;
                    $scope.ImageFileName = webApiUri + response.ImageFileName;
                },
                error: function (jqXHR, status, e) {
                    alert(jqXHR.responseText);
                }
            }).always(function () {
                $scope.$apply();
            });
        }
        , GetFoodProductCustFormatterFromWebApi: function ($scope, foodProd) {
            $scope.showDetails = false;
            $scope.showEdit = false;
            $scope.showAdd = false;
            $scope.showDetailsCustFormatter = true;
            $scope.showMessage = false;

            $.ajax({
                url: webApiUri + "/api/FoodProducts/Get/" + foodProd.Id,
                type: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage["token"],
                    'Content-Type': 'application/json',
                    'Accept': 'text/custom_food_products'
                },
                crossDomain: true,
                crossOrigin: true,
                success: function (response, status, s) {                    
                    $scope.FoodProductDetailText = response;
                },
                error: function (jqXHR, status, e) {
                    alert(jqXHR.responseText);
                }

            }).always(function () {
                $scope.$apply();
            });
        }
        , GetFoodProductAddEditFromWebApi: function ($scope, $filter, foodProd,mode) {
            var config = {
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage["token"],
                    'Content-Type': 'application/xml',
                    'Accept': 'text/json'
                },
                crossDomain: true,
                crossOrigin: true,
            };
            $scope.showDetails = false;
            $scope.showEdit = true;
            $scope.showDetailsCustFormatter = false;
            $scope.showMessage = false;
            if (mode == 'E') {
                $scope.showAdd = false
            }
            else {
                $scope.showAdd = true;
            }

            $http.get(webApiUri + "/api/FoodProducts/GetCategories", config).then(function (categoriesResult) {
                $scope.Categories = [];
                categoriesResult.data.forEach(function (val, i) {
                    $scope.Categories.push(val);
                });
                if (mode == 'E') {
                    $http.get(webApiUri + "/api/FoodProducts/Get/" + foodProd.Id, config).then(function (result) {
                        var response = result.data; 
                        $scope.ProductId = "";
                        $scope.ProductName = "";
                        $scope.CategoryName = "";
                        $scope.Price = "";
                        $scope.Quantity = "";
                        $scope.ImageFileName = "";

                        var selCatg = $filter('filter')($scope.Categories, { CategoryId: response.ProductCategory.CategoryId });
                        $scope.ProductId = response.Id;
                        $scope.editProductName = response.ProductName;
                        $scope.editCategoryName = selCatg[0];
                        $scope.editPrice = $filter('currency')(response.Price, '', 2);
                        $scope.editQuantity = response.Quantity;
                        $scope.ImageFileName = webApiUri + response.ImageFileName;
                    }, function (error) {
                        console.log(error);
                    });
                }
                else
                {
                    $scope.ProductId = "";
                    $scope.ProductName = "";
                    $scope.CategoryName = "";
                    $scope.Price = "";
                    $scope.Quantity = "";
                    $scope.ImageFileName = "";

                    $scope.editProductName = "";
                    $scope.editCategoryName = "";
                    $scope.editPrice = "";
                    $scope.editQuantity = "";
                }

            }, function (error) {
                console.log(error);
            });
        }
        , PutFoodProduct: function ($scope, GetFoodProducts) {
            var config = {
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage["token"],
                    'Content-Type': 'application/json',
                    'Accept': 'text/json',
                },
                crossDomain: true,
                crossOrigin: true,
            }; 
            var data = JSON.stringify({ Id: $scope.ProductId, ProductName: $scope.editProductName, CategoryId: $scope.editCategoryName.CategoryId, Price: $scope.editPrice, Quantity: $scope.editQuantity, ImageFileName: $scope.ImageFileName.replace(webApiUri, '') });
            $http.put(webApiUri + "/api/FoodProducts/", data, config).then(function (result) {
                console.log(result.data);
                GetFoodProducts.GetFromWebApi($scope);
            });
        }
        , PostFoodProduct: function ($scope, GetFoodProducts) {
            var config = {
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage['token'],
                    'Content-Type':'application/json',
                    'Accept':'text/json'
                },
                crossDomain: true,
                crossOrigin:true
            }
            var data = JSON.stringify({ Id: $scope.ProductId, ProductName: $scope.editProductName, CategoryId: $scope.editCategoryName.CategoryId, Price: $scope.editPrice, Quantity: $scope.editQuantity, ImageFileName: $scope.ImageFileName.replace(webApiUri, '') });
            $http.post(webApiUri + "/api/FoodProducts/", data, config).then(function (result) {
                console.log(result.data);
                GetFoodProducts.GetFromWebApi($scope);
            });
        }
        , UploadProductImage: function ($scope, GetFoodProducts) {
            var config = {
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage['token'],
                    'Content-Type': 'application/json',
                    'Accept': 'text/json'
                },
                crossDomain: true,
                crossOrigin:true
            }; 

            var fileObj = document.getElementById("imgProduct");
            var file = fileObj.files[0];
            var r = new FileReader();
            r.onloadend = function (e) {
                var arr = Array.from(new Uint8Array(e.target.result));
                var data = {
                    Id: $scope.ProductId,
                    Name: $scope.editProductName,
                    Bytes: arr
                }; 
                $http.post(webApiUri + "/api/FoodProducts/UploadImage", data, config).then(function (result) {
                    console.log('Uploaded file');
                    GetFoodProducts.GetFromWebApi($scope);
                    fileObj.value = null;
                    $scope.showMessage = true;
                    $scope.MessageText = result.data;
                    console.log($scope.MessageText);
                });
            }
            r.readAsArrayBuffer(file);
        }
    };
});
app.controller("FoodProductsController", function ($scope, $http, $filter, GetFoodProducts) {
    GetFoodProducts.GetFromWebApi($scope);

    //Accept:text/json (or) Accept:text/xml 
    $scope.GetFoodProduct = function (foodProd) {
        GetFoodProducts.GetFoodProductFromWebApi($scope, foodProd);
    }

    // Accept:text/custom_food_products
    $scope.GetFoodProductCustFormatter = function (foodProd) {
        GetFoodProducts.GetFoodProductCustFormatterFromWebApi($scope, foodProd);
    }

    //Get data for edit mode
    $scope.GetFoodProductEdit = function (foodProd) {
        GetFoodProducts.GetFoodProductAddEditFromWebApi($scope, $filter, foodProd,'E');
    }

    $scope.PutFoodProduct = function () {
        GetFoodProducts.PutFoodProduct($scope, GetFoodProducts);
    }

    //Get data for edit mode
    $scope.GetFoodProductAdd = function () {
        GetFoodProducts.GetFoodProductAddEditFromWebApi($scope, $filter, null, 'A');
    }

    $scope.PostFoodProduct = function () {
        GetFoodProducts.PostFoodProduct($scope, GetFoodProducts);
    }

    $scope.UploadProductImg = function () {
        GetFoodProducts.UploadProductImage($scope, GetFoodProducts);
    }

    $scope.EnableDisableSave = function () {

        $scop.enableSave = true;
    }
});