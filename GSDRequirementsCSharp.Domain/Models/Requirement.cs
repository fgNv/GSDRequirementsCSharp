namespace GSDRequirementsCSharp.Persistence
{
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Requirement")]
    public class Requirement : IEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requirement()
        {
            RequirementContents = new HashSet<RequirementContent>();
            RequirementRisks = new HashSet<RequirementRisk>();
        }
        
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        public int rank { get; set; }

        public int difficulty { get; set; }

        public int type { get; set; }

        public Guid creator_id { get; set; }

        public Guid contact_id { get; set; }

        public virtual Contact Contact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequirementContent> RequirementContents { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequirementRisk> RequirementRisks { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
