var Models;
(function (Models) {
    var IssueCommentContent = (function () {
        function IssueCommentContent(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return IssueCommentContent;
    })();
    Models.IssueCommentContent = IssueCommentContent;
})(Models || (Models = {}));
