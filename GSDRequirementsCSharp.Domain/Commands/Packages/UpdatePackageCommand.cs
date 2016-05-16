using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    [CommandDescription(nameof(Sentences.packageEdited))]
    public class UpdatePackageCommand : SavePackageCommand
    {
        [Required]
        public Guid? Id { get; set; }
    }
}
