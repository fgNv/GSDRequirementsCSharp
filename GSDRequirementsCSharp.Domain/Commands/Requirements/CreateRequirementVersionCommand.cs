using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.ComponentModel.DataAnnotations;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    [CommandDescription(nameof(Sentences.newRequirementVersionCreated))]
    public class CreateRequirementVersionCommand : SaveRequirementCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.requirementIdIsARequiredField))]
        public Guid? Id { get; set; }
    }
}
