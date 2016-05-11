module Models {
    export class UseCaseEntityRelationContent {
        public locale: string
        public description: string

        public constructor(locale: string, description: string = null) {
            this.locale = locale
            this.description = description
        }
    }
}