(function (app) {
    app.service('ClassDiagramResource', ['$resource', function ($resource) {
        return $resource('/api/classDiagram/:id',
                         { 'id': '@id' },
                        {
                            'update': { method: 'PUT' },
                            'remove': { method: 'DELETE' }
                        });
    }]);
})(angular.module(GSDRequirements.angularModuleName));