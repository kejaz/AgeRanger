//var app = angular.module('myApp', []);
//app.controller('myCtrl', function ($scope) {
//    $scope.firstName = "John";
//    $scope.lastName = "Doe";
//});
(function (app) {
    'use strict';

    app.controller('homeCtrl', function ($scope) {
        $scope.FirstName = "Kashif";
        $scope.LastName = "Ejaz";
    });

})(angular.module('ageranger'));