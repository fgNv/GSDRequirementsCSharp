using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class DeleteIssueCommentCommandHandler : ICommandHandler<DeleteIssueCommentCommand>
    {
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<IssueComment, Guid> _issueCommentRepository;
        
        public DeleteIssueCommentCommandHandler(ICurrentUserRetriever<User> currentUserRetriever,
                                                IRepository<IssueComment, Guid> issueCommentRepository)
        {
            _currentUserRetriever = currentUserRetriever;
            _issueCommentRepository = issueCommentRepository;
        }

        public void Handle(DeleteIssueCommentCommand command)
        {
            var issueComment = _issueCommentRepository.Get(command.IssueCommentId.Value);
            issueComment.Active = false;
        }
    }
}
