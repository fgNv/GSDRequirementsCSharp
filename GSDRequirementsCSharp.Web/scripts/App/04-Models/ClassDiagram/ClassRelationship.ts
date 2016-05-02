module Models {
    export class ClassRelationship {
        public source: ClassData
        public target: ClassData
        public targetId: string
        public sourceId: string
        public type: RelationType
        public cell: any
        constructor(data: Object = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
            }
        }
    }
}