using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Models.UseCases;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UseCaseDiagramItemsPersister
    {
        private readonly IRepository<UseCaseDiagramContent, LocaleKey> _classDiagramContentRepository;
        private readonly IRepository<UseCase, Guid> _useCaseRepository;
        private readonly IRepository<UseCaseContent, LocaleKey> _useCaseContentRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<ActorContent, LocaleKey> _actorContentRepository;
        private readonly IRepository<UseCasesRelation, Guid> _useCasesRelationRepository;
        private readonly IRepository<UseCaseEntityRelation, Guid> _useCaseEntityRelationRepository;

        public UseCaseDiagramItemsPersister(IRepository<UseCaseDiagramContent, LocaleKey> classDiagramContentRepository,
                                            IRepository<UseCase, Guid> useCaseRepository,
                                            IRepository<UseCaseContent, LocaleKey> useCaseContentRepository,
                                            IRepository<Actor, Guid> actorRepository,
                                            IRepository<ActorContent, LocaleKey> actorContentRepository,
                                            IRepository<UseCasesRelation, Guid> useCasesRelationRepository,
                                            IRepository<UseCaseEntityRelation, Guid> useCaseEntityRelationRepository)
        {
            _classDiagramContentRepository = classDiagramContentRepository;
            _useCaseRepository = useCaseRepository;
            _useCaseContentRepository = useCaseContentRepository;
            _actorRepository = actorRepository;
            _actorContentRepository = actorContentRepository;
            _useCasesRelationRepository = useCasesRelationRepository;
            _useCaseEntityRelationRepository = useCaseEntityRelationRepository;
        }
        
        private void PersistActor(UseCaseDiagram useCaseEntity, ActorItem actorData)
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

            useCaseEntity.Entities.Add(actor);
            _actorRepository.Add(actor);
        }

        private void PersistUseCase(UseCaseDiagram useCaseDiagram, UseCaseItem useCaseData)
        {
            var useCaseEntity = new UseCase();
            
            useCaseEntity.Id = useCaseData.Cell.Id;
            useCaseEntity.X = useCaseData.Cell.Position.X;
            useCaseEntity.Y = useCaseData.Cell.Position.Y;

            foreach (var contentData in useCaseData.Contents)
            {
                var useCaseContent = new UseCaseContent();
                useCaseContent.Id = Guid.NewGuid();
                useCaseContent.Name = contentData.Name;
                useCaseContent.Locale = contentData.Locale;

                useCaseEntity.Contents.Add(useCaseContent);
                _useCaseContentRepository.Add(useCaseContent);
            }

            foreach(var postCondition in useCaseData.PostConditions)
            {

            }

            foreach (var preCondition in useCaseData.PreConditions)
            {

            }

            useCaseDiagram.Entities.Add(useCaseEntity);
            _useCaseRepository.Add(useCaseEntity);
        }

        public void Persist(UseCaseDiagram useCaseDiagram, CreateUseCaseDiagramCommand command)
        {
            foreach (var contentItem in command.Contents)
            {
                var useCaseDiagramContent = new UseCaseDiagramContent();
                useCaseDiagramContent.Id = Guid.NewGuid();
                useCaseDiagramContent.Locale = contentItem.Locale;
                useCaseDiagramContent.Name = contentItem.Name;
                useCaseDiagram.Contents.Add(useCaseDiagramContent);
                _classDiagramContentRepository.Add(useCaseDiagramContent);
            }

            foreach (var useCaseData in command.UseCases)
                PersistUseCase(useCaseDiagram, useCaseData);

            foreach (var actorData in command.Actors)
                PersistActor(useCaseDiagram, actorData);

            foreach (var relationData in command.UseCaseEntitiesRelations)
            {
                var useCasesRelation = new UseCaseEntityRelation();
                useCasesRelation.Id = Guid.NewGuid();
                useCasesRelation.SourceId = relationData.SourceId.Value;
                useCasesRelation.TargetId = relationData.TargetId.Value;

                useCaseDiagram.Relations.Add(useCasesRelation);
                _useCaseEntityRelationRepository.Add(useCasesRelation);
            }

            foreach (var relationData in command.UseCasesRelations)
            {
                var useCasesRelation = new UseCasesRelation();
                useCasesRelation.Id = Guid.NewGuid();
                useCasesRelation.SourceId = relationData.SourceId.Value;                
                useCasesRelation.TargetId = relationData.TargetId.Value;                
                useCasesRelation.Type = relationData.Type.Value;

                useCaseDiagram.UseCaseRelations.Add(useCasesRelation);
                _useCasesRelationRepository.Add(useCasesRelation);
            }
        }
    }
}
