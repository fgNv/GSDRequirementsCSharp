using GSDRequirementsCSharp.Domain.Models;
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
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly IRepository<PackageContent, LocaleKey> _packageContentRepository;
        private readonly ICurrentLocaleName _currentLocaleName;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public AddPackageTranslationCommandHandler(IRepository<Package, Guid> packageRepository,
                                                   IRepository<PackageContent, LocaleKey> packageContentRepository,
                                                   ICurrentLocaleName currentLocaleName,
                                                   ICurrentProjectContextId currentProjectContextId)
        {
            _packageRepository = packageRepository;
            _packageContentRepository = packageContentRepository;
            _currentLocaleName = currentLocaleName;
            _currentProjectContextId = currentProjectContextId;
        }

        public void Handle(AddPackageTranslationCommand command)
        {
            var currentLocale = _currentLocaleName.Get();
            var package = _packageRepository.Get(command.PackageId);

            if (package == null)
                throw new Exception(Sentences.packageNotFound);
            
            var translation = new PackageContent();
            translation.Description = command.Description;
            translation.Id = package.Id;
            translation.Locale = currentLocale;
            translation.Package = package;

            _packageContentRepository.Add(translation);
        }
    }
}
