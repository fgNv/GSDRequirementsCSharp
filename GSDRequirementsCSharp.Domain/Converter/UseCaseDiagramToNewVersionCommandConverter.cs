using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.Converter;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.Converter
{
    public class UseCaseDiagramToNewVersionCommandConverter : IConverter<UseCaseDiagram, CreateUseCaseDiagramNewVersionCommand>
    {
        private readonly IQueryHandler<ActorsByDiagramQuery, IEnumerable<Actor>> _actorsByDiagramQueryHandler;
        private readonly IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> _useCasesByDiagramQueryHandler;

        public UseCaseDiagramToNewVersionCommandConverter(IQueryHandler<ActorsByDiagramQuery, IEnumerable<Actor>> actorsByDiagramQueryHandler,
                                                          IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> useCasesByDiagramQueryHandler)
        {
            _actorsByDiagramQueryHandler = actorsByDiagramQueryHandler;
            _useCasesByDiagramQueryHandler = useCasesByDiagramQueryHandler;
        }

        private PreConditionData FromModel(UseCasePreCondition preCondition)
        {
            return new PreConditionData
            {
                Contents = preCondition.Contents.Select(c =>
                new PreConditionContentItem
                {
                    Description = c.Description,
                    Locale = c.Locale
                })
            };
        }

        private PostConditionData FromModel(UseCasePostCondition postCondition)
        {
            return new PostConditionData
            {
                Contents = postCondition.Contents.Select(c =>
                new PostConditionContentItem
                {
                    Description = c.Description,
                    Locale = c.Locale
                })
            };
        }

        private UseCaseItem FromModel(UseCase useCase)
        {
            return new UseCaseItem
            {
                Cell = new Cell
                {
                    Id = useCase.Id,
                    Position = new Position
                    {
                        X = useCase.X,
                        Y = useCase.Y
                    }
                },
                Contents = useCase.Contents.Select(c =>                
                    new UseCaseContentItem
                    {
                        Description = c.Description,
                        Locale = c.Locale,
                        Name = c.Name,
                        Path = c.Path
                    }
                ),
                Identifier = useCase.Identifier,
                PostConditions = useCase.PostConditions.Select(FromModel),
                PreConditions = useCase.PreConditions.Select(FromModel)
            };
        }

        private ActorItem FromModel(Actor actor)
        {
            return new ActorItem
            {
                Cell = new Cell
                {
                    Id = actor.Id,
                    Position = new Position
                    {
                        X = actor.X,
                        Y = actor.Y
                    }
                },
                Contents = actor.Contents.Select(c => new ActorContentItem
                {
                    Locale = c.Locale,
                    Name = c.Name
                })
            };
        }

        public CreateUseCaseDiagramNewVersionCommand Convert(UseCaseDiagram input)
        {
            var result = new CreateUseCaseDiagramNewVersionCommand();

            var actorsQuery = new ActorsByDiagramQuery { DiagramId = input.Id, Version = input.Version };
            var actors = _actorsByDiagramQueryHandler.Handle(actorsQuery);

            var useCasesQuery = new UseCasesByDiagramQuery { DiagramId = input.Id, Version = input.Version};
            var useCases = _useCasesByDiagramQueryHandler.Handle(useCasesQuery);

            result.Actors = actors.Select(FromModel);
            result.UseCases = useCases.Select(FromModel);
            result.Contents = input.Contents.Select(c => new UseCaseDiagramContentItem
            {
                Locale = c.Locale,
                Name = c.Name
            });
            result.PackageId = input.SpecificationItem.PackageId;
            result.EntitiesRelations = input.EntitiesRelations.Select(r => new UseCaseEntitiesRelationItem
            {
                Contents = r.Contents.Select(c => new UseCaseEntitiesRelationContent
                {
                    Description = c.Description,
                    Locale = c.Locale
                }),
                SourceId = r.SourceId,
                TargetId = r.TargetId
            });
            result.Id = input.Id;
            result.UseCasesRelations = input.UseCasesRelations.Select(r => new UseCasesRelationItem
            {
                SourceId = r.SourceId,
                TargetId = r.TargetId,
                Type = r.Type
            });

            result.PackageId = input.SpecificationItem.PackageId;

            return result;
        }
    }
}
