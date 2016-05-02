using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Packages;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    class UpdatePackageCommandHandler : ICommandHandler<UpdatePackageCommand>
    {
        private readonly IQueryHandler<Guid, Package> _packageWithContentsQueryHandler;
        private readonly IRepository<PackageContent, LocaleKey> _packageContentRepository;

        public UpdatePackageCommandHandler(IQueryHandler<Guid, Package> packageWithContentsQueryHandler,
                                           IRepository<PackageContent, LocaleKey> packageContentRepository)
        {   
            _packageWithContentsQueryHandler = packageWithContentsQueryHandler;
            _packageContentRepository = packageContentRepository;
        }

        public void Handle(UpdatePackageCommand command)
        {
            var package = _packageWithContentsQueryHandler.Handle(command.Id.Value);
            foreach (var content in package.Contents)
            {
                if (command.Items.Any(i => content.Locale == i.Locale))
                {
                    var item = command.Items.FirstOrDefault(i => content.Locale == i.Locale);
                    content.IsUpdated = true;
                    content.Description = item.Description;
                }
                else
                {
                    content.IsUpdated = false;
                }
            }

            foreach (var item in command.Items)
            {
                if (package.Contents.Any(p => p.Locale == item.Locale))
                    continue;

                var content = new PackageContent();
                content.Id = package.Id;
                content.IsUpdated = true;
                content.Locale = item.Locale;
                content.Description = item.Description;
                content.Package = package;

                _packageContentRepository.Add(content);
            }
        }
    }
}
