module Models {
    export class IssueContent {
        public description: string
        public locale: string
        public isUpdated: boolean
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}