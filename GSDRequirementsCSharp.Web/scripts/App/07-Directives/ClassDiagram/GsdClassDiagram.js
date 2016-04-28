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
                    $scope.relationsOnEdit = [];
                    $scope.editRelations = function () {
                        window.location.href = "#/diagram/relations";
                        $scope.editingRelations = true;
                        $scope.relationsOnEdit = [];
                        _.each($scope.relations, function (relation) {
                            var clone = {};
                            for (var property in relation) {
                                clone[property] = relation[property];
                            }
                            $scope.relationsOnEdit.push(clone);
                        });
                    };
                    $scope.getClassOptions = function (relation) {
                        return $scope.classes;
                    };
                    $scope.isDiagramVisible = function () {
                        return !$scope.currentClass && !$scope.editingRelations;
                    };
                    $scope.addRelation = function () {
                        $scope.relationsOnEdit.push({});
                    };
                    $scope.removeRelation = function (relation) {
                        $scope.relationsOnEdit = _.filter($scope.relationsOnEdit, function (r) { return r != relation; });
                    };
                    function removeRelationFromDiagram(relation) {
                        $scope.relations = _.filter($scope.relations, function (r) { return r != relation; });
                        relation.cell.remove();
                    }
                    $scope.saveRelations = function () {
                        if (!graph)
                            return;
                        $scope.relations = [];
                        _.each($scope.relationsOnEdit, function (relation) {
                            if (relation.cell != null) {
                                removeRelationFromDiagram(relation);
                            }
                            var cell = Views.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $scope.relations.push(relation);
                            $timeout(function () { graph.addCell(cell); });
                        });
                        $scope.relationsOnEdit = [];
                        $scope.backToDiagram();
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
                        $scope.editingRelations = false;
                        window.location.href = "#/diagram";
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
                            var result = Views.startClassDiagram(function (cellView) {
                                $scope.selectClass(cellView.model.id);
                            });
                            graph = result.graph;
                            paper = result.paper;
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
                        if (!graph)
                            return;
                        if (data.cell != null) {
                            removeClass(data);
                        }
                        var cell = Views.buildClass(data);
                        if (!cell)
                            return;
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
