namespace GSDRequirementsCSharp.Domain
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClassMethod
    {
        public ClassMethod()
        {
            ClassMethodParameters = new HashSet<ClassMethodParameter>();
        }

        public Guid Id { get; set; }
        
        [StringLength(100)]
        public string ReturnType { get; set; }

        public Visibility Visibility { get; set; }

        public Guid ClassId { get; set; }

        public virtual Class Class { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ClassMethodParameter> ClassMethodParameters { get; set; }
    }
}
