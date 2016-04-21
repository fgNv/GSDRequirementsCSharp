using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class UpdateIssueCommand : IProjectCollaboratorCommand
    {
        public Guid IssueId { get; set; }
        
        public IEnumerable<IssueContentItem> Contents { get; set; }
    }
}
