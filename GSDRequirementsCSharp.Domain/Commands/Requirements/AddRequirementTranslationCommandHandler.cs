﻿using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class AddRequirementTranslationCommandHandler : ICommandHandler<AddRequirementTranslationCommand>
    {
        private readonly IQueryHandler<DetailedRequirementQuery, Requirement> _lastVersionRequirementQueryHandler;
        private readonly IRepository<RequirementContent, LocaleKey> _requirementContentRepository;

        public AddRequirementTranslationCommandHandler(IQueryHandler<DetailedRequirementQuery, Requirement> lastVersionRequirementQuery,
                            IRepository<RequirementContent, LocaleKey> requirementContentRepository)
        {
            _lastVersionRequirementQueryHandler = lastVersionRequirementQuery;
            _requirementContentRepository = requirementContentRepository;
        }

        public void Handle(AddRequirementTranslationCommand command)
        {
            var requirement = _lastVersionRequirementQueryHandler.Handle(command.Id);

            foreach (var item in command.Items)
            {
                var content = new RequirementContent();
                content.Action = item.Action;
                content.Subject = item.Subject;
                content.Version = requirement.Version;
                content.Condition = item.Condition;
                content.Id = requirement.Id;
                content.Locale = item.Locale;
                content.Requirement = requirement;
                _requirementContentRepository.Add(content);
            }
        }
    }
}
