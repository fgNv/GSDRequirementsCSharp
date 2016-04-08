using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Issue;
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

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class CreateIssueCommandHandler : ICommandHandler<CreateIssueCommand>
    {
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<Issue, Guid> _issueRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IQueryHandler<IssueNextIdQuery, int> _issueNextIdQueryHandler;
        private readonly IRepository<IssueContent, LocaleKey> _issueContentRepository;

        public CreateIssueCommandHandler(ICurrentUserRetriever<User> currentUserRetriever,
                                         IRepository<Issue, Guid> issueRepository,
                                         ICurrentProjectContextId currentProjectContextId,
                                         IQueryHandler<IssueNextIdQuery, int> issueNextIdQueryHandler,
                                         IRepository<IssueContent, LocaleKey> issueContentRepository)
        {
            _currentUserRetriever = currentUserRetriever;
            _issueRepository = issueRepository;
            _currentProjectContextId = currentProjectContextId;
            _issueNextIdQueryHandler = issueNextIdQueryHandler;
            _issueContentRepository = issueContentRepository;
        }

        public void Handle(CreateIssueCommand command)
        {
            var creator = _currentUserRetriever.Get();

            var issue = new Issue();
            issue.Creator = creator;
            issue.SpecificationItemId = command.SpecificationItemId.Value;
            issue.Id = Guid.NewGuid();

            foreach (var item in command.Contents)
            {
                var content = new IssueContent();
                content.Description = item.Description;
                content.Locale = item.Locale;
                content.Issue = issue;
                content.IsUpdated = true;
                _issueContentRepository.Add(content);
            }

            issue.LastModification = DateTime.Now;

            var currentProjectId = _currentProjectContextId.Get();

            if (currentProjectId == null)
                throw new Exception(Sentences.noProjectInContext);

            issue.Identifier = _issueNextIdQueryHandler.Handle(currentProjectId);
            issue.ProjectId = currentProjectId.Value;
            issue.Concluded = false;
            issue.CreatedAt = DateTime.Now;

            _issueRepository.Add(issue);
        }
    }
}
