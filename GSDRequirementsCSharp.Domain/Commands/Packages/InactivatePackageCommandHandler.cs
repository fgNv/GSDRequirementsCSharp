using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
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
        private readonly IQueryHandler<SpecificationItemsByPackageQuery, IEnumerable<SpecificationItem>>
            _specificationItemsByPackageQueryHandler;

        public InactivatePackageCommandHandler(
            IRepository<Package, Guid> packagesRepository,
            IQueryHandler<SpecificationItemsByPackageQuery, IEnumerable<SpecificationItem>> specificationItemsByPackageQueryHandler
        )
        {
            _packagesRepository = packagesRepository;
            _specificationItemsByPackageQueryHandler = specificationItemsByPackageQueryHandler;
        }

        public void Handle(InactivatePackageCommand command)
        {
            var package = _packagesRepository.Get(command.Id.Value);
            package.Active = false;

            var specificationItems = _specificationItemsByPackageQueryHandler.Handle(command.Id);
            foreach(var item in specificationItems)
            {
                item.Active = false;
            }
        }
    }
}
