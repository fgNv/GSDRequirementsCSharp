using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Projects
{
    public class ProjectWithCurrentCultureContentsQueryHandler : IQueryHandler<Guid, ProjectWithCurrentCultureContentsQueryResult>
    {
        private readonly GSDRequirementsContext _context;

        public ProjectWithCurrentCultureContentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public ProjectWithCurrentCultureContentsQueryResult Handle(Guid projectId)
        {
            var currentLocale = Thread.CurrentThread.CurrentCulture.Name;
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            var content = _context.ProjectContents.FirstOrDefault(cp => cp.Project.Id == projectId &&
                                                                        cp.Locale == currentLocale);
            var result = new ProjectWithCurrentCultureContentsQueryResult();
            result.Project = project;
            result.ProjectContent = content;
            return result;
        }
    }
}
