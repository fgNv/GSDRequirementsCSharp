using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Issue
{
    public class IssueCommentWithContentsQuery
    {
        public Guid IssueCommentId { get; set; }
        public static implicit operator IssueCommentWithContentsQuery(Guid id)
        {
            return new IssueCommentWithContentsQuery { IssueCommentId = id };
        }
    }
}
