using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class ProjectWithCurrentCultureContentsQueryResult
    {
        public Project Project { get; set; }
        public ProjectContent ProjectContent { get; set; }
    }
}
