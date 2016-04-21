using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Persistence.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class UpdateProjectCommand : IProjectOwnerCommand
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get { return Id; } }

        public IEnumerable<ProjectContentItem> Items { get; set; }        
    }
}
