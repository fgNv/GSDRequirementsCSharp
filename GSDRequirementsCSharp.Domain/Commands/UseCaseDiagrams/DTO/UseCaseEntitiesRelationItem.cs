using GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
