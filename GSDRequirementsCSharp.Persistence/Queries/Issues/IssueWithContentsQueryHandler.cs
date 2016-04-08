using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Issue;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Issues
{
    class IssueWithContentsQueryHandler : IQueryHandler<IssueWithContentsQuery, Issue>
    {
        private readonly GSDRequirementsContext _context;

        public IssueWithContentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Issue Handle(IssueWithContentsQuery query)
        {
            return _context.Issues
                           .Include(i => i.Contents)
                           .FirstOrDefault(i => i.Id == query.IssueId);
        }
    }
}
