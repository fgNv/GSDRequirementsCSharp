using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UpdateProjectCommand : IProjectOwnerCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.projectIdIsARequiredField))]
        public Guid? Id { get; set; }

        [Required]
        public Guid? ProjectId { get { return Id.Value; } }

        [ValidateCollection]
        public IEnumerable<ProjectContentItem> Items { get; set; }        
    }
}
