(function (app) {
    app.factory('UserResource', ['$resource', function($resource){
        return $resource('/api/user/');
    }]);
})(angular.module(GSDRequirements.angularModuleName));