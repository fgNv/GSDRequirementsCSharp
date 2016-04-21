using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class InactivateProjectCommand : IProjectOwnerCommand
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get { return Id; } }

        public static implicit operator InactivateProjectCommand(Guid id)
        {
            return new InactivateProjectCommand { Id = id };
        }
    }
}
