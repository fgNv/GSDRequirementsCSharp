using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Permissions
{
    public class PermissionItem
    {
        [Required]
        public int? UserId { get; set; }

        [Required]
        public Profile? Profile { get; set; }
    }
}
