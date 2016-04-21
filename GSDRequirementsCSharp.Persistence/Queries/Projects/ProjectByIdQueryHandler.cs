using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Projects
{
    class ProjectByIdQueryHandler : IQueryHandler<Guid, ProjectViewModel>
    {
        private GSDRequirementsContext _context;

        public ProjectByIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public ProjectViewModel Handle(Guid id)
        {
            var project = _context.Projects
                                  .Include(p => p.ProjectContents)
                                  .Include(p => p.Packages.Select(pk => pk.Contents))
                                  .FirstOrDefault(p => p.Id == id);

            if (project == null)
                return null;

            return ProjectViewModel.FromModel(project);
        }
    }
}
