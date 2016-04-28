﻿module Views {

    declare var joint: any;
    declare var _: any;
    declare var $: any;
    declare var V: any;
    
    export function buildClass(data: Models.ClassData) {
        switch (data.type) {
            case Models.ClassType.Abstract:
                return buildAbstractClass(data)
            case Models.ClassType.Concrete:
                return buildConcreteClass(data)
            case Models.ClassType.Interface:
                return buildInterface(data)
        }
        return null
    }

    function buildConcreteClass(classData: Models.ClassData) {
        var height = (classData.classProperties.length + classData.classMethods.length) * 33;
        height += 30

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
        height += 40

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

    function buildAbstractClass(classData: Models.ClassData) {
        var height = (classData.classProperties.length + classData.classMethods.length) * 33;
        height += 40

        return new joint.shapes.uml.Abstract({
            position: { x: 80, y: 80 },
            size: { width: 260, height: height },
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
    
    var paperElementId = '#classDiagramPaper'

    export function buildRelation(relationData: Models.ClassRelationship) {
        var sourceId = relationData.source.cell.id
        var targetId = relationData.target.cell.id

        var isSelfReference = sourceId == targetId
        
        var cell = null

        switch (relationData.type) {
            case Models.RelationType.Aggregation:
                cell = new joint.shapes.uml.Aggregation({
                    source: { id: sourceId },
                    target: { id: targetId }
                })
                break;
            case Models.RelationType.Association:
                cell = new joint.shapes.uml.Association({
                    source: { id: sourceId },
                    target: { id: targetId }
                })
                break;
            case Models.RelationType.Composition:
                return new joint.shapes.uml.Composition({
                    source: { id: sourceId },
                    target: { id: targetId }
                })
                break;
            case Models.RelationType.Inheritance:
                cell = new joint.shapes.uml.Generalization({
                    source: { id: sourceId },
                    target: { id: targetId }
                })
                break;
            case Models.RelationType.Realization:
                cell = new joint.shapes.uml.Implementation({
                    source: { id: sourceId },
                    target: { id: targetId }
                })
                break;
        }

        if (!cell) return null
        if (!isSelfReference) return cell
        
        //@TODO manage self reference 

        return cell
    }

    export function startClassDiagram(cellClickCallback = null) {
        var element = $(paperElementId);
        if (element.length == 0) {
            $("#classDiagramPaperContainer").append($("<div id='classDiagramPaper' />"))
            var element = $(paperElementId);
        }

        var graph = new joint.dia.Graph();

        var paper = new joint.dia.Paper({
            el: element,
            width: 1200,
            height: 700,
            gridSize: 1,
            model: graph
        });
        
        paper.on('cell:pointerclick', (cellView) => {
            _.each(graph.getElements(), function (el) {
                var vectorized = V(paper.findViewByModel(el).el);
                if (vectorized.hasClass("selectedCell")) {
                    vectorized.removeClass("selectedCell")
                }
            })

            V(cellView.el).addClass('selectedCell')
            if (cellClickCallback) {
                cellClickCallback(cellView)
            }
        })

        return { graph: graph, paper: paper };
    }
}