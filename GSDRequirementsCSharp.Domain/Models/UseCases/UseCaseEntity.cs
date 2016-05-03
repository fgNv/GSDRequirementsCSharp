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
        public Guid Id { get; set; }

        public UseCaseEntityType Type { get; set; }
        
        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }

        public Guid UseCaseDiagramId { get; set; }
    }
}
