var Models;
(function (Models) {
    (function (profile) {
        profile[profile["projectOwner"] = 1] = "projectOwner";
        profile[profile["editor"] = 50] = "editor";
        profile[profile["collaborator"] = 300] = "collaborator";
        profile[profile["observer"] = 600] = "observer";
    })(Models.profile || (Models.profile = {}));
    var profile = Models.profile;
})(Models || (Models = {}));
//# sourceMappingURL=ProfileEnum.js.map