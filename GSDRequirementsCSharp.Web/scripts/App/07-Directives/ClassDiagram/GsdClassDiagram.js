var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdClassDiagram = (function () {
        function GsdClassDiagram() {
            this.scope = {
                'classDiagram': '=classDiagram',
                'afterSave': '=afterSave',
                'currentClass': '=currentClass',
                'editingRelations': '=editingRelations'
            };
            this.controller = ['$timeout', '$scope', function ($timeout, $scope) {
                    var graph = null;
                    var paper = null;
                    $scope.currentClass = null;
                    $scope.editingRelations = false;
                    $scope.selectedClass = null;
                    $scope.classes = [];
                    $scope.relations = [];
                    $scope.editRelations = function () {
                        window.location.href = "#/diagram/relations";
                        $scope.editingRelations = true;
                    };
                    $scope.getClassOptions = function (relation) {
                        return $scope.classes;
                    };
                    $scope.isDiagramVisible = function () {
                        return !$scope.currentClass && !$scope.editingRelations;
                    };
                    $scope.addRelation = function () {
                        $scope.relations.push({});
                    };
                    $scope.selectClass = function (id) {
                        var classToBeSelected = _.find($scope.classes, function (c) { return c.cell.id == id; });
                        if (!classToBeSelected)
                            return;
                        $scope.selectedClass = classToBeSelected;
                        $scope.$digest();
                    };
                    function removeClass(classEntity) {
                        $scope.classes = _.filter($scope.classes, function (c) { return c != classEntity; });
                        classEntity.cell.remove();
                    }
                    $scope.removeSelectedClass = function () {
                        removeClass($scope.selectedClass);
                        $scope.selectedClass = null;
                    };
                    $scope.editSelectedClass = function () {
                        window.location.href = "#/diagram/form";
                        $scope.currentClass = $scope.selectedClass;
                    };
                    $scope.backToDiagram = function () {
                        $scope.currentClass = null;
                        window.location.href = "#/diagram";
                    };
                    $scope.editSelectedClassRelations = function () {
                        $scope.classToEditRelations = $scope.selectedClass;
                        window.location.href = "#/diagram/relations";
                    };
                    $scope.$watch('classDiagram', function (newValue, oldValue) {
                        $scope.classes = [];
                        $scope.relations = [];
                        if (graph) {
                            graph.clear();
                            paper.remove();
                        }
                        if (!newValue) {
                            graph = null;
                            paper = null;
                            return;
                        }
                        $timeout(function () {
                            var result = Views.startClassDiagram();
                            graph = result.graph;
                            paper = result.paper;
                            paper.on('cell:pointerclick', function (cellView) {
                                _.each(graph.getElements(), function (el) {
                                    var vectorized = V(paper.findViewByModel(el).el);
                                    if (vectorized.hasClass("selectedCell")) {
                                        vectorized.removeClass("selectedCell");
                                    }
                                });
                                V(cellView.el).addClass('selectedCell');
                                $scope.selectClass(cellView.model.id);
                            });
                        });
                    });
                    $scope.classTypeOptions = Globals.enumerateEnum(Models.ClassType);
                    $scope.relationTypeOptions = Globals.enumerateEnum(Models.RelationType);
                    $scope.visibilityOptions = Globals.enumerateEnum(Models.Visibility);
                    $scope.newClass = function () {
                        window.location.href = "#/diagram/form";
                        $scope.selectedClass = null;
                        $scope.currentClass = new Models.ClassData();
                    };
                    $scope.saveClass = function (data) {
                        var cell = null;
                        var uml = joint.shapes.uml;
                        if (!graph)
                            return;
                        if (data.cell != null) {
                            removeClass(data);
                        }
                        switch (data.type) {
                            case Models.ClassType.Abstract:
                                cell = Views.buildAbstractClass(data);
                                break;
                            case Models.ClassType.Concrete:
                                cell = Views.buildConcreteClass(data);
                                break;
                            case Models.ClassType.Interface:
                                cell = Views.buildInterface(data);
                                break;
                        }
                        if (!cell)
                            return;
                        console.log('$scope.currentClass');
                        console.log($scope.currentClass);
                        $scope.currentClass.cell = cell;
                        $scope.classes.push($scope.currentClass);
                        $scope.currentClass = null;
                        $timeout(function () { graph.addCell(cell); });
                        $scope.selectedClass = null;
                    };
                }];
            this.templateUrl = GSDRequirements.baseUrl + 'classDiagram/management';
        }
        GsdClassDiagram.Factory = function () {
            return new GsdClassDiagram();
        };
        return GsdClassDiagram;
    })();
    app.directive('gsdClassDiagram', GsdClassDiagram.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdClassDiagram.js.map