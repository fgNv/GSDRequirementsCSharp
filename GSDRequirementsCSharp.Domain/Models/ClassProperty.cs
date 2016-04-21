namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class ClassProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassProperty()
        {
            ClassPropertyContents = new HashSet<ClassPropertyContent>();
        }

        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public int Visibility { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public virtual Class Class { get; set; }
        
        public virtual ICollection<ClassPropertyContent> ClassPropertyContents { get; set; }
    }
}
