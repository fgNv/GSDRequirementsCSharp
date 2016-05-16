using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    [CommandDescription(nameof(Sentences.issueCommentEdited))]
    public class UpdateIssueCommentCommand : IProjectCollaboratorCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.issueCommentIdIsARequiredField))]
        public Guid? IssueCommentId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueCommentContentItem> Contents { get; set; }
    }
}
