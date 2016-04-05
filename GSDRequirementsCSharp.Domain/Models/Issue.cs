namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Issue
    {
        public Issue()
        {
            IssueComments = new HashSet<IssueComment>();
        }

        public Guid Id { get; set; }

        public bool Concluded { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string Description { get; set; }

        public Guid specification_item_id { get; set; }

        public int? CreatorId { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IssueComment> IssueComments { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
