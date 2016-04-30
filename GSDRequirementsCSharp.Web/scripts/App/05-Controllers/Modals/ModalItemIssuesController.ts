module Controllers {

    declare var angular: any
    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData
    var app = angular.module(GSDRequirements.angularModuleName)

    class ModalItemIssuesController {
        private loadIssues($scope, itemIssuesResource, specificationItem, onAllIssuesConcluded) {
            $scope.pendingRequests++;
            itemIssuesResource.query({ id: specificationItem.id })
                .$promise
                .then((issues): void => {
                    $scope.issues = _.map(issues, i => new Models.Issue(i))
                    if (issues.length == 0 && onAllIssuesConcluded) {
                        onAllIssuesConcluded()
                    }
                })
                .catch((error): void => {
                    Notification.notifyError(Sentences.errorLoadingIssues, error.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--;
                });
        }
        private populateDetailData($scope, issue) {
            var locales = _.chain(issue.contents)
                .map(i => i.locale)
                .sortBy(l => l)
                .value()

            $scope.availableContentLocales = locales

            $scope.displayLocale =
                _.find(locales, l => l == GSDRequirements.currentLocale) ||
                _.find(locales, l => l == "en-US") ||
                locales[0]

            this.initializeCommentData($scope)

            this.defineDisplayContent($scope, issue, $scope.displayLocale)
        }
        private initializeCommentData($scope) {
            $scope.commentData = {}
            $scope.commentData.locale = GSDRequirements.currentLocale
            $scope.locales = _.map(GSDRequirements.localesAvailable, l => l.name)

            $scope.comments = {}

            _.each(GSDRequirements.localesAvailable, (l): void => {
                $scope.comments[l.name] = {}
                $scope.comments[l.name].description = ''
                $scope.comments[l.name].locale = l.name
            })
        }
        private defineDisplayContent($scope, issue, locale) {
            var content = _.find(issue.contents, c => c.locale == locale)
            if (!content) return;
            $scope.currentDescription = content.description
        }

        private definePlaceholder($scope, locale, $q) {
            var deferred = $q.defer()

            if (!$scope.commentData)
                return deferred.promise;

            if ($scope.commentData.locale == locale) {
                deferred.reject()
                return deferred.promise;
            }

            var content = $scope.comments[locale]
            if (!content.description) {
                deferred.reject()
                return deferred.promise;
            }

            $scope.commentPlaceholder = content.description
            deferred.resolve()

            return deferred.promise
        }
        private loadComments($scope, issue, IssueCommentResource) {
            $scope.pendingRequests++;

            IssueCommentResource.query({ 'issueId': issue.id })
                .$promise
                .then((comments): void => {
                    issue.comments = _.map(comments, c => new Models.IssueComment(c))
                })
                .finally((): void => {
                    $scope.pendingRequests--
                });
        }
        constructor($scope, $uibModalInstance, itemIssuesResource,
            specificationItem, $rootScope, $location, IssueCommentResource,
            IssueConclusionResource, onAllIssuesConcluded, $q) {
            $scope.pendingRequests = 0
            $scope.specificationItem = specificationItem
            $scope.issues = []
            $scope.availableContentLocales = []
            $scope.currentDescription = ''

            $scope.markAsConcluded = (issue) => {
                $scope.pendingRequests++
                IssueConclusionResource.update({ id: issue.id })
                    .$promise
                    .then(() => {
                        Notification.notifySuccess(Sentences.issueSuccessfullyConcluded);
                        window.location.href = "#"
                        this.initializeCommentData($scope)
                        this.loadIssues($scope, itemIssuesResource, specificationItem, onAllIssuesConcluded)
                    })
                    .catch((error) => {
                        Notification.notifyError(Sentences.errorConcludingIssue, error.data.messages)
                    })
                    .finally((): void=> {
                        $scope.pendingRequests--
                    });
            }

            $scope.addComment = (issue) => {
                $scope.pendingRequests++;
                var request = { 'issueId': issue.id, 'contents': [] }

                _.each($scope.comments, (val, key) => {
                    val.locale = key
                    if (val.description)
                        request.contents.push(val)
                })

                IssueCommentResource.save(request)
                    .$promise
                    .then(() => {
                        $scope.commentPlaceholder = ''
                        Notification.notifySuccess(Sentences.commentSuccessfullyAdded);
                        this.loadComments($scope, $scope.issueInDetail, IssueCommentResource)
                        this.initializeCommentData($scope)
                    })
                    .catch((error) => {
                        Notification.notifyError(Sentences.errorAddingComment, error.data.messages)
                    })
                    .finally(() => {
                        $scope.pendingRequests--;
                    });
            }

            $scope.utility = {}
            $scope.utility.newCommentContainsLocale =
                (l) => $scope.comments &&
                    $scope.comments[l] &&
                    $scope.comments[l].description


            $scope.locales = _.map(GSDRequirements.localesAvailable, (l: Models.Locale) => l.name)

            $scope.$watch('displayLocale', (newValue, oldValue) => {
                if (!$scope.issueInDetail) return

                this.defineDisplayContent($scope, $scope.issueInDetail, newValue)
            })

            $scope.$watch('commentData.locale', (newValue, oldValue) => {
                var self = this

                self.definePlaceholder($scope, GSDRequirements.currentLocale, $q)
                    .catch(() => self.definePlaceholder($scope, 'en-US', $q))
                    .catch(() => {
                        var firstContent = _.find($scope.comments, i => i.description)
                        if (firstContent)
                            $scope.commentPlaceholder = firstContent.description
                    })
            })

            $rootScope.$on('$locationChangeStart', (event, newUrl, oldUrl): void => {
                var pathValues = $location.path().split('/')
                var step = pathValues[1];
                var identifier = pathValues[2];

                if (step == "issue" && identifier) {
                    var issue = _.find($scope.issues, i => i.identifier == identifier)
                    if (!issue) return;
                    $scope.issueInDetail = issue;
                    this.populateDetailData($scope, issue)
                }

                if (!step) {
                    $scope.issueInDetail = null
                }
            });

            this.loadIssues($scope, itemIssuesResource, specificationItem, onAllIssuesConcluded)
        }
    }

    app.controller('ModalItemIssuesController', ["$scope", "$uibModalInstance",
        "ItemIssuesResource", 'specificationItem', '$rootScope', '$location',
        "IssueCommentResource", "IssueConclusionResource", "onAllIssuesConcluded",
        "$q", ModalItemIssuesController]);
}