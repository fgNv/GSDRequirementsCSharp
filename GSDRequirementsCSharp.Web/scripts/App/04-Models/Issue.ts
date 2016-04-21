module Models {

    declare var _: any;
    declare var GSDRequirements: Globals.GSDRequirementsData;

    export class Issue {
        public lastModificationLabel: string
        public id: string
        public identifier: number
        public contents: Array<IssueContent>
        public comments: Array<IssueComment>
        public description: string
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }

            this.contents = _.map(this.contents, (c) => new IssueContent(c))
            this.comments = _.map(this.comments, (c) => new IssueComment(c))
            
            this.defineContent()
        }
        private defineContent() {
            var currentLocale = _.find(this.contents,
                (c) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale) {
                this.description = currentLocale.description
                return;
            }

            var enUsLocale = _.find(this.contents,
                (c) => c.locale == "en-US")
            if (enUsLocale) {
                this.description = enUsLocale.description
                return;
            }
            
            if (this.contents.length > 0) {
                this.description = this.contents[0].description
            }
        }
    }
}