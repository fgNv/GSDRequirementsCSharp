namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassMethod")]
    public partial class ClassMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassMethod()
        {
            ClassMethodContents = new HashSet<ClassMethodContent>();
            ClassMethodParameters = new HashSet<ClassMethodParameter>();
        }

        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string return_type { get; set; }

        public int visibility { get; set; }

        public Guid class_id { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMethodContent> ClassMethodContents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMethodParameter> ClassMethodParameters { get; set; }
    }
}
