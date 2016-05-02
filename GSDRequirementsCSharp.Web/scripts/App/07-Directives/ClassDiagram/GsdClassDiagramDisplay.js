var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdClassDiagram = (function () {
        function GsdClassDiagram() {
            this.scope = {
                'classDiagram': '=classDiagram'
            };
            this.controller = ['$timeout', '$scope', 'ClassDiagramResource', '$q',
                function ($timeout, $scope, ClassDiagramResource, $q) {
                    var graph = null;
                    var paper = null;
                    function redrawRelations() {
                        _.each($scope.classDiagram.relations, function (relation) {
                            var cell = Views.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $timeout(function () { graph.addCell(cell); });
                        });
                    }
                    $scope.$watch('classDiagram', function (newValue, oldValue) {
                        if (graph) {
                            graph.clear();
                            paper.remove();
                        }
                        if (!newValue) {
                            graph = null;
                            paper = null;
                            return;
                        }
                        var paperDefer = $q.defer();
                        var classesDefer = $q.defer();
                        var drawClasses = function () {
                            $timeout(function () {
                                _.each(newValue.classes, function (c) {
                                    var cell = Views.buildClass(c);
                                    c.cell = cell;
                                    graph.addCell(cell);
                                });
                                classesDefer.resolve();
                            });
                            return classesDefer.promise;
                        };
                        var drawRelations = function () {
                            $timeout(function () {
                                _.each(newValue.relations, function (r) {
                                    var cell = Views.buildRelation(r);
                                    r.cell = cell;
                                    graph.addCell(cell);
                                });
                            });
                        };
                        var drawPaper = function () {
                            $timeout(function () {
                                var cellClickCallback = function (cellView) {
                                    $scope.selectClass(cellView.model.id);
                                };
                                var result = Views.startClassDiagram(cellClickCallback);
                                graph = result.graph;
                                paper = result.paper;
                                paperDefer.resolve();
                            });
                            return paperDefer.promise;
                        };
                        drawPaper()
                            .then(drawClasses())
                            .then(drawRelations());
                    });
                }];
            this.templateUrl = GSDRequirements.baseUrl + 'classDiagram/display';
        }
        GsdClassDiagram.Factory = function () {
            return new GsdClassDiagram();
        };
        return GsdClassDiagram;
    })();
    app.directive('gsdClassDiagramDisplay', GsdClassDiagram.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdClassDiagramDisplay.js.map