module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class ActivitiesInCurrentProjectController {
        constructor($scope, ProjectAuditingResource) {

            $scope.projectId = null
            $scope.pendingRequests = 0
            $scope.activities = []

            function loadActivities() {
                $scope.pendingRequests++

                ProjectAuditingResource.query({ id: $scope.projectId })
                    .$promise
                    .then((auditings): void => {
                        $scope.activities = auditings
                    })
                    .catch((error): void => {
                        Notification.notifyError(Sentences.errorRetrievingActivities, error.data.messages);
                    })
                    .finally((): void => {
                        $scope.pendingRequests--
                    })
            }

            $scope.$watch('projectId', (newValue, oldValue) => {
                if (!newValue)
                    return

                loadActivities()
            })
        }
    }

    app.controller('ActivitiesInCurrentProjectController', ["$scope", "ProjectAuditingResource",
        ActivitiesInCurrentProjectController]);

}