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
        private readonly IQueryHandler<Guid, PackageWithCurrentCultureContentsQueryResult> _packageWithCurrentCultureContentsQueryHandler;

        public UpdatePackageCommandHandler(IRepository<Package, Guid> packageRepository,
                                           IQueryHandler<Guid, PackageWithCurrentCultureContentsQueryResult> packageWithCurrentCultureContentsQueryHandler)
        {
            _packageRepository = packageRepository;
            _packageWithCurrentCultureContentsQueryHandler = packageWithCurrentCultureContentsQueryHandler;
        }

        public void Handle(UpdatePackageCommand command)
        {
            var packageAndContent = _packageWithCurrentCultureContentsQueryHandler
                                        .Handle(command.Id);
            packageAndContent.PackageContent.Description = command.Description;
        }
    }
}
