using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
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
        private ICurrentUserRetriever<User> _currentUserRetriever;

        public SaveRequirementCommandHandler(
             IRepository<Requirement, VersionKey> requirementRepository,
             IRepository<RequirementContent, LocaleKey> requirementContentRepository,
             IRepository<SpecificationItem, Guid> specificationItemRepository,
             IRepository<Package, Guid> packageRepository,
             ICurrentLocaleName currentLocaleName,
             ICurrentUserRetriever<User> currentUserRetriever
            )
        {
            _requirementRepository = requirementRepository;
            _requirementContentRepository = requirementContentRepository;
            _specificationItemRepository = specificationItemRepository;
            _packageRepository = packageRepository;
            _currentLocaleName = currentLocaleName;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(SaveRequirementCommand command)
        {
            var requirement = new Requirement();
            var currentUser = _currentUserRetriever.Get();
            requirement.CreatorId = currentUser.Id;
            requirement.Difficulty = command.Difficulty;
            requirement.Id = Guid.NewGuid();
            requirement.IsLastVersion = true;
            requirement.Rank = command.Rank;

            var content = new RequirementContent();
            content.Action = command.Action;
            content.Condition = command.Condition;
            content.Id = requirement.Id;

            var currentLocale = _currentLocaleName.Get();
            content.Locale = currentLocale;
            content.Requirement = requirement;
            content.Subject = command.Subject;

            var specificationItem = new SpecificationItem();
            specificationItem.Active = true;
            specificationItem.Id = requirement.Id;

            var package = _packageRepository.Get(command.PackageId);
            specificationItem.Package = package;

            requirement.SpecificationItem = specificationItem;
            requirement.Type = command.RequirementType;
            requirement.User = currentUser;
            requirement.Version = 1;

            _specificationItemRepository.Add(specificationItem);
            _requirementRepository.Add(requirement);
            _requirementContentRepository.Add(content);
        }
    }
}
