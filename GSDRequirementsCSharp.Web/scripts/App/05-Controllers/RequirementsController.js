var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var RequirementsController = (function () {
        function RequirementsController($scope, RequirementResource, SpecificationItemResource) {
            this.$scope = $scope;
            this.RequirementResource = RequirementResource;
            this.SpecificationItemResource = SpecificationItemResource;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.requirements = [];
            $scope.pendingRequests = 0;
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                    GSDRequirements.currentProfile == Models.profile.projectOwner;
            var pageSize = 10;
            this.SetScopeMethods($scope, RequirementResource, SpecificationItemResource, pageSize);
            this.LoadRequirements(RequirementResource, $scope, pageSize);
        }
        RequirementsController.prototype.SetScopeMethods = function ($scope, RequirementResource, SpecificationItemResource, pageSize) {
            var _this = this;
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadRequirements();
            };
            $scope.setCurrentRequirement = function (r) { $scope.currentRequirement = r; };
            $scope.setRequirementToTranslate = function (r) { $scope.requirementToTranslate = r; };
            $scope.showList = function () {
                return !$scope.currentRequirement &&
                    !$scope.requirementToTranslate &&
                    !$scope.requirementToShowDetails &&
                    !$scope.requirementToAddIssues &&
                    !$scope.requirementToManageLinks;
            };
            $scope.loadRequirements = function () { return _this.LoadRequirements(RequirementResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.inactivateRequirement = function (r) {
                _this.InactivateRequirement(SpecificationItemResource, $scope, r);
            };
        };
        RequirementsController.prototype.LoadRequirements = function (requirementResource, $scope, pageSize) {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: pageSize };
            requirementResource.get(request)
                .$promise
                .then(function (response) {
                $scope.requirements = _.map(response.requirements, function (p) { return new Models.Requirement(p); });
                $scope.maxPages = response.maxPages;
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingRequirements, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        RequirementsController.prototype.InactivateRequirement = function (specificationItemResource, $scope, requirement) {
            $scope.pendingRequests++;
            specificationItemResource.remove({ id: requirement.id })
                .$promise
                .then(function (r) {
                Notification.notifySuccess(Sentences.requirementInactivatedSuccessfully);
                $scope.loadRequirements();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorInactivatingRequirement, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return RequirementsController;
    })();
    app.controller('RequirementsController', ["$scope", "RequirementResource", "SpecificationItemResource", RequirementsController]);
})(Controllers || (Controllers = {}));
