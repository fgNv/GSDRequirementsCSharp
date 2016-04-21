using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class RequirementContentItem
    {
        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(150)]
        public string Condition { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        public string Locale { get; set; }
    }
}
