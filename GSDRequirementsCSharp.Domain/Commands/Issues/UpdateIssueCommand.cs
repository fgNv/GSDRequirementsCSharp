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
    public class UpdateIssueCommand : IProjectCollaboratorCommand
    {
        [Required]
        public Guid? IssueId { get; set; }

        [ValidateCollection]
        public IEnumerable<IssueContentItem> Contents { get; set; }
    }
}
