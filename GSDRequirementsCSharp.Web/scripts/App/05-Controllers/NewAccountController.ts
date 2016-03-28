module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;

    var app = angular.module(GSDRequirements.angularModuleName);

    export class NewAccountController {
        constructor(
            private $scope: any,
            private UserResource: any
        ) {
            $scope.save = () => this.Save(UserResource, $scope)
            this.$scope.UserData = new UserData()
        }
        private Save(userResource: any, $scope : any): void {
            userResource.save($scope.UserData)
                        .$promise
                        .then((response) => {

                        })
                        .catch((error) => {

                        })
                        .finally(() => {

                        });
        }
    }
    app.controller('NewAccountController', ["$scope", "UserResource", ($scope, UserResource) =>
        new NewAccountController($scope, UserResource)]);
}