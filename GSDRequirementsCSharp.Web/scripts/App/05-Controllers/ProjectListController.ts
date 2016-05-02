module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData
    import Project = Models.Project

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class ProjectListController {
        constructor($scope, ProjectResource, $rootScope, $location) {
            $scope.currentPage = 1
            $scope.maxPages = 1
            $scope.pendingRequests = 0
            $scope.projects = []
            var pageSize = 10

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];

                if (!step) {
                    $scope.currentProject = null
                    $scope.projectToTranslate = null
                }
            });

            window.location.href = "#"

            $scope.addProject = () => {
                $scope.currentProject = {}
                window.location.href = "#/form"
            }

            $scope.loadPage = (page) => {
                $scope.currentPage = page
                $scope.loadProjects()
            }

            $scope.setCurrentProject = (p): void => {
                $scope.currentProject = p
                window.location.href = "#/form"
            }
            $scope.setProjectToTranslate = (p): void => {
                $scope.projectToTranslate = p
                window.location.href = "#/translate"
            }

            $scope.loadProjects = () => this.LoadProjects(ProjectResource,
                $scope,
                pageSize)

            $scope.inactivateProject = (p): void => {
                this.InactivateProject(ProjectResource, $scope, p)
            }

            $scope.getPaginationRange = function () {
                return _.range(1, $scope.maxPages + 1);
            };

            $scope.loadProjects()
            $scope.UserData = new UserData()
        }
        private InactivateProject(projectResource: any, $scope: any, project: Project): void {
            if (!confirm(Sentences.areYouCertainYouWishToRemoveThisItem)) {
                return;
            }

            $scope.pendingRequests++;
            projectResource.remove({ id: project.id })
                .$promise
                .then(r => {
                    Notification.notifySuccess(Sentences.projectInactivatedSuccessfully)
                    $scope.$emit(Globals.EventNames.projectListChanged)
                    $scope.loadProjects()
                })
                .catch(error => {
                    Notification.notifyError(Sentences.errorInactivatingProject, error.data.messages)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
        private LoadProjects(projectResource: any, $scope: any, size: number): void {
            $scope.pendingRequests++;
            var request = { page: $scope.currentPage, pageSize: size }
            projectResource.get(request)
                .$promise
                .then((response) => {
                    $scope.projects = _.map(response.projects,
                        (p) => new Models.Project(p))
                    $scope.maxPages = response.maxPages
                })
                .catch((error) => {
                    Notification.notifyError(Sentences.errorLoadingProjects,
                        error.data.errors)
                })
                .finally(() => {
                    $scope.pendingRequests--;
                });
        }
    }
    app.controller('ProjectListController', ["$scope", "ProjectResource",
        "$rootScope", "$location", ProjectListController]);
}