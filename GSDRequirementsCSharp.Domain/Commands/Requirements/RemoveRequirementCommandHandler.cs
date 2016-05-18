using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class RemoveRequirementCommandHandler : ICommandHandler<RemoveRequirementCommand>
    {
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public RemoveRequirementCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(RemoveRequirementCommand command)
        {
            var item = _specificationItemRepository.Get(command.Id);
            if (item == null)
                throw new Exception(Sentences.specificationItemNotFound);

            item.Active = false;
        }
    }
}
