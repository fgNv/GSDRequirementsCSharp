using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UpdateIssueCommandHandler : ICommandHandler<UpdateIssueCommand>
    {
        private readonly IQueryHandler<IssueWithContentsQuery, Issue> _issueWithContentsQueryHandler;
        private readonly IRepository<IssueContent, LocaleKey> _issueContentRepository;

        public UpdateIssueCommandHandler(IQueryHandler<IssueWithContentsQuery, Issue> issueWithContentsQueryHandler,
                                         IRepository<IssueContent, LocaleKey> issueContentRepository)
        {
            _issueWithContentsQueryHandler = issueWithContentsQueryHandler;
            _issueContentRepository = issueContentRepository;
        }

        public void Handle(UpdateIssueCommand command)
        {
            var issue = _issueWithContentsQueryHandler.Handle(command.IssueId);
            foreach (var content in issue.Contents)
            {
                if (command.Contents.Any(i => content.Locale == i.Locale))
                {
                    var item = command.Contents.FirstOrDefault(i => content.Locale == i.Locale);
                    content.IsUpdated = true;
                    content.Description = item.Description;
                }
                else
                {
                    content.IsUpdated = false;
                }
            }

            foreach (var item in command.Contents)
            {
                if (issue.Contents.Any(p => p.Locale == item.Locale))
                    continue;

                var content = new IssueContent();
                content.Id = issue.Id;
                content.IsUpdated = true;
                content.Locale = item.Locale;
                content.Description = item.Description;
                content.Issue = issue;

                _issueContentRepository.Add(content);
            }
            
            issue.LastModification = DateTime.Now;
        }
    }
}
