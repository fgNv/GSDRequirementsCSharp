using GSDRequirementsCSharp.Domain.ViewModels;
using System.Collections.Generic;

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
