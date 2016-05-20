using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    class InactivateSpecificationItemCommandHandler : ICommandHandler<InactivateSpecificationItemCommand>
    {
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public InactivateSpecificationItemCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(InactivateSpecificationItemCommand command)
        {
            var specificationItem = _specificationItemRepository.Get(command.Id.Value);
            specificationItem.Active = false;
        }
    }
}
