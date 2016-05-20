using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class CreateClassDiagramCommandHandler : ICommandHandler<CreateClassDiagramCommand>
    {
        private readonly IRepository<ClassDiagram, VersionKey> _classDiagramRepository;
        private readonly ClassDiagramItemsPersister _classDiagramItemsPersister;
        private readonly IQueryHandler<ClassDiagramNextIdQuery, int> _classDiagramNextIdQueryHandler;
        private readonly IRepository<SpecificationItem, Guid> _specifiationItemRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateClassDiagramCommandHandler(IRepository<ClassDiagram, VersionKey> classDiagramRepository,
                                                IRepository<SpecificationItem, Guid> specifiationItemRepository,
                                                ICurrentProjectContextId currentProjectContextId,                                              
                                                IQueryHandler<ClassDiagramNextIdQuery, int> classDiagramNextIdQueryHandler,
                                                IRepository<Package, Guid> packageRepository,
                                                ClassDiagramItemsPersister classDiagramItemsPersister)
        {
            _classDiagramRepository = classDiagramRepository;
            _specifiationItemRepository = specifiationItemRepository;
            _currentProjectContextId = currentProjectContextId;
            _classDiagramNextIdQueryHandler = classDiagramNextIdQueryHandler;
            _classDiagramItemsPersister = classDiagramItemsPersister;
            _packageRepository = packageRepository;
        }

        public void Handle(CreateClassDiagramCommand command)
        {
            var id = Guid.NewGuid();
            
            var projectId = _currentProjectContextId.Get();
            if (projectId == null) throw new Exception(Sentences.noProjectInContext);

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var nextId = _classDiagramNextIdQueryHandler.Handle(projectId.Value);

            var specificationItem = new SpecificationItem();
            specificationItem.Id = id;
            specificationItem.Active = true;
            specificationItem.Type = SpecificationItemType.ClassDiagram;
            specificationItem.PackageId = command.PackageId.Value;
            specificationItem.Label = $"CD{nextId}";

            var classDiagram = new ClassDiagram();
            classDiagram.Id = id;
            classDiagram.SpecificationItem = specificationItem;
            classDiagram.ProjectId = projectId.Value;
            classDiagram.Version = 1;
            classDiagram.IsLastVersion = true;
            classDiagram.Identifier = nextId;

            _classDiagramItemsPersister.Persist(classDiagram, command);

            _classDiagramRepository.Add(classDiagram);
            _specifiationItemRepository.Add(specificationItem);
        }
    }
}
