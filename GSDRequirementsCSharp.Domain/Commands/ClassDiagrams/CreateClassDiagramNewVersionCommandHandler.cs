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
    public class CreateClassDiagramNewVersionCommandHandler : ICommandHandler<CreateClassDiagramNewVersionCommand>
    {
        private readonly IRepository<ClassDiagram, VersionKey> _classDiagramRepository;
        private readonly ClassDiagramItemsPersister _classDiagramItemsPersister;
        private readonly IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItemWithClassDiagramsQueryResult> _specificationItemWithClassDiagramsQueryHandler;
        private readonly IRepository<Package, Guid> _packageRepository;

        public CreateClassDiagramNewVersionCommandHandler(IRepository<ClassDiagram, VersionKey> classDiagramRepository,
                                                IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItemWithClassDiagramsQueryResult> specificationItemWithClassDiagramsQueryHandler,
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
            var queryResult = _specificationItemWithClassDiagramsQueryHandler.Handle(command.Id.Value);
            var latestVersion = queryResult.ClassDiagrams.FirstOrDefault(s => s.IsLastVersion);
            foreach (var oldRequirementVersion in queryResult.ClassDiagrams)
            {
                oldRequirementVersion.IsLastVersion = false;
            }

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);

            var classDiagram = new ClassDiagram();
            classDiagram.Id = command.Id.Value;
            classDiagram.SpecificationItem = queryResult.SpecificationItem;
            classDiagram.ProjectId = latestVersion.ProjectId;
            classDiagram.Version = latestVersion.Version + 1;
            classDiagram.IsLastVersion = true;
            classDiagram.Identifier = latestVersion.Identifier;

            _classDiagramItemsPersister.Persist(classDiagram, command);

            var oldToNewClassesIds = new Dictionary<Guid, Guid>();

            foreach (var classEntity in classDiagram.Classes)
            {
                var oldId = classEntity.Id;
                var newId = Guid.NewGuid();
                classEntity.Id = newId;
                oldToNewClassesIds[oldId] = newId;
            }

            foreach (var relation in classDiagram.Relationships)
            {
                relation.SourceId = oldToNewClassesIds[relation.SourceId];
                relation.TargetId = oldToNewClassesIds[relation.TargetId];
            }

            _classDiagramRepository.Add(classDiagram);
        }
    }
}
