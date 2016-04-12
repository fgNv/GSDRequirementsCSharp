using GSDRequirementsCSharp.Domain.Queries.Issue;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Issues
{
    class IssueNextIdQueryHandler : IQueryHandler<IssueNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public IssueNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(IssueNextIdQuery query)
        {
            var dbQuery = _context.Issues
                                  .Where(p => p.ProjectId == query.ProjectId);

            if (!dbQuery.Any())
                return 1;

            var previousId = dbQuery.Max(p => p.Identifier);
            return previousId + 1;
        }
    }
}
