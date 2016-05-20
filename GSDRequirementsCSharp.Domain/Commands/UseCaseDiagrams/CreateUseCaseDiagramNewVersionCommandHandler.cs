using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        ///     Set the former use cases cell id, to match the specification item id
        /// </summary>
        /// <param name="command"></param>
        /// <param name="oldUseCasesVersions"></param>
        private void RetrieveSpecificationItemIdsForCommandUseCases
            (CreateUseCaseDiagramNewVersionCommand command, IEnumerable<UseCase> oldUseCasesVersions)
        {
            foreach (var useCaseItem in command.UseCases)
            {
                if (!useCaseItem.Identifier.HasValue) continue;
                var oldUseCase = oldUseCasesVersions
                    .FirstOrDefault(uc => uc.Identifier == useCaseItem.Identifier.Value);

                foreach (var relation in command.EntitiesRelations)
                {
                    if (relation.SourceId == useCaseItem.Cell.Id)
                        relation.SourceId = oldUseCase.Id;

                    if (relation.TargetId == useCaseItem.Cell.Id)
                        relation.TargetId = oldUseCase.Id;
                }

                foreach (var relation in command.UseCasesRelations)
                {
                    if (relation.SourceId == useCaseItem.Cell.Id)
                        relation.SourceId = oldUseCase.Id;

                    if (relation.TargetId == useCaseItem.Cell.Id)
                        relation.TargetId = oldUseCase.Id;
                }
                useCaseItem.Cell.Id = oldUseCase.Id;
            }
        }

        public void Handle(CreateUseCaseDiagramNewVersionCommand command)
        {
            var queryResult = _specificationItemWithUseCaseDiagramsQueryHandler.Handle(command.Id.Value);

            var latestVersion = queryResult.UseCaseDiagrams.FirstOrDefault(s => s.IsLastVersion);
            foreach (var oldUseCaseDiagramsVersion in queryResult.UseCaseDiagrams)
                oldUseCaseDiagramsVersion.IsLastVersion = false;

            var oldUseCasesVersions = _useCasesByDiagramQueryHandler.Handle(command.Id);

            foreach (var oldUseCaseVersion in oldUseCasesVersions)
                oldUseCaseVersion.IsLastVersion = false;

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

            RetrieveSpecificationItemIdsForCommandUseCases(command, oldUseCasesVersions);
            _useCaseDiagramItemsPersister.Persist(useCaseDiagram, command, oldUseCasesVersions);
                        
            _useCaseDiagramRepository.Add(useCaseDiagram);
        }
    }
}
