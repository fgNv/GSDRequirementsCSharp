var Controllers;
(function (Controllers) {
    var UserData = NewAccount.UserData;
    var app = angular.module(GSDRequirements.angularModuleName);
    var ProjectListController = (function () {
        function ProjectListController($scope, ProjectResource) {
            var _this = this;
            this.$scope = $scope;
            this.ProjectResource = ProjectResource;
            $scope.currentPage = 1;
            $scope.maxPages = 1;
            $scope.projects = [];
            var pageSize = 10;
            $scope.loadPage = function (page) {
                $scope.currentPage = page;
                $scope.loadProjects();
            };
            $scope.setCurrentProject = function (p) { return $scope.currentProject = p; };
            $scope.loadProjects = function () { return _this.LoadProjects(ProjectResource, $scope, pageSize); };
            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };
            $scope.loadProjects();
            $scope.pendingRequests = 1;
            this.$scope.UserData = new UserData();
        }
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
    })();
    Controllers.ProjectListController = ProjectListController;
    app.controller('ProjectListController', ["$scope", "ProjectResource", function ($scope, ProjectResource) {
            return new ProjectListController($scope, ProjectResource);
        }]);
})(Controllers || (Controllers = {}));
