module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;

    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdClassDiagram {
        public scope = {
            'classDiagram': '=classDiagram'
        }
        public controller = ['$timeout', '$scope', 'ClassDiagramResource', '$q',
            ($timeout, $scope, ClassDiagramResource, $q) => {
                var graph = null;
                var paper = null;

                function redrawRelations() {
                    _.each($scope.classDiagram.relations, (relation: Models.ClassRelationship) => {
                        var cell = Views.buildRelation(relation)
                        if (!cell) return
                        relation.cell = cell
                        $timeout((): void => { graph.addCell(cell) })
                    })
                }

                $scope.$watch('classDiagram', (newValue: Models.ClassDiagram, oldValue) => {

                    if (graph) {
                        graph.clear()
                        paper.remove()
                    }

                    if (!newValue) {
                        graph = null
                        paper = null
                        return;
                    }
                    
                    var paperDefer = $q.defer()
                    var classesDefer = $q.defer()

                    var drawClasses = () => {
                        $timeout((): void => {
                            _.each(newValue.classes, (c) => {
                                var cell = Views.buildClass(c)
                                c.cell = cell
                                graph.addCell(cell)
                            })
                            classesDefer.resolve()
                        })
                        return classesDefer.promise
                    }

                    var drawRelations = () => {
                        $timeout((): void => {
                            _.each(newValue.relations, (r) => {
                                var cell = Views.buildRelation(r)
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

                            var result = Views.startClassDiagram(cellClickCallback)
                            graph = result.graph
                            paper = result.paper

                            paperDefer.resolve()
                        })

                        return paperDefer.promise
                    }

                    drawPaper()
                        .then(drawClasses())
                        .then(drawRelations())
                })
                
            }]
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/display'
        public static Factory() {
            return new GsdClassDiagram();
        }
    }
    app.directive('gsdClassDiagramDisplay', GsdClassDiagram.Factory)
}