namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Issue")]
    public partial class Issue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Issue()
        {
            IssueComments = new HashSet<IssueComment>();
        }

        public Guid id { get; set; }

        public bool concluded { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        public Guid specification_item_id { get; set; }

        public int? creator_id { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IssueComment> IssueComments { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
