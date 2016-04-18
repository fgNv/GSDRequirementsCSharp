module Models {
    export class SpecificationItem {
        public label: string
        public type: ArtifactType
        public typeLabel: string
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
        }
    }
}