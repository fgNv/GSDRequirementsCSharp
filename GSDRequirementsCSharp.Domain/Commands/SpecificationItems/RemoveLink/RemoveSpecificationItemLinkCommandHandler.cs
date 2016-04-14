using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.SpecificationItems
{
    public class RemoveSpecificationItemLinkCommandHandler : ICommandHandler<RemoveSpecificationItemLinkCommand>
    {
        private IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public RemoveSpecificationItemLinkCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(RemoveSpecificationItemLinkCommand command)
        {
            var origin = _specificationItemRepository.Get(command.OriginItemId.Value);
            var target = _specificationItemRepository.Get(command.TargetItemId.Value);

            if (origin == null || target == null)
                throw new Exception(Sentences.specificationItemNotFound);

            origin.LinkedItems.Remove(target);
            if (target.LinkedItems.Any(o => o.Id == origin.Id))
                target.LinkedItems.Remove(origin);
        }
    }
}
