using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class UpdatePackageCommand : SavePackageCommand
    {
        public Guid Id { get; set; }
    }
}
