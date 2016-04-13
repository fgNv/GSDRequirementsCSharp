var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalItemIssuesController = (function () {
        function ModalItemIssuesController($scope, $uibModalInstance, itemIssuesResource, specificationItem, $rootScope, $location, IssueCommentResource) {
            var _this = this;
            $scope.pendingRequests = 0;
            $scope.specificationItem = specificationItem;
            $scope.issues = [];
            $scope.availableContentLocales = [];
            $scope.currentDescription = '';
            $scope.addComment = function (issue) {
                $scope.pendingRequests++;
                var request = { 'issueId': issue.id, 'contents': [] };
                _.each($scope.comments, function (val, key) {
                    val.locale = key;
                    if (val.description)
                        request.contents.push(val);
                });
                IssueCommentResource.save(request)
                    .$promise
                    .then(function () {
                    Notification.notifySuccess(Sentences.commentSuccessfullyAdded);
                    _this.loadComments($scope, $scope.issueInDetail, IssueCommentResource);
                    _this.initializeCommentData($scope);
                })
                    .catch(function (error) {
                    Notification.notifyError(Sentences.errorAddingComment, error.messages);
                })
                    .finally(function () {
                    $scope.pendingRequests--;
                });
            };
            $scope.utility = {};
            $scope.utility.newCommentContainsLocale =
                function (l) {
                    return $scope.comments &&
                        $scope.comments[l] &&
                        $scope.comments[l].description;
                };
            $scope.locales = _.map(GSDRequirements.localesAvailable, function (l) { return l.name; });
            $scope.$watch('displayLocale', function (newValue, oldValue) {
                if (!$scope.issueInDetail)
                    return;
                _this.defineDisplayContent($scope, $scope.issueInDetail, newValue);
            });
            $rootScope.$on('$locationChangeStart', function (event, newUrl, oldUrl) {
                var pathValues = $location.path().split('/');
                var step = pathValues[1];
                var identifier = pathValues[2];
                if (step == "issue" && identifier) {
                    var issue = _.find($scope.issues, function (i) { return i.identifier == identifier; });
                    if (!issue)
                        return;
                    $scope.issueInDetail = issue;
                    _this.populateDetailData($scope, issue);
                }
                if (!step) {
                    $scope.issueInDetail = null;
                }
            });
            this.loadIssues($scope, itemIssuesResource, specificationItem);
        }
        ModalItemIssuesController.prototype.loadIssues = function ($scope, itemIssuesResource, specificationItem) {
            $scope.pendingRequests++;
            itemIssuesResource.query({ id: specificationItem.id })
                .$promise
                .then(function (issues) {
                $scope.issues = _.map(issues, function (i) { return new Models.Issue(i); });
            })
                .catch(function (error) {
                Notification.notifyError(Sentences.errorLoadingIssues, error.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        ModalItemIssuesController.prototype.populateDetailData = function ($scope, issue) {
            var locales = _.chain(issue.contents)
                .map(function (i) { return i.locale; })
                .sortBy(function (l) { return l; })
                .value();
            $scope.availableContentLocales = locales;
            $scope.displayLocale =
                _.find(locales, function (l) { return l == GSDRequirements.currentLocale; }) ||
                    _.find(locales, function (l) { return l == "en-US"; }) ||
                    locales[0];
            this.initializeCommentData($scope);
            this.defineDisplayContent($scope, issue, $scope.displayLocale);
        };
        ModalItemIssuesController.prototype.initializeCommentData = function ($scope) {
            $scope.commentData = {};
            $scope.commentData.locale = GSDRequirements.currentLocale;
            $scope.locales = _.map(GSDRequirements.localesAvailable, function (l) { return l.name; });
            $scope.comments = {};
            _.each(GSDRequirements.localesAvailable, function (l) {
                $scope.comments[l.name] = {};
                $scope.comments[l.name].description = '';
                $scope.comments[l.name].locale = l.name;
            });
        };
        ModalItemIssuesController.prototype.defineDisplayContent = function ($scope, issue, locale) {
            var content = _.find(issue.contents, function (c) { return c.locale == locale; });
            if (!content)
                return;
            $scope.currentDescription = content.description;
        };
        ModalItemIssuesController.prototype.loadComments = function ($scope, issue, IssueCommentResource) {
            $scope.pendingRequests++;
            IssueCommentResource.query({ 'issueId': issue.id })
                .$promise
                .then(function (comments) {
                issue.comments = _.map(comments, function (c) { return new Models.IssueComment(c); });
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        return ModalItemIssuesController;
    })();
    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', '$rootScope', '$location',
        "IssueCommentResource", ModalItemIssuesController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalItemIssuesController.js.map