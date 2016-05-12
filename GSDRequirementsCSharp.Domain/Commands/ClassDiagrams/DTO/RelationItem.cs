using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class RelationItem
    {
        [Required(
         ErrorMessageResourceType =typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.sourceIsARequiredField))]
        public Guid? SourceId { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.targetIsARequiredField))]
        public Guid? TargetId { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.typeIsARequiredField))]
        public RelationType? Type { get; set; }
        
        public string TargetMultiplicity { get; set; }
        
        public string SourceMultiplicity { get; set; }
    }
}
