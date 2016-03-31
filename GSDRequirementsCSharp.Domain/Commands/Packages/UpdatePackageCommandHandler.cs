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
        private readonly IRepository<Package, PackageKey> _packageRepository;
        private readonly ICurrentLocaleName _currentLocaleName;

        public UpdatePackageCommandHandler(IRepository<Package, PackageKey> packageRepository,
                                           ICurrentLocaleName currentLocaleName)
        {
            _packageRepository = packageRepository;
            _currentLocaleName = currentLocaleName;
        }

        public void Handle(UpdatePackageCommand command)
        {
            var currentLocale = _currentLocaleName.Get();
            var key = new PackageKey { Id = command.Id, Locale = currentLocale };
            var package = _packageRepository.Get(key);
            package.Description = command.Description;
        }
    }
}
