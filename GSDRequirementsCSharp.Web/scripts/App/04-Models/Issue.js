var Models;
(function (Models) {
    var Issue = (function () {
        function Issue(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            this.contents = _.map(this.contents, function (c) { return new Models.IssueContent(c); });
            this.comments = _.map(this.comments, function (c) { return new Models.IssueComment(c); });
            this.defineContent();
        }
        Issue.prototype.defineContent = function () {
            var currentLocale = _.find(this.contents, function (c) { return c.locale == GSDRequirements.currentLocale; });
            if (currentLocale) {
                this.description = currentLocale.description;
                return;
            }
            var enUsLocale = _.find(this.contents, function (c) { return c.locale == "en-US"; });
            if (enUsLocale) {
                this.description = enUsLocale.description;
                return;
            }
            if (this.contents.length > 0) {
                this.description = this.contents[0].description;
            }
        };
        return Issue;
    })();
    Models.Issue = Issue;
})(Models || (Models = {}));
