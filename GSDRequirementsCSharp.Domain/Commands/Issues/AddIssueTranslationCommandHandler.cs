using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Issue;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class AddIssueTranslationCommandHandler : ICommandHandler<AddIssueTranslationCommand>
    {
        private readonly IQueryHandler<IssueWithContentsQuery, Issue> _issueWithContentsQueryHandler;
        private readonly IRepository<IssueContent, LocaleKey> _issueContentRepository;

        public AddIssueTranslationCommandHandler(IQueryHandler<IssueWithContentsQuery, Issue> issueWithContentsQueryHandler,
                                                 IRepository<IssueContent, LocaleKey> issueContentRepository)
        {
            _issueWithContentsQueryHandler = issueWithContentsQueryHandler;
            _issueContentRepository = issueContentRepository;
        }

        public void Handle(AddIssueTranslationCommand command)
        {
            var issue = _issueWithContentsQueryHandler.Handle(command.IssueId);
            foreach (var item in command.Contents)
            {
                var content = issue.Contents.FirstOrDefault(c => c.Locale == item.Locale);

                if (content != null && content.IsUpdated)
                    continue;

                if (content == null)
                {
                    content = new IssueContent();
                    content.Issue = issue;
                    content.Locale = item.Locale;
                    _issueContentRepository.Add(content);
                }

                content.IsUpdated = true;
                content.Description = item.Description;
            }
        }
    }
}
