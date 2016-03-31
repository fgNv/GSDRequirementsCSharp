namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassMethodParameter")]
    public partial class ClassMethodParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassMethodParameter()
        {
            ClassMethodParameterContents = new HashSet<ClassMethodParameterContent>();
        }

        public Guid id { get; set; }

        public Guid class_method_id { get; set; }

        [Required]
        [StringLength(100)]
        public string type { get; set; }

        public virtual ClassMethod ClassMethod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMethodParameterContent> ClassMethodParameterContents { get; set; }
    }
}
