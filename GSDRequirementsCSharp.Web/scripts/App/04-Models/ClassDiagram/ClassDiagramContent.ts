module Models {
    export class ClassDiagramContent {
        public name: string
        public locale: string
        public constructor(data: any) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}