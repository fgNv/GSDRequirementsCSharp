using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Issues.BySpecificationItem
{
    class SpecificationItemIssuesQueryHandler : IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<Issue>>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemIssuesQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<Issue> Handle(SpecificationItemIssuesQuery query)
        {
            return _context.Issues
                           .Include(i => i.IssueComments)
                           .Where(i => i.SpecificationItemId == query.SpeficiationItemId)
                           .OrderByDescending(i => i.LastModification)
                           .ToList();
        }
    }
}
