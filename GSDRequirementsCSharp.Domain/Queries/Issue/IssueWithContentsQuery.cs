using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Issue
{
    public class IssueWithContentsQuery
    {
        public Guid IssueId { get; set; }
        public static implicit operator IssueWithContentsQuery(Guid id)
        {
            return new IssueWithContentsQuery { IssueId = id };
        }
    }
}
