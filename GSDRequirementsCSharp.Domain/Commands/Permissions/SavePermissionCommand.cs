using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Permissions
{
    public class SavePermissionCommand : ICommand
    {
        public IEnumerable<PermissionItem> Items { get; set; }
    }
}
