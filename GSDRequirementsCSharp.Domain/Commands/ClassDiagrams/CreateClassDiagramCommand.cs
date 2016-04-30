using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class CreateClassDiagramCommand : IProjectCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.packageIsARequiredField))]
        public Guid? PackageId { get; set; }

        [ValidateCollection]
        public IEnumerable<ClassDiagramContentItem> Contents { get; set; }

        [ValidateCollection]
        public IEnumerable<ClassItem> Classes { get; set; }

        [ValidateCollection]
        public IEnumerable<RelationItem> Relations { get; set; }
    }
}
