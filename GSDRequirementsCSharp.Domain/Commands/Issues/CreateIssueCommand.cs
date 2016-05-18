using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.issueAdded))]
    public class CreateIssueCommand : IProjectCollaboratorCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.specificationItemIsARequiredField))]
        public Guid? SpecificationItemId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueContentItem> Contents { get; set; }
    }
}
