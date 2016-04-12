using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Issues
{
    public class IssueContentItem
    {
        [Required]
        [MaxLength(10)]
        public string Locale { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
