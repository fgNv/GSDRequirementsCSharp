using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    [CommandDescription(nameof(Sentences.newClassDiagramVersionAdded))]
    public class CreateClassDiagramNewVersionCommand : CreateClassDiagramCommand
    {
        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.classDiagramIdIsRequiredField))]
        public Guid? Id { get; set; }
    }
}
