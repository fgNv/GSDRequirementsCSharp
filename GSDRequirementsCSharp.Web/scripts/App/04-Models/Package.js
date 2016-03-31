var Models;
(function (Models) {
    var Package = (function () {
        function Package(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        Package.prototype.canAddTranslation = function () {
            return this.locale != GSDRequirements.currentLocale;
        };
        Package.prototype.canEdit = function () {
            return this.locale == GSDRequirements.currentLocale;
        };
        return Package;
    })();
    Models.Package = Package;
})(Models || (Models = {}));
//# sourceMappingURL=Package.js.map