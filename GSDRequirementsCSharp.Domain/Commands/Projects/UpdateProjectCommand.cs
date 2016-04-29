using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using GSDRequirementsCSharp.Persistence.Commands.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class UpdateProjectCommand : IProjectOwnerCommand
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public Guid? ProjectId { get { return Id.Value; } }

        [ValidateCollection]
        public IEnumerable<ProjectContentItem> Items { get; set; }        
    }
}
