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
        public const string PREFIX = "UCD";
         
        [Required]
        public Guid Id { get; set; }

        [Required]        
        public int Version { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }
                
        public int Identifier { get; set; }

        public bool IsLastVersion { get; set; }

        public ICollection<UseCases.UseCaseEntity> Entities { get; set; }

        public ICollection<UseCaseEntityRelation> EntitiesRelations { get; set; }

        public ICollection<UseCasesRelation> UseCasesRelations { get; set; }

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
            EntitiesRelations = new HashSet<UseCaseEntityRelation>();
            Contents = new HashSet<UseCaseDiagramContent>();
            UseCasesRelations = new HashSet<UseCasesRelation>();
        }
    }
}
