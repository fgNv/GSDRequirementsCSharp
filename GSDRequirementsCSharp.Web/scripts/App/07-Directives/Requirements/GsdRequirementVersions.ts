module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdRequirementVersions {
        public scope = {
            'artifactId': '=artifactId',
            'afterRestore': '=?'
        }
        private loadVersions($scope, VersionResource, artifactId) {
            $scope.pendingRequests++

            VersionResource.query({
                id: artifactId,
                artifact: 'requirement'
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
        public controller = ["$scope", "RequirementResource", "VersionResource",
            ($scope, RequirementResource, VersionResource) => {
                $scope.pendingRequests = 0

                $scope.versions = []
                $scope.selected = null

                $scope.select = (version): void => {
                    $scope.selected = version
                }

                $scope.restoreVersion = (): void => {
                    $scope.pendingRequests++

                    VersionResource.save({
                        id: $scope.selected.id,
                        version: $scope.selected.version,
                        artifact: 'requirement'
                    }).$promise
                        .then((items): void => {
                            Notification.notifySuccess(Sentences.versionRestoredSuccessfully)
                            if ($scope.afterRestore) {
                                $scope.afterRestore()
                            }
                            window.location.href = "#"
                        })
                        .catch((err): void => {
                            Notification.notifyError(Sentences.errorRestoringVersion, err.messages)
                        })
                        .finally((): void => {
                            $scope.pendingRequests--
                        })
                }

                $scope.$watch('artifactId', (newValue: Models.Requirement, oldValue) => {
                    if (!newValue) {
                        return
                    }
                    $scope.selectedItem = null
                    this.loadVersions($scope, VersionResource, $scope.artifactId)
                })

            }]
        public templateUrl = GSDRequirements.baseUrl + 'requirement/versions'
        public static Factory() {
            return new GsdRequirementVersions();
        }
    }
    app.directive('gsdRequirementVersions', GsdRequirementVersions.Factory)

}