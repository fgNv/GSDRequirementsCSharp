using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class CreateIssueCommand : IProjectCollaboratorCommand
    {
        [Required]
        public Guid? SpecificationItemId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueContentItem> Contents { get; set; }
    }
}
