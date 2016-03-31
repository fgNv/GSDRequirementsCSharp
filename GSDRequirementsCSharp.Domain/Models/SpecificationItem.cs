namespace GSDRequirementsCSharp.Persistence
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecificationItem")]
    public partial class SpecificationItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SpecificationItem()
        {
            ClassDiagrams = new HashSet<ClassDiagram>();
            Issues = new HashSet<Issue>();
        }

        public Guid id { get; set; }

        public Guid package_id { get; set; }

        public int version { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassDiagram> ClassDiagrams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }

        public virtual Requirement Requirement { get; set; }

        public virtual UserCase UserCase { get; set; }
    }
}
