var Resources;
(function (Resources) {
    var AbstractResource = (function () {
        function AbstractResource() {
        }
        AbstractResource.prototype.query = function (request) {
        };
        AbstractResource.prototype.get = function (request) {
        };
        AbstractResource.prototype.save = function (request) {
        };
        return AbstractResource;
    })();
    Resources.AbstractResource = AbstractResource;
})(Resources || (Resources = {}));
