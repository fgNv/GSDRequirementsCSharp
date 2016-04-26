module Models {

    declare var _: any;

    export class ClassMethod {
        public id: string
        public returnType: string
        public name: string
        public classMethodParameters: Array<ClassMethodParameter>
        public Visibility: Visibility

        public constructor(data: any = null) {
            if (data) {
                for (var prop in data) {
                    this[prop] = data[prop]
                }
                this.classMethodParameters = _.map(data.parameters,
                                                   (cm) => new ClassMethodParameter(cm))
            } else {
                this.classMethodParameters = []
            }
        }

        public addParameter(): void {
            this.classMethodParameters.push(new ClassMethodParameter())
        }

        public removeProperty(parameter): void {
            this.classMethodParameters = _.filter(this.classMethodParameters,
                (p) => p != parameter)
        }

        public getDescription() {
            var visibility = this.Visibility == Visibility.Public ? "+" : "-"
            var parameters = _.map(this.classMethodParameters,
                                  (p: ClassMethodParameter): string => p.getDescription())

            return `${visibility} ${this.name}(${parameters.join(', ')}) : ${this.returnType}`
        }
    }
}
