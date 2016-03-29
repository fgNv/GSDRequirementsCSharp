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
        Project.prototype.canEdit = function () {
            return _.any(this.ProjectContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        Project.prototype.getCommandModel = function () {
            return {
                'name': this['Name'],
                'description': this['Description'],
                'id': this['Id']
            };
        };
        return Project;
    })();
    Models.Project = Project;
})(Models || (Models = {}));
//# sourceMappingURL=Project.js.map