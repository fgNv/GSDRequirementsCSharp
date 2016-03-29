var Models;
(function (Models) {
    var Project = (function () {
        function Project(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        Project.prototype.canAddTranslation = function () {
            return !_.any(this.ProjectContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        return Project;
    })();
    Models.Project = Project;
})(Models || (Models = {}));
