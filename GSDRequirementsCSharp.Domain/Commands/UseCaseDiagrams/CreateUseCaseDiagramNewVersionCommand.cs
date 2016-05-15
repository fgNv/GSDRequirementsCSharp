using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.newUseCaseDiagramVersionCreated))]
    public class CreateUseCaseDiagramNewVersionCommand : CreateUseCaseDiagramCommand
    {
        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.useCaseDiagramIdIsRequiredField))]
        public Guid? Id { get; set; }
    }
}
