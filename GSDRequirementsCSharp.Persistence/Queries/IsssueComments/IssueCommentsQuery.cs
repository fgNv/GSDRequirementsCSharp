using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.IsssueComments
{
    public class IssueCommentsQuery 
    {
        public Guid IssueId { get; set; }

        public static implicit operator IssueCommentsQuery(Guid issueId)
        {
            return new IssueCommentsQuery { IssueId = issueId };
        }
    }
}
