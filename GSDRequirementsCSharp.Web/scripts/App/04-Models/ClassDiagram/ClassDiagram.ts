﻿module Models {

    declare var _: any
    declare var GSDRequirements: Globals.GSDRequirementsData;

    export class ClassDiagram {
        public id: string
        public identifier: number
        public name: string
        public locale: string
        public classes: Array<ClassData>
        public relations: Array<ClassRelationship>
        public contents: Array<ClassDiagramContent>
        private defineContent() {
            var currentLocale = _.find(this.contents,
                (c) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale) {
                this.fillWithContent(currentLocale)
                return;
            }

            var enUsLocale = _.find(this.contents,
                (c) => c.locale == "en-US")
            if (enUsLocale) {
                this.fillWithContent(enUsLocale)
                return;
            }
            this.fillWithContent(this.contents[0])
        }
        private fillWithContent(content: ClassDiagramContent) {
            this.name = content.name
            this.locale = content.locale
        }
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            this.contents = _.map(data['contents'], (c) => new ClassDiagramContent(c))
            this.defineContent()
        }
    }
}