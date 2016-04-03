using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Projects.ProjectsPaginated
{
    public class ProjectViewModel
    {
        public IEnumerable<ProjectContent> ProjectContents { get; set; }
        public string Name { get; set; }
        public bool IsCurrentUserOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public string Identifier { get; set; }
    }
}
