var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var ProjectListController = (function () {
        function ProjectListController($scope, ProjectResource, $rootScope, $location) {
            var _this = this;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.pendingRequests = 0;
            $scope.projects = [];
            var pageSize = 10;
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                if (!step) {
                    $scope.currentProject = null;
                    $scope.projectToTranslate = null;
                }
            });
            window.location.href = "#";
            $scope.addProject = function () {
                $scope.currentProject = {};
                window.location.href = "#/form";
            };
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadProjects();
            };
            $scope.setCurrentProject = function (p) {
                $scope.currentProject = p;
                window.location.href = "#/form";
            };
            $scope.setProjectToTranslate = function (p) {
                $scope.projectToTranslate = p;
                window.location.href = "#/translate";
            };
            $scope.loadProjects = function () { return _this.LoadProjects(ProjectResource, $scope, pageSize); };
            $scope.inactivateProject = function (p) {
                _this.InactivateProject(ProjectResource, $scope, p);
            };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.loadProjects();
            $scope.UserData = new UserData();
        }
        ProjectListController.prototype.InactivateProject = function (projectResource, $scope, project) {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }
            $scope.pendingRequests++;
            projectResource.remove({ id: project.id })
                .$promise
                .then(function (r) {
                Notification.notifySuccess(Sentences.projectInactivatedSuccessfully);
                $scope.$emit(Globals.EventNames.projectListChanged);
                $scope.loadProjects();
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorInactivatingProject, error.data.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        ProjectListController.prototype.LoadProjects = function (projectResource, $scope, size) {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size };
            projectResource.get(request)
                .$promise
                .then(function (response) {
                $scope.projects = _.map(response.projects, function (p) { return new Models.Project(p); });
                $scope.maxPages = response.maxPages;
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingProjects, error.data.errors);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return ProjectListController;
    }());
    app.controller('ProjectListController', ["$scope", "ProjectResource",
        "$rootScope", "$location", ProjectListController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ProjectListController.js.map