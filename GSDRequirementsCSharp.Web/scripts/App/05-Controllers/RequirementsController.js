/// <reference path="../angularapplication.js" />
/// <reference path="../gsdrequirementsnamespace.js" />
(function (angularModuleName, angular) {

    angular.module(angularModuleName).controller("RequirementsController", ["$scope", function ($scope) {
        ///<param name="$scope" type="Object"/>

        $scope.step = "list";
        $scope.requirement = {};

        $scope.difficultyOptions = ['Fácil', 'Médio', 'Difícil'];
        $scope.requirementTypeOptions = [
            { 'Label': 'Requisito funcional', 'IdPrefix': 'FR' },
            { 'Label': 'Requisito não funcional', 'IdPrefix': 'NFR' }
        ];
                
        $scope.packages = [
            { 'Description': 'PKG01' }, { 'Description': 'PKG02' }
        ];

        $scope.versions = [
            { 'Number': 3, 'Creator': 'Dev', 'CreationDate': new Date(2011, 6, 3) },
            { 'Number': 2, 'Creator': 'Dev', 'CreationDate': new Date(2011, 4, 2) },
            { 'Number': 1, 'Creator': 'Dev', 'CreationDate': new Date(2011, 2, 1) }
        ];

        $scope.links = [
            { 'Item': 'CD01', 'ItemType': 'Diagrama de classes', 'Creator': 'Dev', 'CreationDate': new Date(2011, 6, 3) }
        ];

        $scope.specificationItems = [
            { 'Item': 'CD01', 'ItemType': 'Diagrama de classes' },
            { 'Item': 'UC01', 'ItemType': 'Caso de uso' },
            { 'Item': 'FR01', 'ItemType': 'Requisito' }
        ];

        $scope.setDefaultOptions = function () {
            $scope.requirement.Difficulty = $scope.difficultyOptions[0];
            $scope.requirement.Type = $scope.requirementTypeOptions[0];
            $scope.requirement.Package = $scope.packages[0];
        };

        $scope.addNewRequiremnt = function () {
            $scope.requirement = {};
            $scope.step = "form";
            $scope.setDefaultOptions();
        };

        $scope.addIssue = function (r) {
            $scope.requirement = r;
            $scope.step = "newIssue";
            $scope.setDefaultOptions();
        };

        $scope.remove = function (r) {
            if (confirm("Você realmente deseja excluir o requisito " + r.Id + " e todos os itens relacionados?")) {

            }
        };

        $scope.addTranslation = function (r) {
            $scope.requirement = r;
            $scope.step = "translation";
            $scope.setDefaultOptions();
        };

        $scope.showDetails = function (r) {
            $scope.requirement = r;
            $scope.step = "details";
        };

        $scope.manageLinks = function (r) {
            $scope.requirement = r;
            $scope.step = "linksList";
        };

        $scope.addNewLink = function (r) {
            $scope.requirement = r;
            $scope.step = "addNewLink";
        };

        $scope.addNewVersion = function (r) {
            $scope.requirement = r;
            $scope.step = "form";
            $scope.setDefaultOptions();
        };

        //$scope.issues = [
        //    {"" : ""}
        //];

        $scope.requirements = [{
            "Id": "FR01",
            "Description": "Quando o usuário tiver permissão o sistema deve permitir gerenciar produtos",
            "Type": $scope.requirementTypeOptions[0],
            "Package": $scope.packages[0],
            "Version": 1,
            "CurrentLanguageAvailable": true,
            "HasPendingIssues" : true
        },
        {
            "Id": "FR02",
            "Description": "O sistema deve permitir realizar compras",
            "Type": $scope.requirementTypeOptions[0],
            "Package": $scope.packages[0],
            "Version": 1,
            "CurrentLanguageAvailable": true,
            "HasPendingIssues" : false
        },
        {
            "Id": "FR03",
            "Description": "The system should allow users to login",
            "Type": $scope.requirementTypeOptions[0],
            "Package": $scope.packages[0],
            "Version": 3,
            "CurrentLanguageAvailable": false,
            "HasPendingIssues": false
        }];
    }]);

})(window.GSDRequirements.angularModuleName, angular);