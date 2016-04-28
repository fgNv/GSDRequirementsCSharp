module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    class GsdClassDiagram {
        public scope = {
            'classDiagram': '=classDiagram',
            'afterSave': '=afterSave',
            'currentClass': '=currentClass',
            'editingRelations': '=editingRelations'
        };
        public controller = ['$timeout', '$scope', ($timeout, $scope) => {
            var graph = null;
            var paper = null;

            $scope.currentClass = null
            $scope.editingRelations = false
            $scope.selectedClass = null
            $scope.classes = []
            $scope.relations = []
            $scope.relationsOnEdit = []

            $scope.editRelations = () => {
                window.location.href = "#/diagram/relations"
                $scope.editingRelations = true
                $scope.relationsOnEdit = []

                _.each($scope.relations, (relation) => {
                    var clone = {}
                    for (var property in relation) {
                        clone[property] = relation[property]
                    }
                    $scope.relationsOnEdit.push(clone)
                })
            }

            $scope.getClassOptions = (relation) => {
                return $scope.classes;
            }

            $scope.isDiagramVisible = () => {
                return !$scope.currentClass && !$scope.editingRelations;
            }

            $scope.addRelation = () => {
                $scope.relationsOnEdit.push({})
            }

            $scope.removeRelation = (relation) => {
                $scope.relationsOnEdit = _.filter($scope.relationsOnEdit,
                                                  (r) => r != relation)
            }

            function removeRelationFromDiagram(relation: Models.ClassRelationship) {
                $scope.relations = _.filter($scope.relations,
                    (r) => r != relation)
                relation.cell.remove()
            }

            $scope.saveRelations = () => {    
                if (!graph) return
                            
                $scope.relations = []

                _.each($scope.relationsOnEdit, (relation: Models.ClassRelationship) => {
                    if (relation.cell != null) { removeRelationFromDiagram(relation) }
                    var cell = Views.buildRelation(relation)
                    if (!cell) return
                    relation.cell = cell
                    $scope.relations.push(relation)
                    $timeout((): void => { graph.addCell(cell) })
                })

                $scope.relationsOnEdit = []
                $scope.backToDiagram();
            }

            $scope.selectClass = (id) => {
                var classToBeSelected = _.find($scope.classes,
                    (c) => c.cell.id == id);
                if (!classToBeSelected)
                    return;
                $scope.selectedClass = classToBeSelected
                $scope.$digest()
            }

            function removeClass(classEntity) {
                $scope.classes = _.filter($scope.classes,
                    (c) => c != classEntity)
                classEntity.cell.remove()
            }

            $scope.removeSelectedClass = () => {
                removeClass($scope.selectedClass)
                $scope.selectedClass = null
            }

            $scope.editSelectedClass = () => {
                window.location.href = "#/diagram/form"
                $scope.currentClass = $scope.selectedClass
            }

            $scope.backToDiagram = () => {
                $scope.currentClass = null
                $scope.editingRelations = false
                window.location.href = "#/diagram"
            }
            
            $scope.$watch('classDiagram', (newValue, oldValue) => {
                $scope.classes = []
                $scope.relations = []
                if (graph) {
                    graph.clear()
                    paper.remove()
                }

                if (!newValue) {
                    graph = null
                    paper = null
                    return;
                }

                $timeout((): void => {
                    var result = Views.startClassDiagram((cellView): void => {
                        $scope.selectClass(cellView.model.id)
                    })
                    graph = result.graph
                    paper = result.paper
                })
            })

            $scope.classTypeOptions = Globals.enumerateEnum(Models.ClassType)
            $scope.relationTypeOptions = Globals.enumerateEnum(Models.RelationType)
            $scope.visibilityOptions = Globals.enumerateEnum(Models.Visibility)

            $scope.newClass = () => {
                window.location.href = "#/diagram/form"
                $scope.selectedClass = null
                $scope.currentClass = new Models.ClassData()
            }

            $scope.saveClass = (data: Models.ClassData) => {                
                if (!graph) return
                if (data.cell != null) { removeClass(data) }
                var cell = Views.buildClass(data)
                if (!cell) return
                $scope.currentClass.cell = cell
                $scope.classes.push($scope.currentClass)

                $scope.currentClass = null
                $timeout((): void => { graph.addCell(cell) })

                $scope.selectedClass = null
            }
        }]
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/management'
        public static Factory() {
            return new GsdClassDiagram();
        }
    }
    app.directive('gsdClassDiagram', GsdClassDiagram.Factory)
}