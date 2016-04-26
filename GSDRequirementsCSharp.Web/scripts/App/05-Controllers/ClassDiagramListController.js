var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var ClassDiagramListController = (function () {
        function ClassDiagramListController($scope, ClassDiagramResource, $rootScope, $location) {
            var _this = this;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.classDiagrams = [];
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
                $scope.currentClassDiagram = {};
                window.location.href = "#/form";
            };
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                if (!step) {
                    $scope.currentClassDiagram = null;
                    $scope.classDiagramToTranslate = null;
                }
            });
            window.location.href = "#";
            $scope.setCurrentClassDiagram = function (cd) {
                $scope.currentClassDiagram = cd;
                window.location.href = "#/form";
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
                Notification.notifyError(Sentences.errorInactivatingClassDiagram, error.messages);
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
        "$rootScope", "$location", ClassDiagramListController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ClassDiagramListController.js.map