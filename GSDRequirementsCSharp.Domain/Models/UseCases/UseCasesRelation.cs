using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    /// <summary>
    /// Used to describe relations between two Use Cases
    /// </summary>
    public class UseCasesRelation
    {
        public Guid Id { get; set; }

        [Required]
        public UseCase Target { get; set; }

        [Required]
        public UseCase Source { get; set; }

        public UseCasesRelationType Type { get; set; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }

        public Guid UseCaseDiagramId { get; set; }
    }
}
