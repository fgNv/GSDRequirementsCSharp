namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClassProperty : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Visibility Visibility { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public virtual Class Class { get; set; }

        public string Name { get; set; }
    }
}
