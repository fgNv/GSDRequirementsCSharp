using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class UpdateIssueCommentCommand : IProjectCollaboratorCommand
    {
        [Required]
        public Guid? IssueCommentId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueCommentContentItem> Contents { get; set; }
    }
}
