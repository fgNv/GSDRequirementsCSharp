using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Persistence.Queries.Projects.ProjectsPaginated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class ProjectsPaginatedQueryResult
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public int MaxPages { get; set; }

        public ProjectsPaginatedQueryResult(IEnumerable<ProjectViewModel> projects,
                                            int maxPages)
        {
            Projects = projects;
            MaxPages = maxPages;
        }
    }
}
