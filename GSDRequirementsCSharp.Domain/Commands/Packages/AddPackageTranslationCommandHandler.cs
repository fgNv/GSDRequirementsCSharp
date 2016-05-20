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

namespace GSDRequirementsCSharp.Domain.Commands
{
    class AddPackageTranslationCommandHandler : ICommandHandler<AddPackageTranslationCommand>
    {
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly IRepository<PackageContent, LocaleKey> _packageContentRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IQueryHandler<Guid, Package> _packageWithContents;

        public AddPackageTranslationCommandHandler(IRepository<Package, Guid> packageRepository,
                                                   IRepository<PackageContent, LocaleKey> packageContentRepository,
                                                   ICurrentProjectContextId currentProjectContextId,
                                                   IQueryHandler<Guid, Package> packageWithContents)
        {
            _packageRepository = packageRepository;
            _packageContentRepository = packageContentRepository;
            _currentProjectContextId = currentProjectContextId;
            _packageWithContents = packageWithContents;
        }

        public void Handle(AddPackageTranslationCommand command)
        {
            var package = _packageWithContents.Handle(command.Id.Value);
            foreach (var item in command.Items)
            {
                var content = package.Contents.FirstOrDefault(c => c.Locale == item.Locale);

                if (content != null && content.IsUpdated)
                    continue;

                if (content == null)
                {
                    content = new PackageContent();
                    content.Package = package;
                    content.Locale = item.Locale;
                    content.Id = package.Id;
                    _packageContentRepository.Add(content);
                }

                content.IsUpdated = true;
                content.Description = item.Description;
            }
        }
    }
}
