using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class ProjectContentItem
    {
        [Required]
        public string Locale { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(Sentences),
         ErrorMessageResourceName = "nameIsARequiredField")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(65535)]
        [Required]
        public string Description { get; set; }
    }
}
