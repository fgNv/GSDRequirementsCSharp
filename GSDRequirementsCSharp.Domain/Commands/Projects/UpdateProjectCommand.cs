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

        public UpdateProjectCommand()
        {

        }

        public UpdateProjectCommand(Guid id, SaveProjectCommand command)
        {
            Id = id;
            Items = command.Items;
        }
    }
    
}
