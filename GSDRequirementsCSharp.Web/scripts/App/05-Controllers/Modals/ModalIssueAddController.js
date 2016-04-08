var Controllers;
(function (Controllers) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var ModalIssueAddController = (function () {
        function ModalIssueAddController($scope, $uibModalInstance, issueResource) {
        }
        return ModalIssueAddController;
    })();
    app.controller('ModalIssueAddController', ["$scope", "$uibModalInstance",
        "IssueResource", ModalIssueAddController]);
})(Controllers || (Controllers = {}));
//# sourceMappingURL=ModalIssueAddController.js.map