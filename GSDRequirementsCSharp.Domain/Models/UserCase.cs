namespace GSDRequirementsCSharp.Domain
{
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class UserCase
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(65535)]
        public string Description { get; set; }

        public Guid? ActorId { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
