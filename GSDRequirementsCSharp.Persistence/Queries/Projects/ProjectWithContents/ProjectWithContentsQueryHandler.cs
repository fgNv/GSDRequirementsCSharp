using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Domain.Queries.Projects
{
    internal class ProjectWithContentsQueryHandler : IQueryHandler<Guid, Project>
    {
        private readonly GSDRequirementsContext _context;

        public ProjectWithContentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Project Handle(Guid query)
        {
            return _context.Projects
                           .Include(p => p.ProjectContents)
                           .FirstOrDefault(p => p.Id == query);
        }
    }
}
