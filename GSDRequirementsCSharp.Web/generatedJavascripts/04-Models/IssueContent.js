var Models;
(function (Models) {
    var IssueContent = (function () {
        function IssueContent(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return IssueContent;
    }());
    Models.IssueContent = IssueContent;
})(Models || (Models = {}));
//# sourceMappingURL=IssueContent.js.map