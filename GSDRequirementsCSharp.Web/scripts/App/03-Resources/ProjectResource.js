(function (app) {
    app.service('ProjectResource', ['$resource', function ($resource) {
        return $resource('/api/project/:id',
                        { 'id': '@id' },
                        { 'update': { method: 'PUT' } });
    }]);
})(angular.module(GSDRequirements.angularModuleName));