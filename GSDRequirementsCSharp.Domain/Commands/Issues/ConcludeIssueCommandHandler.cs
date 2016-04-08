using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class ConcludeIssueCommandHandler : ICommandHandler<ConcludeIssueCommand>
    {
        private readonly IRepository<Issue, Guid> _issueRepository;

        public ConcludeIssueCommandHandler(IRepository<Issue, Guid> issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public void Handle(ConcludeIssueCommand command)
        {
            var issue = _issueRepository.Get(command.IssueId);
            issue.Concluded = true;
            issue.ConcludedAt = DateTime.Now;
            issue.LastModification = DateTime.Now;
        }
    }
}
