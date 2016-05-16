(function (app) {
    app.service('UseCaseResource', ['$resource', function ($resource) {
        return $resource('/api/useCase/:id',
                         {
                             'id': '@id'
                         });
    }]);

})(angular.module(GSDRequirements.angularModuleName));