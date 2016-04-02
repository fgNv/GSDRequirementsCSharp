using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class PackageContentItem
    {
        [Required]
        public string Locale { get; set; }

        [Required]
        [MaxLength(65535)]
        public string Description { get; set; }
    }
}
