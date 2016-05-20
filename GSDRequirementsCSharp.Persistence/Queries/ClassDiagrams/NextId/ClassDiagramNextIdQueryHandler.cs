using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.NextId
{
    class ClassDiagramNextIdQueryHandler : IQueryHandler<ClassDiagramNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public ClassDiagramNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(ClassDiagramNextIdQuery query)
        {
            var dbQuery = _context.ClassDiagrams
                                  .Where(r => r.ProjectId == query.ProjectId);
            var ae = dbQuery.ToList();
            if (!dbQuery.Any())
                return 1;

            var previousIdentifier = dbQuery.Max(r => r.Identifier);
            return previousIdentifier + 1;
        }
    }
}
