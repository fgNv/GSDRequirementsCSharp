module Models {
    
    export class ClassDiagram {
        public id: string
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}