using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO
{
    public class UseCaseContentItem
    {
        [Required(
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = nameof(ValidationMessages.useCaseNameIsARequiredField))]
        public string Name { get; set; }

        [Required(
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = nameof(ValidationMessages.useCaseDescriptionIsARequiredField))]
        public string Description { get; set; }
        
        public string Path { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.useCaseLocaleIsARequiredField))]
        public string Locale { get; set; }
    }
}
