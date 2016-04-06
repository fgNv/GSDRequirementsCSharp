using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class AddRequirementTranslationCommandHandler : IProjectCommandHandler<AddRequirementTranslationCommand>
    {
        private readonly IQueryHandler<LastVersionRequirementQuery, Requirement> _lastVersionRequirementQueryHandler;
        private readonly IRepository<RequirementContent, LocaleKey> _requirementContentRepository;

        public AddRequirementTranslationCommandHandler(IQueryHandler<LastVersionRequirementQuery, Requirement> lastVersionRequirementQuery,
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
