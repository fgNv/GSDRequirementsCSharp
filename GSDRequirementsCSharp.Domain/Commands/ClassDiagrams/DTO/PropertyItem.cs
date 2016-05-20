using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class PropertyItem
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.propertyNameIsARequiredField))]
        public string Name { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.propertyTypeIsARequiredField))]
        public string Type { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.propertyVisibilityIsARequiredField))]
        public Visibility? Visibility { get; set; }
    }
}
