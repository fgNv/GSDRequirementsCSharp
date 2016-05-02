var Globals;
(function (Globals) {
    function getValues(e) {
        return Object.keys(e).map(function (v) { return parseInt(v, 10); }).filter(function (v) { return !isNaN(v); });
    }
    function enumerateEnum(e) {
        return getValues(e).map(function (v) {
            return { label: Sentences[e[v]], value: v, key: e[v] };
        });
    }
    Globals.enumerateEnum = enumerateEnum;
})(Globals || (Globals = {}));
