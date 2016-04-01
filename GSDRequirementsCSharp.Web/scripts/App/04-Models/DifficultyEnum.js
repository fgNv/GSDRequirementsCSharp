var Models;
(function (Models) {
    (function (difficulty) {
        difficulty[difficulty["easy"] = 10] = "easy";
        difficulty[difficulty["medium"] = 70] = "medium";
        difficulty[difficulty["hard"] = 900] = "hard";
    })(Models.difficulty || (Models.difficulty = {}));
    var difficulty = Models.difficulty;
})(Models || (Models = {}));
