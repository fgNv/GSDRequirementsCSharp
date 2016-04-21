namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClassMethod
    {
        public ClassMethod()
        {
            ClassMethodContents = new HashSet<ClassMethodContent>();
            ClassMethodParameters = new HashSet<ClassMethodParameter>();
        }

        public Guid Id { get; set; }
        
        [StringLength(100)]
        public string ReturnType { get; set; }

        public int Visibility { get; set; }

        public Guid ClassId { get; set; }

        public virtual Class Class { get; set; }
        
        public virtual ICollection<ClassMethodContent> ClassMethodContents { get; set; }
        
        public virtual ICollection<ClassMethodParameter> ClassMethodParameters { get; set; }
    }
}
