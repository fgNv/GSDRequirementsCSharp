module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdUseCaseDiagramVersions {
        public scope = {
            'artifactId': '=artifactId',
            'afterRestore': '=?'
        }
        private loadVersions($scope, VersionResource, artifactId) {
            $scope.pendingRequests++

            VersionResource.query({
                id: artifactId,
                artifact: 'useCaseDiagram'
            }).$promise
                .then((items): void => {
                    $scope.versions = items
                })
                .catch((err): void => {
                    Notification.notifyError(Sentences.errorLoadingVersions, err.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public controller = ["$scope", "UseCaseDiagramResource", "VersionResource",
            ($scope, UseCaseDiagramResource, VersionResource) => {
                $scope.pendingRequests = 0

                $scope.versions = []
                $scope.selected = null
                
                $scope.restoreVersion = (): void => {
                    $scope.pendingRequests++

                    VersionResource.save({
                        id: $scope.selected.id,
                        version: $scope.selected.version,
                        artifact: 'useCaseDiagram'
                    }).$promise
                        .then((items): void => {
                            Notification.notifySuccess(Sentences.versionRestoredSuccessfully)
                            if ($scope.afterRestore) {
                                $scope.afterRestore()
                            }
                            window.location.href = "#"
                        })
                        .catch((err): void => {
                            Notification.notifyError(Sentences.errorRestoringVersion, err.data.messages)
                        })
                        .finally((): void => {
                            $scope.pendingRequests--
                        })
                }

                $scope.select = (version): void => {
                    $scope.selected = version
                }

                $scope.$watch('artifactId', (newValue: Models.ClassDiagram, oldValue) => {
                    if (!newValue) {
                        return
                    }
                    $scope.selectedItem = null
                    this.loadVersions($scope, VersionResource, $scope.artifactId)
                })

            }]
        public templateUrl = GSDRequirements.baseUrl + 'useCaseDiagram/versions'
        public static Factory() {
            return new GsdUseCaseDiagramVersions();
        }
    }
    app.directive('gsdUseCaseDiagramVersions', GsdUseCaseDiagramVersions.Factory)

}