using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UseCaseEntitiesRelationItem
    {
        [Required(
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = nameof(ValidationMessages.sourceIsARequiredField))]
        public Guid? SourceId { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.targetIsARequiredField))]
        public Guid? TargetId { get; set; }

        [ValidateCollection]
        public IEnumerable<UseCaseEntitiesRelationContent> Contents { get; set; }

    }
}
