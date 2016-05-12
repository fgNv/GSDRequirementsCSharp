namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ClassMethodParameter : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid ClassMethodId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public virtual ClassMethod ClassMethod { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
