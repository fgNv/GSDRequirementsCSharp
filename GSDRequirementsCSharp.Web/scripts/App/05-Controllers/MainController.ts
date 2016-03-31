module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;
    declare var sentences: any;
    declare var baseUrl: string;

    var app = angular.module(GSDRequirements.angularModuleName);

    class MainController {
        constructor(
            private $scope: any
        ) {
            $scope.pendingRequests = 0
            $scope.$on(Globals.EventNames.projectListChanged, () => {
                $scope.$broadcast(Globals.EventNames.updateProjectList)
            })
        }
    }

    app.controller('MainController', ["$scope", ($scope) =>
        new MainController($scope)]);
}