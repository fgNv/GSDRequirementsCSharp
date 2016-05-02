var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdClassDiagram = (function () {
        function GsdClassDiagram() {
            this.scope = {
                'specificationItem': '=specificationItem'
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
                    function drawDiagram(classDiagram) {
                        if (graph) {
                            graph.clear();
                            paper.remove();
                        }
                        if (!classDiagram) {
                            graph = null;
                            paper = null;
                            return;
                        }
                        var paperDefer = $q.defer();
                        var classesDefer = $q.defer();
                        var drawClasses = function () {
                            $timeout(function () {
                                _.each(classDiagram.classes, function (c) {
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
                                _.each(classDiagram.relations, function (r) {
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
                                var result = Views.startClassDiagram(cellClickCallback, "classDiagramDisplayPaper", "classDiagramDisplayPaperContainer");
                                graph = result.graph;
                                paper = result.paper;
                                paperDefer.resolve();
                            });
                            return paperDefer.promise;
                        };
                        drawPaper().then(drawClasses()).then(drawRelations());
                    }
                    $scope.$watch('specificationItem', function (newValue, oldValue) {
                        if (!newValue || !newValue.id) {
                            return;
                        }
                        ClassDiagramResource.get({ 'id': newValue.id })
                            .$promise
                            .then(function (response) {
                            var classDiagram = new Models.ClassDiagram(response);
                            drawDiagram(classDiagram);
                        })
                            .catch(function (err) {
                            Notification.notifyError(Sentences.errorLoadingClassDiagrams, err.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
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