using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;

namespace GSDRequirementsCSharp.Domain.Commands
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
