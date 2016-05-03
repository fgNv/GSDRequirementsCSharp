using GSDRequirementsCSharp.Domain.Models.UseCases;
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
    public class UseCaseEntityRelation
    {
        public Guid Id { get; set; }

        [Required]
        public UseCaseEntity Target { get; set; }

        [Required]
        public UseCaseEntity Source { get; set; }

        [Required]
        public UseCaseDiagram UseCaseDiagram { get; set; }

        public Guid UseCaseDiagramId { get; set; }

        public Guid SourceId { get; set; }

        public Guid TargetId { get; set; }
    }
}
