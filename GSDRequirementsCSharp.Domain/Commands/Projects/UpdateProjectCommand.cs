using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Persistence.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class UpdateProjectCommand : SaveProjectCommand
    {
        public Guid Id { get; set; }

        public UpdateProjectCommand(Guid id, SaveProjectCommand command)
        {
            this.Id = id;
            this.Name = command.Name;
            this.Description = command.Description;
        }
    }
    
}
