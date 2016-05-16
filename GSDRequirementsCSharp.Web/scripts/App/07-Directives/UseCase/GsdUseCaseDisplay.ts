module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class GsdUseCaseDisplay {
        public scope = {
            'specificationItem': '=specificationItem'
        }
        public controller = ['$scope', 'UseCaseResource',
            ($scope, UseCaseResource) => {

                $scope.useCase = null
                $scope.availableLocales = []
                $scope.currentLocale = []

                $scope.$watch('specificationItem', (newValue: Models.UseCaseDiagram, oldValue) => {
                    if (!newValue || !newValue.id) {
                        $scope.useCase = null
                        $scope.availableLocales = []
                        return
                    }

                    UseCaseResource.get({ 'id': newValue.id })
                        .$promise
                        .then((response) => {
                            var useCase = new Models.UseCase(response)
                            $scope.useCase = useCase
                            $scope.availableLocales = _.map(useCase.contents, (c: Models.UseCaseContent) => c.locale)

                            if (_.contains($scope.availableLocales, GSDRequirements.currentLocale)) {
                                $scope.currentLocale = GSDRequirements.currentLocale
                                return
                            }

                            if (_.contains($scope.availableLocales, "en-US")) {
                                $scope.currentLocale = "en-US"
                                return
                            }
                            $scope.currentLocale = $scope.availableLocales[0]
                        })
                        .catch((err) => {
                            Notification.notifyError(Sentences.errorLoadingUseCase, err.data.messages)
                        })
                        .finally(() => {
                            $scope.pendingRequests--
                        });
                })

            }]
        public templateUrl = GSDRequirements.baseUrl + 'useCase/display'
        public static Factory() {
            return new GsdUseCaseDisplay();
        }
    }
    app.directive('gsdUseCaseDisplay', GsdUseCaseDisplay.Factory)
}
