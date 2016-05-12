(function (app) {
    app.service('UseCaseDiagramResource', ['$resource', function ($resource) {
        return $resource('/api/useCaseDiagram/:id/:page/:pageSize',
                         {
                             'id': '@id',
                             'page': '@page',
                             'pageSize': '@pageSize'
                         },
                         {
                             'update': { method: 'PUT' },
                             'remove': { method: 'DELETE' }
                         });
    }]);


    app.service('UseCasesByDiagramResource', ['$resource', function ($resource) {
        return $resource('/api/useCaseDiagram/:id/useCases',
                         {
                             'id': '@id'
                         });
    }]);

})(angular.module(GSDRequirements.angularModuleName));