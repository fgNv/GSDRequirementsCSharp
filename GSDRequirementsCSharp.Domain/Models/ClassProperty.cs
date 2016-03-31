namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassProperty")]
    public partial class ClassProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassProperty()
        {
            ClassPropertyContents = new HashSet<ClassPropertyContent>();
        }

        public Guid id { get; set; }

        public Guid class_id { get; set; }

        public int visibility { get; set; }

        [Required]
        [StringLength(100)]
        public string type { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassPropertyContent> ClassPropertyContents { get; set; }
    }
}
