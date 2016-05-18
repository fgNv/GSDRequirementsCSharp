using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class MethodItem
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.methodNameIsARequiredField))]
        public string Name { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.returnTypeIsARequiredField))]
        public string ReturnType { get; set; }

        [ValidateCollection]
        public IEnumerable<ParameterItem> ClassMethodParameters { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.methodVisibilityIsARequiredField))]
        public Visibility? Visibility { get; set; }
    }
}
