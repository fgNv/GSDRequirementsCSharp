using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class CreateIssueCommentCommand : IProjectCollaboratorCommand
    {
        [Required]
        public Guid? IssueId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueCommentContentItem> Contents { get; set; }
    }
}
