using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Permissions
{
    public class PermissionItem
    {
        public int UserId { get; set; }
        public Profile Profile { get; set; }
    }
}
