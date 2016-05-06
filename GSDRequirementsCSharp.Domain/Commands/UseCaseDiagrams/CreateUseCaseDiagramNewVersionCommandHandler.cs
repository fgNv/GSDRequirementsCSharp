using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class CreateUseCaseDiagramNewVersionCommandHandler : ICommandHandler<CreateUseCaseDiagramNewVersionCommand>
    {
        private readonly IRepository<UseCaseDiagram, VersionKey> _useCaseDiagramRepository;
        private readonly UseCaseDiagramItemsPersister _useCaseDiagramItemsPersister;
        private readonly IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItemWithUseCaseDiagramsQueryResult> _specificationItemWithUseCaseDiagramsQueryHandler;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateUseCaseDiagramNewVersionCommandHandler(IRepository<UseCaseDiagram, VersionKey> useCaseDiagramRepository,
                                                IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItemWithUseCaseDiagramsQueryResult> specificationItemWithUseCaseDiagramsQueryHandler,
                                                IRepository<Package, Guid> packageRepository,
                                                UseCaseDiagramItemsPersister useCaseDiagramItemsPersister)
        {
            _useCaseDiagramRepository = useCaseDiagramRepository;
            _useCaseDiagramItemsPersister = useCaseDiagramItemsPersister;
            _packageRepository = packageRepository;
            _specificationItemWithUseCaseDiagramsQueryHandler = specificationItemWithUseCaseDiagramsQueryHandler;
        }

        public void Handle(CreateUseCaseDiagramNewVersionCommand command)
        {
            var queryResult = _specificationItemWithUseCaseDiagramsQueryHandler.Handle(command.Id.Value);
            var latestVersion = queryResult.UseCaseDiagrams.FirstOrDefault(s => s.IsLastVersion);
            foreach (var oldRequirementVersion in queryResult.UseCaseDiagrams)
            {
                oldRequirementVersion.IsLastVersion = false;
            }

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var useCaseDiagram = new UseCaseDiagram();
            useCaseDiagram.Id = command.Id.Value;
             
            useCaseDiagram.SpecificationItem = queryResult.SpecificationItem;
            useCaseDiagram.ProjectId = latestVersion.ProjectId;
            useCaseDiagram.Version = latestVersion.Version + 1;
            useCaseDiagram.IsLastVersion = true;
            useCaseDiagram.Identifier = latestVersion.Identifier;

            _useCaseDiagramItemsPersister.Persist(useCaseDiagram, command);

            var oldToNewEntitiesIds = new Dictionary<Guid, Guid>();

            foreach (var useCaseEntity in useCaseDiagram.Entities)
            {
                var oldId = useCaseEntity.Id;
                var newId = Guid.NewGuid();
                useCaseEntity.Id = newId;
                oldToNewEntitiesIds[oldId] = newId;
            }

            foreach (var classEntity in useCaseDiagram.Entities)
            {
                var oldId = classEntity.Id;
                var newId = Guid.NewGuid();
                classEntity.Id = newId;
                oldToNewEntitiesIds[oldId] = newId;
            }

            foreach (var relation in useCaseDiagram.UseCaseRelations)
            {
                relation.SourceId = oldToNewEntitiesIds[relation.SourceId];
                relation.TargetId = oldToNewEntitiesIds[relation.TargetId];
            }

            foreach (var relation in useCaseDiagram.Relations)
            {
                relation.SourceId = oldToNewEntitiesIds[relation.SourceId];
                relation.TargetId = oldToNewEntitiesIds[relation.TargetId];
            }

            _useCaseDiagramRepository.Add(useCaseDiagram);
        }
    }
}
