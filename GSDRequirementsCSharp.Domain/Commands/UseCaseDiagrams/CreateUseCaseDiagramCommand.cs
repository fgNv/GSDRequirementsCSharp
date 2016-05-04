using GSDRequirementsCSharp.Infrastructure;
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
    public class CreateUseCaseDiagramCommand : IProjectCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.packageIsARequiredField))]
        public Guid? PackageId { get; set; }

        [ValidateCollection]
        [AtLeastOneElement(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.nameIsARequiredField))]
        public IEnumerable<UseCaseDiagramContentItem> Contents { get; set; }

        [ValidateCollection]
        public IEnumerable<UseCaseItem> UseCases { get; set; }

        [ValidateCollection]
        public IEnumerable<ActorItem> Actors { get; set; }

        [ValidateCollection]
        public IEnumerable<UseCasesRelationItem> UseCasesRelations { get; set; }

        [ValidateCollection]
        public IEnumerable<UseCaseEntitiesRelationItem> UseCaseEntitiesRelations { get; set; }
    }
}
