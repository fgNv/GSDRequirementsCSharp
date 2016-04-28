var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdProjectContext = (function () {
        function GsdProjectContext() {
            var _this = this;
            this.scope = {
                'currentProjectId': '=currentProjectId',
                'currentProjectName': '=currentProjectName',
                'pendingRequests': '=pendingRequests'
            };
            this.templateUrl = GSDRequirements.baseUrl + 'project/context';
            this.loadProjects = function ($scope, CurrentUserProjectResource) {
                $scope.pendingRequests++;
                CurrentUserProjectResource.query()
                    .$promise
                    .then(function (projects) {
                    $scope.projectsOptions = projects;
                    _this.setCurrentProjectName($scope.currentProjectId, $scope);
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorLoadingProjects, error.data.errors);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            this.setCurrentProjectName = function (projectId, $scope) {
                if (!projectId) {
                    $scope.currentProjectName = Sentences.noProjectInContext;
                    return;
                }
                var project = _.find($scope.projectsOptions, function (p) { return p.id == projectId; });
                if (project && project.name) {
                    $scope.currentProjectName = project.name;
                }
                else {
                    $scope.currentProjectName = Sentences.noProjectInContext;
                }
            };
            this.controller = ['$scope', 'CurrentUserProjectResource',
                function ($scope, CurrentUserProjectResource) {
                    $scope.pendingRequests = 0;
                    $scope.projectsOptions = [];
                    _this.setCurrentProjectName($scope.currentProjectId, $scope);
                    var self = _this;
                    $scope.$watch('currentProjectId', function (newValue, oldValue) {
                        return self.setCurrentProjectName(newValue, $scope);
                    });
                    var self = _this;
                    $scope.loadProjects =
                        function () { return self.loadProjects($scope, CurrentUserProjectResource); };
                    $scope.$on(Globals.EventNames.updateProjectList, $scope.loadProjects);
                    $scope.loadProjects();
                }];
        }
        GsdProjectContext.Factory = function () {
            return new GsdProjectContext();
        };
        return GsdProjectContext;
    })();
    app.directive('gsdProjectContext', GsdProjectContext.Factory);
})(Directives || (Directives = {}));
