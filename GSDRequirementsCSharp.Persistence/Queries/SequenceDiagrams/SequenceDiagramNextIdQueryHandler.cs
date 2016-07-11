using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    class SequenceDiagramNextIdQueryHandler : IQueryHandler<SequenceDiagramNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public SequenceDiagramNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(SequenceDiagramNextIdQuery query)
        {
            var dbQuery = _context.SequenceDiagrams
                                  .Where(r => r.ProjectId == query.ProjectId);
            if (!dbQuery.Any())
                return 1;

            return dbQuery.Max(s => s.Identifier) + 1;
        }
    }
}
