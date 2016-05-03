module Directives {

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
        backToDiagram : () => void
        removeRelation: (i) => void
        backToList: () => void
        content: Object
        currentActor: any
        currentUseCase: any
        editSelectedActor: (uc) => void
        editSelectedUseCase: (uc) => void
        editingRelations: boolean
        editRelations: () => void
        getRelationEntityOptions: (i) => any
        newActor: () => void
        newUseCase: () => void
        isDiagramVisible: () => boolean
        pendingRequests: number
        relationsOnEdit: Array<Object>
        removeSelectedActor: (actor) => void
        removeSelectedUseCase: (uc) => void
        save: () => void
        saveActor: (data: Models.Actor) => void
        saveUseCase: (data: Models.UseCase) => void
        saveRelations : () => void
        selectedActor: any
        selectEntity : any
        selectedUseCase: any
        selectUseCase: (id) => void
        selectActor: (id) => void
        utility: utility
        useCaseDiagram: Models.UseCaseDiagram
        $digest: any
        $watch : any
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
                $scope.relationsOnEdit = []

                this.LoadPackagesOptions(PackageResource, $scope)

                $scope.utility = <utility>{}
                $scope.utility.contentContainsLocale =
                    (i) => $scope.content &&
                        $scope.content[i] &&
                        $scope.content[i].name

                $scope.editRelations = () => {
                    window.location.href = "#/diagram/relations"
                    $scope.editingRelations = true
                    $scope.relationsOnEdit = []

                    _.each($scope.useCaseDiagram.relations, (relation) => {
                        var clone = {}
                        for (var property in relation) {
                            clone[property] = relation[property]
                        }
                        $scope.relationsOnEdit.push(clone)
                    })
                }

                $scope.getRelationEntityOptions = (relation) => {
                    return _.union($scope.useCaseDiagram.useCases,
                        $scope.useCaseDiagram.actors);
                }

                $scope.backToList = () => {
                    $scope.useCaseDiagram = null
                    window.location.href = "#"
                }

                $scope.isDiagramVisible = () => {
                    return !$scope.currentUseCase &&
                        !!$scope.currentActor && !$scope.editingRelations;
                }

                $scope.addRelation = () => {
                    $scope.relationsOnEdit.push({})
                }

                $scope.removeRelation = (relation) => {
                    $scope.relationsOnEdit = _.filter($scope.relationsOnEdit,
                        (r) => r != relation)
                }

                function removeRelationFromDiagram(relation: Models.UseCaseRelationship) {
                    $scope.useCaseDiagram.relations = _.filter(
                        $scope.useCaseDiagram.relations,
                        (r) => r != relation)
                    relation.cell.remove()
                }

                function redrawRelations() {
                    _.each($scope.useCaseDiagram.relations, (relation: Models.UseCaseRelationship) => {
                        var cell = Views.UseCaseDiagram.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $timeout((): void => { graph.addCell(cell) })
                    })
                }

                $scope.saveRelations = () => {
                    if (!graph) return

                    _.each($scope.useCaseDiagram.relations, (relation: Models.UseCaseRelationship) => {
                        if (relation.cell != null) { removeRelationFromDiagram(relation) }
                    })

                    $scope.useCaseDiagram.relations = []

                    _.each($scope.relationsOnEdit, (relation: Models.UseCaseRelationship) => {
                        var cell = Views.UseCaseDiagram.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $scope.useCaseDiagram.relations.push(relation)
                        $timeout((): void => { graph.addCell(cell) })
                    })

                    $scope.relationsOnEdit = []
                    $scope.backToDiagram();
                }

                $scope.selectEntity = (id) => {
                    if (_.any($scope.useCaseDiagram, (uc) => uc.id == id))
                        $scope.selectedUseCase(id)
                    else
                        $scope.selectedActor(id)
                }

                $scope.selectActor = (id) => {
                    var actorToBeSelected = _.find($scope.useCaseDiagram.actors,
                        (c) => c.cell.id == id);
                    if (!actorToBeSelected)
                        return;
                    $scope.selectedActor = actorToBeSelected
                    $scope.$digest()
                }

                $scope.selectUseCase = (id) => {
                    var useCaseToBeSelected = _.find($scope.useCaseDiagram.useCases,
                        (c) => c.cell.id == id);
                    if (!useCaseToBeSelected)
                        return;
                    $scope.selectedUseCase = useCaseToBeSelected
                    $scope.$digest()
                }

                function removeUseCase(useCase) {
                    $scope.useCaseDiagram.useCases = _.filter(
                        $scope.useCaseDiagram.useCases,
                        (c) => c != useCase)
                    useCase.cell.remove()
                }

                function removeActor(actor) {
                    $scope.useCaseDiagram.actors = _.filter(
                        $scope.useCaseDiagram.actors,
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
                }

                $scope.editSelectedActor = () => {
                    window.location.href = "#/diagram/actorForm"
                    $scope.currentActor = $scope.selectedActor
                }

                $scope.backToDiagram = () => {
                    $scope.currentUseCase = null
                    $scope.currentActor = null
                    $scope.editingRelations = false
                    window.location.href = "#/diagram"
                }

                $scope.save = () => {
                    $scope.pendingRequests++

                    var contents = _.chain($scope.content)
                        .filter(i => i.name)
                        .value()

                    $scope.useCaseDiagram.contents = contents

                    var promise = $scope.useCaseDiagram.id ?
                        UseCaseDiagramResource.update($scope.useCaseDiagram).$promise :
                        UseCaseDiagramResource.save($scope.useCaseDiagram).$promise

                    promise.then((): void => {
                        Notification.notifySuccess(Sentences.classDiagramSavedSuccessfully);
                        if ($scope.afterSave) { $scope.afterSave() }
                        window.location.href = "#"
                        $scope.useCaseDiagram = null
                    })
                        .catch((err): void => {
                            Notification.notifyError(Sentences.errorSavingClassDiagram, err.data.messages)
                        })
                        .finally((): void => {
                            $scope.pendingRequests--
                        })
                }
                
                $scope.$watch('useCaseDiagram', (newValue: Models.UseCaseDiagram) => {

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
                            _.each(newValue.actors, (a) => {
                                var cell = Views.UseCaseDiagram.buildActor(a)
                                a.cell = cell
                                graph.addCell(cell)
                            })
                            _.each(newValue.useCases, (uc) => {
                                var cell = Views.UseCaseDiagram.buildUseCase(uc)
                                uc.cell = cell
                                graph.addCell(cell)
                            })
                            entitiesDefer.resolve()
                        })
                        return entitiesDefer.promise
                    }

                    var drawRelations = () => {
                        $timeout((): void => {
                            _.each(newValue.relations, (r) => {
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
                    $scope.currentUseCase = new Models.UseCase()
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
                    $scope.useCaseDiagram.useCases.push($scope.currentUseCase)

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
                    $scope.useCaseDiagram.actors.push($scope.currentActor)

                    $scope.currentActor = null
                    $timeout((): void => {
                        graph.addCell(cell)
                        redrawRelations()
                    })

                    $scope.selectedActor = null
                }
            }]
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/management'
        public static Factory() {
            return new GsdUseCaseDiagram();
        }
    }
    app.directive('gsdUseCaseDiagram', GsdUseCaseDiagram.Factory)
}