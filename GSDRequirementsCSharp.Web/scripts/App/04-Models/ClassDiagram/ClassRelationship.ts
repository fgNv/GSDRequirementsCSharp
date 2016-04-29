module Models {
    export class ClassRelationship {
        public source: ClassData
        public target: ClassData
        public targetId: string
        public sourceId: string
        public type: RelationType
        public cell : any
    }
}