namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class ClassDiagram
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
