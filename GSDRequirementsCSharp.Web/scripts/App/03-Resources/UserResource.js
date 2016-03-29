(function (app) {
    app.service('UserResource', ['$resource', function ($resource) {
        return $resource('/api/user/'); 
    }]);
})(angular.module(GSDRequirements.angularModuleName));