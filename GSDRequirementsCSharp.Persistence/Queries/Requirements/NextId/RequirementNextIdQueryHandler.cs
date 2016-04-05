using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Requirements.NextId
{
    internal class RequirementNextIdQueryHandler : IQueryHandler<RequirementNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public RequirementNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(RequirementNextIdQuery query)
        {
            var dbQuery = _context.Requirements
                                  .Where(r => r.ProjectId == query.ProjectId &&
                                              r.Type == query.RequirementType);
            if (!dbQuery.Any())
                return 1;

            var previousIdentifier = dbQuery.Max(r => r.Identifier);
            return previousIdentifier + 1;
        }
    }
}
