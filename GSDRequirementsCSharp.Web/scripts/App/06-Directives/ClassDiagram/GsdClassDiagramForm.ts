module Directives {

    import GSDRequirementsData = Globals.GSDRequirementsData
    
    declare var angular: any;
    declare var _: any;
    declare var joint: any;
    declare var $: any;
    declare var GSDRequirements: GSDRequirementsData;
    var app = angular.module(GSDRequirements.angularModuleName);

    var paperElementId = '#classDiagramPaper'

    function buildAbstract(classData: Models.ClassData) {
        var height = (classData.classProperties.length + classData.classMethods.length) * 33;

        return new joint.shapes.uml.Abstract({
            position: { x: 80, y: 80 },
            size: { width: 260, height: 100 },
            name: classData.name,
            attributes: _.map(classData.classProperties,
                (p: Models.ClassProperty): string => p.getDescription()),
            methods: _.map(classData.classMethods,
                (p: Models.ClassMethod) => p.getDescription()),
            attrs: {
                '.uml-class-name-rect': {
                    fill: '#68ddd5',
                    stroke: '#ffffff',
                    'stroke-width': 0.5
                },
                '.uml-class-attrs-rect, .uml-class-methods-rect': {
                    fill: '#9687fe',
                    stroke: '#fff',
                    'stroke-width': 0.5
                },
                '.uml-class-methods-text, .uml-class-attrs-text': {
                    fill: '#fff'
                }
            }
        })
    }

    function buildConcrete(classData: Models.ClassData) {
        var height = (classData.classProperties.length + classData.classMethods.length) * 33;

        return new joint.shapes.uml.Class({
            position: { x: 20, y: 20 },
            size: { width: 220, height: height },
            name: classData.name,
            attributes: _.map(classData.classProperties,
                (p: Models.ClassProperty): string => p.getDescription()),
            methods: _.map(classData.classMethods,
                (p: Models.ClassMethod) => p.getDescription()),
            attrs: {
                '.uml-class-name-rect': {
                    fill: '#ff8450',
                    stroke: '#fff',
                    'stroke-width': 0.5,
                },
                '.uml-class-attrs-rect, .uml-class-methods-rect': {
                    fill: '#fe976a',
                    stroke: '#fff',
                    'stroke-width': 0.5
                },
                '.uml-class-attrs-text': {
                    ref: '.uml-class-attrs-rect',
                    'ref-y': 0.5,
                    'y-alignment': 'middle'
                },
                '.uml-class-methods-text': {
                    ref: '.uml-class-methods-rect',
                    'ref-y': 0.5,
                    'y-alignment': 'middle'
                }
            }
        })
    }

    function buildInterface(classData: Models.ClassData) {
        var height = (classData.classProperties.length + classData.classMethods.length) * 33;

        return new joint.shapes.uml.Interface({
            position: { x: 50, y: 50 },
            size: { width: 280, height: height },
            name: classData.name,
            attributes: _.map(classData.classProperties,
                (p: Models.ClassProperty): string => p.getDescription()),
            methods: _.map(classData.classMethods,
                           (p: Models.ClassMethod) => p.getDescription()),
            attrs: {
                '.uml-class-name-rect': {
                    fill: '#feb662',
                    stroke: '#ffffff',
                    'stroke-width': 0.5
                },
                '.uml-class-attrs-rect, .uml-class-methods-rect': {
                    fill: '#fdc886',
                    stroke: '#fff',
                    'stroke-width': 0.5
                },
                '.uml-class-attrs-text': {
                    ref: '.uml-class-attrs-rect',
                    'ref-y': 0.5,
                    'y-alignment': 'middle'
                },
                '.uml-class-methods-text': {
                    ref: '.uml-class-methods-rect',
                    'ref-y': 0.5,
                    'y-alignment': 'middle'
                }
            }
        })
    }

    function startGraph() {
        var element = $(paperElementId);
        if (element.length == 0) {
            $("#classDiagramPaperContainer").append($("<div id='classDiagramPaper' />"))
            var element = $(paperElementId);
        }

        var graph = new joint.dia.Graph();

        var paper = new joint.dia.Paper({
            el: element,
            width: 800,
            height: 600,
            gridSize: 1,
            model: graph
        });

        var uml = joint.shapes.uml;

        var classes = {
            mammal: new uml.Interface({
                position: { x: 300, y: 50 },
                size: { width: 240, height: 100 },
                name: 'Mammal',
                attributes: ['dob: Date'],
                methods: ['+ setDateOfBirth(dob: Date): Void', '+ getAgeAsDays(): Numeric'],
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#feb662',
                        stroke: '#ffffff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#fdc886',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-text': {
                        ref: '.uml-class-attrs-rect',
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    },
                    '.uml-class-methods-text': {
                        ref: '.uml-class-methods-rect',
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    }
                }
            }),

            person: new uml.Abstract({
                position: { x: 300, y: 300 },
                size: { width: 260, height: 100 },
                name: 'Person',
                attributes: ['firstName: String', 'lastName: String'],
                methods: ['+ setName(first: String, last: String): Void', '+ getName(): String'],
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#68ddd5',
                        stroke: '#ffffff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#9687fe',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-methods-text, .uml-class-attrs-text': {
                        fill: '#fff'
                    }
                }
            }),

            bloodgroup: new uml.Class({
                position: { x: 20, y: 190 },
                size: { width: 220, height: 100 },
                name: 'BloodGroup',
                attributes: ['bloodGroup: String'],
                methods: ['+ isCompatible(bG: String): Boolean'],
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#ff8450',
                        stroke: '#fff',
                        'stroke-width': 0.5,
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#fe976a',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-text': {
                        ref: '.uml-class-attrs-rect',
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    },
                    '.uml-class-methods-text': {
                        ref: '.uml-class-methods-rect',
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    }
                }
            }),

            address: new uml.Class({
                position: { x: 630, y: 190 },
                size: { width: 160, height: 100 },
                name: 'Address',
                attributes: ['houseNumber: Integer', 'streetName: String', 'town: String', 'postcode: String'],
                methods: [],
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#ff8450',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#fe976a',
                        stroke: '#fff',
                        'stroke-width': 0.5,
                    },
                    '.uml-class-attrs-text': {
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    }
                },

            }),

            man: new uml.Class({
                position: { x: 200, y: 500 },
                size: { width: 180, height: 50 },
                name: 'Man',
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#ff8450',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#fe976a',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    }
                }
            }),

            woman: new uml.Class({
                position: { x: 450, y: 500 },
                size: { width: 180, height: 50 },
                name: 'Woman',
                methods: ['+ giveABrith(): Person []'],
                attrs: {
                    '.uml-class-name-rect': {
                        fill: '#ff8450',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-attrs-rect, .uml-class-methods-rect': {
                        fill: '#fe976a',
                        stroke: '#fff',
                        'stroke-width': 0.5
                    },
                    '.uml-class-methods-text': {
                        'ref-y': 0.5,
                        'y-alignment': 'middle'
                    }
                }
            })
        };

        _.each(classes, function (c) { graph.addCell(c); });

        var relations = [
            new uml.Generalization({
                source: { id: classes.man.id },
                target: { id: classes.person.id }
            }),
            new uml.Composition({
                source: { id: classes.woman.id },
                target: { id: classes.person.id }
            }),
            new uml.Implementation({
                source: { id: classes.person.id },
                target: { id: classes.mammal.id }
            }),
            new uml.Aggregation({
                source: { id: classes.person.id },
                target: { id: classes.address.id }
            }),
            new uml.Composition({
                source: { id: classes.person.id },
                target: { id: classes.bloodgroup.id }
            })
        ];

        _.each(relations, function (r) { graph.addCell(r); });

        return { graph: graph, paper: paper };
    }

    class GsdClassDiagramForm {
        public scope = {
            'classDiagram': '=classDiagram',
            'afterSave': '=afterSave'
        };
        public controller = ['$timeout', '$scope', ($timeout, $scope) => {
            var graph = null;
            var paper = null;
            $scope.classes = []
            $scope.relations = []

            $scope.$watch('classDiagram', (newValue, oldValue) => {
                $scope.classes = []
                $scope.relations = []
                if (graph) {
                    graph.clear()
                    paper.remove()
                }

                if (newValue) {
                    $timeout((): void => {
                        var result = startGraph()
                        graph = result.graph
                        paper = result.paper
                    })
                } else {
                    graph = null
                    paper = null
                }
            });

            $scope.removeClass = (classEntity) => {
                $scope.classes
            }

            $scope.addClass = (data: Models.ClassData) => {
                var cell = null
                var uml = joint.shapes.uml;

                switch (data.type) {
                    case Models.ClassType.Abstract:
                        break;
                    case Models.ClassType.Concrete:
                        cell = buildInterface(data)
                        break;
                    case Models.ClassType.Interface:
                        break;
                } 
            }
        }]
        public templateUrl = GSDRequirements.baseUrl + 'classDiagram/form'
        public static Factory() {
            return new GsdClassDiagramForm();
        }
    }
    app.directive('gsdClassDiagramForm', GsdClassDiagramForm.Factory)
}