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

        public ICollection<Actor> Actors { get; set; }

        public ICollection<UseCase> UseCases { get; set; }

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
            Actors = new HashSet<Actor>();
            UseCases = new HashSet<UseCase>();
            Relations = new HashSet<UseCaseEntityRelation>();
            Contents = new HashSet<UseCaseDiagramContent>();
            UseCaseRelations = new HashSet<UseCasesRelation>();
        }
    }
}
