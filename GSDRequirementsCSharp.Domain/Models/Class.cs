namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Class
    {
        public Class()
        {
            ClassMethods = new HashSet<ClassMethod>();
            ClassProperties = new HashSet<ClassProperty>();
        }

        public Guid Id { get; set; }

        public int Visibility { get; set; }

        public Guid ClassDiagramId { get; set; }

        public ClassType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ClassMethod> ClassMethods { get; set; }

        public virtual ICollection<ClassProperty> ClassProperties { get; set; }
    }
}
