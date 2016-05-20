using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UpdateIssueCommentCommandHandler : ICommandHandler<UpdateIssueCommentCommand>
    {
        private readonly IQueryHandler<IssueCommentWithContentsQuery, IssueComment> _issueCommentWithContentsQueryHandler;
        private readonly IRepository<IssueCommentContent, LocaleKey> _issueCommentContentRepository;

        public UpdateIssueCommentCommandHandler(IQueryHandler<IssueCommentWithContentsQuery, IssueComment> issueCommentWithContentsQueryHandler,
                                                        IRepository<IssueCommentContent, LocaleKey> issueCommentContentRepository)
        {
            _issueCommentWithContentsQueryHandler = issueCommentWithContentsQueryHandler;
            _issueCommentContentRepository = issueCommentContentRepository;
        }

        public void Handle(UpdateIssueCommentCommand command)
        {
            var issueComment = _issueCommentWithContentsQueryHandler.Handle(command.IssueCommentId);
            foreach (var content in issueComment.Contents)
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
                if (issueComment.Contents.Any(p => p.Locale == item.Locale))
                    continue;

                var content = new IssueCommentContent();
                content.Id = issueComment.Id;
                content.IsUpdated = true;
                content.Locale = item.Locale;
                content.Description = item.Description;
                content.IssueComment = issueComment;

                _issueCommentContentRepository.Add(content);
            }
            
            issueComment.LastModification = DateTime.Now;
        }
    }
}
