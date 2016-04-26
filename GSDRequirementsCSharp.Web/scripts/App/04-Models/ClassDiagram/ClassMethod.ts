module Models {

    declare var _: any;

    export class ClassMethod {
        public id: string
        public returnType: string
        public name: string
        public parameters: Array<ClassMethodParameter>
        public Visibility: Visibility

        public getDescription() {
            var visibility = this.Visibility == Visibility.Public ? "+" : "-"
            var parameters = _.map(this.parameters,
                                  (p: ClassMethodParameter): string => p.getDescription())

            return `${visibility} ${this.name}(${parameters.join(', ')}) : ${this.returnType}`
        }
    }
}
