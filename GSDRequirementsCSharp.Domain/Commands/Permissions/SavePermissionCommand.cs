using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Permissions
{
    public class SavePermissionCommand : IProjectCommand
    {
        [ValidateCollection]
        public IEnumerable<PermissionItem> Items { get; set; }
    }
}
