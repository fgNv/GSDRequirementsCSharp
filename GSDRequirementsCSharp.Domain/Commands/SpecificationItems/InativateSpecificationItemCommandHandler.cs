using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.SpecificationItems
{
    class InativateSpecificationItemCommandHandler : IProjectCommandHandler<InativateSpecificationItemCommand>
    {
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public InativateSpecificationItemCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(InativateSpecificationItemCommand command)
        {
            var specificationItem = _specificationItemRepository.Get(command.Id);
            specificationItem.Active = false;
        }
    }
}
