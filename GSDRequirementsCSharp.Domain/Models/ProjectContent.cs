namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;    
    
    public class ProjectContent : IEntity<Guid>
    {   
        public Guid Id { get; set; }
        
        [StringLength(65535)]
        public string Description { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
