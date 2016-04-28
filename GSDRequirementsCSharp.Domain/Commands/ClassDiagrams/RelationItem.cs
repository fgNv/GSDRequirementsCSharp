using GSDRequirementsCSharp.Domain.Models;
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
        [Required]
        public Guid? SourceId { get; set; }

        [Required]
        public Guid? TargetId { get; set; }

        [Required]
        public RelationType? Type { get; set; }

        [Required]
        public string TargetMultiplicity { get; set; }

        [Required]
        public string SourceMultiplicity { get; set; }
    }
}
