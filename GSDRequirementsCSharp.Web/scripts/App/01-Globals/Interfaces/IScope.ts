module Globals {
    export interface IScope {
        pendingRequests: number
    }

    export interface IListScope extends IScope {
        currentPage: number
        maxPages: number
    }
}