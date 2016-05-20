using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class PermissionItem
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.userIdIsARequiredField))]
        public int? UserId { get; set; }

        [Required]
        public Profile? Profile { get; set; }
    }
}
