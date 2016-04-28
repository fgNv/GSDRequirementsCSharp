namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Class : IEntity<Guid>
    {
        public Class()
        {
            ClassMethods = new HashSet<ClassMethod>();
            ClassProperties = new HashSet<ClassProperty>();
        }

        public Guid Id { get; set; }

        public Visibility Visibility { get; set; }
        
        public ClassType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ClassMethod> ClassMethods { get; set; }

        public virtual ICollection<ClassProperty> ClassProperties { get; set; }
    }
}
