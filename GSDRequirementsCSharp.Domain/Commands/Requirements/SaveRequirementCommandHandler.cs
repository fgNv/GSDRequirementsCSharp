using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class SaveRequirementCommandHandler : ICommandHandler<SaveRequirementCommand>
    {
        private IRepository<Requirement, VersionKey> _requirementRepository;
        private IRepository<RequirementContent, LocaleKey> _requirementContentRepository;
        private IRepository<SpecificationItem, Guid> _specificationItemRepository;
        private IRepository<Package, Guid> _packageRepository;
        private ICurrentLocaleName _currentLocaleName;

        public SaveRequirementCommandHandler()
        {

        }

        public void Handle(SaveRequirementCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
