module Models {
    export class UseCaseDiagram {
        public actors: any
        public contents: any
        public entitiesRelations: any
        public entities: Array<IUseCaseEntity>
        public id: string
        public relations: any
        public useCasesRelations: any
        public useCases: any
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