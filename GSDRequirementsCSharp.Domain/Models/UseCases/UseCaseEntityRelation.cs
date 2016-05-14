using GSDRequirementsCSharp.Domain.Models.UseCases;
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
    /// Used to describe relations between two Actors or one Actor and one Use Case
    /// </summary>
    public class UseCaseEntityRelation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public UseCaseEntity Target { get; set; }
        
        public UseCaseEntity Source { get; set; }

        public int SourceVersion { get; set; }
        public int TargetVersion { get; set; }
        public Guid SourceId { get; set; }
        public Guid TargetId { get; set; }

        public ICollection<UseCaseEntityRelationContent> Contents { get; set; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }

        public Guid UseCaseDiagramId { get; set; }
        
        public UseCaseEntityRelation()
        {
            Contents = new HashSet<UseCaseEntityRelationContent>();
        }
    }
}
