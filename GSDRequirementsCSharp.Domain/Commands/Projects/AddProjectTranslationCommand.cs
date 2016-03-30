using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class AddProjectTranslationCommand : ICommand
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
