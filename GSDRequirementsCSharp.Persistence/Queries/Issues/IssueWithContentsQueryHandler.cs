using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Data.Entity;
using System.Linq;

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
