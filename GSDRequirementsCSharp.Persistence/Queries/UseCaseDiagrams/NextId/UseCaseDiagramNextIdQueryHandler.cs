using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.NextId
{
    class UseCaseDiagramNextIdQueryHandler : IQueryHandler<UseCaseDiagramNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseDiagramNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(UseCaseDiagramNextIdQuery query)
        {
            var dbQuery = _context.UseCaseDiagrams
                                .Where(r => r.ProjectId == query.ProjectId);
            var ae = dbQuery.ToList();
            if (!dbQuery.Any())
                return 1;

            var previousIdentifier = dbQuery.Max(r => r.Identifier);
            return previousIdentifier + 1;
        }
    }
}
