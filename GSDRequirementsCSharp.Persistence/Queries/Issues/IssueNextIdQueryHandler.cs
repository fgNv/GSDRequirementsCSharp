using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

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
