using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class UseCaseDiagram : IEntity<VersionKey>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]        
        public int Version { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }

        public Guid SpecificationItemId { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }

        public bool IsLastVersion { get; set; }

        public int Identifier { get; set; }

        public ICollection<UseCases.UseCaseEntity> Entities { get; set; }

        public ICollection<UseCaseEntityRelation> Relations { get; set; }

        public ICollection<UseCasesRelation> UseCaseRelations { get; set; }

        public ICollection<UseCaseDiagramContent> Contents { get; set; }

        VersionKey IEntity<VersionKey>.Id
        {
            get
            {
                return new VersionKey { Id = Id, Version = Version };
            }
        }

        public UseCaseDiagram()
        {
            Entities = new HashSet<UseCases.UseCaseEntity>();
            Relations = new HashSet<UseCaseEntityRelation>();
            Contents = new HashSet<UseCaseDiagramContent>();
            UseCaseRelations = new HashSet<UseCasesRelation>();
        }
    }
}
