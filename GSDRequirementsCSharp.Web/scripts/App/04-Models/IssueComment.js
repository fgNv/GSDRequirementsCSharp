var Models;
(function (Models) {
    var IssueComment = (function () {
        function IssueComment(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return IssueComment;
    })();
    Models.IssueComment = IssueComment;
})(Models || (Models = {}));
