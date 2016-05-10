﻿module Models {

    declare var GSDRequirements: Globals.GSDRequirementsData;
    declare var _: any;

    export class UseCaseDiagram {
        public actors: any //output / input
        public contents: Array<UseCaseDiagramContent>
        public entitiesRelations: any //input / output
        public entities: Array<IUseCaseEntity>
        public id: string
        public relations: any
        public useCasesRelations: any //input / output
        public useCases: any //output / input
        public getName() {
            var currentLocale = <UseCaseDiagramContent>_.find(this.contents,
                (c: UseCaseDiagramContent) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale && currentLocale.name)
                return currentLocale.name

            var enUs = <UseCaseDiagramContent>_.find(this.contents,
                (c: UseCaseDiagramContent) => c.locale == "en-US")
            if (enUs && enUs.name)
                return enUs.name

            var anyContent = this.contents[0]
            if (anyContent && anyContent.name)
                return anyContent.name

            return ""
        }
        constructor(data = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
            } else {
                this.entities = []
                this.relations = []
                this.contents = []
            }
        }
    }
}