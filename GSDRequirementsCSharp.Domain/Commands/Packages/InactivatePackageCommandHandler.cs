using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    class InactivatePackageCommandHandler : ICommandHandler<InactivatePackageCommand>
    {
        private readonly IRepository<Package, Guid> _packagesRepository;

        public InactivatePackageCommandHandler(IRepository<Package, Guid> packagesRepository)
        {
            _packagesRepository = packagesRepository;
        }

        public void Handle(InactivatePackageCommand command)
        {
            var package = _packagesRepository.Get(command.Id);
            package.Active = false;
        }
    }
}
