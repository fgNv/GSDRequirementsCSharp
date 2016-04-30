var Models;
(function (Models) {
    var SpecificationItem = (function () {
        function SpecificationItem(data) {
            for (var prop in data) {
                this[prop] = data[prop];
            }
        }
        return SpecificationItem;
    })();
    Models.SpecificationItem = SpecificationItem;
})(Models || (Models = {}));
