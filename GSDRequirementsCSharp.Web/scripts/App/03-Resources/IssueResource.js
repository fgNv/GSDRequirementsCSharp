(function (app) {
    app.service('IssueResource', ['$resource', function ($resource) {
        return $resource('/api/issue/:id',
                         { 'id': '@id' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);
})(angular.module(GSDRequirements.angularModuleName));