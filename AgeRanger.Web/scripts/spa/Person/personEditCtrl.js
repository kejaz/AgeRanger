(function (app) {
    'use strict';

    app.controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$location', '$modalInstance', '$timeout', 'apiService'];

    function personEditCtrl($scope, $location, $modalInstance, $timeout, apiService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updatePerson = updatePerson;
        $scope.PreviousStatePerson = $scope.EditedPerson;
               
        function updatePerson() {
            console.log($scope.EditedPerson);
            apiService.post('/api/person/Update/', $scope.EditedPerson,
            updatePersonCompleted,
            updatePersonLoadFailed);
        }

        function updatePersonCompleted(response) {
            alert($scope.EditedPerson.FirstName + ' ' + $scope.EditedPerson.LastName + ' has been updated');
            $scope.EditedPerson = {};
            $modalInstance.dismiss();
            $location.url('persons/');
        }

        function updatePersonLoadFailed(response) {
            alert(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
            $location.url('persons/');
            
        }
    }

})(angular.module('ageranger'));