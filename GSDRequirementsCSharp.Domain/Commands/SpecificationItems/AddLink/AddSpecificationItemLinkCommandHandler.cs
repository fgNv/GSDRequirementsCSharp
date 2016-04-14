using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.SpecificationItems
{
    public class AddSpecificationItemLinkCommandHandler : ICommandHandler<AddSpecificationItemLinkCommand>
    {
        private IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public AddSpecificationItemLinkCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(AddSpecificationItemLinkCommand command)
        {
            var origin = _specificationItemRepository.Get(command.Id.Value);
            var target = _specificationItemRepository.Get(command.TargetItemId.Value);

            if (origin == null || target == null)
                throw new Exception(Sentences.specificationItemNotFound);

            origin.LinkedItems.Add(target);

            if (command.IsBidirectional)
                target.LinkedItems.Add(origin);
        }
    }
}
