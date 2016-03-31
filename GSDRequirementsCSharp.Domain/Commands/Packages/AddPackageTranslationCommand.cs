using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class AddPackageTranslationCommand : ICommand
    {
        [Required]
        [MaxLength(65535)]
        public string Description { get; set; }

        [Required]
        public Guid PackageId { get; set; }
        
    }
}
