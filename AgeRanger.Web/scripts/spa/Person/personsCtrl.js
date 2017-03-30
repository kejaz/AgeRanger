(function (app) {
    'use strict';
    
    app.controller('personsCtrl', personsCtrl);

    personsCtrl.$inject = ['$scope', '$modal', 'apiService'];


    function personsCtrl($scope, $modal, apiService) {

        $scope.pageClass = 'page-persons';
        $scope.loadingPersons = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Persons = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openAddDialog = openAddDialog;
        $scope.openEditDialog = openEditDialog;
        
        function search(page) {
            page = page || 0;

            $scope.loadingPersons = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 100,
                    filter: $scope.filterPersons
                }
            };

            apiService.get('/api/person/search/', config,
            personsLoadCompleted,
            personsLoadFailed);
        }

        function openEditDialog(person) {
            $scope.EditedPerson = person;
            $modal.open({
                templateUrl: 'scripts/spa/person/editPerson.html',
                controller: 'personEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }
      
        function openAddDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/person/addPerson.html',
                controller: 'personAddCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function personsLoadCompleted(result) {
            $scope.Persons = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingPersons = false;

            if ($scope.filterPersons && $scope.filterPersons.length) {
                alert(result.data.Items.length + ' persons found');
          }

        }

        function personsLoadFailed(response) {
            alert(response.data);
        }

        function clearSearch() {
            $scope.filterPersons = '';
            search();
        }

        $scope.search();
    }

})(angular.module('ageranger'));