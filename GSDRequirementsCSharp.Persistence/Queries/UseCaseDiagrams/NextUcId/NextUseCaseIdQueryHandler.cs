using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.NextUcId
{
    class NextUseCaseIdQueryHandler : IQueryHandler<UseCaseNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public NextUseCaseIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(UseCaseNextIdQuery query)
        {
            var dbQuery = _context.UseCases
                               .Where(r => r.ProjectId == query.ProjectId);
            var ae = dbQuery.ToList();
            if (!dbQuery.Any())
                return 1;

            var previousIdentifier = dbQuery.Max(r => r.Identifier);
            return previousIdentifier + 1;
        }
    }
}
