var Models;
(function (Models) {
    var Requirement = (function () {
        function Requirement(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            if (!data['package'])
                return;
            this.package = new Models.Package(data['package']);
            switch (this.type) {
                case Models.RequirementType.functional:
                    this.prefix = "FR";
                    break;
                case Models.RequirementType.nonFunction:
                    this.prefix = "NFR";
                    break;
            }
            this.defineContent();
            this.packageId = this.package.id;
            this.requirementType = this.type;
            this.defineDescription();
        }
        Requirement.prototype.getLabel = function () {
            return "" + this.prefix + this.identifier;
        };
        Requirement.prototype.defineDescription = function () {
            this.description = (this.condition || "") + " " + (this.subject || "") + " " + (this.action || "");
        };
        Requirement.prototype.setLocale = function (locale) {
            var content = _.find(this.requirementContents, function (rc) { return rc.locale == locale; });
            if (content)
                this.fillWithContent(content);
        };
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
            this.defineDescription();
        };
        Requirement.prototype.canAddTranslation = function () {
            return this.requirementContents.length < GSDRequirements.localesAvailable.length;
        };
        return Requirement;
    })();
    Models.Requirement = Requirement;
})(Models || (Models = {}));
