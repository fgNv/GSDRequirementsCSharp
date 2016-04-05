using GSDRequirementsCSharp.Domain.Queries.Packages;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    internal class PackageNextIdQueryHandler : IQueryHandler<PackageNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public PackageNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(PackageNextIdQuery query)
        {
            var dbQuery = _context.Packages
                                  .Where(p => p.ProjectId == query.ProjectId);

            if (!dbQuery.Any())
                return 1;

            var previousId = dbQuery.Max(p => p.Identifier);
            return previousId + 1;
        }
    }
}
