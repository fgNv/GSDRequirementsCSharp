module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData;

    export class IssueComment {
        public lastModificationLabel: string
        public id: string
        public description: string
        public locale: string
        public contents: Array<IssueCommentContent>
        public defineContent() {
            var content = _.find(this.contents, c => c.locale == GSDRequirements.currentLocale) ||
                _.find(this.contents, c => c.locale == GSDRequirements.currentLocale) ||
                this.contents[0]

            this.description = content.description
            this.locale = content.locale
        }
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            this.contents = _.map(this.contents, c => new IssueCommentContent(c))
            this.defineContent()
        }
    }
}