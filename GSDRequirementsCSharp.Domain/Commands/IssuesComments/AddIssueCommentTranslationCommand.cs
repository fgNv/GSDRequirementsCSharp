using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class AddIssueCommentTranslationCommand : IProjectCollaboratorCommand
    {
        public Guid? IssueCommentId { get; set; }
        public IEnumerable<IssueCommentContentItem> Contents { get; set; }
    }
}
