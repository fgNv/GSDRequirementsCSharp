module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdProjectContext {
        public scope = {
            'currentProjectId': '=currentProjectId',
            'currentProjectName': '=currentProjectName',
            'pendingRequests': '=pendingRequests'
         };
        public templateUrl = GSDRequirements.baseUrl + 'project/context'
        private loadProjects = ($scope, CurrentUserProjectResource) => {
            $scope.pendingRequests++;
            
            CurrentUserProjectResource.query()
                .$promise
                .then((projects) => {
                    $scope.projectsOptions = projects
                    this.setCurrentProjectName($scope.currentProjectId, $scope)
                })
                .catch((error) => {
                    Notification.notifyError(Sentences.errorLoadingProjects,
                        error.data.errors)
                })
                .finally(() => {
                    $scope.pendingRequests--
                });
        }
        private setCurrentProjectName = (projectId, $scope) => {
           
            if (!projectId) {
                $scope.currentProjectName = Sentences.noProjectInContext
                return
            }
            var project = _.find($scope.projectsOptions, (p) => p.id == projectId)
           
            if (project && project.name) {
                $scope.currentProjectName = project.name
            } else {
                $scope.currentProjectName = Sentences.noProjectInContext
            }
        }
        public controller = ['$scope', 'CurrentUserProjectResource',
                             ($scope: any, CurrentUserProjectResource: any) => {
            $scope.pendingRequests = 0
            $scope.projectsOptions = []

            this.setCurrentProjectName($scope.currentProjectId, $scope)
            var self = this
            $scope.$watch('currentProjectId',
                (newValue, oldValue): void =>
                    self.setCurrentProjectName(newValue, $scope))
            
            var self = this
            $scope.loadProjects =
                () => self.loadProjects($scope, CurrentUserProjectResource)
            $scope.$on(Globals.EventNames.updateProjectList, $scope.loadProjects)
            $scope.loadProjects()
        }]
        public static Factory() {
            return new GsdProjectContext();
        }
    }
    app.directive('gsdProjectContext', GsdProjectContext.Factory)
}