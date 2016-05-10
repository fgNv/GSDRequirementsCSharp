module Models {
    export interface IUseCaseEntity {
        getType: () => UseCaseEntityType
        cell : any
    }
}