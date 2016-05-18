using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class RemoveClassDiagramCommandHandler : ICommandHandler<RemoveClassDiagramCommand>
    {
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;

        public RemoveClassDiagramCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository)
        {
            _specificationItemRepository = specificationItemRepository;
        }

        public void Handle(RemoveClassDiagramCommand command)
        {
            var specificationItem = _specificationItemRepository.Get(command.Id);
            if (specificationItem == null)
                throw new Exception(Sentences.specificationItemNotFound);

            specificationItem.Active = false;
        }
    }
}
