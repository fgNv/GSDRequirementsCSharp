using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class InactivateProjectCommand : IProjectOwnerCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.projectIdIsARequiredField))]
        public Guid? Id { get; set; }

        [Required]
        public Guid? ProjectId { get { return Id; } }

        public static implicit operator InactivateProjectCommand(Guid id)
        {
            return new InactivateProjectCommand { Id = id };
        }
    }
}
