using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class ConcludeIssueCommand : IProjectCollaboratorCommand
    {
        [Required]
        public Guid? IssueId { get; set; }
    }
}
