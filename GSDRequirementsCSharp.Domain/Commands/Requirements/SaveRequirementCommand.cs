using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class SaveRequirementCommand : ICommand
    {
        [Required]
        public Guid PackageId { get; set; }

        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(150)]
        public string Condition { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        public RequirementType RequirementType { get; set; }
    }
}
