namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class RequirementRisk : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(65535)]
        public string Description { get; set; }
        
        public virtual Requirement Requirement { get; set; }
    }
}
