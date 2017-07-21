var app = angular.module("app", ["ngRoute", "simpleControls"])
    .config(function ($routeProvider) {

        $routeProvider.when("/", {
            templateUrl: "/views/PublicationView.html",
            controller: "PublicationController"
        });
    });
