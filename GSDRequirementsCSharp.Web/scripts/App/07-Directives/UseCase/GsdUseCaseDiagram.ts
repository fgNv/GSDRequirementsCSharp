﻿module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    interface utility {
        contentContainsLocale: (i) => boolean
    }

    interface GsdUseCaseDiagramScope {
        addRelation: () => void
        afterSave: () => void
        backToDiagram: () => void
        removeRelation: (i) => void
        backToList: () => void
        content: Object
        currentActor: any
        currentUseCase: any
        relationTypeOptions: Array<Object>
        editSelectedActor: (uc) => void
        editSelectedUseCase: (uc) => void
        editingRelations: boolean
        editRelations: () => void
        getRelationEntityOptions: (i) => any
        newActor: () => void
        newUseCase: () => void
        isDiagramVisible: () => boolean
        isUseCasesRelation: (r: Models.UseCaseRelationship) => boolean
        isUseCaseActorRelation: (r: Models.UseCaseRelationship) => boolean
        pendingRequests: number
        relationsOnEdit: Array<Object>
        removeSelectedActor: (actor) => void
        removeSelectedUseCase: (uc) => void
        save: () => void
        saveActor: (data: Models.Actor) => void
        saveUseCase: (data: Models.UseCase) => void
        saveRelations: () => void
        selectedActor: any
        selectEntity: any
        selectedUseCase: any
        selectUseCase: (id) => void
        selectActor: (id) => void
        utility: utility
        useCaseDiagram: Models.UseCaseDiagram
        $digest: any
        $watch: any
    }

    class GsdUseCaseDiagram {
        public scope = {
            'useCaseDiagram': '=useCaseDiagram',
            'afterSave': '=afterSave',
            'currentUseCase': '=currentUseCase',
            'currentActor': '=currentActor',
            'editingRelations': '=editingRelations'
        };
        private LoadPackagesOptions(packageResource, $scope): void {
            $scope.pendingRequests++
            packageResource.query()
                .$promise
                .then((response) => {
                    $scope.packagesOptions = _.map(response, (r) => new Models.Package(r))
                })
                .catch((err) => {
                    Notification.notifyError(Sentences.errorLoadingPackages, err.data.messages)
                })
                .finally((): void => {
                    $scope.pendingRequests--
                })
        }
        private initializeContentData($scope, initialData: Array<Models.ClassDiagramContent>) {
            $scope.contentData = {}
            $scope.contentData.locale = GSDRequirements.currentLocale
            $scope.locales = _.map(GSDRequirements.localesAvailable, l => l.name)

            $scope.content = {}

            _.each(GSDRequirements.localesAvailable, (l): void => {
                $scope.content[l.name] = {}

                var previousContent = null
                if (initialData)
                    previousContent = _.find(initialData, (d) => d.locale == l.name)

                $scope.content[l.name].name = previousContent ? previousContent.name : ''
                $scope.content[l.name].locale = l.name
            })
        }
        public controller = ['$timeout', '$scope', 'PackageResource', 'UseCaseDiagramResource',
            '$q',
            ($timeout, $scope: GsdUseCaseDiagramScope, PackageResource, UseCaseDiagramResource, $q) => {
                var graph = null;
                var paper = null;
 
                $scope.pendingRequests = 0
                $scope.currentActor = null
                $scope.currentUseCase = null
                $scope.editingRelations = false
                $scope.selectedActor = null
                $scope.selectedUseCase = null
                $scope.relationsOnEdit = new Models.UseCaseRelationsOnEdit()

                this.LoadPackagesOptions(PackageResource, $scope)

                $scope.relationTypeOptions =
                    _.filter(Globals.enumerateEnum(Models.UseCasesRelationType),
                        (o) => o.value != Models.UseCasesRelationType.association)

                $scope.utility = <utility>{}
                $scope.utility.contentContainsLocale =
                    (i) => $scope.content &&
                        $scope.content[i] &&
                        $scope.content[i].name

                $scope.editRelations = () => {
                    window.location.href = "#/diagram/relations"
                    $scope.editingRelations = true
                    $scope.relationsOnEdit = new Models.UseCaseRelationsOnEdit()

                    _.each($scope.useCaseDiagram.relations, (relation) => {
                        var clone = {}
                        for (var property in relation) {
                            clone[property] = relation[property]
                        }
                        $scope.relationsOnEdit.push(clone)
                    })
                }

                $scope.getRelationEntityOptions = (relation) => {
                    return $scope.useCaseDiagram.entities;
                }

                $scope.backToList = () => {
                    $scope.useCaseDiagram = null
                    window.location.href = "#"
                }

                $scope.isDiagramVisible = () => {
                    return !$scope.currentUseCase &&
                        !$scope.currentActor && !$scope.editingRelations;
                }

                $scope.addRelation = () => {
                    $scope.relationsOnEdit.push(new Models.UseCaseRelationship())
                }

                $scope.removeRelation = (relation) => {
                    var entities = _.filter($scope.relationsOnEdit,
                        (r) => r != relation)
                    $scope.relationsOnEdit = new Models.UseCaseRelationsOnEdit(entities)
                }

                function removeRelationFromDiagram(relation: Models.UseCaseRelationship) {
                    $scope.useCaseDiagram.relations = _.filter(
                        $scope.useCaseDiagram.relations,
                        (r) => r != relation)
                    relation.cell.remove()
                }

                function redrawRelations() {
                    Views.removeAllLinks()

                    _.each($scope.useCaseDiagram.relations, (relation: Models.UseCaseRelationship) => {
                        var cell = Views.UseCaseDiagram.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $timeout((): void => { graph.addCell(cell) })
                    })
                }

                $scope.isUseCaseActorRelation = (r) => {

                    if (!r) {
                        return false
                    }
                    var source = <Models.IUseCaseEntity>_.find($scope.useCaseDiagram.entities,
                        (e) => e.cell.id == r.sourceId)
                    var target = <Models.IUseCaseEntity>_.find($scope.useCaseDiagram.entities,
                        (e) => e.cell.id == r.targetId)

                    if (!source || !target) {
                        return false;
                    }

                    var isUseCaseActorRelation = source.getType() == Models.UseCaseEntityType.useCase &&
                        target.getType() == Models.UseCaseEntityType.actor ||
                        source.getType() == Models.UseCaseEntityType.actor &&
                        target.getType() == Models.UseCaseEntityType.useCase

                    if (isUseCaseActorRelation) {
                        r.type = Models.UseCasesRelationType.association
                    }

                    return isUseCaseActorRelation
                }

                $scope.isUseCasesRelation = (r) => {
                    if (!r) {
                        return false
                    }
                    var source = <Models.IUseCaseEntity>_.find($scope.useCaseDiagram.entities,
                        (e) => e.cell.id == r.sourceId)
                    var target = <Models.IUseCaseEntity>_.find($scope.useCaseDiagram.entities,
                        (e) => e.cell.id == r.targetId)

                    if (!source || !target) {
                        return false;
                    }

                    return source.getType() == Models.UseCaseEntityType.useCase &&
                        target.getType() == Models.UseCaseEntityType.useCase
                }

                function determineRelationType(relation: Models.UseCaseRelationship, useCaseDiagram: Models.UseCaseDiagram) {
                    var source = <Models.IUseCaseEntity>_.find(useCaseDiagram.entities,
                        e => e.cell.id == relation.sourceId)
                    var target = <Models.IUseCaseEntity>_.find(useCaseDiagram.entities,
                        e => e.cell.id == relation.targetId)

                    if (source.getType() == Models.UseCaseEntityType.actor && target.getType() == Models.UseCaseEntityType.actor) {
                        relation.type = Models.UseCasesRelationType.generalization
                    } else if (source.getType() != Models.UseCaseEntityType.useCase || target.getType() != Models.UseCaseEntityType.useCase) {
                        relation.type = Models.UseCasesRelationType.association
                    }
                }

                $scope.saveRelations = () => {
                    if (!graph) return

                    _.each($scope.useCaseDiagram.relations, (relation: Models.UseCaseRelationship) => {
                        if (relation.cell != null) { removeRelationFromDiagram(relation) }
                    })


                    $scope.useCaseDiagram.relations = []

                    _.each($scope.relationsOnEdit, (relation: Models.UseCaseRelationship) => {
                        determineRelationType(relation, $scope.useCaseDiagram)

                        var cell = Views.UseCaseDiagram.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $scope.useCaseDiagram.relations.push(relation)
                        $timeout((): void => { graph.addCell(cell) })
                    })

                    $scope.relationsOnEdit = new Models.UseCaseRelationsOnEdit()
                    $scope.backToDiagram();
                }

                $scope.selectEntity = (id) => {
                    var entity = <Models.IUseCaseEntity>_.find($scope.useCaseDiagram.entities,
                        (e) => e.cell.id == id)

                    if (!entity) {
                        return
                    }

                    $scope.selectedActor = null
                    $scope.selectedUseCase = null

                    if (entity.getType() == Models.UseCaseEntityType.actor) {
                        $scope.selectActor(id)
                    } else if (entity.getType() == Models.UseCaseEntityType.useCase) {
                        $scope.selectUseCase(id)
                    }
                }

                $scope.selectActor = (id) => {
                    var actorToBeSelected = _.find($scope.useCaseDiagram.entities,
                        (c) => c.cell.id == id);

                    if (!actorToBeSelected)
                        return;
                    $scope.selectedActor = actorToBeSelected
                    $scope.$digest()
                }

                $scope.selectUseCase = (id) => {
                    var useCaseToBeSelected = _.find($scope.useCaseDiagram.entities,
                        (c) => c.cell.id == id);

                    if (!useCaseToBeSelected)
                        return;
                    $scope.selectedUseCase = useCaseToBeSelected
                    $scope.$digest()
                }

                function removeUseCase(useCase) {
                    $scope.useCaseDiagram.entities = _.filter(
                        $scope.useCaseDiagram.entities,
                        (c) => c != useCase)
                    useCase.cell.remove()
                }

                function removeActor(actor) {
                    $scope.useCaseDiagram.entities = _.filter(
                        $scope.useCaseDiagram.entities,
                        (c) => c != actor)
                    actor.cell.remove()
                }

                $scope.removeSelectedActor = () => {
                    removeActor($scope.selectedActor)
                    $scope.selectedActor = null
                }

                $scope.removeSelectedUseCase = () => {
                    removeUseCase($scope.selectedUseCase)
                    $scope.selectedUseCase = null
                }

                $scope.editSelectedUseCase = () => {
                    window.location.href = "#/diagram/useCaseForm"
                    $scope.currentUseCase = $scope.selectedUseCase
                    $scope.selectedUseCase = null
                    Views.UseCaseDiagram.removeSelections(graph, paper)
                }

                $scope.editSelectedActor = () => {
                    window.location.href = "#/diagram/actorForm"
                    $scope.currentActor = $scope.selectedActor
                    $scope.selectedActor = null
                    Views.UseCaseDiagram.removeSelections(graph, paper)
                }

                $scope.backToDiagram = () => {
                    $scope.currentUseCase = null
                    $scope.currentActor = null
                    $scope.editingRelations = false
                    window.location.href = "#/diagram"
                }

                $scope.save = () => {

                    var contents = _.chain($scope.content)
                        .filter(i => i.name)
                        .value()

                    $scope.useCaseDiagram.contents = contents

                    $scope.useCaseDiagram.actors = _.chain($scope.useCaseDiagram.entities)
                        .filter((e: Models.IUseCaseEntity) =>
                            e.getType() == Models.UseCaseEntityType.actor)
                        .each((e: Models.Actor): void => { e.populateContents() })
                        .value()

                    $scope.useCaseDiagram.useCases = _.chain($scope.useCaseDiagram.entities)
                        .filter((e: Models.IUseCaseEntity) =>
                            e.getType() == Models.UseCaseEntityType.useCase)
                        .each((e: Models.UseCase): void => { e.populateContents() })
                        .value()

                    _.each($scope.useCaseDiagram.relations,
                        (r: Models.UseCaseRelationship): void => { r.populateContents() })

                    $scope.useCaseDiagram.useCasesRelations = _.filter(
                        $scope.useCaseDiagram.relations,
                        (e: Models.UseCaseRelationship) => e.isUseCasesRelation($scope.useCaseDiagram))

                    $scope.useCaseDiagram.entitiesRelations = _.filter(
                        $scope.useCaseDiagram.relations,
                        (e: Models.UseCaseRelationship) => !e.isUseCasesRelation($scope.useCaseDiagram))

                    $scope.pendingRequests++

                    var promise = $scope.useCaseDiagram.id ?
                        UseCaseDiagramResource.update($scope.useCaseDiagram).$promise :
                        UseCaseDiagramResource.save($scope.useCaseDiagram).$promise

                    promise.then((): void => {
                        Notification.notifySuccess(Sentences.classDiagramSavedSuccessfully);
                        if ($scope.afterSave) { $scope.afterSave() }
                        window.location.href = "#"
                        $scope.useCaseDiagram = null
                    }).catch((err): void => {
                        Notification.notifyError(Sentences.errorSavingClassDiagram, err.data.messages)
                    }).finally((): void => {
                        $scope.pendingRequests--
                    })
                }

                $scope.$watch('useCaseDiagram', (newValue: Models.UseCaseDiagram) => {
                    $scope.currentUseCase = null
                    if (graph) {
                        graph.clear()
                        paper.remove()
                    }

                    if (!newValue) {
                        graph = null
                        paper = null
                        return;
                    }

                    this.initializeContentData($scope, newValue.contents)

                    var paperDefer = $q.defer()
                    var entitiesDefer = $q.defer()

                    var drawEntities = () => {
                        $timeout((): void => {
                            _.each(newValue.entities, (a: Models.IUseCaseEntity) => {
                                var cell = null

                                switch (a.getType()) {
                                    case Models.UseCaseEntityType.actor:
                                        cell = Views.UseCaseDiagram.buildActor(a as Models.Actor)
                                        break
                                    case Models.UseCaseEntityType.useCase:
                                        cell = Views.UseCaseDiagram.buildUseCase(a as Models.UseCase)
                                        break
                                    default:
                                        return
                                }

                                a.cell = cell
                                graph.addCell(cell)
                            })
                            entitiesDefer.resolve()
                        })
                        return entitiesDefer.promise
                    }

                    var drawRelations = () => {
                        $timeout((): void => {
                            _.each(newValue.relations, (r) => {
                                determineRelationType(r, $scope.useCaseDiagram)
                                var cell = Views.UseCaseDiagram.buildRelation(r)
                                r.cell = cell
                                graph.addCell(cell)
                            })
                        })
                    }

                    var drawPaper = () => {
                        $timeout((): void => {
                            var cellClickCallback = (cellView): void => {
                                $scope.selectEntity(cellView.model.id)
                            }

                            var result = Views.UseCaseDiagram.startDiagram(cellClickCallback)
                            graph = result.graph
                            paper = result.paper

                            paperDefer.resolve()
                        })

                        return paperDefer.promise
                    }

                    drawPaper()
                        .then(drawEntities())
                        .then(drawRelations())
                })

                $scope.newUseCase = () => {

                    window.location.href = "#/diagram/formUseCase"
                    $scope.selectedUseCase = null
                    if ($scope.useCaseDiagram) {
                        $scope.currentUseCase = new Models.UseCase()
                    }
                }

                $scope.newActor = () => {
                    window.location.href = "#/diagram/formActor"
                    $scope.selectedActor = null
                    $scope.currentActor = new Models.Actor()
                }

                $scope.saveUseCase = (data: Models.UseCase) => {
                    if (!graph) return
                    if (data.cell != null) { removeUseCase(data) }
                    var cell = Views.UseCaseDiagram.buildUseCase(data)
                    if (!cell) return
                    $scope.currentUseCase.cell = cell
                    $scope.useCaseDiagram.entities.push($scope.currentUseCase)

                    $scope.currentUseCase = null
                    $timeout((): void => {
                        graph.addCell(cell)
                        redrawRelations()
                    })

                    $scope.selectedUseCase = null
                }

                $scope.saveActor = (data: Models.Actor) => {
                    if (!graph) return
                    if (data.cell != null) { removeActor(data) }
                    var cell = Views.UseCaseDiagram.buildActor(data)
                    if (!cell) return

                    $scope.currentActor.cell = cell
                    $scope.useCaseDiagram.entities.push($scope.currentActor)

                    $scope.currentActor = null
                    $timeout((): void => {
                        graph.addCell(cell)
                        redrawRelations()
                    })

                    $scope.backToDiagram()
                }
            }]
        public templateUrl = GSDRequirements.baseUrl + 'useCaseDiagram/management'
        public static Factory() {
            return new GsdUseCaseDiagram();
        }
    }
    app.directive('gsdUseCaseDiagram', GsdUseCaseDiagram.Factory)
}