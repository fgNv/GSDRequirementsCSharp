module Models {
    export class ClassRelationship {
        public source: ClassData
        public target: ClassData
        public type: RelationType
        public cell : any
    }
}