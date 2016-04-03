var Models;
(function (Models) {
    var Package = (function () {
        function Package(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
            this.defineContent();
        }
        Package.prototype.defineContent = function () {
            var currentLocale = _.find(this.contents, function (c) { return c.locale == GSDRequirements.currentLocale; });
            if (currentLocale) {
                this.fillWithContent(currentLocale);
                return;
            }
            var enUsLocale = _.find(this.contents, function (c) { return c.locale == "en-US"; });
            if (enUsLocale) {
                this.fillWithContent(enUsLocale);
                return;
            }
            this.fillWithContent(this.contents[0]);
        };
        Package.prototype.fillWithContent = function (content) {
            this.description = content.description;
            this.locale = content.locale;
        };
        Package.prototype.canAddTranslation = function () {
            return _.any(this.contents, function (c) { return !c.isUpdated; }) ||
                this.contents.length < GSDRequirements.localesAvailable.length;
        };
        return Package;
    })();
    Models.Package = Package;
})(Models || (Models = {}));
//# sourceMappingURL=Package.js.map