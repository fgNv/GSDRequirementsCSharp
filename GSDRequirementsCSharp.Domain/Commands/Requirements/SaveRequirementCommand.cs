using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.requirementCreated))]
    public class SaveRequirementCommand : IProjectCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.packageIsARequiredField))]
        public Guid? PackageId { get; set; }

        [ValidateCollection]
        public IEnumerable<RequirementContentItem> Items { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.difficultyIsARequiredField))]
        public Difficulty? Difficulty { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.rankIsARequiredField))]
        public int? Rank { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.requirementTypeIsARequiredField))]
        public RequirementType? RequirementType { get; set; }
    }
}
