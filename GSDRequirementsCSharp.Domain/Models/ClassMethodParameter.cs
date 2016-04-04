namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ClassMethodParameter
    {
        public ClassMethodParameter()
        {
            ClassMethodParameterContents = new HashSet<ClassMethodParameterContent>();
        }

        public Guid Id { get; set; }

        public Guid ClassMethodId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public virtual ClassMethod ClassMethod { get; set; }
        
        public virtual ICollection<ClassMethodParameterContent> ClassMethodParameterContents { get; set; }
    }
}
