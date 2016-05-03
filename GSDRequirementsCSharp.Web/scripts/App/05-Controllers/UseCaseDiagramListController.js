var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var UseCaseDiagramListController = (function () {
        function UseCaseDiagramListController($scope, UseCaseDiagramResource, $rootScope, $location, SpecificationItemResource) {
            var _this = this;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.useCasesDiagrams = [];
            var pageSize = 10;
            $scope.pendingRequests = 0;
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                    GSDRequirements.currentProfile == Models.profile.projectOwner;
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadUseCaseDiagrams();
            };
            $scope.addUseCaseDiagram = function () {
                $scope.currentUseCaseDiagram = new Models.UseCaseDiagram;
                window.location.href = "#/diagram";
            };
            $scope.setUseCaseToManageLinks = function (uc) {
                $scope.useCaseDiagramToManageLinks = uc;
                window.location.href = "#/links";
            };
            $scope.inactivateUseCaseDiagram = function (useCaseDiagram) {
                if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                    return;
                }
                $scope.pendingRequests++;
                SpecificationItemResource
                    .remove({ id: useCaseDiagram.id })
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.useCaseDiagramRemovedSuccessfully);
                    $scope.loadUseCaseDiagrams();
                })
                    .catch(function (err) {
                    Notification.notifyError(Sentences.errorRemovingUseCaseDiagram, err.data.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                if (!step) {
                    $scope.currentUseCaseDiagram = null;
                    $scope.useCaseDiagramToTranslate = null;
                }
            });
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            window.location.href = "#";
            $scope.setCurrentUseCaseDiagram = function (cd) {
                $scope.pendingRequests++;
                UseCaseDiagramResource.get({ 'id': cd.id })
                    .$promise
                    .then(function (response) {
                    $scope.currentUseCaseDiagram = new Models.UseCaseDiagram(response);
                    window.location.href = "#/diagram";
                })
                    .catch(function (err) {
                    Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            $scope.loadUseCaseDiagrams = function () { return _this.LoadUseCaseDiagrams(UseCaseDiagramResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.loadUseCaseDiagrams();
            $scope.UserData = new UserData();
        }
        UseCaseDiagramListController.prototype.LoadUseCaseDiagrams = function (useCaseDiagramResource, $scope, size) {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size };
            useCaseDiagramResource.get(request)
                .$promise
                .then(function (response) {
                $scope.useCaseDiagrams = _.map(response.useCaseDiagrams, function (p) { return new Models.UseCaseDiagram(p); });
                $scope.maxPages = response.maxPages;
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return UseCaseDiagramListController;
    })();
    app.controller('UseCaseDiagramListController', ["$scope", "UseCaseDiagramResource",
        "$rootScope", "$location", "SpecificationItemResource", UseCaseDiagramListController]);
})(Controllers || (Controllers = {}));
