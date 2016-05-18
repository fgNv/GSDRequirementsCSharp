using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class IssueNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator IssueNextIdQuery(Guid projectId)
        {
            return new IssueNextIdQuery { ProjectId = projectId };
        }
    }
}
