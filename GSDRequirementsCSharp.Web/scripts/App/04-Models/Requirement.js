var Models;
(function (Models) {
    var Requirement = (function () {
        function Requirement(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            this.defineContent();
        }
        Requirement.prototype.defineContent = function () {
            var currentLocale = _.find(this.requirementContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
            if (currentLocale) {
                this.fillWithContent(currentLocale);
                return;
            }
            var enUsLocale = _.find(this.requirementContents, function (c) { return c.locale == "en-US"; });
            if (enUsLocale) {
                this.fillWithContent(enUsLocale);
                return;
            }
            this.fillWithContent(this.requirementContents[0]);
        };
        Requirement.prototype.fillWithContent = function (content) {
            this.condition = content.condition;
            this.subject = content.subject;
            this.action = content.action;
            this.locale = content.locale;
        };
        Requirement.prototype.canAddTranslation = function () {
            return !_.any(this.requirementContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        Requirement.prototype.canEdit = function () {
            return _.any(this.requirementContents, function (c) { return c.locale == GSDRequirements.currentLocale; });
        };
        return Requirement;
    })();
    Models.Requirement = Requirement;
})(Models || (Models = {}));
