module Models {
    export class ClassProperty {
        public id: string
        public type: string
        public name: string
        public visibility: Visibility

        public getDescription() {
            var visibility = GetVisibilityUmlRepresentation(this.visibility)
            return `${visibility} ${this.name} : ${this.type}`
        }

        public constructor(data: any = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
            }
        }
    }
}
