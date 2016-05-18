using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class RemoveClassDiagramCommand : IProjectCommand
    {
        public Guid Id { get; set; }
        
    }
}
