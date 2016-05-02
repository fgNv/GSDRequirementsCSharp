using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class CreateClassDiagramNewVersionCommandHandler : ICommandHandler<CreateClassDiagramNewVersionCommand>
    {
        private readonly IRepository<ClassDiagram, VersionKey> _classDiagramRepository;
        private readonly ClassDiagramItemsPersister _classDiagramItemsPersister;
        private readonly IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItem> _specificationItemWithClassDiagramsQueryHandler;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateClassDiagramNewVersionCommandHandler(IRepository<ClassDiagram, VersionKey> classDiagramRepository,
                                                IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItem> specificationItemWithClassDiagramsQueryHandler,
                                                IRepository<Package, Guid> packageRepository,
                                                ClassDiagramItemsPersister classDiagramItemsPersister)
        {
            _classDiagramRepository = classDiagramRepository;
            _classDiagramItemsPersister = classDiagramItemsPersister;
            _packageRepository = packageRepository;
            _specificationItemWithClassDiagramsQueryHandler = specificationItemWithClassDiagramsQueryHandler;
        }

        public void Handle(CreateClassDiagramNewVersionCommand command)
        {
            var specificationItem = _specificationItemWithClassDiagramsQueryHandler.Handle(command.Id.Value);
            var latestVersion = specificationItem.Requirements.FirstOrDefault(s => s.IsLastVersion);
            foreach (var oldRequirementVersion in specificationItem.Requirements)
            {
                oldRequirementVersion.IsLastVersion = false;
            }

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var classDiagram = new ClassDiagram();
            classDiagram.Id = command.Id.Value;
            classDiagram.SpecificationItem = specificationItem;
            classDiagram.ProjectId = latestVersion.ProjectId;
            classDiagram.Version = latestVersion.Version + 1;
            classDiagram.IsLastVersion = true;
            classDiagram.Identifier = latestVersion.Identifier;

            _classDiagramItemsPersister.Persist(classDiagram, command);

            _classDiagramRepository.Add(classDiagram);
        }
    }
}
