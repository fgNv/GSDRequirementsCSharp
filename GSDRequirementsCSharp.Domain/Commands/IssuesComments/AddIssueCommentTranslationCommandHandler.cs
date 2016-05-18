using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class AddIssueCommentTranslationCommandHandler : ICommandHandler<AddIssueCommentTranslationCommand>
    {
        private readonly IQueryHandler<IssueCommentWithContentsQuery, IssueComment> _issueCommentWithContentsQueryHandler;
        private readonly IRepository<IssueCommentContent, LocaleKey> _issueCommentContentRepository;

        public AddIssueCommentTranslationCommandHandler(IQueryHandler<IssueCommentWithContentsQuery, IssueComment> issueCommentWithContentsQueryHandler,
                                                        IRepository<IssueCommentContent, LocaleKey> issueCommentContentRepository)
        {
            _issueCommentWithContentsQueryHandler = issueCommentWithContentsQueryHandler;
            _issueCommentContentRepository = issueCommentContentRepository;
        }

        public void Handle(AddIssueCommentTranslationCommand command)
        {
            var issueComment = _issueCommentWithContentsQueryHandler.Handle(command.IssueCommentId);
            foreach (var item in command.Contents)
            {
                var content = issueComment.Contents.FirstOrDefault(c => c.Locale == item.Locale);

                if (content != null && content.IsUpdated)
                    continue;

                if (content == null)
                {
                    content = new IssueCommentContent();
                    content.IssueComment = issueComment;
                    content.Locale = item.Locale;
                    _issueCommentContentRepository.Add(content);
                }

                content.IsUpdated = true;
                content.Description = item.Description;
            }
        }
    }
}
