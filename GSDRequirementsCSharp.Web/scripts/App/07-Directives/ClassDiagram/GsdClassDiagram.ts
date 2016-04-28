module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData

    declare var angular: any;
    declare var _: any;
    declare var joint: any;
    declare var $: any;
    declare var V: any;
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
            $scope.editRelations = () => {
                window.location.href = "#/diagram/relations"
                $scope.editingRelations = true
            }

            $scope.getClassOptions = (relation) => {
                return $scope.classes;
            }

            $scope.isDiagramVisible = () => {
                return !$scope.currentClass && !$scope.editingRelations;
            }

            $scope.addRelation = () => {
                $scope.relations.push({})
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
                window.location.href = "#/diagram"
            }

            $scope.editSelectedClassRelations = () => {
                $scope.classToEditRelations = $scope.selectedClass
                window.location.href = "#/diagram/relations"
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
                    var result = Views.startClassDiagram()
                    graph = result.graph
                    paper = result.paper

                    paper.on('cell:pointerclick', (cellView) => {
                        _.each(graph.getElements(), function (el) {
                            var vectorized = V(paper.findViewByModel(el).el);
                            if (vectorized.hasClass("selectedCell")) {
                                vectorized.removeClass("selectedCell")
                            }
                        })

                        V(cellView.el).addClass('selectedCell')
                        $scope.selectClass(cellView.model.id)
                    })
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
                var cell = null
                var uml = joint.shapes.uml
                
                if (!graph) return
                
                if (data.cell != null) {
                    removeClass(data)
                }

                switch (data.type) {
                    case Models.ClassType.Abstract:
                        cell = Views.buildAbstractClass(data)
                        break
                    case Models.ClassType.Concrete:
                        cell = Views.buildConcreteClass(data)
                        break
                    case Models.ClassType.Interface:
                        cell = Views.buildInterface(data)
                        break
                }

                if (!cell) return

                console.log('$scope.currentClass')
                console.log($scope.currentClass)
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