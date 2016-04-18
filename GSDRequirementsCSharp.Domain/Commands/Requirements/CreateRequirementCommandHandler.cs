using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class CreateRequirementCommandHandler : ICommandHandler<SaveRequirementCommand>
    {
        private readonly IRepository<Requirement, VersionKey> _requirementRepository;
        private readonly IRepository<RequirementContent, LocaleKey> _requirementContentRepository;
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IQueryHandler<RequirementNextIdQuery, int> _requirementNextIdQueryHandler;

        public CreateRequirementCommandHandler(
             IRepository<Requirement, VersionKey> requirementRepository,
             IRepository<RequirementContent, LocaleKey> requirementContentRepository,
             IRepository<SpecificationItem, Guid> specificationItemRepository,
             IRepository<Package, Guid> packageRepository,
             ICurrentUserRetriever<User> currentUserRetriever,
             IRepository<Project, Guid> projectRepository,
             ICurrentProjectContextId currentProjectContextId,
             IQueryHandler<RequirementNextIdQuery, int> requirementNextIdQueryHandler
            )
        {
            _requirementRepository = requirementRepository;
            _requirementContentRepository = requirementContentRepository;
            _specificationItemRepository = specificationItemRepository;
            _packageRepository = packageRepository;
            _currentUserRetriever = currentUserRetriever;
            _projectRepository = projectRepository;
            _currentProjectContextId = currentProjectContextId;
            _requirementNextIdQueryHandler = requirementNextIdQueryHandler;
        }

        public void Handle(SaveRequirementCommand command)
        {
            var requirement = new Requirement();
            var currentUser = _currentUserRetriever.Get();
            requirement.CreatorId = currentUser.Id;
            requirement.Difficulty = command.Difficulty.Value;
            requirement.Id = Guid.NewGuid();
            requirement.IsLastVersion = true;
            requirement.Rank = command.Rank.Value;
            
            foreach (var item in command.Items)
            {
                var content = new RequirementContent();
                content.Action = item.Action;
                content.Subject = item.Subject;
                content.Condition = item.Condition;
                content.Id = requirement.Id;
                content.Locale = item.Locale;
                content.Version = 1;
                content.Requirement = requirement;
                _requirementContentRepository.Add(content);
            }
            
            var currentProjectId = _currentProjectContextId.Get();
            if (currentProjectId == null)
                throw new Exception(Sentences.noProjectInContext);

            var project = _projectRepository.Get(currentProjectId.Value);

            var nextIdQuery = new RequirementNextIdQuery
            {
                ProjectId = project.Id,
                RequirementType = command.RequirementType
            };

            var identifier = _requirementNextIdQueryHandler.Handle(nextIdQuery);

            var specificationItem = new SpecificationItem();
            specificationItem.Active = true;
            specificationItem.Id = requirement.Id;
            specificationItem.Type = SpecificationItemType.Requirement;
            var prefix = RequirementViewModel.GetPrefixFromType(command.RequirementType.Value);
            specificationItem.Label = $"{prefix}{identifier}";

            var package = _packageRepository.Get(command.PackageId.Value);
            specificationItem.Package = package;
            
            requirement.SpecificationItem = specificationItem;
            requirement.Type = command.RequirementType.Value;
            requirement.Creator = currentUser;
            requirement.Version = 1;
            requirement.Project = project;

            requirement.Identifier = identifier;

            _specificationItemRepository.Add(specificationItem);
            _requirementRepository.Add(requirement);
        }
    }
}
