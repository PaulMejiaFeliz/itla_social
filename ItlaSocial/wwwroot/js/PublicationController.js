app.controller("PublicationController", function ($scope, $http) {

    $scope.isBusy = true;

    $http.get("/api/publications")
        .then(function (response) {
            $scope.data = response.data;
            console.log($scope.data);
        }, function (error) {
            console.log(error);
        })
        .finally(function () {
            $scope.isBusy = false;
        });

});