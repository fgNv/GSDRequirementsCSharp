module Views {

    var actorSvg =
        `<g>
          <rect />
          <circle cx="45" cy="25" r="15" style="fill:none;stroke:black;stroke-width:2;" />
          <line x1="45" y1="40" x2="45" y2="80" style="stroke:black;stroke-width:2;" />
          <line x1="15" y1="55" x2="75" y2="55" style="stroke:black;stroke-width:2;" />
          <line x1="45" y1="80" x2="15" y2="110" style="stroke:black;stroke-width:2;" />
          <line x1="45" y1="80" x2="75" y2="110" style="stroke:black;stroke-width:2;" /> 
          <text class="name"/>
        </g>`;

    var useCaseSvg =
        `<g>
          <ellipse />          
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
                    stroke: 'white',
                    width: 90,
                    height: 145
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
                'ellipse': {
                    cx: 0,
                    cy: 0,
                    rx: 100,
                    ry: 60,
                    fill: '#FFFFFF',
                    stroke: 'black',
                    'stroke-width' : 2
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

        export function buildActor(actor: Models.Actor) {
            var newActor = new joint.shapes.uml.Actor({
                position: getPosition(actor),
                name: actor.getName(),
                attrs: {
                    '.name': {
                        text: actor.getName()
                    }
                }
            });

            if (actor.cell && actor.cell.id) {
                newActor.set('id', actor.cell.id)
            } else if (actor.id){
                newActor.set('id', actor.id)
            }

            return newActor;
        }

        export function buildUseCase(useCase: Models.UseCase) {
            var name = joint.util.breakText(useCase.getName(), {
                width: 190
            });

            var newUseCase = new joint.shapes.uml.UseCase({
                position: getPosition(useCase),
                name: useCase.getName(),
                attrs: {
                    '.name': {
                        text: `UC${useCase.identifier}. ${name}`
                    }
                }
            });

            if (useCase.cell && useCase.cell.id) {
                newUseCase.set('id', useCase.cell.id)
            } else if (useCase.id) {
                newUseCase.set('id', useCase.id)
            }

            return newUseCase;
        }

        var paperElementId = ''
        var paperElementContainerId = ''

        export function removeSelections(graph, paper) {
            _.each(graph.getElements(), function (el) {
                var vectorized = V(paper.findViewByModel(el).el);
                if (vectorized.hasClass("selectedCell")) {
                    vectorized.removeClass("selectedCell")
                }
            })
        }

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
                var cellViewVectorized = V(cellView.el);
                if (cellViewVectorized.hasClass('link')) {
                    return;
                }

                removeSelections(graph, paper)
                
                cellViewVectorized.addClass('selectedCell')
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
                case Models.UseCasesRelationType.association:
                    cell = new joint.shapes.uml.Association({
                        source: { id: sourceId },
                        target: { id: targetId },
                        labels: [
                            {
                                position: 0.5, attrs: {
                                    text: {
                                        text: relationData.getDescription()
                                    }
                                }
                            }
                        ]
                    })
                    break;
                case Models.UseCasesRelationType.extend:
                    cell = new joint.shapes.uml.Implementation({
                        source: { id: sourceId },
                        target: { id: targetId },
                        labels: [
                            { position: 0.5, attrs: { text: { text: "<<extend>>" } } }
                        ]
                    })
                    break;
                case Models.UseCasesRelationType.generalization:
                    return new joint.shapes.uml.Generalization({
                        source: { id: sourceId },
                        target: { id: targetId }
                    })
                case Models.UseCasesRelationType.include:
                    cell = new joint.shapes.uml.Implementation({
                        source: { id: sourceId },
                        target: { id: targetId },
                        labels: [
                            { position: 0.5, attrs: { text: { text: "<<include>>" } } }
                        ]
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