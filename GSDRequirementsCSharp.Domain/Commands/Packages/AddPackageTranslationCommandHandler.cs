using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    class AddPackageTranslationCommandHandler : ICommandHandler<AddPackageTranslationCommand>
    {
        private readonly IRepository<Package, PackageKey> _packageRepository;
        private readonly ICurrentLocaleName _currentLocaleName;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IQueryHandler<Guid, IEnumerable<Package>> _packagesByGuidQueryHandler;

        public AddPackageTranslationCommandHandler(IRepository<Package, PackageKey> packageRepository,
                                                   ICurrentLocaleName currentLocaleName,
                                                   IRepository<Project, Guid> projectRepository,
                                                   ICurrentProjectContextId currentProjectContextId,
                                                   IQueryHandler<Guid, IEnumerable<Package>> packagesByGuidQueryHandler)
        {
            _packageRepository = packageRepository;
            _currentLocaleName = currentLocaleName;
            _projectRepository = projectRepository;
            _currentProjectContextId = currentProjectContextId;
            _packagesByGuidQueryHandler = packagesByGuidQueryHandler;
        }

        public void Handle(AddPackageTranslationCommand command)
        {
            var currentLocale = _currentLocaleName.Get();
            var originalPackages = _packagesByGuidQueryHandler.Handle(command.PackageId);

            if (!originalPackages.Any())
                throw new Exception(Sentences.packageNotFound);

            var originalPackage = originalPackages.FirstOrDefault();

            var translatedPackage = new Package();
            translatedPackage.Description = command.Description;
            translatedPackage.CreatorId = originalPackage.CreatorId;
            translatedPackage.Id = originalPackage.Id;

            translatedPackage.Locale = currentLocale;

            var currentProjectId = _currentProjectContextId.Get();
            var project = _projectRepository.Get(currentProjectId);
            translatedPackage.Project = project;

            _packageRepository.Add(translatedPackage);
        }
    }
}
