(function (app) {
    app.service('AuditingResource', ['$resource', function ($resource) {
        return $resource('/api/auditing/');
    }]);

    app.service('ProjectAuditingResource', ['$resource', function ($resource) {
        return $resource('/api/project/:id/auditing',
                         {
                             'id': '@id'
                         });
    }]);
})(angular.module(GSDRequirements.angularModuleName));