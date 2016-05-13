using GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Models.UseCases;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UseCaseDiagramItemsPersister
    {
        private readonly IRepository<UseCaseDiagramContent, LocaleKey> _useCaseDiagramContentRepository;
        private readonly IRepository<UseCase, Guid> _useCaseRepository;
        private readonly IRepository<UseCaseContent, LocaleKey> _useCaseContentRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<UseCaseEntity, Guid> _useCaseEntityRepository;
        private readonly IRepository<ActorContent, LocaleKey> _actorContentRepository;
        private readonly IRepository<UseCasesRelation, Guid> _useCasesRelationRepository;
        private readonly IRepository<UseCaseEntityRelation, Guid> _useCaseEntityRelationRepository;
        private readonly IRepository<UseCaseEntityRelationContent, LocaleKey> _useCaseEntityRelationContentRepository;
        private readonly IRepository<UseCasePreCondition, Guid> _preConditionRepository;
        private readonly IRepository<UseCasePreConditionContent, LocaleKey> _preConditionContentRepository;
        private readonly IRepository<UseCasePostCondition, Guid> _postConditionRepository;
        private readonly IRepository<UseCasePostConditionContent, LocaleKey> _postConditionContentRepository;
        private readonly IQueryHandler<UseCaseNextIdQuery, int> _useCaseNextIdQueryHandler;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public UseCaseDiagramItemsPersister(IRepository<UseCaseDiagramContent, LocaleKey> classDiagramContentRepository,
                                            IRepository<UseCase, Guid> useCaseRepository,
                                            IRepository<UseCaseContent, LocaleKey> useCaseContentRepository,
                                            IRepository<Actor, Guid> actorRepository,
                                            IRepository<UseCaseEntity, Guid> useCaseEntityRepository,
                                            IRepository<ActorContent, LocaleKey> actorContentRepository,
                                            IRepository<UseCasesRelation, Guid> useCasesRelationRepository,
                                            IRepository<UseCaseEntityRelation, Guid> useCaseEntityRelationRepository,
                                            IRepository<UseCaseEntityRelationContent, LocaleKey> useCaseEntityRelationContentRepository,
                                            ICurrentProjectContextId currentProjectContextId,
                                            IRepository<UseCasePreCondition, Guid> preConditionRepository,
                                            IRepository<UseCasePreConditionContent, LocaleKey> preConditionContentRepository,
                                            IRepository<UseCasePostCondition, Guid> postConditionRepository,
                                            IRepository<UseCasePostConditionContent, LocaleKey> postConditionContentRepository,
                                            IQueryHandler<UseCaseNextIdQuery, int> useCaseNextIdQueryHandler,
                                            IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _useCaseDiagramContentRepository = classDiagramContentRepository;
            _useCaseRepository = useCaseRepository;
            _useCaseContentRepository = useCaseContentRepository;
            _actorRepository = actorRepository;
            _actorContentRepository = actorContentRepository;
            _useCasesRelationRepository = useCasesRelationRepository;
            _useCaseEntityRelationRepository = useCaseEntityRelationRepository;
            _specificationItemRepository = specificationItemRepository;
            _useCaseEntityRelationContentRepository = useCaseEntityRelationContentRepository;
            _useCaseEntityRepository = useCaseEntityRepository;

            _currentProjectContextId = currentProjectContextId;

            _preConditionRepository = preConditionRepository;
            _preConditionContentRepository = preConditionContentRepository;
            _postConditionRepository = postConditionRepository;
            _postConditionContentRepository = postConditionContentRepository;

            _useCaseNextIdQueryHandler = useCaseNextIdQueryHandler;
        }

        private void PersistActor(UseCaseDiagram useCaseDiagram, ActorItem actorData)
        {
            var actor = new Actor();
            actor.Id = actorData.Cell.Id;
            actor.X = actorData.Cell.Position.X;
            actor.Y = actorData.Cell.Position.Y;

            foreach (var contentData in actorData.Contents)
            {
                var actorContent = new ActorContent();
                actorContent.Id = Guid.NewGuid();
                actorContent.Name = contentData.Name;
                actorContent.Locale = contentData.Locale;

                actor.Contents.Add(actorContent);
                _actorContentRepository.Add(actorContent);
            }

            actor.UseCaseDiagram = useCaseDiagram;
            useCaseDiagram.Entities.Add(actor);
            _actorRepository.Add(actor);
            _useCaseEntityRepository.Add(actor);
        }

        private void PersistPostCondition(UseCase useCaseEntity, PostConditionData postCondition)
        {
            var postConditionEntity = new UseCasePostCondition();
            postConditionEntity.Id = Guid.NewGuid();
            postConditionEntity.UseCase = useCaseEntity;

            foreach (var postConditionContent in postCondition.Contents)
            {
                var postConditionContentEntity = new UseCasePostConditionContent();
                postConditionContentEntity.Locale = postConditionContent.Locale;
                postConditionContentEntity.Description = postConditionContent.Description;
                postConditionContentEntity.Id = Guid.NewGuid();
                postConditionContentEntity.UseCasePostCondition = postConditionEntity;

                _postConditionContentRepository.Add(postConditionContentEntity);
            }

            _postConditionRepository.Add(postConditionEntity);
        }

        private void PersistPreCondition(UseCase useCaseEntity, PreConditionData preCondition)
        {
            var preConditionEntity = new UseCasePreCondition();
            preConditionEntity.Id = Guid.NewGuid();
            preConditionEntity.UseCase = useCaseEntity;

            foreach (var preConditionContent in preCondition.Contents)
            {
                var preConditionContentEntity = new UseCasePreConditionContent();
                preConditionContentEntity.Locale = preConditionContent.Locale;
                preConditionContentEntity.Description = preConditionContent.Description;
                preConditionContentEntity.Id = Guid.NewGuid();
                preConditionContentEntity.UseCasePreCondition = preConditionEntity;

                _preConditionContentRepository.Add(preConditionContentEntity);
            }

            _preConditionRepository.Add(preConditionEntity);
        }

        private void PersistUseCase(UseCaseDiagram useCaseDiagram, UseCaseItem useCaseData, int identifier, IEnumerable<UseCase> oldVersionUseCases = null)
        {
            var useCaseEntity = new UseCase();

            useCaseEntity.Id = useCaseData.Cell.Id;
            useCaseEntity.X = useCaseData.Cell.Position.X;
            useCaseEntity.Y = useCaseData.Cell.Position.Y;
            useCaseEntity.Identifier = identifier;
            useCaseEntity.Version = useCaseDiagram.Version;
            useCaseEntity.IsLastVersion = true;

            foreach (var contentData in useCaseData.Contents)
            {
                var useCaseContent = new UseCaseContent();
                useCaseContent.Id = Guid.NewGuid();
                useCaseContent.Name = contentData.Name;
                useCaseContent.Description = contentData.Description;
                useCaseContent.Path = contentData.Path;
                useCaseContent.Locale = contentData.Locale;

                useCaseEntity.Contents.Add(useCaseContent);
                _useCaseContentRepository.Add(useCaseContent);
            }

            foreach (var postCondition in useCaseData.PostConditions)
                PersistPostCondition(useCaseEntity, postCondition);

            foreach (var preCondition in useCaseData.PreConditions)
                PersistPreCondition(useCaseEntity, preCondition);

            if (oldVersionUseCases == null || !oldVersionUseCases.Any(o => o.Id == useCaseEntity.Id))
            {
                var specificationItem = new SpecificationItem();
                specificationItem.Id = useCaseEntity.Id;
                specificationItem.Label = $"{UseCase.PREFIX}{useCaseEntity.Identifier}";
                specificationItem.Active = true;
                specificationItem.Type = SpecificationItemType.UseCase;
                specificationItem.PackageId = useCaseDiagram.SpecificationItem.PackageId;
                _specificationItemRepository.Add(specificationItem);
                useCaseEntity.SpecificationItem = specificationItem;
            }
            else
            {
                var oldUseCase = oldVersionUseCases.FirstOrDefault(o => o.Id == useCaseEntity.Id);
                useCaseEntity.SpecificationItem = oldUseCase.SpecificationItem;
                useCaseEntity.SpecificationItem.PackageId = useCaseDiagram.SpecificationItem.PackageId;
                useCaseEntity.SpecificationItemId = oldUseCase.SpecificationItemId;
            }

            useCaseEntity.UseCaseDiagram = useCaseDiagram;

            useCaseEntity.ProjectId = useCaseDiagram.ProjectId;
            useCaseDiagram.Entities.Add(useCaseEntity);
            _useCaseRepository.Add(useCaseEntity);
            _useCaseEntityRepository.Add(useCaseEntity);
        }

        public void Persist(UseCaseDiagram useCaseDiagram, CreateUseCaseDiagramCommand command, IEnumerable<UseCase> oldVersionUseCases = null)
        {
            foreach (var contentItem in command.Contents)
            {
                var useCaseDiagramContent = new UseCaseDiagramContent();
                useCaseDiagramContent.Id = Guid.NewGuid();
                useCaseDiagramContent.Locale = contentItem.Locale;
                useCaseDiagramContent.Name = contentItem.Name;
                useCaseDiagram.Contents.Add(useCaseDiagramContent);
                _useCaseDiagramContentRepository.Add(useCaseDiagramContent);
            }

            var projectId = _currentProjectContextId.Get();
            var useCaseId = _useCaseNextIdQueryHandler.Handle(projectId);

            foreach (var useCaseData in command.UseCases)
            {
                var currentUseCaseId = useCaseData.Identifier == null ?
                    useCaseId++ :
                    useCaseData.Identifier.Value;
                PersistUseCase(useCaseDiagram, useCaseData, currentUseCaseId, oldVersionUseCases);
            }

            foreach (var actorData in command.Actors)
                PersistActor(useCaseDiagram, actorData);

            foreach (var relationData in command.EntitiesRelations)
            {
                var useCasesRelation = new UseCaseEntityRelation();
                useCasesRelation.Id = Guid.NewGuid();
                useCasesRelation.SourceId = relationData.SourceId.Value;
                useCasesRelation.TargetId = relationData.TargetId.Value;

                if (relationData.Contents != null)
                {
                    foreach (var content in relationData.Contents)
                    {
                        var relationContent = new UseCaseEntityRelationContent();
                        relationContent.Description = content.Description;
                        relationContent.Locale = content.Locale;
                        relationContent.Id = useCasesRelation.Id;
                        _useCaseEntityRelationContentRepository.Add(relationContent);
                    }
                }

                useCaseDiagram.EntitiesRelations.Add(useCasesRelation);
                _useCaseEntityRelationRepository.Add(useCasesRelation);
            }

            foreach (var relationData in command.UseCasesRelations)
            {
                var useCasesRelation = new UseCasesRelation();
                useCasesRelation.Id = Guid.NewGuid();
                useCasesRelation.SourceId = relationData.SourceId.Value;
                useCasesRelation.TargetId = relationData.TargetId.Value;
                useCasesRelation.Type = relationData.Type.Value;

                useCaseDiagram.UseCasesRelations.Add(useCasesRelation);
                _useCasesRelationRepository.Add(useCasesRelation);
            }
        }
    }
}
