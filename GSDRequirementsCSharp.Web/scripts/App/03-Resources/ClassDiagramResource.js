(function (app) {
    app.service('ClassDiagramResource', ['$resource', function ($resource) {
        return $resource('/api/classDiagram/:id/:page/:pageSize',
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
})(angular.module(GSDRequirements.angularModuleName));