var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var ClassDiagramListController = (function () {
        function ClassDiagramListController($scope, ClassDiagramResource, $rootScope, $location, SpecificationItemResource) {
            var _this = this;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.classDiagrams = [];
            $scope.currentClass = null;
            $scope.editingRelations = false;
            var pageSize = 10;
            $scope.pendingRequests = 0;
            $scope.hasEditPermission =
                GSDRequirements.currentProfile == Models.profile.editor ||
                    GSDRequirements.currentProfile == Models.profile.projectOwner;
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadClassDiagrams();
            };
            $scope.addClassDiagram = function () {
                $scope.currentClassDiagram = new Models.ClassDiagram();
                window.location.href = "#/diagram";
            };
            $scope.inactivateClassDiagram = function (classDiagram) {
                if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                    return;
                }
                $scope.pendingRequests++;
                SpecificationItemResource
                    .remove({ id: classDiagram.id })
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.classDiagramRemovedSuccessfully);
                    $scope.loadClassDiagrams();
                })
                    .catch(function (err) {
                    Notification.notifyError(Sentences.errorRemovingClassDiagram, err.data.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                if (!step) {
                    $scope.currentClassDiagram = null;
                    $scope.classDiagramToTranslate = null;
                }
                if (pathValues.length == 2) {
                    $scope.currentClass = null;
                    $scope.editingRelations = false;
                }
            });
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            window.location.href = "#";
            $scope.setCurrentClassDiagram = function (cd) {
                $scope.pendingRequests++;
                ClassDiagramResource.get({ 'id': cd.id })
                    .$promise
                    .then(function (response) {
                    $scope.currentClassDiagram = new Models.ClassDiagram(response);
                    window.location.href = "#/diagram";
                })
                    .catch(function (err) {
                    Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            $scope.loadClassDiagrams = function () { return _this.LoadClassDiagrams(ClassDiagramResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.inactivateClassDiagrams = function (p) {
                _this.InactivateClassDiagram(ClassDiagramResource, $scope, p);
            };
            $scope.loadClassDiagrams();
            $scope.UserData = new UserData();
        }
        ClassDiagramListController.prototype.InactivateClassDiagram = function (classDiagramResource, $scope, classDiagram) {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }
            $scope.pendingRequests++;
            classDiagramResource.remove({ id: classDiagram.id })
                .$promise
                .then(function (r) {
                Notification.notifySuccess(Sentences.classDiagramInactivatedSuccessfully);
                $scope.loadClassDiagrams();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorInactivatingClassDiagram, error.data.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        ClassDiagramListController.prototype.LoadClassDiagrams = function (classDiagramResource, $scope, size) {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size };
            classDiagramResource.get(request)
                .$promise
                .then(function (response) {
                $scope.classDiagrams = _.map(response.classDiagrams, function (p) { return new Models.ClassDiagram(p); });
                $scope.maxPages = response.maxPages;
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return ClassDiagramListController;
    })();
    app.controller('ClassDiagramListController', ["$scope", "ClassDiagramResource",
        "$rootScope", "$location", "SpecificationItemResource", ClassDiagramListController]);
})(Controllers || (Controllers = {}));
