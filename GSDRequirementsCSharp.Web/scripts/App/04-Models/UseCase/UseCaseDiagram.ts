module Models {

    declare var GSDRequirements: Globals.GSDRequirementsData;
    declare var _: any;

    export class UseCaseDiagram {
        public actors: any //output / input
        public contents: Array<UseCaseDiagramContent>
        public entitiesRelations: any //input / output
        public entities: Array<IUseCaseEntity>
        public id: string
        public identifier: string
        public relations: any
        public useCasesRelations: any //input / output
        public useCases: any //output / input
        public getLabel() {
            return `UCD${this.identifier}`
        }
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
                this.useCases = _.map(this.useCases, uc => new UseCase(uc))
                this.actors = _.map(this.actors, a => new Actor(a))
                this.entities = _.union(this.useCases, this.actors)
                this.relations = _.map(_.union(this.useCasesRelations, this.entitiesRelations),
                                      (r) => new Models.UseCaseRelationship(r))
            } else {
                this.entities = []
                this.relations = []
                this.contents = []
            }
        }
    }
}