using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class CreateIssueCommentCommandHandler : ICommandHandler<CreateIssueCommentCommand>
    {
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<IssueComment, Guid> _issueCommentRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IRepository<IssueCommentContent, LocaleKey> _issueCommentContentRepository;

        public CreateIssueCommentCommandHandler(ICurrentUserRetriever<User> currentUserRetriever,
                                         IRepository<IssueComment, Guid> issueCommentRepository,
                                         ICurrentProjectContextId currentProjectContextId,
                                         IRepository<IssueCommentContent, LocaleKey> issueCommentContentRepository)
        {
            _currentUserRetriever = currentUserRetriever;
            _issueCommentRepository = issueCommentRepository;
            _currentProjectContextId = currentProjectContextId;
            _issueCommentContentRepository = issueCommentContentRepository;
        }

        public void Handle(CreateIssueCommentCommand command)
        {
            var creator = _currentUserRetriever.Get();

            var issueComment = new IssueComment();
            issueComment.Creator = creator;
            issueComment.IssueId = command.IssueId.Value;
            issueComment.Id = Guid.NewGuid();

            foreach (var item in command.Contents)
            {
                var content = new IssueCommentContent();
                content.Description = item.Description;
                content.Locale = item.Locale;
                content.IssueComment = issueComment;
                content.IsUpdated = true;
                _issueCommentContentRepository.Add(content);
            }

            issueComment.LastModification = DateTime.Now;

            var currentProjectId = _currentProjectContextId.Get();

            if (currentProjectId == null)
                throw new Exception(Sentences.noProjectInContext);

            issueComment.Active = true;
            issueComment.CreatedAt = DateTime.Now;
            _issueCommentRepository.Add(issueComment);
        }
    }
}
