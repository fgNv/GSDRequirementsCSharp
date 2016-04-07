module Controllers {
    declare var angular: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;

    var app = angular.module(GSDRequirements.angularModuleName);

    class UpdateUserDataController {
        constructor($scope: any, UserResource: any) {
            $scope.pendingRequests = 0
            $scope.userData = {}

            $scope.save = () => {
                $scope.pendingRequests++

                UserResource.update($scope.userData)
                    .$promise
                    .then((): void=> {
                        Notification.notifySuccess(Sentences.dataSuccessfullyUpdated);
                        setTimeout((): void => {
                            window.location.reload()
                        }, 2500)
                    })
                    .catch((error): void => {
                        Notification.notifyError(Sentences.errorUpdatingData, error.messages);
                        $scope.pendingRequests--
                    })
            }
        }
    }

    app.controller('UpdateUserDataController', ["$scope", "UserResource", UpdateUserDataController]);
}