var Models;
(function (Models) {
    var IssueComment = (function () {
        function IssueComment(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            this.contents = _.map(this.contents, function (c) { return new Models.IssueCommentContent(c); });
            this.defineContent();
        }
        IssueComment.prototype.defineContent = function () {
            var content = _.find(this.contents, function (c) { return c.locale == GSDRequirements.currentLocale; }) ||
                _.find(this.contents, function (c) { return c.locale == GSDRequirements.currentLocale; }) ||
                this.contents[0];
            this.description = content.description;
            this.locale = content.locale;
        };
        return IssueComment;
    })();
    Models.IssueComment = IssueComment;
})(Models || (Models = {}));
