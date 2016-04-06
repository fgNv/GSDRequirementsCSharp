using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class SaveRequirementCommand : IProjectCommand
    {
        [Required]
        public Guid PackageId { get; set; }

        [ValidateCollection]
        public IEnumerable<RequirementContentItem> Items { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        public RequirementType RequirementType { get; set; }
    }
}
