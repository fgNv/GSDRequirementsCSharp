module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdClassDiagramVersions {
        public scope = {
            'artifactId': '=artifactId'
        }
        private loadVersions($scope, VersionResource, artifactId) {
            $scope.pendingRequests++

            VersionResource.query({
                id: artifactId,
                artifact: 'classDiagram'
            }).$promise
                .then((items): void => {
                    $scope.versions = items
                })
                .catch((err): void => {
                    Notification.notifyError(Sentences.errorLoadingVersions, err.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        public controller = ["$scope", "ClassDiagramResource", "VersionResource",
            ($scope, ClassDiagramResource, VersionResource) => {
                $scope.pendingRequests = 0

                $scope.versions = []
                $scope.selected = null

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
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/versions'
        public static Factory() {
            return new GsdClassDiagramVersions();
        }
    }
    app.directive('gsdClassDiagramVersions', GsdClassDiagramVersions.Factory)

}