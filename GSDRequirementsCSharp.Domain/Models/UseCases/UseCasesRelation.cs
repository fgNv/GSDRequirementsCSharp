using GSDRequirementsCSharp.Infrastructure.Persistence;
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
    public class UseCasesRelation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public UseCase Target { get; set; }
        
        public UseCase Source { get; set; }

        public UseCasesRelationType Type { get; set; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }

        public Guid UseCaseDiagramId { get; set; }

        public Guid SourceId { get; set; }

        public Guid TargetId { get; set; }
    }
}
