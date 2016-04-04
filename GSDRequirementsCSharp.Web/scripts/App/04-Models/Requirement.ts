﻿module Models {

    import GSDRequirementsData = Globals.GSDRequirementsData
    declare var GSDRequirements: GSDRequirementsData;
    declare var _: any;

    export class Requirement {
        public condition: string
        public subject: string
        public action: string
        public locale: string
        public identifier: number
        public type: requirementType
        public requirementType: requirementType
        public difficulty: difficulty
        public prefix: string
        public packageId: string
        public package: Models.Package
        public id: string
        public description : string
        public requirementContents: Array<Models.RequirementContent>
        constructor(data: Object) {
            for (var prop in data) {
                this[prop] = data[prop]
            }
            this.package = new Models.Package(data['specificationItem']['package'])
            switch (this.type) {
                case requirementType.functional:
                    this.prefix = "FR"
                    break;
                case requirementType.nonFunction:
                    this.prefix = "NFR"
                    break;
            }
            this.defineContent()
            this.packageId = this.package.id
            this.requirementType = this.type
            this.description = `${this.condition || ""} ${this.subject || ""} ${this.action || ""}`;
        }
        private defineContent() {
            var currentLocale = _.find(this.requirementContents,
                (c) => c.locale == GSDRequirements.currentLocale)
            if (currentLocale) {
                this.fillWithContent(currentLocale)
                return;
            }
            var enUsLocale = _.find(this.requirementContents,
                (c) => c.locale == "en-US")
            if (enUsLocale) {
                this.fillWithContent(enUsLocale)
                return;
            }
            this.fillWithContent(this.requirementContents[0])
        }
        private fillWithContent(content: RequirementContent) {
            this.condition = content.condition
            this.subject = content.subject
            this.action = content.action
            this.locale = content.locale
        }
        public canAddTranslation() {
            return !_.any(this.requirementContents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
        public canEdit() {
            return _.any(this.requirementContents,
                (c) => c.locale == GSDRequirements.currentLocale)
        }
    }
}
