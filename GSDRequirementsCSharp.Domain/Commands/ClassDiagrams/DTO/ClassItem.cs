using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using GSDRequirementsCSharp.Infrastructure.Internationalization;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class ClassItem
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.nameIsARequiredField))]
        public string Name { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.classTypeIsARequiredField))]
        public ClassType? Type { get; set; }

        [ValidateCollection]
        public IEnumerable<MethodItem> ClassMethods { get; set; }

        [ValidateCollection]
        public IEnumerable<PropertyItem> ClassProperties { get; set; }
        
        public Cell Cell { get; set; }
    }
}
