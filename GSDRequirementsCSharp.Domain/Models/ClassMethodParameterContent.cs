namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ClassMethodParameterContent
    {
        public Guid Id { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ClassMethodParameter ClassMethodParameter { get; set; }
    }
}
