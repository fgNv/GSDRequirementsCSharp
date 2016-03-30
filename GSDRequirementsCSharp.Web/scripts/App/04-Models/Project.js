var Models;
(function (Models) {
    var Project = (function () {
        function Project(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        Project.prototype.canAddTranslation = function () {
            return !_.any(this.projectContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        Project.prototype.canEdit = function () {
            return _.any(this.projectContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        Project.prototype.getCommandModel = function () {
            var projectContent = _.find(this.projectContents, function (p) { return p.locale == GSDRequirements.currentLocale; });
            var result = {
                'name': this['name'],
                'description': projectContent.description,
                'id': this['id']
            };
            return result;
        };
        return Project;
    })();
    Models.Project = Project;
})(Models || (Models = {}));
//# sourceMappingURL=Project.js.map