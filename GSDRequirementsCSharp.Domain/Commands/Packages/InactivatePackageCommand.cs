using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class InactivatePackageCommand : IProjectCommand
    {
        public Guid Id { get; set; }

        public static implicit operator InactivatePackageCommand(Guid id)
        {
            return new InactivatePackageCommand { Id = id };
        }
    }
}
