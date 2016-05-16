using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    [CommandDescription(nameof(Sentences.issueConcluded))]
    public class ConcludeIssueCommand : IProjectCollaboratorCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.issueIdIsARequiredField))]
        public Guid? IssueId { get; set; }
    }
}
