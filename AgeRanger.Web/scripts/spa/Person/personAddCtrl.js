(function (app) {
    'use strict';

    app.controller('personAddCtrl', personAddCtrl);

    personAddCtrl.$inject = ['$scope', '$location', '$modalInstance', '$timeout', 'apiService'];

    function personAddCtrl($scope, $location, $modalInstance, $timeout, apiService) {

        $scope.cancelEdit = cancelEdit;
        $scope.addPerson = addPerson;
        $scope.AddPerson = {
            FirstName: null,
            LastName: null,
            Age: null
        }
        function addPerson() {
            //console.log($scope.AddPerson);
            apiService.post('/api/person/add/', $scope.AddPerson,
            addPersonCompleted,
            addPersonLoadFailed);
        }

        function addPersonCompleted(response) {
            alert($scope.AddPerson.FirstName + ' ' + $scope.AddPerson.LastName + ' has been added');
            $scope.AddPerson = {};
            $modalInstance.dismiss();
            $location.url('persons/');
        }

        function addPersonLoadFailed(response) {
            alert(response.data);

        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    }
    

})(angular.module('ageranger'));