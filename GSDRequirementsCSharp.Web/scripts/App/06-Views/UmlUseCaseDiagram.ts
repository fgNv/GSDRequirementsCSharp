module Views {

    var actorSvg =
        `<g>
          <circle cx="45" cy="25" r="15" style="fill:none;stroke:black;stroke-width:2;" />
          <line x1="45" y1="40" x2="45" y2="80" style="stroke:black;stroke-width:2;" />
          <line x1="15" y1="55" x2="75" y2="55" style="stroke:black;stroke-width:2;" />
          <line x1="45" y1="80" x2="15" y2="110" style="stroke:black;stroke-width:2;" />
          <line x1="45" y1="80" x2="75" y2="110" style="stroke:black;stroke-width:2;" /> 
          <text class="name"/>
        </g>`;

    var useCaseSvg =
        `<g>
          <ellipse cx="0" cy="0" rx="100" ry="60" 
                   style="fill='white';stroke:black;stroke-width:2;" />          
          <text class="name" />
        </g>`;

    declare var joint: any;
    declare var _: any;
    declare var $: any;
    declare var V: any;

    joint.shapes.uml.Actor = joint.shapes.basic.Generic.extend({
        markup: actorSvg,
        defaults: joint.util.deepSupplement({
            type: 'basic.Rect',
            attrs: {
                'rect': {
                    fill: '#FFFFFF',
                    stroke: 'black',
                    width: 100,
                    height: 60
                },
                'text': {
                    'font-size': 14,
                    text: '',
                    'ref-x': 45,
                    'ref-y': 130,
                    'y-alignment': 'middle',
                    'x-alignment': 'middle',
                    fill: 'black',
                    'font-family': 'Arial, helvetica, sans-serif'
                }
            }
        }, joint.shapes.basic.Generic.prototype.defaults)
    });

    joint.shapes.uml.UseCase = joint.shapes.basic.Generic.extend({
        markup: useCaseSvg,
        defaults: joint.util.deepSupplement({
            type: 'basic.Rect',
            attrs: {
                'rect': {
                    fill: '#FFFFFF',
                    stroke: 'black',
                    width: 100,
                    height: 60
                },
                'text': {
                    'font-size': 12,
                    text: '',
                    'y-alignment': 'middle',
                    'x-alignment': 'middle',
                    fill: 'black',
                    'font-family': 'Arial, helvetica, sans-serif'
                }
            }
        }, joint.shapes.basic.Generic.prototype.defaults)
    });

    export module UseCaseDiagram {

        function getPosition(element: Models.IDiagramElement) {
            var position = { x: element.x || 130, y: element.y || 90 };
            if (element.cell) {
                position = element.cell.attributes.position
            }
            return position;
        }

        export function buildActor(actor) {
            return new joint.shapes.uml.Actor({
                position: getPosition(actor),
                name: actor.name,
                attrs: {
                    '.name': {
                        text: actor.name
                    }
                }
            });
        }

        export function buildUseCase(useCase: Models.UseCase) {
            var name = joint.util.breakText(useCase.name, {
                width: 190
            });

            return new joint.shapes.uml.UseCase({
                position: getPosition(useCase),
                name: useCase.name,
                attrs: {
                    '.name': {
                        text: name
                    }
                }
            });
        }

        var paperElementId = ''
        var paperElementContainerId = ''

        export function startDiagram(
            cellClickCallback = null,
            paperElementIdArg = null,
            paperElementContainerIdArg = null) {

            if (paperElementIdArg) {
                paperElementId = paperElementIdArg
            } else {
                paperElementId = 'useCaseDiagramPaper'
            }
            if (paperElementContainerIdArg) {
                paperElementContainerId = paperElementContainerIdArg
            } else {
                var paperElementContainerId = 'useCaseDiagramPaperContainer'
            }

            var element = $(`#${paperElementId}`);
            if (element.length == 0) {
                $(`#${paperElementContainerId}`).append($(`<div id='${paperElementId}' />`))
                element = $(`#${paperElementId}`);
            }

            var graph = new joint.dia.Graph();

            var paper = new joint.dia.Paper({
                el: element,
                width: 1200,
                interactive: (cellView) => {
                    return { vertexAdd: false };
                },
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

        export function buildRelation(relationData: Models.UseCaseRelationship) {
            var sourceId = relationData.sourceId
            var targetId = relationData.targetId

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

            cell.addVertex = false

            if (!cell) return null
            if (!isSelfReference) return cell

            //@TODO manage self reference 

            return cell
        }
    }
}