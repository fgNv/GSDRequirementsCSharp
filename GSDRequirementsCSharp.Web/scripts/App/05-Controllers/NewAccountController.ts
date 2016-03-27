module Controllers {

    import UserData = NewAccount.UserData
    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var GSDRequirements: GSDRequirementsData;

    var app = angular.module(GSDRequirements.angularModuleName);
    
    export class NewAccountController {
        static $inject = ["$scope", "UserResource"];
        constructor(protected $scope: any,
                    protected UserResource: any) {
            // todo
        }
        public UserData: UserData
        public Save(): void {
            
            this.UserResource
                .save(this.UserData)
                .$promise
                .then((response) => {

                })
                .catch( (error) => {

                })
                .finally(() => {

                });
        }
    }
    app.controller('NewAccountController', NewAccountController);
}