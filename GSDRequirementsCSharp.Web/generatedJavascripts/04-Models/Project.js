var Models;
(function (Models) {
    var Project = (function () {
        function Project(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            var contentCurrentLocale = _.find(this.projectContents, function (pc) { return pc.locale == GSDRequirements.currentLocale; });
            this.isOutdated = contentCurrentLocale && !contentCurrentLocale.isUpdated;
        }
        Project.prototype.canAddTranslation = function () {
            return _.any(this.projectContents, function (c) { return !c.isUpdated; }) ||
                this.projectContents.length < GSDRequirements.localesAvailable.length;
        };
        Project.prototype.getCommandModel = function () {
            var projectContent = _.find(this.projectContents, function (p) { return p.locale == GSDRequirements.currentLocale; });
            var result = { 'id': this['id'] };
            if (projectContent != null) {
                result['name'] = projectContent.name;
                result['description'] = projectContent.description;
            }
            else {
                result['name'] = this['name'];
                result['description'] = this['description'];
            }
            return result;
        };
        return Project;
    }());
    Models.Project = Project;
})(Models || (Models = {}));
//# sourceMappingURL=Project.js.map