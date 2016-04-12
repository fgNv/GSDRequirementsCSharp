module Models {
    export class IssueComment {
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}