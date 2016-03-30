(function (app) {
    app.service('ProjectContentResource', ['$resource', function ($resource) {
        return $resource('/api/projectContent');
    }]);
})(angular.module(GSDRequirements.angularModuleName));