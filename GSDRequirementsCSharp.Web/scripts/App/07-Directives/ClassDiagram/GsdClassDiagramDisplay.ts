module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdClassDiagram {
        public scope = {
            'specificationItem': '=specificationItem'
        }
        public controller = ['$timeout', '$scope', 'ClassDiagramResource', '$q',
            ($timeout, $scope, ClassDiagramResource, $q) => {
                var graph = null;
                var paper = null;

                function redrawRelations() {
                    _.each($scope.classDiagram.relations, (relation: Models.ClassRelationship) => {
                        var cell = Views.ClassDiagram.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $timeout((): void => { graph.addCell(cell) })
                    })
                }

                function drawDiagram(classDiagram) {

                    if (graph) {
                        graph.clear()
                        paper.remove()
                    }

                    if (!classDiagram) {
                        graph = null
                        paper = null
                        return;
                    }

                    var paperDefer = $q.defer()
                    var classesDefer = $q.defer()

                    var drawClasses = () => {
                        $timeout((): void => {
                            _.each(classDiagram.classes, (c) => {
                                var cell = Views.ClassDiagram.buildClass(c)
                                c.cell = cell
                                graph.addCell(cell)
                            })
                            classesDefer.resolve()
                        })
                        return classesDefer.promise
                    }

                    var drawRelations = () => {
                        $timeout((): void => {
                            _.each(classDiagram.relations, (r) => {
                                var cell = Views.ClassDiagram.buildRelation(r)
                                r.cell = cell
                                graph.addCell(cell)
                            })
                        })
                    }

                    var drawPaper = () => {
                        $timeout((): void => {
                            var cellClickCallback = (cellView): void => {
                                $scope.selectClass(cellView.model.id)
                            }

                            var result = Views.ClassDiagram.startClassDiagram(cellClickCallback,
                                "classDiagramDisplayPaper", "classDiagramDisplayPaperContainer")
                            graph = result.graph
                            paper = result.paper

                            paperDefer.resolve()
                        })

                        return paperDefer.promise
                    }

                    drawPaper().then(drawClasses()).then(drawRelations())
                }

                $scope.$watch('specificationItem', (newValue: Models.ClassDiagram, oldValue) => {
                    if (!newValue || !newValue.id) {
                        return
                    }

                    ClassDiagramResource.get({ 'id': newValue.id })
                        .$promise
                        .then((response) => {
                            var classDiagram = new Models.ClassDiagram(response)
                            drawDiagram(classDiagram)
                        })
                        .catch((err) => {
                            Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages)
                        })
                        .finally(() => {
                            $scope.pendingRequests--
                        });
                })
                
            }]
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/display'
        public static Factory() {
            return new GsdClassDiagram();
        }
    }
    app.directive('gsdClassDiagramDisplay', GsdClassDiagram.Factory)
}