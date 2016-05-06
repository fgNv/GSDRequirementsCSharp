module Models {
    export class UseCaseDiagram {
        public id: string
        public relations: any
        public entities: any
        public contents: any
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