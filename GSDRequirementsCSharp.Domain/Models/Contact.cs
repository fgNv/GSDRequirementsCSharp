namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public class Contact : IEntity<Guid>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            Requirements = new HashSet<Requirement>();
            Users = new HashSet<User>();
        }
        
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string mobilePhone { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string phone { get; set; }
        
        public virtual ICollection<Requirement> Requirements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
