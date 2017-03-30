(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    app.factory('sharedData', function ($location) {
        return { appPath: '' };
        //return { appPath: "http://" + $location.host() + '/suzukifamily' };
    });

    apiService.$inject = ['$http', 'sharedData', '$location', 'notificationService', '$rootScope'];

    function apiService($http, sharedData, $location, notificationService, $rootScope) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(sharedData.appPath + url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(sharedData.appPath + url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        return service;
    }

})(angular.module('common.core'));