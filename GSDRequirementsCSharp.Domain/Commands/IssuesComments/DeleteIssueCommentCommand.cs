using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class DeleteIssueCommentCommand : ICommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.issueCommentIdIsARequiredField))]
        public Guid? IssueCommentId { get; set; }

        public static implicit operator DeleteIssueCommentCommand(Guid id)
        {
            return new DeleteIssueCommentCommand { IssueCommentId = id };
        }
    }
}
