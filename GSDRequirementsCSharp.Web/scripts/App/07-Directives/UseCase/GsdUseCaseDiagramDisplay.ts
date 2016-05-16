module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);


    class GsdUseCaseDiagramDisplay {
        public scope = {
            'specificationItem': '=specificationItem',
            'version': '=?'
        }
        public controller = ['$timeout', '$scope', 'UseCaseDiagramResource', '$q',
            ($timeout, $scope, UseCaseDiagramResource, $q) => {
                var graph = null;
                var paper = null;

                $scope.pendingRequests = 0

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

                function drawDiagram(useCaseDiagram) {
                    if (graph) {
                        graph.clear()
                        paper.remove()
                    }

                    if (!useCaseDiagram) {
                        graph = null
                        paper = null
                        return;
                    }
                    
                    var paperDefer = $q.defer()
                    var entitiesDefer = $q.defer()

                    var drawEntities = () => {
                        $timeout((): void => {
                            _.each(useCaseDiagram.entities, (a: Models.IUseCaseEntity) => {
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
                            _.each(useCaseDiagram.relations, (r) => {
                                determineRelationType(r, useCaseDiagram)
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

                            var result = Views.UseCaseDiagram.startDiagram(
                                cellClickCallback,
                                'useCaseDiagramDisplayPaper',
                                'useCaseDiagramDisplayPaperContainer'
                            )
                            graph = result.graph
                            paper = result.paper

                            paperDefer.resolve()
                        })

                        return paperDefer.promise
                    }

                    drawPaper()
                        .then(drawEntities())
                        .then(drawRelations())
                }

                $scope.$watch('specificationItem', (newValue: Models.UseCaseDiagram, oldValue) => {
                    if (!newValue || !newValue.id) {
                        return
                    }

                    $scope.pendingRequests++
                    UseCaseDiagramResource.get({ 'id': newValue.id, version: $scope.version })
                        .$promise
                        .then((response) => {
                            var useCaseDiagram = new Models.UseCaseDiagram(response)
                            if (!$scope.version)
                                $scope.version = useCaseDiagram.version
                            
                            drawDiagram(useCaseDiagram)
                        })
                        .catch((err) => {
                            Notification.notifyError(Sentences.errorLoadingUseCaseDiagram, err.data.messages)
                        })
                        .finally(() => {
                            $scope.pendingRequests--
                        });
                })

            }]
        public templateUrl = GSDRequirements.baseUrl + 'useCaseDiagram/display'
        public static Factory() {
            return new GsdUseCaseDiagramDisplay();
        }
    }
    app.directive('gsdUseCaseDiagramDisplay', GsdUseCaseDiagramDisplay.Factory)
}