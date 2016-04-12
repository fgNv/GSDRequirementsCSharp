using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class CreateIssueCommentCommand : IProjectCollaboratorCommand
    {
        public Guid? IssueId { get; set; }
        public IEnumerable<IssueCommentContentItem> Contents { get; set; }
    }
}
