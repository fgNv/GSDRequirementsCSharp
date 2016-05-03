var Views;
(function (Views) {
    var actorSvg = "<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" width=\"90\" height=\"140\">\n          <circle cx=\"45\" cy=\"25\" r=\"15\" style=\"fill:none;stroke:black;stroke-width:2;\" />\n          <line x1=\"45\" y1=\"40\" x2=\"45\" y2=\"80\" style=\"stroke:black;stroke-width:2;\" />\n          <line x1=\"15\" y1=\"55\" x2=\"75\" y2=\"55\" style=\"stroke:black;stroke-width:2;\" />\n          <line x1=\"45\" y1=\"80\" x2=\"15\" y2=\"110\" style=\"stroke:black;stroke-width:2;\" />\n          <line x1=\"45\" y1=\"80\" x2=\"75\" y2=\"110\" style=\"stroke:black;stroke-width:2;\" />          \n        </svg>";
    var UseCaseDiagram;
    (function (UseCaseDiagram) {
        function buildActor(actor) {
            throw "UseCaseDiagram.buildActor not implemented yet";
        }
        UseCaseDiagram.buildActor = buildActor;
        function buildUseCase(useCase) {
            throw "UseCaseDiagram.buildUseCase not implemented yet";
        }
        UseCaseDiagram.buildUseCase = buildUseCase;
        function startDiagram(cellClickCallback) {
            throw "UseCaseDiagram.startDiagram not implemented yet";
            return { graph: null, paper: null };
        }
        UseCaseDiagram.startDiagram = startDiagram;
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
                    break;
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
        UseCaseDiagram.buildRelation = buildRelation;
    })(UseCaseDiagram = Views.UseCaseDiagram || (Views.UseCaseDiagram = {}));
})(Views || (Views = {}));
//# sourceMappingURL=UmlUseCaseDiagram.js.map