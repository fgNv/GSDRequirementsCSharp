using GSDRequirementsCSharp.Domain.Queries.Projects;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Projects.NextId
{
    internal class ProjectNextIdQueryHandler : IQueryHandler<ProjectNextIdQuery, int>
    {
        private readonly GSDRequirementsContext _context;

        public ProjectNextIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public int Handle(ProjectNextIdQuery query)
        {
            var dbQuery = _context.Projects.Where(p => p.CreatorId == query.UserId);

            if (!dbQuery.Any())
                return 1;

            var previousId = dbQuery.Max(p => p.Identifier);

            return previousId + 1;
        }
    }
}
