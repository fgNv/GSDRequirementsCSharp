var Models;
(function (Models) {
    var Permission = (function () {
        function Permission(data) {
            this.id = data['id'];
            this.profile = data['profile'];
            this.user = {
                'name': data['user']['contact']['name'],
                'email': data['user']['contact']['email'],
                'id': data['user']['id']
            };
        }
        return Permission;
    })();
    Models.Permission = Permission;
})(Models || (Models = {}));
