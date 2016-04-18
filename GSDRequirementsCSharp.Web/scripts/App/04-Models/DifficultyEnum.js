var Models;
(function (Models) {
    (function (Difficulty) {
        Difficulty[Difficulty["easy"] = 10] = "easy";
        Difficulty[Difficulty["medium"] = 70] = "medium";
        Difficulty[Difficulty["hard"] = 900] = "hard";
    })(Models.Difficulty || (Models.Difficulty = {}));
    var Difficulty = Models.Difficulty;
})(Models || (Models = {}));
