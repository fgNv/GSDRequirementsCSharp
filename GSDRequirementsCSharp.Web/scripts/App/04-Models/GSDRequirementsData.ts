module Globals {
    export class GSDRequirementsData {
        public angularModuleName: string
        public canAddArtifacts: boolean
        public angularDependencies: string
        public baseUrl: string
        public currentLocale: string
        public localesAvailable: Array<Models.Locale>
        public currentProfile: Models.profile
    }
}