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
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly IQueryHandler<Guid, Package> _packageWithContentsQueryHandler;

        public UpdatePackageCommandHandler(IRepository<Package, Guid> packageRepository,
                                           IQueryHandler<Guid, Package> packageWithContentsQueryHandler)
        {
            _packageRepository = packageRepository;
            _packageWithContentsQueryHandler = packageWithContentsQueryHandler;
        }

        public void Handle(UpdatePackageCommand command)
        {
            var package = _packageWithContentsQueryHandler.Handle(command.Id);
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
        }
    }
}
