using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models.UseCases
{
    public abstract class UseCaseEntity : IEntity<VersionKey>
    {
        public virtual Guid Id { get; set; }

        public virtual int Version { get; set; }

        abstract public UseCaseEntityType Type { get; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }
        
        public int X { get; set; }

        public int Y { get; set; }

        VersionKey IEntity<VersionKey>.Id
        {
            get { return new VersionKey { Id = Id, Version = Version }; }
        }
    }
}
