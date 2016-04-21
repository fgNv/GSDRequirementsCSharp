namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
