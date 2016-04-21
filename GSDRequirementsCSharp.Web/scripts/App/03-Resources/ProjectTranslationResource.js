(function (app) {
    app.service('ProjectTranslationResource', ['$resource', function ($resource) {
        return $resource('/api/project/:id/translation',
                         { 'id': '@id' });
    }]);
})(angular.module(GSDRequirements.angularModuleName));