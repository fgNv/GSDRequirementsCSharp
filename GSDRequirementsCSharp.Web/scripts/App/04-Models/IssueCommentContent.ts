module Models {
    export class IssueCommentContent {
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}