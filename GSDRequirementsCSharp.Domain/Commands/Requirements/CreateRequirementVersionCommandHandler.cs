using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class CreateRequirementVersionCommandHandler : ICommandHandler<CreateRequirementVersionCommand>
    {
        private readonly IRepository<Requirement, VersionKey> _requirementRepository;
        private readonly IRepository<RequirementContent, LocaleKey> _requirementContentRepository;
        private readonly IQueryHandler<SpecificationItemWithRequirementsQuery,SpecificationItem> _specificationItemWithRequirementsQueryHandler;
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CreateRequirementVersionCommandHandler(
            IRepository<Requirement, VersionKey> requirementRepository,
            IRepository<RequirementContent, LocaleKey> requirementContentRepository,
            IQueryHandler<SpecificationItemWithRequirementsQuery, SpecificationItem> specificationItemWithRequirementsQueryHandler,
            IRepository<Package, Guid> packageRepository,
            ICurrentUserRetriever<User> currentUserRetriever
            )
        {
            _requirementRepository = requirementRepository;
            _requirementContentRepository = requirementContentRepository;
            _specificationItemWithRequirementsQueryHandler = specificationItemWithRequirementsQueryHandler;
            _packageRepository = packageRepository;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(CreateRequirementVersionCommand command)
        {
            var specificationItem = _specificationItemWithRequirementsQueryHandler.Handle(command.Id);
            var latestVersion = specificationItem.Requirements.FirstOrDefault(s => s.IsLastVersion);
            foreach(var oldRequirementVersion in specificationItem.Requirements)
            {
                oldRequirementVersion.IsLastVersion = false;
            }

            var package = _packageRepository.Get(command.PackageId.Value);
            if (!package.Active) throw new Exception(Sentences.thisPackageWasRemoved);
            specificationItem.Package = package;

            var currentUser = _currentUserRetriever.Get();

            var requirement = new Requirement();
            requirement.Id = command.Id.Value;
            requirement.CreatorId = currentUser.Id;
            requirement.Difficulty = command.Difficulty.Value;
            requirement.Identifier = latestVersion.Identifier;
            requirement.IsLastVersion = true; ;
            requirement.ProjectId = latestVersion.ProjectId;
            requirement.Rank = command.Rank.Value;
            
            requirement.SpecificationItem = specificationItem;
            requirement.CreatorId = latestVersion.CreatorId;
            requirement.Version = latestVersion.Version + 1;
            requirement.Type = latestVersion.Type;

            foreach (var item in command.Items)
            {
                var content = new RequirementContent();
                content.Action = item.Action;
                content.Subject = item.Subject;
                content.Condition = item.Condition;
                content.Id = requirement.Id;
                content.Locale = item.Locale;
                content.Version = requirement.Version;
                content.Requirement = requirement;
                _requirementContentRepository.Add(content);
            }

            _requirementRepository.Add(requirement); 
        }
    }
}
