using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class ProjectsPaginatedQueryResult
    {
        public IEnumerable<Project> Projects { get; set; }
        public int MaxPages { get; set; }

        public ProjectsPaginatedQueryResult(IEnumerable<Project> projects,
                                            int maxPages)
        {
            Projects = projects;
            MaxPages = maxPages;
        }
    }
}
