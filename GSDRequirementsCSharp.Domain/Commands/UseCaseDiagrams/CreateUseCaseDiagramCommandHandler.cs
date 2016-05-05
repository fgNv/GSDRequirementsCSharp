using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.ClassDiagrams;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class CreateUseCaseDiagramCommandHandler : ICommandHandler<CreateUseCaseDiagramCommand>
    {
        private readonly IRepository<UseCaseDiagram, VersionKey> _useCaseDiagramRepository;
        private readonly UseCaseDiagramItemsPersister _useCaseDiagramItemsPersister;
        private readonly IQueryHandler<UseCaseDiagramNextIdQuery, int> _useCaseDiagramNextIdQueryHandler;
        private readonly IRepository<SpecificationItem, Guid> _specifiationItemRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateUseCaseDiagramCommandHandler(IRepository<UseCaseDiagram, VersionKey> useCaseDiagramRepository,
                                                IRepository<SpecificationItem, Guid> specifiationItemRepository,
                                                ICurrentProjectContextId currentProjectContextId,                                              
                                                IQueryHandler<UseCaseDiagramNextIdQuery, int> useCaseDiagramNextIdQueryHandler,
                                                IRepository<Package, Guid> packageRepository,
                                                UseCaseDiagramItemsPersister useCaseDiagramItemsPersister)
        {
            _useCaseDiagramRepository = useCaseDiagramRepository;
            _specifiationItemRepository = specifiationItemRepository;
            _currentProjectContextId = currentProjectContextId;
            _useCaseDiagramNextIdQueryHandler = useCaseDiagramNextIdQueryHandler;
            _useCaseDiagramItemsPersister = useCaseDiagramItemsPersister;
            _packageRepository = packageRepository;
        }

        public void Handle(CreateUseCaseDiagramCommand command)
        {
            var id = Guid.NewGuid();
            
            var projectId = _currentProjectContextId.Get();
            if (projectId == null) throw new Exception(Sentences.noProjectInContext);

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var nextId = _useCaseDiagramNextIdQueryHandler.Handle(projectId.Value);

            var specificationItem = new SpecificationItem();
            specificationItem.Id = id;
            specificationItem.Active = true;
            specificationItem.Type = SpecificationItemType.ClassDiagram;
            specificationItem.PackageId = command.PackageId.Value;
            specificationItem.Label = $"UC{nextId}";

            var useCaseDiagram = new UseCaseDiagram();
            useCaseDiagram.Id = id;
            useCaseDiagram.SpecificationItem = specificationItem;
            useCaseDiagram.ProjectId = projectId.Value;
            useCaseDiagram.Version = 1;
            useCaseDiagram.IsLastVersion = true;
            useCaseDiagram.Identifier = nextId;

            _useCaseDiagramItemsPersister.Persist(useCaseDiagram, command);

            _useCaseDiagramRepository.Add(useCaseDiagram);
            _specifiationItemRepository.Add(specificationItem);
        }
    }
}
