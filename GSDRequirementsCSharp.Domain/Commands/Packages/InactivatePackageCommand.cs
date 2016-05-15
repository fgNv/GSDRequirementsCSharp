using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    [CommandDescription(nameof(Sentences.packageRemoved))]
    public class InactivatePackageCommand : IProjectCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.packageIdIsARequiredField))]
        public Guid? Id { get; set; }

        public static implicit operator InactivatePackageCommand(Guid id)
        {
            return new InactivatePackageCommand { Id = id };
        }
    }
}
