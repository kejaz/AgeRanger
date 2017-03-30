(function () {
    'use strict';
    angular.module('ageranger', ['common.core', 'common.ui'])
        .factory('apiService', function ($http, $location, $rootScope) {
            var service = {
                get: get,
                post: post
            };
            function get(url, config, success, failure) {
                return $http.get(url, config)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            if (error.status == '401') {
                                $rootScope.previousState = $location.path();
                                $location.path('/');
                            }
                            else if (failure != null) {
                                failure(error);
                            }
                        });
            }

            function post(url, data, success, failure) {
                return $http.post(url, data)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            if (error.status == '401') {
                                $rootScope.previousState = $location.path();
                                $location.path('/');
                            }
                            else if (failure != null) {
                                failure(error);
                            }
                        });
            }

            return service;
        })
        .config(config);
        
    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
             .when("/", {
                 templateUrl: "scripts/spa/Home/Home.html",
                 controller: "homeCtrl"
             })
            .when("/persons", {
                templateUrl: "scripts/spa/Person/Persons.html",
                controller: "personsCtrl"
            })
            .when("/persons/add/:id", {
                templateUrl: "scripts/spa/person/addCity.html",
                controller: "personAddCtrl"
                
            });
    }        
})();