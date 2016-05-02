var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var UpdateUserDataController = (function () {
        function UpdateUserDataController($scope, UserResource) {
            $scope.pendingRequests = 0;
            $scope.userData = {};
            $scope.save = function () {
                $scope.pendingRequests++;
                UserResource.update($scope.userData)
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.dataSuccessfullyUpdated);
                    setTimeout(function () {
                        window.location.reload();
                    }, 2500);
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorUpdatingData, error.data.messages);
                    $scope.pendingRequests--;
                });
            };
        }
        return UpdateUserDataController;
    })();
    app.controller('UpdateUserDataController', ["$scope", "UserResource", UpdateUserDataController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=UpdateUserDataController.js.map