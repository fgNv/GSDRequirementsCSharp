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
        private readonly IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> _useCasesByDiagramQueryHandler;
        private readonly IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItemWithUseCaseDiagramsQueryResult> _specificationItemWithUseCaseDiagramsQueryHandler;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateUseCaseDiagramNewVersionCommandHandler(IRepository<UseCaseDiagram, VersionKey> useCaseDiagramRepository,
                                                IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItemWithUseCaseDiagramsQueryResult> specificationItemWithUseCaseDiagramsQueryHandler,
                                                IRepository<Package, Guid> packageRepository,
                                                UseCaseDiagramItemsPersister useCaseDiagramItemsPersister,
                                                IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> useCasesByDiagramQueryHandler)
        {
            _useCaseDiagramRepository = useCaseDiagramRepository;
            _useCaseDiagramItemsPersister = useCaseDiagramItemsPersister;
            _packageRepository = packageRepository;
            _specificationItemWithUseCaseDiagramsQueryHandler = specificationItemWithUseCaseDiagramsQueryHandler;
            _useCasesByDiagramQueryHandler = useCasesByDiagramQueryHandler;
        }

        public void Handle(CreateUseCaseDiagramNewVersionCommand command)
        {
            var queryResult = _specificationItemWithUseCaseDiagramsQueryHandler.Handle(command.Id.Value);
            
            var latestVersion = queryResult.UseCaseDiagrams.FirstOrDefault(s => s.IsLastVersion);
            foreach (var oldUseCaseDiagramsVersion in queryResult.UseCaseDiagrams)
                oldUseCaseDiagramsVersion.IsLastVersion = false;

            var oldUseCasesVersions = _useCasesByDiagramQueryHandler.Handle(command.Id);
            
            foreach (var oldUseCasesVersion in oldUseCasesVersions) {
                oldUseCasesVersion.IsLastVersion = false;
            }

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var useCaseDiagram = new UseCaseDiagram();
            useCaseDiagram.Id = command.Id.Value;
             
            useCaseDiagram.SpecificationItem = queryResult.SpecificationItem;
            useCaseDiagram.SpecificationItem.PackageId = package.Id;
            useCaseDiagram.ProjectId = latestVersion.ProjectId;
            useCaseDiagram.Version = latestVersion.Version + 1;
            useCaseDiagram.Identifier = latestVersion.Identifier;
            useCaseDiagram.IsLastVersion = true;

            _useCaseDiagramItemsPersister.Persist(useCaseDiagram, command, oldUseCasesVersions);

            var oldToNewEntitiesIds = new Dictionary<Guid, Guid>();

            foreach (var useCaseEntity in useCaseDiagram.Entities)
            {
                var oldId = useCaseEntity.Id;
                var newId = Guid.NewGuid();
                useCaseEntity.Id = newId;             
                oldToNewEntitiesIds[oldId] = newId;
            }

            foreach (var relation in useCaseDiagram.UseCasesRelations)
            {
                relation.SourceId = oldToNewEntitiesIds[relation.SourceId];
                relation.TargetId = oldToNewEntitiesIds[relation.TargetId];
            }

            foreach (var relation in useCaseDiagram.EntitiesRelations)
            {
                relation.SourceId = oldToNewEntitiesIds[relation.SourceId];
                relation.TargetId = oldToNewEntitiesIds[relation.TargetId];
            }

            _useCaseDiagramRepository.Add(useCaseDiagram);
        }
    }
}
