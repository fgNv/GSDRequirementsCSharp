(function (app) {
    app.service('IssueCommentResource', ['$resource', function ($resource) {
        return $resource('/api/issue/:issueId/comment',
                         { 'issueId': '@issueId' },
                        {
                            'update': { method: 'PUT' }
                        });
    }]);
})(angular.module(GSDRequirements.angularModuleName));