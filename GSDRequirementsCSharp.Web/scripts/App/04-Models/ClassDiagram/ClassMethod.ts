module Models {
    export class ClassMethod {
        public id: string
        public returnType: string
        public name: string
        public parameters: Array<ClassMethodParameter>
        public Visibility: Visibility
    }
}
