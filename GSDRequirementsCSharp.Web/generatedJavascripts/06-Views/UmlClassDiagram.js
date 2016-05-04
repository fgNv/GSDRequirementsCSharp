var Views;
(function (Views) {
    var ClassDiagram;
    (function (ClassDiagram) {
        function buildClass(data) {
            var result = null;
            switch (data.type) {
                case Models.ClassType.Abstract:
                    result = buildAbstractClass(data);
                    break;
                case Models.ClassType.Concrete:
                    result = buildConcreteClass(data);
                    break;
                case Models.ClassType.Interface:
                    result = buildInterface(data);
                    break;
            }
            if (!result) {
                return null;
            }
            if (data.cell && data.cell.id) {
                result.set('id', data.cell.id);
            }
            else if (data.id) {
                result.set('id', data.id);
            }
            return result;
        }
        ClassDiagram.buildClass = buildClass;
        function getClassHeight(classData) {
            var height = (classData.classProperties.length + classData.classMethods.length) * 22;
            height += 60;
            return height;
        }
        function getPosition(classData) {
            var position = { x: classData.x || 10, y: classData.y || 10 };
            if (classData.cell) {
                position = classData.cell.attributes.position;
            }
            return position;
        }
        function buildConcreteClass(classData) {
            var height = getClassHeight(classData);
            return new joint.shapes.uml.Class({
                position: getPosition(classData),
                size: { width: 220, height: height },
                name: classData.name,
                attributes: _.map(classData.classProperties, function (p) { return p.getDescription(); }),
                methods: _.map(classData.classMethods, function (p) { return p.getDescription(); }),
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
            });
        }
        function buildInterface(classData) {
            var height = getClassHeight(classData);
            return new joint.shapes.uml.Interface({
                position: getPosition(classData),
                size: { width: 280, height: height },
                name: classData.name,
                attributes: _.map(classData.classProperties, function (p) { return p.getDescription(); }),
                methods: _.map(classData.classMethods, function (p) { return p.getDescription(); }),
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
            });
        }
        function buildAbstractClass(classData) {
            var height = getClassHeight(classData);
            return new joint.shapes.uml.Abstract({
                position: getPosition(classData),
                size: { width: 260, height: height },
                name: classData.name,
                attributes: _.map(classData.classProperties, function (p) { return p.getDescription(); }),
                methods: _.map(classData.classMethods, function (p) { return p.getDescription(); }),
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
            });
        }
        var paperElementId = '';
        var paperElementContainerId = '';
        function buildRelation(relationData) {
            var sourceId = relationData.sourceId;
            var targetId = relationData.targetId;
            var isSelfReference = sourceId == targetId;
            var cell = null;
            switch (relationData.type) {
                case Models.RelationType.Aggregation:
                    cell = new joint.shapes.uml.Aggregation({
                        source: { id: sourceId },
                        target: { id: targetId }
                    });
                    break;
                case Models.RelationType.Association:
                    cell = new joint.shapes.uml.Association({
                        source: { id: sourceId },
                        target: { id: targetId }
                    });
                    break;
                case Models.RelationType.Composition:
                    return new joint.shapes.uml.Composition({
                        source: { id: sourceId },
                        target: { id: targetId }
                    });
                case Models.RelationType.Inheritance:
                    cell = new joint.shapes.uml.Generalization({
                        source: { id: sourceId },
                        target: { id: targetId }
                    });
                    break;
                case Models.RelationType.Realization:
                    cell = new joint.shapes.uml.Implementation({
                        source: { id: sourceId },
                        target: { id: targetId }
                    });
                    break;
            }
            cell.addVertex = false;
            if (!cell)
                return null;
            if (!isSelfReference)
                return cell;
            //@TODO manage self reference 
            return cell;
        }
        ClassDiagram.buildRelation = buildRelation;
        function startClassDiagram(cellClickCallback, paperElementIdArg, paperElementContainerIdArg) {
            if (cellClickCallback === void 0) { cellClickCallback = null; }
            if (paperElementIdArg === void 0) { paperElementIdArg = null; }
            if (paperElementContainerIdArg === void 0) { paperElementContainerIdArg = null; }
            if (paperElementIdArg) {
                paperElementId = paperElementIdArg;
            }
            else {
                paperElementId = 'classDiagramPaper';
            }
            if (paperElementContainerIdArg) {
                paperElementContainerId = paperElementContainerIdArg;
            }
            else {
                var paperElementContainerId = 'classDiagramPaperContainer';
            }
            var element = $("#" + paperElementId);
            if (element.length == 0) {
                $("#" + paperElementContainerId).append($("<div id='" + paperElementId + "' />"));
                element = $("#" + paperElementId);
            }
            var graph = new joint.dia.Graph();
            var paper = new joint.dia.Paper({
                el: element,
                width: 1200,
                interactive: function (cellView) {
                    return { vertexAdd: false };
                },
                height: 700,
                gridSize: 1,
                model: graph
            });
            paper.on('cell:pointerclick', function (cellView) {
                _.each(graph.getElements(), function (el) {
                    var vectorized = V(paper.findViewByModel(el).el);
                    if (vectorized.hasClass("selectedCell")) {
                        vectorized.removeClass("selectedCell");
                    }
                });
                V(cellView.el).addClass('selectedCell');
                if (cellClickCallback) {
                    cellClickCallback(cellView);
                }
            });
            return { graph: graph, paper: paper };
        }
        ClassDiagram.startClassDiagram = startClassDiagram;
    })(ClassDiagram = Views.ClassDiagram || (Views.ClassDiagram = {}));
})(Views || (Views = {}));
//# sourceMappingURL=UmlClassDiagram.js.map