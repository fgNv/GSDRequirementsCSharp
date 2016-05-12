using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models.UseCases
{
    public abstract class UseCaseEntity : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        
        abstract public UseCaseEntityType Type { get; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }
        
    }
}
