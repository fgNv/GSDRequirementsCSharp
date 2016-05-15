module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;
    declare var _: any;

    var app = angular.module(GSDRequirements.angularModuleName);

    class LastActivitiesInProjectsController {
        constructor($scope, AuditingResource) {
            $scope.pendingRequests = 0
            $scope.projects = []
            $scope.period = Models.AuditingPeriod.lastWeek
            $scope.periodOptions = Globals.enumerateEnum(Models.AuditingPeriod)
            $scope.currentResultsPeriod = null
            
            $scope.loadActivities = () => {
                $scope.pendingRequests++

                AuditingResource.query({ period: $scope.period })
                    .$promise
                    .then((auditings): void => {
                        var groupedByProjectId = _.groupBy(auditings, 'projectId');

                        var projects = _.map(groupedByProjectId, (v, k) => {
                            return {
                                id: k,
                                name: v[0].projectName,
                                activities: v
                            }
                        });

                        $scope.projects = projects

                        var item = Models.AuditingPeriod[Models.AuditingPeriod.lastWeek]
                        $scope.currentResultsPeriod = Sentences[item]
                    })
                    .catch((error): void => {
                        Notification.notifyError(Sentences.errorRetrievingActivities, error.data.messages);
                    })
                    .finally((): void => {
                        $scope.pendingRequests--
                    })
            }

            $scope.loadActivities()
        }
    }

    app.controller('LastActivitiesInProjectsController', ["$scope", "AuditingResource",
        LastActivitiesInProjectsController]);

}